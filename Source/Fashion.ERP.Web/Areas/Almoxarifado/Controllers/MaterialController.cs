﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
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
using Fashion.Framework.UnitOfWork.DinamicFilter;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class MaterialController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Subcategoria> _subcategoriaRepository;
        private readonly IRepository<Familia> _familiaRepository;
        private readonly IRepository<MarcaMaterial> _marcaMaterialRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<TipoItem> _tipoItemRepository;
        private readonly IRepository<GeneroFiscal> _generoFiscalRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<OrigemSituacaoTributaria> _origemSituacaoTributariaRepository;
        private readonly IRepository<ReferenciaExterna> _referenciaExternaRepository;
        private readonly IRepository<UltimoNumero> _ultimoNumeroRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger _logger;
        private Dictionary<string, string> _colunasPesquisaMaterial;
        private readonly string[] _tipoRelatorio = { "Detalhado", "Listagem", "Sintético" };
        public const string ChaveReferenciaExterna = "BBA07134-0372-45d5-9B37-5979EB88C221";
        #endregion

        #region Construtores
        public MaterialController(ILogger logger, IRepository<Material> materialRepository,
            IRepository<Categoria> categoriaRepository, IRepository<Subcategoria> subcategoriaRepository,
            IRepository<Familia> familiaRepository, IRepository<MarcaMaterial> marcaMaterialRepository,
            IRepository<UnidadeMedida> unidadeMedidaRepository, IRepository<TipoItem> tipoItemRepository,
            IRepository<GeneroFiscal> generoFiscalRepository, IRepository<Pessoa> pessoaRepository,
            IRepository<OrigemSituacaoTributaria> origemSituacaoTributariaRepository, IRepository<ReferenciaExterna> referenciaExternaRepository,
            IRepository<UltimoNumero> ultimoNumeroRepository, IRepository<EstoqueMaterial> estoqueMaterialRepository,
            IRepository<Usuario> usuarioRepository)
        {
            _materialRepository = materialRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _familiaRepository = familiaRepository;
            _marcaMaterialRepository = marcaMaterialRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _tipoItemRepository = tipoItemRepository;
            _generoFiscalRepository = generoFiscalRepository;
            _pessoaRepository = pessoaRepository;
            _origemSituacaoTributariaRepository = origemSituacaoTributariaRepository;
            _referenciaExternaRepository = referenciaExternaRepository;
            _ultimoNumeroRepository = ultimoNumeroRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;

            PreecheColunasPesquisa();
        }
        #endregion

        #region Colunas Ordenação

        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"Referencia", "Referencia"},
            {"Descricao", "Descricao"},
            {"MarcaMaterial", "MarcaMaterial.Nome"},
            {"Categoria", "Subcategoria.Categoria.Nome"},
            {"Subcategoria", "Subcategoria.Nome"},
            {"Familia", "Familia.Nome"},
            {"UnidadeMedida", "UnidadeMedida.Descricao"},
        };
        #endregion

        #region Views

        #region Index
        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaMaterialModel { ModoConsulta = "Listar" };

            return View(model);
        }
        
        private IQueryable<Material> ObtenhaQueryFiltrada(PesquisaMaterialModel model, StringBuilder filtros)
        {
            var materiais = _materialRepository.Find();
            if (!string.IsNullOrWhiteSpace(model.Referencia))
            {
                materiais = materiais.Where(p => p.Referencia == model.Referencia);
                filtros.AppendFormat("Referência: {0}, ", model.Referencia);
            }

            if (!string.IsNullOrWhiteSpace(model.Descricao))
            {
                materiais = materiais.Where(p => p.Descricao.Contains(model.Descricao));
                filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
            }

            if (model.MarcaMaterial.HasValue)
            {
                materiais = materiais.Where(p => p.MarcaMaterial.Id == model.MarcaMaterial);
                filtros.AppendFormat("Marca do material: {0}, ", _marcaMaterialRepository.Get(model.MarcaMaterial.Value).Nome);
            }

            if (model.Categoria.HasValue)
            {
                materiais = materiais.Where(p => p.Subcategoria.Categoria.Id == model.Categoria);
                filtros.AppendFormat("Categoria: {0}, ", _categoriaRepository.Get(model.Categoria.Value).Nome);
            }

            if (model.Subcategoria.HasValue)
            {
                materiais = materiais.Where(p => p.Subcategoria.Id == model.Subcategoria);
                filtros.AppendFormat("Subcategoria: {0}, ", _subcategoriaRepository.Get(model.Subcategoria.Value).Nome);
            }

            if (model.Familia.HasValue)
            {
                materiais = materiais.Where(p => p.Familia.Id == model.Familia);
                filtros.AppendFormat("Família: {0}, ", _familiaRepository.Get(model.Familia.Value).Nome);
            }

            if (model.UnidadeMedida.HasValue)
            {
                materiais = materiais.Where(p => p.UnidadeMedida.Id == model.UnidadeMedida);
                filtros.AppendFormat("Unidade de medida: {0}, ", _unidadeMedidaRepository.Get(model.UnidadeMedida.Value).Descricao);
            }

            if (model.TipoItem.HasValue)
            {
                materiais = materiais.Where(p => p.TipoItem.Id == model.TipoItem);
                filtros.AppendFormat("Tipo item: {0}, ", _tipoItemRepository.Get(model.TipoItem.Value).Descricao);
            }

            if (model.GeneroFiscal.HasValue)
            {
                materiais = materiais.Where(p => p.GeneroFiscal.Id == model.GeneroFiscal);
                filtros.AppendFormat("Gênero fiscal: {0}, ", _generoFiscalRepository.Get(model.GeneroFiscal.Value).Descricao);
            }

            if (model.Ativo.HasValue)
            {
                materiais = materiais.Where(p => p.Ativo == model.Ativo);
                filtros.AppendFormat("Ativo: {0}, ", model.Ativo.Value.ToSimNao());
            }

            if (!string.IsNullOrWhiteSpace(model.CodigoBarra))
            {
                materiais = materiais.Where(p => p.CodigoBarra == model.CodigoBarra);
                filtros.AppendFormat("Código de barras: {0}, ", model.CodigoBarra);
            }

            if (!string.IsNullOrWhiteSpace(model.Ncm))
            {
                materiais = materiais.Where(p => p.Ncm == model.Ncm);
                filtros.AppendFormat("NCM: {0}, ", model.Ncm);
            }

            if (model.OrigemSituacaoTributaria.HasValue)
            {
                materiais = materiais.Where(p => p.OrigemSituacaoTributaria.Id == model.OrigemSituacaoTributaria);
                filtros.AppendFormat("Origem: {0}, ", _origemSituacaoTributariaRepository.Get(model.OrigemSituacaoTributaria.Value).Codigo);
            }

            if (!string.IsNullOrWhiteSpace(model.Localizacao))
            {
                materiais = materiais.Where(p => p.Localizacao.Contains(model.Localizacao));
                filtros.AppendFormat("Localização: {0}, ", model.Localizacao);
            }

            return materiais;
        }
        
        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaMaterialModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var materiais = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        materiais = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? materiais.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : materiais.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                materiais = materiais.OrderByDescending(o => o.DataAlteracao);

                var total = materiais.Count();

                if (request.Page > 0)
                {
                    materiais = materiais.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = materiais.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridMaterialModel
                {
                    Id = (long)p.Id,
                    Referencia = p.Referencia,
                    Descricao = p.Descricao,
                    MarcaMaterial = p.MarcaMaterial.Nome,
                    Categoria = p.Subcategoria.Categoria.Nome,
                    Subcategoria = p.Subcategoria.Nome,
                    Familia = p.Familia != null ? p.Familia.Nome : null,
                    UnidadeMedida = p.UnidadeMedida.Descricao,
                    Foto = (p.Foto != null ? p.Foto.Nome.GetFileUrl() : string.Empty),
                    Ativo = p.Ativo
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
        
        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Index(PesquisaMaterialModel model)
        {
            try
            {
                var filtros = new StringBuilder();
                var materiais = ObtenhaQueryFiltrada(model, filtros);
              
                // Se não é uma listagem, gerar o relatório
                var result = materiais
                    .Fetch(p => p.Foto)
                    .Fetch(p => p.Familia)
                    .Fetch(p => p.GeneroFiscal)
                    .Fetch(p => p.MarcaMaterial)
                    .Fetch(p => p.OrigemSituacaoTributaria)
                    .Fetch(p => p.Subcategoria)
                    .Fetch(p => p.TipoItem)
                    .Fetch(p => p.UnidadeMedida)
                    .FetchMany(p => p.ReferenciaExternas)
                    .ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum item encontrado." });

                #region Montar Relatório
                Report report = null;

                switch (model.TipoRelatorio)
                {
                    case "Detalhado":
                        var ficha = new FichaMaterialReport { DataSource = result };

                        if (filtros.Length > 2)
                            ficha.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        report = ficha;
                        break;
                    case "Listagem":
                        var lista = new ListaMaterialReport { DataSource = result };

                        if (filtros.Length > 2)
                            lista.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        var grupo = lista.Groups.First(p => p.Name.Equals("Grupo"));

                        if (model.AgruparPor != null)
                        {
                            grupo.Groupings.Add("=Fields." + model.AgruparPor);

                            var key = _colunasPesquisaMaterial.First(p => p.Value == model.AgruparPor).Key;
                            var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
                            grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
                        }
                        else
                        {
                            lista.Groups.Remove(grupo);
                        }

                        report = lista;
                        break;
                    case "Sintético":
                        var total = new TotalMaterialReport();

                        if (filtros.Length > 2)
                            total.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

                        total.Grafico.DataSource = result;

                        // Altura do gráfico
                        total.Grafico.Height = Unit.Pixel(ObtenhaCountAgrupamentoMaterial(result, model.AgruparPor) * 30 + 50);

                        // Agrupar
                        total.Grafico.Series[0].CategoryGroup.Groupings[0].Expression = "=Fields." + model.AgruparPor;

                        // Título
                        total.Grafico.Titles[0].Text = _colunasPesquisaMaterial.FirstOrDefault(p => p.Value == model.AgruparPor).Key;

                        report = total;
                        break;
                }

                if (model.OrdenarPor != null)
                    report.Sortings.Add("=Fields." + model.OrdenarPor, model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);
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

        private int ObtenhaCountAgrupamentoMaterial(IEnumerable<Material> result,  string agruparPor)
        {switch (agruparPor)
            {
                case "Aliquota":
                    return result.GroupBy(x => x.Aliquota).Count();
                case "Ativo":
                    return result.GroupBy(x => x.Ativo).Count();
                case "Subcategoria.Categoria.Nome":
                    return result.GroupBy(x => x.Subcategoria.Categoria.Nome).Count();
                case "CodigoBarra":
                    return result.GroupBy(x => x.CodigoBarra).Count();
                case "Descricao":
                    return result.GroupBy(x => x.Descricao).Count();
                case "Detalhamento":
                    return result.GroupBy(x => x.Detalhamento).Count();
                case "Familia.Nome":
                    return result.GroupBy(x => x.Familia.Nome).Count();
                case "GeneroFiscal.Codigo":
                    return result.GroupBy(x => x.GeneroFiscal.Codigo).Count();
                case "Localizacao":
                    return result.GroupBy(x => x.Localizacao).Count();
                case "Marca.Nome":
                    return result.GroupBy(x => x.MarcaMaterial.Nome).Count();
                case "Ncm":
                    return result.GroupBy(x => x.Ncm).Count();
                case "Origem":
                    return result.GroupBy(x => x.OrigemSituacaoTributaria.Descricao).Count();
                case "PesoBruto":
                    return result.GroupBy(x => x.PesoBruto).Count();
                case "PesoLiquido":
                    return result.GroupBy(x => x.PesoLiquido).Count();
                case "Referencia":
                    return result.GroupBy(x => x.Referencia).Count();
                case "Subcategoria.Nome":
                    return result.GroupBy(x => x.Subcategoria.Nome).Count();
                case "TipoItem.Codigo":
                    return result.GroupBy(x => x.Aliquota).Count();
                case "UnidadeMedida.Descricao":
                    return result.GroupBy(x => x.Aliquota).Count();
                default:
                    throw new Exception("Agrupador não cadastrado.");
            }
        }

        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            TempData.Remove(ChaveReferenciaExterna);
            
            return View(new MaterialModel());
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(MaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Material>(model);
                    domain.Ativo = true;
                    
                    domain.AddReferenciaExterna(MapearReferencias().ToArray());

                    domain.Foto = !string.IsNullOrWhiteSpace(domain.Foto.Nome) 
                        ? ArquivoController.SalvarArquivo(model.FotoNome, model.Descricao) 
                        : null;

                    if (string.IsNullOrEmpty(domain.Referencia))
                    {
                        domain.Referencia = ObtenhaProximaReferencia().ToString();
                    }

                    _materialRepository.Save(domain);

                    this.AddSuccessMessage("Material cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o material. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private long ObtenhaProximaReferencia()
        {
            var ultimoNumero = _ultimoNumeroRepository.Get(x => x.NomeTabela == "material");
            long numero = 0;

            if (ultimoNumero != null)
            {
                ultimoNumero = ObtenhaProximaReferenciaDisponivel(ultimoNumero);
                numero = ultimoNumero.Numero;
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
            }
            else
            {
                _ultimoNumeroRepository.SaveOrUpdate(new UltimoNumero{NomeTabela = "material", Numero = 1});
                numero = 1;
            }
            
            return numero;
        }

        private UltimoNumero ObtenhaProximaReferenciaDisponivel(UltimoNumero ultimoNumero)
        {
            ultimoNumero.Numero++;
            var material = _materialRepository.Get(x => x.Referencia == ultimoNumero.Numero.ToString());
            return material != null ? ObtenhaProximaReferenciaDisponivel(ultimoNumero) : ultimoNumero;
        }

        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            _materialRepository.Evict(_materialRepository.Load(id));

            var domain = _materialRepository.Get(id);

            if (domain != null)
            {
                TempData[ChaveReferenciaExterna] = new List<ReferenciaExternaModel>(
                    domain.ReferenciaExternas.Select(r => new ReferenciaExternaModel
                    {
                        Id = r.Id.GetValueOrDefault(),
                        Material = r.Material.Id,
                        Referencia = r.Referencia,
                        Descricao = r.Descricao,
                        CodigoBarra = r.CodigoBarra,
                        Preco = r.Preco,
                        Fornecedor = r.Fornecedor.Id,
                        NomeFornecedor = r.Fornecedor.Nome
                    }));

                var model = Mapper.Flat<MaterialModel>(domain);
                model.Categoria = _subcategoriaRepository.Get(model.Subcategoria).Categoria.Id;
                ViewBag.Filename = domain.Foto != null ? domain.Foto.Nome : string.Empty;
                model.Ncm = model.Ncm ?? String.Empty;
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o material.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(MaterialModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Busca o nome do arquivo da foto antiga
                    var material = _materialRepository.Get(model.Id);
                    var nomeFoto = material.Foto != null 
                        ? material.Foto.Nome 
                        : string.Empty;

                    if (string.IsNullOrWhiteSpace(model.FotoNome))
                    {
                        material.Foto = null;
                        model.FotoId = null;
                        model.FotoNome = string.Empty;
                    }

                    var domain = Mapper.Unflat(model, material);

                    domain.ClearReferenciaExterna();
                    domain.AddReferenciaExterna(MapearReferencias().ToArray());

                    if (string.IsNullOrWhiteSpace(model.FotoNome))
                        domain.Foto = null;
                    else if (nomeFoto != model.FotoNome)
                        domain.Foto = ArquivoController.SalvarArquivo(model.FotoNome, model.Descricao);
                    
                    _materialRepository.Update(domain);

                    this.AddSuccessMessage("Material atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o material. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
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
                    var domain = _materialRepository.Get(id);
                    _materialRepository.Delete(domain);
                    Framework.UnitOfWork.Session.Current.Flush();
                    this.AddSuccessMessage("Material excluído com sucesso");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    var nomeTabela = exception.GetMessage().ObtenhaNomeTabelaDependente();
                    ModelState.AddModelError("", "O material não pode ser excluído pois o conceito " + nomeTabela + " depende deste material.");
                     
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region EditarSituacao
        [HttpPost]
        public virtual ActionResult EditarSituacao(long id)
        {
            try
            {
                var domain = _materialRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _materialRepository.Update(domain);
                    this.AddSuccessMessage("Material {0} com sucesso", situacao ? "ativado" : "inativado");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do material: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region ReferenciaExternas

        #region LerReferenciaExternas
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerReferenciaExternas([DataSourceRequest]DataSourceRequest request)
        {
            var referencias = TempData.Peek(ChaveReferenciaExterna) as List<ReferenciaExternaModel>;

            if (referencias != null)
                return Json(referencias.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AdicionarReferenciaExterna
        [HttpPost]
        public virtual ActionResult AdicionarReferenciaExterna(ReferenciaExternaModel referenciaExterna)
        {
            string retorno = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    var referencias = TempData.Peek(ChaveReferenciaExterna) as List<ReferenciaExternaModel>
                                            ?? new List<ReferenciaExternaModel>();

                    // Verificar se já existe referência externa cadastrada
                    if (referencias.Any(p => p.Referencia == referenciaExterna.Referencia && p.Fornecedor == referenciaExterna.Fornecedor))
                    {
                        retorno = "Já existe uma referência externa cadastrada com a mesma referência e fornecedor.";
                    }
                    else
                    {
                        referenciaExterna.Id = Numeric.Random(-100000, -1);
                        referenciaExterna.NomeFornecedor = _pessoaRepository.Get(referenciaExterna.Fornecedor).Nome;
                        referenciaExterna.Material = 0;
                        if (!referenciaExterna.Preco.HasValue) referenciaExterna.Preco = 0;

                        referencias.Add(referenciaExterna);

                        TempData[ChaveReferenciaExterna] = referencias;
                    }
                }
                catch (Exception exception)
                {
                    retorno = exception.GetMessage();
                }
            }
            else
            {
                retorno = string.Join("\r\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            return Content(retorno);
        }
        #endregion

        #region RemoverReferenciaExterna
        [HttpPost]
        public virtual ActionResult RemoverReferenciaExterna(ReferenciaExternaModel referenciaExterna)
        {
            var referencias = TempData.Peek(ChaveReferenciaExterna) as List<ReferenciaExternaModel>;

            if (referencias != null)
            {
                var referencia = referencias.FirstOrDefault(p => p.Id == referenciaExterna.Id);

                if (referencia != null)
                    referencias.Remove(referencia);
            }

            return Content(string.Empty);
        }
        #endregion

        #region MapearReferencias
        /// <summary>
        /// Mapeia as referências externas temporárias para o domínio.
        /// </summary>
        private List<ReferenciaExterna> MapearReferencias()
        {
            var referencias = new List<ReferenciaExterna>();

            var referenciasModel = TempData[ChaveReferenciaExterna] as List<ReferenciaExternaModel>;

            if (referenciasModel != null)
            {
                foreach (var referencia in referenciasModel.Select(Mapper.Unflat<ReferenciaExterna>))
                {
                    referencia.Id = null;
                    referencias.Add(referencia);
                }
            }

            return referencias;
        }
        #endregion

        #endregion

        #region Pesquisar Material

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult Pesquisar()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region PesquisarVarios
        [ChildActionOnly]//OutputCache(Duration = 3600)
        public virtual ActionResult PesquisarVarios()
        {
            PreencheColuna();
            return PartialView();
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltro(PesquisarMaterialModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Ativo
                new FilterExpression("Ativo", ComparisonOperator.IsEqual, true, LogicOperator.And),
                // Filtro da tela
                model.Filtrar<Material>()
            };

            if(model.TipoItemMaterial.HasValue)
                filters.Add(new FilterExpression("TipoItem.Id", ComparisonOperator.IsEqual, model.TipoItemMaterial, LogicOperator.And));

            var materiais = _materialRepository.Find(filters.ToArray()).ToList().OrderBy(o => o.Descricao);

            var list = materiais.Select(p => new GridMaterialModel
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Descricao = p.Descricao,
                MarcaMaterial = p.MarcaMaterial.Nome,
                Categoria = p.Subcategoria.Categoria.Nome,
                Subcategoria = p.Subcategoria.Nome,
                Familia = p.Familia == null ? null : p.Familia.Nome,
                UnidadeMedida = p.UnidadeMedida.Sigla,
                //UltimoCusto = ObtenhaUltimoCustoMaterial(material)
                Foto = (p.Foto != null ? p.Foto.Nome.GetFileUrl() : string.Empty)
            }).ToList();
            return Json(list);
        }

        #endregion

        #region PesquisarReferencia
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarReferencia(string referencia, long? tipoItemMaterial, long? fornecedorId)
        {
            if (string.IsNullOrWhiteSpace(referencia) == false)
            {
                var filters = new List<FilterExpression>
                {
                    new FilterExpression("Ativo", ComparisonOperator.IsEqual, true, LogicOperator.And),
                    new FilterExpression("Referencia", ComparisonOperator.IsEqual, referencia, LogicOperator.And)
                };

                if (tipoItemMaterial.HasValue)
                    filters.Add(new FilterExpression("TipoItem.Id", ComparisonOperator.IsEqual, tipoItemMaterial,
                        LogicOperator.And));

                var material = _materialRepository.Find(filters.ToArray()).FirstOrDefault();

                if (material != null)
                {
                    var referenciaExterna =
                        material.ReferenciaExternas.FirstOrDefault(x => x.Fornecedor.Id == fornecedorId);
                    
                    return
                        Json(
                            new
                            {
                                material.Id,
                                material.Referencia,
                                material.Descricao,
                                UnidadeMedida = material.UnidadeMedida.Sigla,
                                ReferenciaExterna = referenciaExterna != null ? referenciaExterna.Referencia : null
                            }, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { erro = "Nenhum material encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarId(long id)
        {
            var material = _materialRepository.Get(id);

            if (material != null)
                return Json(new { material.Id, material.Referencia, material.Descricao, material.UnidadeMedida.Sigla, Foto = (material.Foto != null ? material.Foto.Nome.GetFileUrl() : string.Empty) }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhum material encontrado." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColuna
        private void PreencheColuna()
        {
            var coluna = new[]
                              {
                                  new { value = "Descricao", text = "Descrição"},
                                  new { value = "Referencia", text = "Referência"},
                                  new { value = "Detalhamento", text = "Detalhamento"},
                                  new { value = "UnidadeMedida.Sigla", text = "Unidade de medida"},
                                  new { value = "Subcategoria.Categoria.Nome", text = "Categoria"},
                                  new { value = "Subcategoria.Nome", text = "Subcategoria"},
                                  new { value = "Familia.Nome", text = "Família"},
                                  new { value = "MarcaMaterial.Nome", text = "Marca do material"},
                                  new { value = "NCM", text = "NCM"},
                                  new { value = "CodigoBarras", text = "Código de barras"},
                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #region Pesquisar ReferenciaExterna

        #region Pesquisar
        [ChildActionOnly, OutputCache(Duration = 3600)]
        public virtual ActionResult PesquisarReferenciaExterna()
        {
            PreencheColunaReferenciaExterna();
            return PartialView();
        }
        #endregion

        #region PesquisarFiltro
        [HttpPost, AjaxOnly]
        public virtual ActionResult PesquisarFiltroReferenciaExterna(PesquisarModel model)
        {
            var filters = new List<FilterExpression>
            {
                // Filtro da tela
                model.Filtrar<ReferenciaExterna>()
            };

            var referenciaExternas = _referenciaExternaRepository.Find(filters.ToArray()).ToList();

            var list = referenciaExternas.Select(p => new GridReferenciaExternaModel
            {
                Id = p.Id.GetValueOrDefault(),
                Referencia = p.Referencia,
                Descricao = p.Descricao,
                MaterialId = p.Material.Id ?? 0,
                Material = p.Material.Referencia + " - " + p.Material.Descricao,
                MaterialReferencia = p.Material.Referencia,
                MaterialDescricao = p.Material.Descricao
            }).ToList();

            return Json(list);
        }
        #endregion

        #region PesquisarReferencia
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarReferenciaReferenciaExterna(string referencia)
        {
            if (string.IsNullOrWhiteSpace(referencia) == false)
            {
                var referenciaExterna = _referenciaExternaRepository.Find(p => p.Referencia == referencia).FirstOrDefault();

                if (referenciaExterna != null)
                    return Json(new
                    {
                        referenciaExterna.Id, 
                        referenciaExterna.Referencia, 
                        referenciaExterna.Descricao,
                        MaterialId = referenciaExterna.Material.Id ?? 0,
                        MaterialReferencia = referenciaExterna.Material.Referencia,
                        MaterialDescricao = referenciaExterna.Material.Descricao
                    }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { erro = "Nenhuma referência externa encontrada." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PesquisarId
        [HttpGet, AjaxOnly]
        public virtual ActionResult PesquisarReferenciaExternaId(long id)
        {
            var referenciaExterna = _referenciaExternaRepository.Get(id);

            if (referenciaExterna != null)
                return Json(new
                {
                    referenciaExterna.Id,
                    referenciaExterna.Referencia,
                    referenciaExterna.Descricao,
                    MaterialId = referenciaExterna.Material.Id ?? 0,
                    MaterialReferencia = referenciaExterna.Material.Referencia,
                    MaterialDescricao = referenciaExterna.Material.Descricao
                }, JsonRequestBehavior.AllowGet);

            return Json(new { erro = "Nenhuma referência externa encontrada." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PreencheColunaReferenciaExterna
        private void PreencheColunaReferenciaExterna()
        {
            var coluna = new[]
                              {
                                  new { value = "Descricao", text = "Descrição"},
                                  new { value = "Referencia", text = "Referência"}

                              };
            ViewData["ColunaPesquisa"] = new SelectList(coluna, "value", "text");
        }
        #endregion

        #endregion

        #region UnidadeMedida
        [AjaxOnly]
        public virtual ActionResult UnidadeMedida(long? id)
        {
            try
            {
                var domain = _materialRepository.Find(c => c.Id == id)
                    .Fetch(f => f.UnidadeMedida).FirstOrDefault();

                if (domain != null)
                {
                    var unidadeMedida =
                        new
                        {
                            Id = domain.UnidadeMedida.Id.GetValueOrDefault(),
                            domain.UnidadeMedida.Sigla,
                            domain.UnidadeMedida.Descricao,
                            domain.UnidadeMedida.Ativo,
                            domain.UnidadeMedida.FatorMultiplicativo
                        };
                    return Json(unidadeMedida, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar a unidade de medida do material." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Qtde. em Estoque
        [AjaxOnly]
        public virtual ActionResult QuantidadeEstoque(long? id, long? depositoId)
        {
            try
            {
                var domain = _estoqueMaterialRepository.Get(c => c.DepositoMaterial.Id == depositoId.Value && c.Material.Id == id);

                if (domain != null)
                {
                    var qtdeEstoque = domain.Quantidade;
                    return Json(qtdeEstoque, JsonRequestBehavior.AllowGet);
                }
                
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.GetMessage());
            }

            return Json(new { Error = "Ocorreu um erro ao buscar a quantidade em estoque do material." }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Custo

        public virtual ActionResult MaterialCusto(long id)
        {
            var material = _materialRepository.Load(id);

            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;

            if (funcionarioId == null)
            {
                this.AddErrorMessage("O usuário logado não possui funcionário associado a ele.");
                return RedirectToAction("Index");
            }

            var model = new MaterialCustoModel()
            {
                Referencia = material.Referencia,
                Descricao = material.Descricao,
                UnidadeMedida = material.UnidadeMedida.Sigla,
                Foto = material.Foto != null ? material.Foto.Nome.GetFileUrl() : string.Empty,
                FuncionarioLogado = usuario.Funcionario.Nome,
                GridItens = new List<GridMaterialCustoModel>()
            };

            material.CustoMaterials.OrderByDescending(x => x.Data).ForEach(custoMaterial =>
            {
                var editavel = custoMaterial.CadastroManual && custoMaterial.Funcionario.Id == usuario.Funcionario.Id;

                var gridItem = new GridMaterialCustoModel
                {
                    Id = custoMaterial.Id,
                    Responsavel = custoMaterial.Funcionario != null ? custoMaterial.Funcionario.Nome : "",
                    Ativo = custoMaterial.Ativo,
                    CadastroManual = custoMaterial.CadastroManual,
                    Custo = custoMaterial.Custo,
                    CustoAquisicao = custoMaterial.CustoAquisicao,
                    DataCustoString = custoMaterial.Data.ToString("dd/MM/yyyy"),
                    Fornecedor = custoMaterial.Fornecedor.Nome,
                    CodigoFornecedor = custoMaterial.Fornecedor.Fornecedor.Codigo,
                    Editavel = editavel
                };

                model.GridItens.Add(gridItem);
            });

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult MaterialCusto(MaterialCustoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var material = _materialRepository.Get(model.Id);

                    var userId = FashionSecurity.GetLoggedUserId();
                    var usuario = _usuarioRepository.Get(userId);
                    if (model.GridItens != null)
                    {
                        model.GridItens.ForEach(modelItem =>
                        {
                            if (modelItem.Id.HasValue && modelItem.Id.Value != 0)
                            {
                                var custoMaterial = material.CustoMaterials.First(x => x.Id == modelItem.Id);
                                custoMaterial.Ativo = modelItem.Ativo;
                                custoMaterial.Custo = modelItem.Custo;
                                custoMaterial.CustoAquisicao = modelItem.CustoAquisicao;
                            }
                            else
                            {
                                var custoMaterial = new CustoMaterial()
                                {
                                    Ativo = modelItem.Ativo,
                                    CadastroManual = true,
                                    Custo = modelItem.Custo,
                                    CustoAquisicao = modelItem.CustoAquisicao,
                                    Data = DateTime.Now,
                                    Fornecedor = _pessoaRepository.Find(p => p.Fornecedor != null
                                        && p.Fornecedor.Codigo == modelItem.CodigoFornecedor).First(),
                                    Funcionario = usuario.Funcionario
                                };

                                material.CustoMaterials.Add(custoMaterial);
                            }
                        });
                    }

                    ExcluaCustoMaterial(model, material);

                    _materialRepository.SaveOrUpdate(material);

                    this.AddSuccessMessage("Custo do material atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar custo do material. Confira se os dados foram informados corretamente: " 
                        + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }


        private void ExcluaCustoMaterial(MaterialCustoModel model, Material material)
        {
            var custoMaterialItensExcluir = new List<CustoMaterial>();
            material.CustoMaterials.ForEach(x =>
            {
                if (model.GridItens.All(y => y.Id != x.Id && x.Id != null))
                {
                    custoMaterialItensExcluir.Add(x);
                }
            });

            custoMaterialItensExcluir.ForEach(x => material.CustoMaterials.Remove(x));

            var custoMaterialAtivo = material.CustoMaterials.FirstOrDefault(x => x.Ativo);
            if (custoMaterialAtivo == null)
            {
                var ultimoCusto = material.CustoMaterials.OrderBy(x => x.Data).LastOrDefault();
                if (ultimoCusto != null)
                {
                    ultimoCusto.Ativo = true;
                }
            }
        }

        #endregion

        #endregion

        #region Métodos

        private string RetornarReferenciaExterna(long materialId, long fornecedorId)
        {
            if (fornecedorId == 0)
            {
                return null;
            }

            var referenciaExterna =
                _referenciaExternaRepository.Find(
                    r => r.Fornecedor.Id == fornecedorId && r.Material.Id == materialId)
                    .Select(r => r.Referencia)
                    .FirstOrDefault();

            return referenciaExterna;
        }

        #region PopulateViewData
        protected void PopulateViewData(IMaterialDropdownModel model)
        {
            var categorias = _categoriaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Categoria"] = categorias.ToSelectList("Nome", model.Categoria);

            var subcategorias = _subcategoriaRepository.Find(p => p.Categoria.Id == model.Categoria && p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Subcategoria"] = subcategorias.ToSelectList("Nome", model.Subcategoria);

            var familias = _familiaRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["Familia"] = familias.ToSelectList("Nome", model.Familia);

            var marcas = _marcaMaterialRepository.Find(p => p.Ativo).OrderBy(o => o.Nome).ToList();
            ViewData["MarcaMaterial"] = marcas.ToSelectList("Nome", model.MarcaMaterial);

            var unidadeMedidas = _unidadeMedidaRepository.Find(p => p.Ativo).OrderBy(o => o.Sigla).ToList();
            ViewData["UnidadeMedida"] = unidadeMedidas.ToSelectList("Sigla", model.UnidadeMedida);

            var origens = _origemSituacaoTributariaRepository.Find().Select(p => new { p.Id, Descricao = p.Codigo + " - " + p.Descricao }).ToList();
            ViewData["OrigemSituacaoTributaria"] = new SelectList(origens, "Id", "Descricao", model.OrigemSituacaoTributaria);

            var tipoItens = _tipoItemRepository.Find().Select(p => new { p.Id, Descricao = p.Codigo + " - " + p.Descricao }).ToList();
            ViewData["TipoItem"] = new SelectList(tipoItens, "Id", "Descricao", model.TipoItem);

            var generoFiscais = _generoFiscalRepository.Find().Select(p => new { p.Id, Descricao = p.Codigo + " - " + p.Descricao }).ToList();
            ViewData["GeneroFiscal"] = new SelectList(generoFiscais, "Id", "Descricao", model.GeneroFiscal);

            if (ValueProvider.GetValue("action").AttemptedValue == "Index")
            {
                ViewBag.TipoRelatorio = new SelectList(_tipoRelatorio);
                ViewBag.AgruparPor = new SelectList(_colunasPesquisaMaterial, "value", "key");
                ViewBag.OrdenarPor = new SelectList(_colunasPesquisaMaterial, "value", "key");
            }
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var material = (MaterialModel)model;

            // Verificar se existe material com esta referência
            if (_materialRepository.Find().Any(p => p.Referencia == material.Referencia && p.Id != material.Id))
                ModelState.AddModelError("Referencia", "Já existe material cadastrado com esta referência.");

            // Verificar se existe material com este código de barras
            if (string.IsNullOrWhiteSpace(material.CodigoBarra) == false &&
                _materialRepository.Find().Any(p => p.CodigoBarra == material.CodigoBarra && p.Id != material.Id))
                ModelState.AddModelError("CodigoBarra", "Já existe material cadastrado com este código de barras.");
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
            _colunasPesquisaMaterial = new Dictionary<string, string>
                           {
                               {"Alíquota", "Aliquota"},
                               {"Ativo", "Ativo"},
                               {"Categoria", "Subcategoria.Categoria.Nome"},
                               {"Código de barras", "CodigoBarra"},
                               {"Descrição", "Descricao"},
                               {"Detalhamento", "Detalhamento"},
                               {"Família", "Familia.Nome"},
                               {"Gênero fiscal", "GeneroFiscal.Codigo"},
                               {"Localização", "Localizacao"},
                               {"Marca", "Marca.Nome"},
                               {"NCM", "Ncm"},
                               {"Origem", "Origem"},
                               {"Peso bruto", "PesoBruto"},
                               {"Peso líquido", "PesoLiquido"},
                               {"Referência", "Referencia"},
                               {"Subcategoria", "Subcategoria.Nome"},
                               {"Tipo do item", "TipoItem.Codigo"},
                               {"Unidade de medida", "UnidadeMedida.Descricao"},
                           };

        }
        #endregion

        #region VerificarReferencia
        [AjaxOnly]
        public virtual JsonResult VerificarReferencia(string referencia)
        {
            bool existeMaterial = false;
            long materialId = 0;
            var material = _materialRepository.Get(p => p.Referencia == referencia);

            if (material != null)
            {
                existeMaterial = true;
                materialId = material.Id.GetValueOrDefault();
            }

            var result = new { existeMaterial, materialId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ValorCusto
        [AjaxOnly]
        public virtual JsonResult ObtenhaCusto(string referencia, long idFornecedor)
        {
            var material = _materialRepository.Get(p => p.Referencia == referencia);
            double valorcusto = 0;
            if (material != null)
            {
                var custoMaterial =
                    material.CustoMaterials.Where(x => x.Fornecedor.Id == idFornecedor).OrderByDescending(x => x.Data).FirstOrDefault();
                if (custoMaterial != null)
                {
                    valorcusto = custoMaterial.Custo;
                }
                
                if (valorcusto == 0)
                {
                    custoMaterial = material.CustoMaterials.FirstOrDefault(x => x.Ativo);
                    if (custoMaterial != null)
                    {
                        valorcusto = custoMaterial.Custo;
                    }    
                } 
                
                if (valorcusto == 0)
                {
                    var referenciaExterna =
                        material.ReferenciaExternas.FirstOrDefault(x => x.Fornecedor.Id == idFornecedor);
                    if (referenciaExterna != null)
                    {
                        valorcusto = referenciaExterna.Preco;
                    }
                }
            }

            var result = new { Custo = valorcusto };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual JsonResult ObtenhaCustoAtual(string referencia)
        {
            var material = _materialRepository.Get(p => p.Referencia == referencia);
            double? valorcusto = null;
            if (material != null)
            {
                var custoMaterial = material.CustoMaterials.FirstOrDefault(x => x.Ativo);
                if (custoMaterial != null)
                {
                    valorcusto = custoMaterial.Custo;
                }
            }

            var result = new { Custo = valorcusto.HasValue ? valorcusto.Value : 0 };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        [AjaxOnly]
        public virtual JsonResult ObtenhaGeneroCategoria(string referencia)
        {
            var material = _materialRepository.Get(p => p.Referencia == referencia);
            
            var generoCategoria = material.Subcategoria.Categoria.GeneroCategoria;
            
            var result = new { GeneroCategoria = generoCategoria };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}