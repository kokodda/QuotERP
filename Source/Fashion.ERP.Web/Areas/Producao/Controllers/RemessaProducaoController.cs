﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Web.Areas.Producao.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ninject.Extensions.Logging;
using WebGrease.Css.Extensions;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RemessaProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<RemessaProducao> _remessaProducaoRepository;
        private readonly IRepository<ClassificacaoDificuldade> _classificacaoDificuldadeRepository;
        private readonly IRepository<Colecao> _colecaoRepository;
        private readonly IRepository<UltimoNumero> _ultimoNumeroRepository;
        private readonly ILogger _logger;

        #region Colunas Ordenação
        private static readonly Dictionary<string, string> ColunasOrdenacaoGrid = new Dictionary<string, string>
        {
            {"LoteAno", "Lote"},
            {"Colecao", "Colecao.Descricao"},
            {"DataProgramada", "DataProgramada"},
            {"Tag", "FichaTecnica.Tag"},
            {"Referencia", "FichaTecnica.Referencia"},
            {"Descricao", "FichaTecnica.Descricao"},
            {"QtdeProgramada", "Quantidade"}
        };
        #endregion

        #endregion

        #region Construtores
        public RemessaProducaoController(ILogger logger, IRepository<RemessaProducao> remessaProducaoRepository,
            IRepository<Colecao> colecaoRepository, IRepository<ClassificacaoDificuldade> classificacaoDificuldadeRepository,
            IRepository<UltimoNumero> ultimoNumeroRepository)
        {
            _remessaProducaoRepository = remessaProducaoRepository;
            _classificacaoDificuldadeRepository = classificacaoDificuldadeRepository;
            _colecaoRepository = colecaoRepository;
            _ultimoNumeroRepository = ultimoNumeroRepository;
            
            _logger = logger;
        }
        #endregion

        #region Novo
        [PopulateViewData("PopulateViewData"), HttpGet]
        public virtual ActionResult Novo()
        {
            return View(new RemessaProducaoModel());
        }

        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(RemessaProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<RemessaProducao>(model);
                    domain.Colecao = _colecaoRepository.Load(model.Colecao);
                    
                    if (model.Numero.HasValue)
                    {
                        domain.Numero = model.Numero.GetValueOrDefault();
                        domain.Ano = model.Ano.GetValueOrDefault();
                    }
                    else
                    {
                        domain.Numero = ObtenhaProximoNumero();
                        domain.Ano = DateTime.Now.Year;
                    }

                    model.GridRemessaProducaoCapacidadeProdutiva.ForEach(gridItem =>
                    {
                        var capacidadeProdutivaDomain = new RemessaProducaoCapacidadeProdutiva
                        {
                            ClassificacaoDificuldade =
                                _classificacaoDificuldadeRepository.Get(long.Parse(gridItem.ClassificacaoDificuldade)),
                            Quantidade = gridItem.CapacidadeProducao
                        };

                        domain.RemessasProducaoCapacidadesProdutivas.Add(capacidadeProdutivaDomain);
                    });

                    _remessaProducaoRepository.Save(domain);

                    this.AddSuccessMessage("Remessa de produção cadastrada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, 
                        "Não é possível salvar a remessa de produção. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        
        #endregion
        
        #region Editar

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _remessaProducaoRepository.Get(id);
            var model = Mapper.Flat<RemessaProducaoModel>(domain);
            model.GridRemessaProducaoCapacidadeProdutiva = new List<RemessaProducaoCapacidadeProdutivaItemModel>();

            domain.RemessasProducaoCapacidadesProdutivas.ForEach(capacidadeProdutiva =>
            {
                var capacidadeProdutivaModel = new RemessaProducaoCapacidadeProdutivaItemModel
                {
                    Id = capacidadeProdutiva.Id,
                    CapacidadeProducao = capacidadeProdutiva.Quantidade,
                    ClassificacaoDificuldade = capacidadeProdutiva.ClassificacaoDificuldade.Id.ToString()
                };

                model.GridRemessaProducaoCapacidadeProdutiva.Add(capacidadeProdutivaModel);
            });

            return View(model);
        }
        
        [HttpPost, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Editar(RemessaProducaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _remessaProducaoRepository.Get(model.Id);

                    var ano = domain.Ano;
                    var numero = domain.Numero;

                    domain = Mapper.Unflat(model, domain);
                    domain.Colecao = _colecaoRepository.Load(model.Colecao);
                    domain.Ano = ano;
                    domain.Numero = numero;

                    model.GridRemessaProducaoCapacidadeProdutiva.ForEach(modelItem => EditarRemessaProducaoCapacidadeProdutiva(domain, modelItem));

                    VerifiqueExcluirRemessaProducaoCapacidadeProdutiva(domain, model);

                    _remessaProducaoRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Remessa de produção atualizada com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar a remessa de produção. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        private void VerifiqueExcluirRemessaProducaoCapacidadeProdutiva(RemessaProducao domain, RemessaProducaoModel model)
        {
            var listaExcluir = new List<RemessaProducaoCapacidadeProdutiva>();

            domain.RemessasProducaoCapacidadesProdutivas.ForEach(remessaProducaoCapacidadeProdutiva =>
            {
                if (model.GridRemessaProducaoCapacidadeProdutiva == null ||
                    model.GridRemessaProducaoCapacidadeProdutiva.All(
                        x => x.Id != remessaProducaoCapacidadeProdutiva.Id && remessaProducaoCapacidadeProdutiva.Id != null))
                {
                    listaExcluir.Add(remessaProducaoCapacidadeProdutiva);
                }
            });

            foreach (var remessaProducaoCapacidadeProdutiva in listaExcluir)
            {
                domain.RemessasProducaoCapacidadesProdutivas.Remove(remessaProducaoCapacidadeProdutiva);
            }
        }

        private void EditarRemessaProducaoCapacidadeProdutiva(RemessaProducao domain, RemessaProducaoCapacidadeProdutivaItemModel modelItem)
        {
            if (modelItem.Id.HasValue && modelItem.Id != 0)
            {
                var remessaProducaoCapacidadeProdutiva =
                    domain.RemessasProducaoCapacidadesProdutivas.First(x => x.Id == modelItem.Id);

                remessaProducaoCapacidadeProdutiva.Quantidade = modelItem.CapacidadeProducao;
                remessaProducaoCapacidadeProdutiva.ClassificacaoDificuldade =
                    _classificacaoDificuldadeRepository.Get(long.Parse(modelItem.ClassificacaoDificuldade));
            }
            else
            {
                var remessaProducaoCapacidadeProdutiva = new RemessaProducaoCapacidadeProdutiva
                {
                    ClassificacaoDificuldade =
                        _classificacaoDificuldadeRepository.Get(long.Parse(modelItem.ClassificacaoDificuldade)),
                    Quantidade = modelItem.CapacidadeProducao
                };

                domain.RemessasProducaoCapacidadesProdutivas.Add(remessaProducaoCapacidadeProdutiva);
            }
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
                    var domain = _remessaProducaoRepository.Get(id);
                    _remessaProducaoRepository.Delete(domain);

                    this.AddSuccessMessage("Remessa de produção excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao excluir a remessa de produção: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }

        #endregion

        #region Index
        [PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index()
        {
            var model = new PesquisaRemessaProducaoModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        public virtual ActionResult Index(PesquisaRemessaProducaoModel model)
        {
            return View(model);
        }

        private IQueryable<RemessaProducao> ObtenhaQueryFiltrada(PesquisaRemessaProducaoModel model, StringBuilder filtros)
        {
            var remessasProducao = _remessaProducaoRepository.Find();
            
            if (model.Numero.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.Numero == model.Numero.GetValueOrDefault());
                filtros.AppendFormat("Número: {0}, ", model.Numero);
            }

            if (model.Ano.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.Ano == model.Ano);
                filtros.AppendFormat("Ano: {0}, ", model.Ano);
            }

            if (model.Colecao.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.Colecao.Id == model.Colecao);
                filtros.AppendFormat("Coleção: {0}, ", _colecaoRepository.Get(model.Colecao.Value).Descricao);
            }

            if (!string.IsNullOrEmpty(model.Descricao))
            {
                remessasProducao = remessasProducao.Where(p => p.Descricao.Contains(model.Descricao));
                filtros.AppendFormat("Descrição: {0}, ", model.Descricao);
            }
            
            if (model.DataInicio.HasValue && !model.DataInicioAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataInicio.Date >= model.DataInicio.Value);

                filtros.AppendFormat("Data de início de '{0}', ", model.DataInicio.Value.ToString("dd/MM/yyyy"));
            }
            
            if (!model.DataInicio.HasValue && model.DataInicioAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataInicio.Date <= model.DataInicioAte.Value);
                    
                filtros.AppendFormat("Data de início até '{0}', ", model.DataInicioAte.Value.ToString("dd/MM/yyyy"));
            }
            
            if(model.DataInicio.HasValue && model.DataInicioAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataInicio.Date >= model.DataInicio.Value
                                                                && p.DataInicio.Date <= model.DataInicioAte.Value);

                filtros.AppendFormat("Data de início de '{0}' até '{1}', ", 
                    model.DataInicio.Value.ToString("dd/MM/yyyy"),
                    model.DataInicioAte.Value.ToString("dd/MM/yyyy"));
            }

            if (model.DataLimite.HasValue && !model.DataLimiteAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataLimite.Date >= model.DataLimite.Value);

                filtros.AppendFormat("Data de limite de '{0}', ", model.DataLimite.Value.ToString("dd/MM/yyyy"));
            }

            if (!model.DataLimite.HasValue && model.DataLimiteAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataLimite.Date <= model.DataLimiteAte.Value);

                filtros.AppendFormat("Data de limite até '{0}', ", model.DataLimiteAte.Value.ToString("dd/MM/yyyy"));
            }
            
            if (model.DataLimite.HasValue && model.DataLimiteAte.HasValue)
            {
                remessasProducao = remessasProducao.Where(p => p.DataLimite.Date >= model.DataLimite.Value
                                                         && p.DataLimite.Date <= model.DataLimiteAte.Value);
                filtros.AppendFormat("Data limite de '{0}' até '{1}', ",
                                     model.DataLimite.Value.ToString("dd/MM/yyyy"),
                                     model.DataLimiteAte.Value.ToString("dd/MM/yyyy"));
            }

            return remessasProducao;
        }
        
        public virtual ActionResult ObtenhaListaGridModel([DataSourceRequest] DataSourceRequest request, PesquisaRemessaProducaoModel model)
        {
            try
            {
                var filtros = new StringBuilder();

                var remessasProducao = ObtenhaQueryFiltrada(model, filtros);

                if (!request.Sorts.IsNullOrEmpty())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        remessasProducao = sortDescriptor.SortDirection.ToString() == "Descending"
                            ? remessasProducao.OrderByDescending(ColunasOrdenacaoGrid[sortDescriptor.Member])
                            : remessasProducao.OrderBy(ColunasOrdenacaoGrid[sortDescriptor.Member]);
                    }
                }

                remessasProducao = remessasProducao.OrderByDescending(o => o.DataAlteracao);

                var total = remessasProducao.Count();

                if (request.Page > 0)
                {
                    remessasProducao = remessasProducao.Skip((request.Page - 1) * request.PageSize);
                }

                var resultado = remessasProducao.Take(request.PageSize).ToList();

                var list = resultado.Select(p => new GridRemessaProducaoModel
                {
                    Id = p.Id.GetValueOrDefault(),
                    Descricao= p.Descricao,
                    Colecao = p.Colecao.Descricao,
                    DataInicio= p.DataInicio.Date,
                    DataLimite = p.DataLimite.Date,
                    CapacidadeProdutiva = p.RemessasProducaoCapacidadesProdutivas.Sum(x => x.Quantidade)
                }).ToList();

                var valorPage = request.Page;
                request.Page = 1;
                var data = list.ToDataSourceResult(request);
                request.Page = valorPage;

                var result = new DataSourceResult
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

        #region PopulateViewDataPesquisa

        protected void PopulateViewDataPesquisa(PesquisaRemessaProducaoModel model)
        {
            ViewBag.Colecao = _colecaoRepository.Find(p => p.Ativo).ToList().ToSelectList("Descricao", model.Colecao);
        }
        #endregion

        #region PopulateViewData

        protected void PopulateViewData(RemessaProducaoModel model)
        {
            ViewBag.Colecao = _colecaoRepository.Find(p => p.Ativo).ToList().ToSelectList("Descricao", model.Colecao);

            var classificacoesDificuldade = _classificacaoDificuldadeRepository.Find().ToList();
            ViewBag.ClassificacoesDificuldade = classificacoesDificuldade.Select(s => new {Id = s.Id.ToString(), s.Descricao}).OrderBy(x => x.Descricao);
            ViewBag.ClassificacaoDificuldadeJson = classificacoesDificuldade.ToDictionary(k => k.Id.ToString(), e => e.Descricao).FromDictionaryToJson();
        }
        #endregion

        private long ObtenhaProximoNumero()
        {
            var ultimoNumero = _ultimoNumeroRepository.Get(x => x.NomeTabela == "remessaproducao");
            long numero = 0;

            if (ultimoNumero != null)
            {
                ultimoNumero = ObtenhaProximoNumeroDisponivel(ultimoNumero);
                numero = ultimoNumero.Numero;
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
            }
            else
            {
                ultimoNumero = new UltimoNumero { NomeTabela = "remessaproducao", Numero = 1 };
                ObtenhaProximoNumeroDisponivel(ultimoNumero);
                _ultimoNumeroRepository.SaveOrUpdate(ultimoNumero);
                numero = ultimoNumero.Numero;
            }

            return numero;
        }

        private UltimoNumero ObtenhaProximoNumeroDisponivel(UltimoNumero ultimoNumero)
        {
            ultimoNumero.Numero++;
            var remessaProducao = _remessaProducaoRepository.Get(x => x.Numero == ultimoNumero.Numero);
            return remessaProducao != null ? ObtenhaProximoNumeroDisponivel(ultimoNumero) : ultimoNumero;
        }
    }
}