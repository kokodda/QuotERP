﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Compras;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class RecebimentoCompraController : BaseController
    {
        #region Variaveis

        private readonly IRepository<PedidoCompra> _pedidoCompraRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<RecebimentoCompra> _recebimentoCompraRepository;
        private readonly IRepository<ParametroModuloCompra> _parametroModuloCompraRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<EntradaMaterial> _entradaMaterialRepository;
        private readonly IRepository<MovimentacaoEstoqueMaterial> _movimentacaoEstoqueMaterialRepository;

        private readonly RecebimentoCompraConverter _fabricaDeObjetos;
        
        private readonly ILogger _logger;
        private readonly string[] _tipoRelatorio = {"Detalhado", "Listagem", "Sintético"};

        private static readonly Dictionary<string, string> ColunasPesquisaRecebimentoCompra = new Dictionary<string, string>
            {
                {"Data", "Data"},
                {"Fornecedor", "Fornecedor.Nome"},
                {"Número", "Numero"},
                {"Situação", "SituacaoRecebimentoCompra"}
            };

        #endregion

        #region Construtores

        public RecebimentoCompraController(ILogger logger, 
            IRepository<PedidoCompra> pedidoCompraRepository,
            IRepository<Pessoa> pessoaRepository,
            IRepository<RecebimentoCompra> recebimentoCompraRepository,
            IRepository<ParametroModuloCompra> parametroModuloCompraRepository,
            IRepository<Material> materialRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<EntradaMaterial> entradaMaterialRepository,
            IRepository<MovimentacaoEstoqueMaterial> movimentacaoEstoqueMaterialRepository,
            RecebimentoCompraConverter fabricaDeObjetos)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _pessoaRepository = pessoaRepository;
            _recebimentoCompraRepository = recebimentoCompraRepository;
            _parametroModuloCompraRepository = parametroModuloCompraRepository;
            _materialRepository = materialRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _entradaMaterialRepository = entradaMaterialRepository;
            _movimentacaoEstoqueMaterialRepository = movimentacaoEstoqueMaterialRepository;
            _fabricaDeObjetos = fabricaDeObjetos;
            _logger = logger;
        }

        #endregion

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Data", "Data"},
            {"FornecedorNome", "Fornecedor.Nome"},
            {"Numero", "Numero"},
            {"Valor", "Valor"},
            {"SituacaoRecebimentoCompra", "SituacaoRecebimentoCompra"}
        };
        #endregion

        #region Index

        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaRecebimentoCompraModel {ModoConsulta = "Listar"};
         
            return View(model);
        }

        private IQueryable<RecebimentoCompra> ObtenhaQueryFiltrada(PesquisaRecebimentoCompraModel model, StringBuilder filtros)
        {
            var recebimentoCompras = _recebimentoCompraRepository.Find();
            if (model.UnidadeEstocadora.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Unidade.Id == model.UnidadeEstocadora);
                filtros.AppendFormat("Unidade: {0}, ",
                                     _pessoaRepository.Get(model.UnidadeEstocadora.Value).NomeFantasia);
            }

            if (model.Numero.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Numero == model.Numero);
                filtros.AppendFormat("Número: {0}, ", model.Numero.Value);
            }

            if (model.Fornecedor.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Fornecedor.Id == model.Fornecedor);
                filtros.AppendFormat("Fornecedor: {0}, ", _pessoaRepository.Get(model.Fornecedor.Value).Nome);
            }

            if (model.SituacaoRecebimentoCompra.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.SituacaoRecebimentoCompra == model.SituacaoRecebimentoCompra);
                filtros.AppendFormat("Situação: {0}, ", model.SituacaoRecebimentoCompra.Value.EnumToString());
            }

            if (model.DataInicio.HasValue && !model.DataFim.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Data.Date >= model.DataInicio.Value);

                filtros.AppendFormat("Data de '{0}', ", model.DataInicio.Value.ToString("dd/MM/yyyy"));
            }

            if (!model.DataInicio.HasValue && model.DataFim.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Data.Date <= model.DataFim.Value);

                filtros.AppendFormat("Data até '{0}', ", model.DataFim.Value.ToString("dd/MM/yyyy"));
            }

            if (model.DataInicio.HasValue && model.DataFim.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Data.Date >= model.DataInicio.Value
                                                         && p.Data.Date <= model.DataFim.Value);
                filtros.AppendFormat("Data de '{0}' até '{1}', ",
                                     model.DataInicio.Value.ToString("dd/MM/yyyy"),
                                     model.DataFim.Value.ToString("dd/MM/yyyy"));
            }

            if (model.ValorInicio.HasValue && model.ValorFim.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.Valor >= model.ValorInicio.Value
                                                         && p.Valor <= model.ValorFim.Value);
                filtros.AppendFormat("Valor de '{0}' até '{1}', ",
                                     model.ValorInicio.Value.ToString("C2"),
                                     model.ValorFim.Value.ToString("C2"));
            }

            if (model.Material.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.RecebimentoCompraItens.Any(i => i.Material.Id == model.Material));
                filtros.AppendFormat("Material: {0}, ", _materialRepository.Get(model.Material.Value).Descricao);
            }

            if (model.NumeroPedidoCompra.HasValue)
            {
                recebimentoCompras = recebimentoCompras.Where(p => p.PedidoCompras.Any(i => i.Id == model.NumeroPedidoCompra));
                filtros.AppendFormat("Pedido de compra: {0}, ", model.NumeroPedidoCompra.Value);
            }

            return recebimentoCompras;
        }


        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaRecebimentoCompraModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var reservaMateriais = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        reservaMateriais = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? reservaMateriais.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : reservaMateriais.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                reservaMateriais = reservaMateriais.OrderByDescending(o => o.DataAlteracao);

                var total = reservaMateriais.Count();

                if (request.Page > 0)
                {
                    reservaMateriais = reservaMateriais.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = reservaMateriais.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridRecebimentoCompraModel()
                {
                    Id = p.Id.GetValueOrDefault(),
                    Data = p.Data.Date,
                    FornecedorNome = p.Fornecedor.Nome,
                    Numero = p.Numero,
                    Valor = p.Valor,
                    SituacaoRecebimentoCompra = p.SituacaoRecebimentoCompra
                }).ToList();

                var valorPage = request.Page;
                request.Page = 1;
                var data = list.ToDataSourceResult(request);
                request.Page = valorPage;

                var result = new DataSourceResult()
                {
                    AggregateResults = data.AggregateResults,
                    Data = data.Data,
                    Total = total
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                var message = ex.GetMessage();
                _logger.Info(message);

                return Json(new DataSourceResult { Errors = message });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaRecebimentoCompraModel model)
        {
            try
            {
                var filtros = new StringBuilder();
                
                var recebimentoCompras = ObtenhaQueryFiltrada(model, filtros);
                
                // Se não é uma listagem, gerar o relatório
                var result = recebimentoCompras
                    .Fetch(p => p.Fornecedor)
                    .ToList();

                if (!result.Any())
                    return Json(new {Error = "Nenhum item encontrado."});

                #region Montar Relatório

                var report = new ListaRecebimentoCompraReport() {DataSource = result};

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= AjusteValores(Fields." + model.AgruparPor + ")");

                    var key = ColunasPesquisaRecebimentoCompra.First(p => p.Value == model.AgruparPor).Key;
                    var titulo = string.Format("= \"{0}: \" + AjusteValores(Fields.{1})", key, model.AgruparPor);
                    grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                }
                else
                {
                    report.Groups.Remove(grupo);
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor,
                                        model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

                #endregion

                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new {Url = filename});
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new {Error = message});

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new RecebimentoCompraModel
            { 
                GridItens = new List<RecebimentoCompraItemModel>()
            };

            return View(model);
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(RecebimentoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fabricaDeObjetos.CrieNovoRecebimentoCompra(model, this);

                    CrieCustoMaterial(domain);
                    
                    domain.EntradaMaterial = domain.SalveEntradaMaterial(model.DepositoMaterial, 
                        _depositoMaterialRepository,
                        _movimentacaoEstoqueMaterialRepository,
                        _estoqueMaterialRepository,
                        _entradaMaterialRepository);

                    domain.SituacaoRecebimentoCompra = SituacaoRecebimentoCompra.Finalizada;

                    _recebimentoCompraRepository.Save(domain);

                    domain.AtualizePedidosCompra(_recebimentoCompraRepository, _pedidoCompraRepository);

                    this.AddSuccessMessage("Recebimento de compra cadastrado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar o recebimento de compra. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());
                    
                    return new JsonResult { Data = "error" };
                }
            }

            return new JsonResult { Data = "sucesso" };
        }

        private void CrieCustoMaterial(RecebimentoCompra recebimentoCompra)
        {
            foreach (var x in recebimentoCompra.RecebimentoCompraItens)
            {
                var valorUnitario = x.ValorUnitario;

                var novoCusto = new CustoMaterial
                    {
                        Custo = valorUnitario,
                        CustoAquisicao = valorUnitario,
                        Data = DateTime.Now,
                        Fornecedor = recebimentoCompra.Fornecedor
                    };

                x.Material.CustoMaterials.Add(novoCusto);
                x.Material.AtualizeCustoAtual();

                _materialRepository.SaveOrUpdate(x.Material);
                x.CustoMaterial = novoCusto;
            }
        }

        #endregion
        
        #region Editar

		[HttpGet, ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long? id)
        {
            var domain = _recebimentoCompraRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RecebimentoCompraModel>(domain);

                if (domain.EntradaMaterial != null)
                {
                    model.DepositoMaterial = domain.EntradaMaterial.DepositoMaterialDestino.Id;
                }

                model.GridItens = new List<RecebimentoCompraItemModel>(domain.RecebimentoCompraItens.OrderBy(x => x.Material.Referencia).Select( x => 
                    {
                        var retorno = Mapper.Flat<RecebimentoCompraItemModel>(x);
                        retorno.PedidosCompra =
                            x.DetalhamentoRecebimentoCompraItens.Select(d => new IdentificadorPedidoCompra { Id = d.PedidoCompra.Id != null ? d.PedidoCompra.Id.Value : 0, 
                                Numero = d.PedidoCompra.Numero }).ToList(); 
                        retorno.PedidoCompraItens =
                            x.DetalhamentoRecebimentoCompraItens.Select(d => d.PedidoCompraItem.Id).ToList();
                        retorno.ValorUnitarioPedido =
                            x.DetalhamentoRecebimentoCompraItens.Select(d => d.PedidoCompraItem.ValorUnitario).Average();
                        retorno.UnidadeMedida = x.Material.UnidadeMedida.Sigla;
                        
                        //rever futuramente esse requisito
                        var primeiroDetalhamento = x.DetalhamentoRecebimentoCompraItens.First();
                        retorno.UnidadeMedidaSigla = primeiroDetalhamento.PedidoCompraItem.Material.UnidadeMedida.Sigla;
                        
                        retorno.QuantidadeEntrada = x.DetalhamentoRecebimentoCompraItens.Select(d => d.Quantidade).Sum();
                        return retorno;
                    }   
                ));

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o recebimento da compra.");
            return RedirectToAction("Index");
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(RecebimentoCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _fabricaDeObjetos.AtualizeRecebimentoCompra(_recebimentoCompraRepository.Get(model.Id), model, this);
                    
                    domain.SituacaoRecebimentoCompra = SituacaoRecebimentoCompra.Finalizada;
                    
                    domain.AtualizeCustoMaterial(_materialRepository, domain.Fornecedor);

                    _recebimentoCompraRepository.SaveOrUpdate(domain);

                    domain.AtualizeEntradaMaterial(model.DepositoMaterial,
                        _depositoMaterialRepository,
                        _movimentacaoEstoqueMaterialRepository,
                        _estoqueMaterialRepository,
                        _entradaMaterialRepository);

                    this.AddSuccessMessage("Recebimento de compra atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar o recebimento de compra. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }

            return new JsonResult { Data = "sucesso" };
        }

        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Excluir(long? id)
        {
			if (ModelState.IsValid)
            {
				try
				{
					var domain = _recebimentoCompraRepository.Get(id);
                    _recebimentoCompraRepository.Delete(domain);

                    domain.AtualizePedidosCompraAoExcluir(_recebimentoCompraRepository, _pedidoCompraRepository);
				    domain.ExcluaEntradaMaterial(_estoqueMaterialRepository, _entradaMaterialRepository);
                    domain.ExcluaCustos(_materialRepository);

				    this.AddSuccessMessage("Recebimento de compra excluído com sucesso");
				    return RedirectToAction("Index");
				}
				catch (Exception exception)
				{
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir o recebimento de compra: " + exception.Message);
					//_logger.Info(exception.GetMessage());
				    throw;
				}
			}

			return RedirectToAction("Editar", new { id });
        }

        #endregion
		
        #region PopulateViewData
        protected void PopulateViewData(RecebimentoCompraModel model)
        {            
            // Unidade
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.Unidade = unidades.ToSelectList("NomeFantasia", model == null ? null : model.Unidade);

            // Depósito
            ViewBag.DepositoMaterial = model != null && model.Unidade!= null && model.Unidade.HasValue ?
                _depositoMaterialRepository.Find(d => d.Unidade.Id == model.Unidade).ToList().ToSelectList("Nome", model.DepositoMaterial) 
                : new List<DepositoMaterial>().ToSelectList("Nome");
        }
        #endregion

        #region PopulateViewDataPesquisa
        protected void PopulateViewDataPesquisa(PesquisaRecebimentoCompraModel model)
        {
            // UnidadeEstocadora
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).ToList();
            ViewBag.UnidadeEstocadora = unidades.ToSelectList("NomeFantasia", model.UnidadeEstocadora);

            ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
            ViewBag.OrdenarPor = new SelectList(ColunasPesquisaRecebimentoCompra, "value", "key");
            ViewBag.AgruparPor = new SelectList(ColunasPesquisaRecebimentoCompra, "value", "key");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
        }

        #endregion
        
        #region Pesquisar Pedido de Compra

        public virtual PartialViewResult PesquisarPedidoCompra(long id, long idRecebimento)
        {
            var pedidoCompra = _pedidoCompraRepository.Get(id);

            if (pedidoCompra == null)
            {
                return PartialView(new PedidoCompraRecebimentoModel());
            }

            var model = _fabricaDeObjetos.CriePedidoCompraRecebimentoModel(pedidoCompra);

            ModelState.Clear();
            return PartialView(model);
        }

        #endregion

        [HttpGet]
        public virtual JsonResult ObtenhaPedidosDeCompraPorFornecedor(long fornecedorId, long unidadeId)
        {
            if(fornecedorId == 0 || unidadeId == 0)
                return new JsonResult { Data = new List<PedidoCompraReduzidoModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            var pedidosCompra = _pedidoCompraRepository.Find(p => p.Fornecedor.Id == fornecedorId 
                && p.UnidadeEstocadora.Id == unidadeId
                && p.SituacaoCompra != SituacaoCompra.Cancelado
                && p.SituacaoCompra != SituacaoCompra.AtendidoTotal);

            var parametro = _parametroModuloCompraRepository.Find().FirstOrDefault();
            if (parametro != null && parametro.ValidaRecebimentoPedido)
            {
                pedidosCompra = pedidosCompra.Where(p => p.Autorizado);
            }

            var pedidoComprasModel = pedidosCompra.Select(p => new PedidoCompraReduzidoModel
            {
                Data = p.DataCompra.ToString("dd/MM/yyyy"),
                Id = p.Id,
                Numero = p.Numero,
                Valor = p.ValorCompra
            }).ToList();

            return new JsonResult { Data = pedidoComprasModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [AjaxOnly]
        public virtual JsonResult ObtenhaRecebimentoItens(List<RecebimentoCompraItemModel> dadosGridItens, 
            List<PedidoCompraItemRecebimentoModel> dadosGridPedidosItens, long pedidoCompraId)
        {
            dadosGridItens = dadosGridItens ?? new List<RecebimentoCompraItemModel>();
            dadosGridPedidosItens = dadosGridPedidosItens ?? new List<PedidoCompraItemRecebimentoModel>();

            var listaRetorno = new List<RecebimentoCompraItemModel>(dadosGridItens);
            var pedidoCompra = _pedidoCompraRepository.Get(pedidoCompraId);

            foreach (var pedidoCompraItemRecebimentoModel in dadosGridPedidosItens)
            {
                var recebimentoCompraItemModel =
                    dadosGridItens.FirstOrDefault(
                        x => x.MaterialReferencia == pedidoCompraItemRecebimentoModel.MaterialReferenciaPedido);

                if (recebimentoCompraItemModel != null)
                {
                    var retorno = _fabricaDeObjetos.AtualizeRecebimentoCompraItemModel(recebimentoCompraItemModel, pedidoCompraItemRecebimentoModel, pedidoCompra);

                    if (retorno != null)
                    {
                        listaRetorno.Remove(
                            dadosGridItens.FirstOrDefault(p => p.MaterialReferencia == retorno.MaterialReferencia));
                        listaRetorno.Add(retorno);
                    }
                } else {
                    listaRetorno.Add(_fabricaDeObjetos.CrieRecebimentoCompraItemModel(pedidoCompraItemRecebimentoModel, pedidoCompra));
                }
            }
            
            return new JsonResult { Data = listaRetorno.OrderBy(x => x.MaterialReferencia) };
        }
    }
}


 
