﻿using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Kendo.Mvc.Extensions;
using Microsoft.Ajax.Utilities;
using Ninject.Extensions.Logging;
using Fashion.ERP.Domain.Comum;
using Kendo.Mvc.UI;
using System.Collections.Generic;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class ProcessoOperacionalController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ProcessoOperacional> _processoRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly ILogger _logger;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<OperacaoProducao> _operacaoProducaoRepository;
        private readonly IRepository<SequenciaOperacional> _sequenciaOperacionalRepository; 

        #endregion

        #region Construtores
        public ProcessoOperacionalController(ILogger logger, IRepository<ProcessoOperacional> processoOperacionalRepository,
            IRepository<Modelo> modeloRepository,
            IRepository<SetorProducao> setorProducaoRepository,
            IRepository<DepartamentoProducao> departamentoRepository,
            IRepository<OperacaoProducao> operacaoProducaoRepository,
            IRepository<SequenciaOperacional> sequenciaOperacionalRepository
            )
        {
            _processoRepository = processoOperacionalRepository;
            _modeloRepository = modeloRepository;
            _logger = logger;
            _departamentoProducaoRepository = departamentoRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _operacaoProducaoRepository = operacaoProducaoRepository;
            _sequenciaOperacionalRepository = sequenciaOperacionalRepository;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var processoOperacionals = _processoRepository.Find();

            var list = processoOperacionals.Select(p => new GridProcessoOperacionalModel()
            {
                Id = p.Id.GetValueOrDefault(),
                Descricao = p.Descricao,
                Ativo = p.Ativo
            }).ToList().OrderBy(o => o.Descricao);

            return View(list);
        }
        #endregion

        #region Novo

        [HttpGet, PopulateViewData("PopulaDepartamentoViewData")]
        public virtual ActionResult Novo()
        {
            return View(new ProcessoOperacionalModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(ProcessoOperacionalModel model)
        {
            return View(model);
        }

        public virtual JsonResult SalvarNovo([DataSourceRequest] DataSourceRequest request,
            IEnumerable<SequenciaOperacionalModel> sequenciaOperacoes, ProcessoOperacionalModel model)
        {
            var results = new List<SequenciaOperacionalModel>();
            //var model = new ProcessoOperacionalModel();
            //model.Descricao = descricao;

            if (sequenciaOperacoes != null && ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<ProcessoOperacional>(model);
                    
                    foreach (SequenciaOperacionalModel s in sequenciaOperacoes)
                    {
                        SequenciaOperacional sequenciaOperacao = null;
                        if (s.Id != null)
                        {
                            sequenciaOperacao = ObtenhaSequenciaOperacaoAtualizada(s);
                        }
                        else
                        {
                            sequenciaOperacao = ObtenhaSequenciaOperacao(s);
                        }

                        domain.AddSequenciaOperacao(sequenciaOperacao);
                    }

                    domain.Ativo = true;
                    _processoRepository.Save(domain);

                    this.AddSuccessMessage("Processo operacional atualizado com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Ocorreu um erro ao salvar o processo operacional. Confira se os dados foram informados corretamente: " +
                       exception.Message;
                    this.AddErrorMessage(errorMsg);

                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }
            else
            {
                var errorMsg =
                    "Obrigatório a inserção de pelo menos um sequência operacional e o campo descrição não pode estar vazio!";
                this.AddErrorMessage(errorMsg);
                return new JsonResult { Data = "error" };    
            }
            

            return new JsonResult { Data = "sucesso" };
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData, PopulateViewData("PopulaDepartamentoViewData")]
        public virtual ActionResult Editar(long id)
        {
            var domain = _processoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<ProcessoOperacionalModel>(domain);
                model.SequenciasOperacionais = new List<SequenciaOperacionalModel>();

                foreach (var sequenciaOperacional in domain.SequenciasOperacionais)
                {
                    var sequenciaOperacionalModel = ObtenhaSequenciaOperacionalModel(sequenciaOperacional);
                    model.SequenciasOperacionais.Add(sequenciaOperacionalModel);
                }

                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar processo operacional");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(ProcessoOperacionalModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var domain = Mapper.Unflat(model, _naturezaRepository.Get(model.Id));

            //        _naturezaRepository.Update(domain);

            //        this.AddSuccessMessage("Natureza do produto atualizada com sucesso.");
            //        return RedirectToAction("Index");
            //    }
            //    catch (Exception exception)
            //    {
            //        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a natureza do produto. Confira se os dados foram informados corretamente: " + exception.Message);
            //        _logger.Info(exception.GetMessage());
            //    }
            //}

            return View(model);
        }

        public virtual JsonResult SalvarAlteracoes([DataSourceRequest] DataSourceRequest request,
           IEnumerable<SequenciaOperacionalModel> sequenciaOperacoes, ProcessoOperacionalModel model)
        {
            var results = new List<SequenciaOperacionalModel>();

            if (sequenciaOperacoes != null && ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _processoRepository.Get(model.Id));

                    foreach (SequenciaOperacionalModel s in sequenciaOperacoes)
                    {
                        SequenciaOperacional sequenciaOperacao = null;
                        if (s.Id != null)
                        {
                            sequenciaOperacao = ObtenhaSequenciaOperacaoAtualizada(s);
                        }
                        else
                        {
                            sequenciaOperacao = ObtenhaSequenciaOperacao(s);
                        }

                        domain.AddSequenciaOperacao(sequenciaOperacao);
                    }

                    _processoRepository.Update(domain);

                    this.AddSuccessMessage("Processo operacional atualizada com sucesso.");
                }
                catch (Exception exception)
                {
                    var errorMsg = "Ocorreu um erro ao salvar o processo operacional. Confira se os dados foram informados corretamente: " +
                       exception.Message;
                    this.AddErrorMessage(errorMsg);

                    _logger.Info(exception.GetMessage());

                    return new JsonResult { Data = "error" };
                }
            }
            else
            {
                var errorMsg =
                    "Obrigatório a inserção de pelo menos um sequência operacional e o campo descrição não pode estar vazio!";
                this.AddErrorMessage(errorMsg);
                return new JsonResult { Data = "error" };
            }


            return new JsonResult { Data = "sucesso" };
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
                    var domain = _processoRepository.Get(id);
                    _processoRepository.Delete(domain);

                    this.AddSuccessMessage("Processo operacional excluída com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("",
                                             "Ocorreu um erro ao excluir o processo operacional: " + exception.Message);
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
                var domain = _processoRepository.Get(id);

                if (domain != null)
                {
                    var situacao = !domain.Ativo;

                    domain.Ativo = situacao;
                    _processoRepository.Update(domain);
                    this.AddSuccessMessage("Processo operacional {0} com sucesso", situacao ? "ativada" : "inativada");
                }
                else
                {
                    this.AddErrorMessage("O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao editar a situação do processo operacional: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Detalhar
        public virtual ActionResult Detalhar(long modeloId)
        {
            return View();
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var processoOperacionalModel = model as ProcessoOperacionalModel;

            // Verificar duplicado
            if (_processoRepository.Find(p => p.Descricao == processoOperacionalModel.Descricao && p.Id != processoOperacionalModel.Id).Any())
                ModelState.AddModelError("Descricao", "Já existe um processo operacional cadastrado com esta descrição.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            var domain = _processoRepository.Get(id);

            // Verificar relacionamento
            //if (_modeloRepository.Find().Any(p => p.Natureza == domain))
            //    ModelState.AddModelError("", "Não é possível excluir esta natureza do produto, pois existe(m) modelo(s) associadas a ela.");
        }
        #endregion


        private SequenciaOperacionalModel ObtenhaSequenciaOperacionalModel(SequenciaOperacional sequenciaOperacional)
        {
            return new SequenciaOperacionalModel
            {
                DepartamentoId = sequenciaOperacional.DepartamentoProducao.Id,
                NomeDepartamento = sequenciaOperacional.DepartamentoProducao.Nome,
                NomeSetor = sequenciaOperacional.SetorProducao.Nome,
                SetorId = sequenciaOperacional.SetorProducao.Id,
                OperacaoId = sequenciaOperacional.Id,
                NomeOperacao = sequenciaOperacional.OperacaoProducao.Descricao,
                Id = sequenciaOperacional.Id
            };
        }
        private SequenciaOperacional ObtenhaSequenciaOperacao(SequenciaOperacionalModel sequenciaOperacionalModel)
        {
            return new SequenciaOperacional
            {
                DepartamentoProducao = 
                    _departamentoProducaoRepository.Find(
                        departamentoProducao =>
                            departamentoProducao.Nome == sequenciaOperacionalModel.NomeDepartamento)
                        .FirstOrDefault(),
                SetorProducao = _setorProducaoRepository.Find(
                    setorProducao =>
                        setorProducao.Nome == sequenciaOperacionalModel.NomeSetor &&
                        setorProducao.DepartamentoProducao.Nome == sequenciaOperacionalModel.NomeDepartamento)
                    .FirstOrDefault(),
                OperacaoProducao = _operacaoProducaoRepository.Find(
                    operacaoProducao =>
                        operacaoProducao.Descricao == sequenciaOperacionalModel.NomeOperacao &&
                        operacaoProducao.SetorProducao.Nome == sequenciaOperacionalModel.NomeSetor)
                    .FirstOrDefault()
            };
        }

        private SequenciaOperacional ObtenhaSequenciaOperacaoAtualizada(SequenciaOperacionalModel sequenciaOperacionalModelModel)
        {
            SequenciaOperacional sequenciaOperacional;
            sequenciaOperacional = _sequenciaOperacionalRepository.Get(sequenciaOperacionalModelModel.Id);
            if (sequenciaOperacional.DepartamentoProducao.Nome != sequenciaOperacionalModelModel.NomeDepartamento)
            {
                sequenciaOperacional.DepartamentoProducao = _departamentoProducaoRepository.Find(
                    departamentoProducao =>
                        departamentoProducao.Nome == sequenciaOperacionalModelModel.NomeDepartamento)
                    .FirstOrDefault();
            }

            if (sequenciaOperacionalModelModel.NomeSetor == null)
            {
                sequenciaOperacional.SetorProducao = null;
            }
            else
            {
                sequenciaOperacional.SetorProducao = _setorProducaoRepository.Find(
                    setorProducao =>
                        setorProducao.Nome == sequenciaOperacionalModelModel.NomeSetor &&
                        setorProducao.DepartamentoProducao.Nome ==
                        sequenciaOperacionalModelModel.NomeDepartamento)
                    .FirstOrDefault();
            }
            return sequenciaOperacional;
        }

        #endregion

        #region PopulaDepartamentoViewData
        protected void PopulaDepartamentoViewData()
        {
            // DepartamentoProducao
            var departamentos = _departamentoProducaoRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Departamento = departamentos.Where(p => p.Ativo).Select(s => new { s.Id, s.Nome });
            ViewBag.DepartamentosDicionario = departamentos.ToDictionary(k => k.Id, v => v.Nome);

            // SetorProducao
            var setores = _setorProducaoRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Setor = new SelectList(Enumerable.Empty<string>());
            ViewBag.SetoresDicionario = setores.ToDictionary(k => k.Id, v => v.Nome);

            // OperacaoProducao
            var operacoes = _operacaoProducaoRepository.Find().OrderBy(p => p.Descricao).ToList();
            ViewBag.Operacao = new SelectList(Enumerable.Empty<string>());
            ViewBag.OperacoesDicionario = operacoes.ToDictionary(k => k.Id, v => v.Descricao);
        }
        #endregion
    }
}