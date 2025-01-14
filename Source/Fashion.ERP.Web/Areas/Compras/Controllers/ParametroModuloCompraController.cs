﻿using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using NHibernate.Util;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Compras.Controllers
{
    public partial class ParametroModuloCompraController : BaseController
    {
		#region Variaveis
        private readonly IRepository<ParametroModuloCompra> _parametroModuloCompraRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ParametroModuloCompraController(ILogger logger, IRepository<ParametroModuloCompra> parametroModuloCompraRepository)
        {
            _parametroModuloCompraRepository = parametroModuloCompraRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var model = new ParametroModuloCompraModel();

            var domain = _parametroModuloCompraRepository.Find().FirstOrDefault();

            if (domain != null)
                model = Mapper.Flat<ParametroModuloCompraModel>(domain);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Index(ParametroModuloCompraModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _parametroModuloCompraRepository.Find().FirstOrDefault();

                    domain = domain == null ? Mapper.Unflat<ParametroModuloCompra>(model) : Mapper.Unflat(model, domain);

                    _parametroModuloCompraRepository.SaveOrUpdate(domain);

                    this.AddSuccessMessage("Parâmetro atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível atualizar o parâmetro. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }

        #endregion

        #endregion

		#region Métodos
		
        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var parametroModuloCompra = model as ParametroModuloCompraModel;
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #endregion
    }
}