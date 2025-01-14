﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Helpers;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Almoxarifado;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class RequisicaoMaterialController : BaseController
    {
        #region Variaveis

        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<ReservaMaterial> _reservaMaterialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<TipoItem> _tipoItemRepository;
        private readonly IRepository<CentroCusto> _centroCustoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<ProgramacaoProducaoMaterial> _programacaoProducaoMaterialRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaReservaMaterial;
        #endregion

        #region Construtores
        public RequisicaoMaterialController(ILogger logger, IRepository<TipoItem> tipoItemRepository,
            IRepository<ReservaMaterial> reservaMaterialRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<Material> materialRepository, IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
            IRepository<RequisicaoMaterial> requisicaoMaterialRepository, IRepository<CentroCusto> centroCustoRepository,
            IRepository<DepositoMaterial> depositoMaterialRepository, IRepository<Usuario> usuarioRepository,
            IRepository<EstoqueMaterial> estoqueMaterialRepository, IRepository<ProgramacaoProducaoMaterial> programacaoProducaoMaterialRepository)
        {
            _reservaMaterialRepository = reservaMaterialRepository;
            _pessoaRepository = pessoaRepository;
            _tipoItemRepository = tipoItemRepository;
            _materialRepository = materialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _centroCustoRepository = centroCustoRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _usuarioRepository = usuarioRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _programacaoProducaoMaterialRepository = programacaoProducaoMaterialRepository;

            _logger = logger;

            PreecheColunasPesquisa();
        }
        #endregion

        #region Colunas de Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Numero", "Numero"},
            {"Data", "Data"},
            {"Requerente", "Requerente.Nome"},
            {"UnidadeRequerente", "UnidadeRequerente.NomeFantasia"},
            {"TipoMaterial", "TipoItem.Descricao"},
            {"Origem", "Origem"},
            {"Situacao", "SituacaoRequisicaoMaterial"}
        };
        #endregion
        
        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            return View(new PesquisaRequisicaoMaterialModel { ModoConsulta = "Listar" });
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaRequisicaoMaterialModel model)
        {
            try
            {
                var filtros = new StringBuilder();
                
                var requisicaoMaterials = ObtenhaRequisicoesMateriaisFiltrado(model, filtros);
                
                // Se não é uma listagem, gerar o relatório
                var result = requisicaoMaterials.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                var report = new ListaRequisicaoMaterialReport() { DataSource = result };

                if (filtros.Length > 2)
                    report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

                if (model.AgruparPor != null)
                {
                    grupo.Groupings.Add("= AjusteValores(Fields." + model.AgruparPor + ")");

                    var key = _colunasPesquisaReservaMaterial.First(p => p.Value == model.AgruparPor).Key;
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

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);

                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { Error = message });

                ModelState.AddModelError(string.Empty, message);
                return View(model);
            }
        }

        private IQueryable<RequisicaoMaterial> ObtenhaRequisicoesMateriaisFiltrado(PesquisaRequisicaoMaterialModel model, StringBuilder filtros)
        {
            var requisicaoMaterials = _requisicaoMaterialRepository.Find();

            #region Filtros

            if (!string.IsNullOrWhiteSpace(model.Origem))
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Origem.Contains(model.Origem));
                filtros.AppendFormat("Origem: {0}, ", model.Origem);
            }

            if (model.Numero.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Numero == model.Numero);
                filtros.AppendFormat("Número: {0}, ", model.Numero.Value);
            }

            if (model.UnidadeRequerente.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.UnidadeRequerente.Id == model.UnidadeRequerente);
                filtros.AppendFormat("Unidade Requerente: {0}, ",
                    _pessoaRepository.Get(model.UnidadeRequerente.Value).NomeFantasia);
            }

            if (model.UnidadeRequisitada.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.UnidadeRequisitada.Id == model.UnidadeRequisitada);
                filtros.AppendFormat("Unidade Requisitada: {0}, ",
                    _pessoaRepository.Get(model.UnidadeRequisitada.Value).NomeFantasia);
            }

            if (model.CentroCusto.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.CentroCusto.Id == model.CentroCusto);
                filtros.AppendFormat("Centro de Custo: {0}, ", _centroCustoRepository.Get(model.CentroCusto.Value).Nome);
            }

            if (model.Funcionario.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Requerente.Id == model.Funcionario);
                filtros.AppendFormat("Requerente: {0}, ", _pessoaRepository.Get(model.Funcionario.Value).Nome);
            }

            if (model.DataInicio.HasValue && !model.DataFim.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Data.Date >= model.DataInicio.Value);

                filtros.AppendFormat("Data de início de '{0}', ", model.DataInicio.Value.ToString("dd/MM/yyyy"));
            }

            if (!model.DataInicio.HasValue && model.DataFim.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Data.Date <= model.DataFim.Value);

                filtros.AppendFormat("Data de início até '{0}', ", model.DataFim.Value.ToString("dd/MM/yyyy"));
            }

            if (model.DataInicio.HasValue && model.DataFim.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.Data.Date >= model.DataInicio.Value
                                                                     && p.Data.Date <= model.DataFim.Value);
                filtros.AppendFormat("Data de '{0}' até '{1}', ",
                    model.DataInicio.Value.ToString("dd/MM/yyyy"),
                    model.DataFim.Value.ToString("dd/MM/yyyy"));
            }

            if (model.SituacaoRequisicaoMaterial.HasValue)
            {
                requisicaoMaterials =
                    requisicaoMaterials.Where(p => p.SituacaoRequisicaoMaterial == model.SituacaoRequisicaoMaterial);
                filtros.AppendFormat("Situação: {0}, ", model.SituacaoRequisicaoMaterial.Value.EnumToString());
            }

            if (model.Material.HasValue)
            {
                requisicaoMaterials =
                    requisicaoMaterials.Where(p => p.RequisicaoMaterialItems.Any(i => i.Material.Id == model.Material));
                filtros.AppendFormat("Material: {0}, ", _materialRepository.Get(model.Material.Value).Descricao);
            }

            if (model.TipoItem.HasValue)
            {
                requisicaoMaterials = requisicaoMaterials.Where(p => p.TipoItem.Id == model.TipoItem);
                filtros.AppendFormat("Tipo de Material: {0}, ", _tipoItemRepository.Get(model.TipoItem.Value).Descricao);
            }

            if (!string.IsNullOrWhiteSpace(model.ReferenciaExterna))
            {
                requisicaoMaterials =
                    requisicaoMaterials.Where(
                        p =>
                            p.RequisicaoMaterialItems.SelectMany(x => x.Material.ReferenciaExternas)
                                .Any(y => y.Referencia == model.ReferenciaExterna));
                filtros.AppendFormat("Referência externa: {0}, ", model.ReferenciaExterna);
            }
            return requisicaoMaterials;
        }

        public virtual ActionResult ObtenhaListaGridRequisicaoMaterialModel([DataSourceRequest] DataSourceRequest request, PesquisaRequisicaoMaterialModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var requisicaoMaterials = ObtenhaRequisicoesMateriaisFiltrado(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        requisicaoMaterials = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? requisicaoMaterials.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : requisicaoMaterials.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                requisicaoMaterials = requisicaoMaterials.OrderByDescending(o => o.DataAlteracao);

                var total = requisicaoMaterials.Count();

                if (request.Page > 0)
                {
                    requisicaoMaterials = requisicaoMaterials.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = requisicaoMaterials.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridRequisicaoMaterialModel()
                {
                    Id = p.Id.Value,
                    Origem = p.Origem,
                    Numero = p.Numero,
                    Data = p.Data.Date,
                    Situacao = p.SituacaoRequisicaoMaterial.EnumToString(),
                    TipoMaterial = p.TipoItem.Descricao,
                    Requerente = p.Requerente.Nome,
                    UnidadeRequerente = p.UnidadeRequerente.NomeFantasia
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

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo()
        {
            //chamada do modelo
            if (TempData.ContainsKey("RequisicaoMaterialModel"))
                return View(TempData["RequisicaoMaterialModel"]);

            var usuarioId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(usuarioId);

            long funcionarioId = 0L;
            if (usuario.Funcionario != null)
            {
                funcionarioId = usuario.Funcionario.Id ?? usuario.Funcionario.Id.Value;
            }

            return View(new RequisicaoMaterialModel
            {
                Funcionario = funcionarioId
            });
        }

        [PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult NovoPreenchido()
        {
            return View("Novo", TempData["RequisicaoMaterialModel"]);
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Novo(RequisicaoMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<RequisicaoMaterial>(model);
                    domain.Data = DateTime.Now;
                    domain.Numero = ProximoNumero();
                    domain.UnidadeRequerente = _pessoaRepository.Get(model.UnidadeRequerente);
                    domain.UnidadeRequisitada = _pessoaRepository.Get(model.UnidadeRequisitada);
                    domain.Requerente = _pessoaRepository.Get(model.Funcionario);
                    domain.CentroCusto = _centroCustoRepository.Get(model.CentroCusto);
                    domain.TipoItem = _tipoItemRepository.Get(model.TipoItem);

                    IncluaNovosRequisicaoMaterialItens(model, domain);

                    domain.AtualizeSituacao();

                    domain.CrieReservaMaterial(ProximoNumeroReservaMaterial(), _reservaEstoqueMaterialRepository);

                    _requisicaoMaterialRepository.Save(domain);

                    this.AddSuccessMessage("Requisição de material cadastrado com sucesso.");

                    return RedirectToAction("Editar", new { domain.Id });
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a requisição de material. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return View(model);
                }
            }

            return View(model);
        }

        private long ProximoNumero()
        {
            try
            {
                return _requisicaoMaterialRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private long ProximoNumeroReservaMaterial()
        {
            try
            {
                return _reservaMaterialRepository.Find().Max(p => p.Numero) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(long? id)
        {
            var domain = _requisicaoMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RequisicaoMaterialModel>(domain);
                model.Funcionario = domain.Requerente.Id;

                model.GridItens = domain.RequisicaoMaterialItems.Select(x =>
                    new RequisicaoMaterialItemModel
                    {
                        Descricao = x.Material.Descricao,
                        Referencia = x.Material.Referencia,
                        SituacaoRequisicaoMaterial = x.SituacaoRequisicaoMaterial.EnumToString(),
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeCancelada = x.RequisicaoMaterialItemCancelado != null ? x.RequisicaoMaterialItemCancelado.QuantidadeCancelada : 0,
                        QuantidadeSolicitada = x.QuantidadeSolicitada,
                        QuantidadeDisponivel = ObtenhaQuantidadeDisponivel_(x.Material.Id, domain.UnidadeRequisitada.Id.Value),
                        UnidadeMedida = x.Material.UnidadeMedida.Sigla,
                        IdRequisicaoMaterialItem = x.Id,
                        Foto = (x.Material.Foto != null ? x.Material.Foto.Nome.GetFileUrl() : string.Empty),
                    }).ToList();

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a requisição de material.");
            return RedirectToAction("Index");
        }

        private double ObtenhaQuantidadeDisponivel_(long? materialId, long unidadeRequisitada)
        {
            double quantidadesEstoqueMaterial = 0;

            var estoques =
                _estoqueMaterialRepository.Find(y => y.Material.Id == materialId && y.DepositoMaterial.Unidade.Id == unidadeRequisitada);

            if (estoques.Any())
            {
                quantidadesEstoqueMaterial = estoques.Sum(x => x.Quantidade);    
            }

            if (quantidadesEstoqueMaterial == 0)
                return 0;

            var reservaEstoque = _reservaEstoqueMaterialRepository.Get(y => y.Material.Id == materialId && y.Unidade.Id == unidadeRequisitada);

            if (reservaEstoque == null)
                return quantidadesEstoqueMaterial;

            return quantidadesEstoqueMaterial - reservaEstoque.Quantidade;
        }

        [HttpGet, AjaxOnly]
        public virtual ActionResult ObtenhaQuantidadeDisponivel(long? materialId, long unidadeRequisitada)
        {
            try
            {
                var quantidadeDisponivel = ObtenhaQuantidadeDisponivel_(materialId, unidadeRequisitada);
                return Json(quantidadeDisponivel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar a quantidade disponível." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, PopulateViewData("PopulateViewDataNovoEditar")]
        public virtual ActionResult Editar(RequisicaoMaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _requisicaoMaterialRepository.Get(model.Id);

                    Framework.UnitOfWork.Session.Current.Evict(domain.Requerente);

                    domain = Mapper.Unflat(model, domain);

                    domain.Requerente = _pessoaRepository.Get(model.Funcionario);
                    domain.UnidadeRequerente = _pessoaRepository.Get(model.UnidadeRequerente);
                    domain.UnidadeRequisitada = _pessoaRepository.Get(model.UnidadeRequisitada);
                    domain.CentroCusto = _centroCustoRepository.Get(model.CentroCusto);
                    domain.TipoItem = _tipoItemRepository.Get(model.TipoItem);

                    ExcluaRequisicaoMaterialItens(model, domain);
                    AtualizeRequisicaoMaterialItens(model, domain);
                    IncluaNovosRequisicaoMaterialItens(model, domain);
                    domain.AtualizeSituacao();
                    domain.AtualizeReservaMaterial(_reservaEstoqueMaterialRepository);

                    _requisicaoMaterialRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Requisição de material atualizado com sucesso.");

                    return RedirectToAction("Editar", new { domain.Id });
                }
                catch (Exception exception)
                {
                    var errorMsg = "Não é possível salvar a requisição de material. Confira se os dados foram informados corretamente: " +
                        exception.Message;
                    this.AddErrorMessage(errorMsg);
                    _logger.Info(exception.GetMessage());

                    return View(model);
                }
            }

            return View(model);
        }

        private void IncluaNovosRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            requisicaoMaterialModel.GridItens.ForEach(x =>
            {
                var requisicaoMaterialItem = requisicaoMaterial.RequisicaoMaterialItems.FirstOrDefault(y => y.Id == x.IdRequisicaoMaterialItem && x.IdRequisicaoMaterialItem != null);
                var material = _materialRepository.Find(y => y.Referencia == x.Referencia).First();
                if (requisicaoMaterialItem == null)
                {
                    requisicaoMaterialItem = new RequisicaoMaterialItem()
                    {
                        Material = material,
                        QuantidadeAtendida = x.QuantidadeAtendida,
                        QuantidadeSolicitada = x.QuantidadeSolicitada,
                        SituacaoRequisicaoMaterial = SituacaoRequisicaoMaterial.NaoAtendido
                    };
                    requisicaoMaterial.RequisicaoMaterialItems.Add(requisicaoMaterialItem);
                }
            });
        }

        private void AtualizeRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            requisicaoMaterialModel.GridItens.ForEach(x =>
            {
                var reservaMaterialItem = requisicaoMaterial.RequisicaoMaterialItems.FirstOrDefault(y => y.Id == x.IdRequisicaoMaterialItem);
                if (reservaMaterialItem != null)
                {
                    reservaMaterialItem.QuantidadeSolicitada = x.QuantidadeSolicitada;
                }
            });
        }

        private void ExcluaRequisicaoMaterialItens(RequisicaoMaterialModel requisicaoMaterialModel, RequisicaoMaterial requisicaoMaterial)
        {
            var requisicaoMaterialsItensExcluir = new List<RequisicaoMaterialItem>();

            requisicaoMaterial.RequisicaoMaterialItems.ForEach(x =>
            {
                if (requisicaoMaterialModel.GridItens.All(y => y.IdRequisicaoMaterialItem != x.Id))
                {
                    requisicaoMaterialsItensExcluir.Add(x);
                }
            });

            requisicaoMaterialsItensExcluir.ForEach(x => requisicaoMaterial.RequisicaoMaterialItems.Remove(x));
        }

        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _requisicaoMaterialRepository.Get(id);
                    
                    domain.ReservaMateriais.ForEach(x =>
                    {
                        x.AtualizeReservaEstoqueMaterialAoExcluir(_reservaEstoqueMaterialRepository);
                        var programacaoProducaoMaterial = _programacaoProducaoMaterialRepository.Find().FirstOrDefault(y => y.ReservaMaterial.Id == x.Id);
                        if (programacaoProducaoMaterial != null)
                        {
                            programacaoProducaoMaterial.ReservaMaterial = null;
                            _programacaoProducaoMaterialRepository.SaveOrUpdate(programacaoProducaoMaterial);
                        }
                    });

                    _requisicaoMaterialRepository.Delete(domain);

                    this.AddSuccessMessage("Requisição de material excluída com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a requisição de material: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(PesquisaRequisicaoMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["UnidadeRequerente"] = unidades.ToSelectList("NomeFantasia", model.UnidadeRequerente);

            var unidadesRequisitadas = _depositoMaterialRepository.Find(p => p.Unidade != null && p.Ativo).Select(x => x.Unidade).ToList()
                .GroupBy(x => x.Id).Select(x => x.First()).OrderBy(o => o.NomeFantasia).ToList();

            ViewData["UnidadeRequisitada"] = unidadesRequisitadas.ToSelectList("NomeFantasia", model.UnidadeRequisitada);

            var tipoItems = _tipoItemRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["TipoItem"] = tipoItems.ToSelectList("Descricao", model.TipoItem);

            var centroCustos = _centroCustoRepository.Find().OrderBy(o => o.Nome).ToList();
            ViewData["CentroCusto"] = centroCustos.ToSelectList("Nome", model.CentroCusto);

            ViewBag.AgruparPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
            ViewBag.OrdenarPor = new SelectList(_colunasPesquisaReservaMaterial, "value", "key");
        }

        protected void PopulateViewDataNovoEditar(RequisicaoMaterialModel model)
        {
            var unidades = _pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo).OrderBy(o => o.NomeFantasia).ToList();
            ViewData["UnidadeRequerente"] = unidades.ToSelectList("NomeFantasia", model.UnidadeRequerente);

            var unidadesRequisitadas = _depositoMaterialRepository.Find(p => p.Unidade != null && p.Ativo).Select(x => x.Unidade).ToList()
                .GroupBy(x => x.Id).Select(x => x.First()).OrderBy(o => o.NomeFantasia).ToList();

            ViewData["UnidadeRequisitada"] = unidadesRequisitadas.ToSelectList("NomeFantasia", model.UnidadeRequisitada);

            var tipoItems = _tipoItemRepository.Find().OrderBy(o => o.Descricao).ToList();
            ViewData["TipoItem"] = tipoItems.ToSelectList("Descricao", model.TipoItem);

            var centroCustos = _centroCustoRepository.Find(x => x.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["CentroCusto"] = centroCustos.ToSelectList("Nome", model.CentroCusto);
        }
        #endregion

        #region ValidaNovoOuEditar

        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #region PreecheColunasPesquisa
        private void PreecheColunasPesquisa()
        {
            _colunasPesquisaReservaMaterial = new Dictionary<string, string>
                           {
                               {"Centro de Custo", "CentroCusto.Nome"},
                               {"Origem", "Origem"},
                               {"Requerente", "Requerente.Nome"},
                               {"Situação", "SituacaoRequisicaoMaterial"},
                               {"Tipo de Material", "TipoItem.Descricao"},
                               {"Unidade Requerente", "UnidadeRequerente.NomeFantasia"},
                               {"Unidade Requisitada", "UnidadeRequisitada.NomeFantasia"}
                           };
        }
        #endregion

        #endregion

        #region Imprimir
        public virtual ActionResult Imprimir(long id)
        {
            var requisicao = _requisicaoMaterialRepository.Get(id);

            var report = new RequisicaoMaterialReport() { DataSource = requisicao };
            var filename = report.ToByteStream().SaveFile(".pdf");

            return File(filename);
        }
        #endregion
    }
}