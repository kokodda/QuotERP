// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class RelatorioController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RelatorioController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RelatorioController Actions { get { return MVC.EngenhariaProduto.Relatorio; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "EngenhariaProduto";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Relatorio";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Relatorio";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ListagemModelos = "ListagemModelos";
            public readonly string ListagemModelosAprovados = "ListagemModelosAprovados";
            public readonly string ConsumoMaterialColecao = "ConsumoMaterialColecao";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ListagemModelos = "ListagemModelos";
            public const string ListagemModelosAprovados = "ListagemModelosAprovados";
            public const string ConsumoMaterialColecao = "ConsumoMaterialColecao";
        }


        static readonly ActionParamsClass_ListagemModelos s_params_ListagemModelos = new ActionParamsClass_ListagemModelos();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ListagemModelos ListagemModelosParams { get { return s_params_ListagemModelos; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ListagemModelos
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ListagemModelosAprovados s_params_ListagemModelosAprovados = new ActionParamsClass_ListagemModelosAprovados();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ListagemModelosAprovados ListagemModelosAprovadosParams { get { return s_params_ListagemModelosAprovados; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ListagemModelosAprovados
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ConsumoMaterialColecao s_params_ConsumoMaterialColecao = new ActionParamsClass_ConsumoMaterialColecao();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ConsumoMaterialColecao ConsumoMaterialColecaoParams { get { return s_params_ConsumoMaterialColecao; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ConsumoMaterialColecao
        {
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string ConsumoMaterialColecao = "ConsumoMaterialColecao";
                public readonly string ConsumoMaterialPorModelo = "ConsumoMaterialPorModelo";
                public readonly string ListagemModelos = "ListagemModelos";
                public readonly string ListagemModelos2 = "ListagemModelos2";
                public readonly string ListagemModelosAprovados = "ListagemModelosAprovados";
            }
            public readonly string ConsumoMaterialColecao = "~/Areas/EngenhariaProduto/Views/Relatorio/ConsumoMaterialColecao.cshtml";
            public readonly string ConsumoMaterialPorModelo = "~/Areas/EngenhariaProduto/Views/Relatorio/ConsumoMaterialPorModelo.cshtml";
            public readonly string ListagemModelos = "~/Areas/EngenhariaProduto/Views/Relatorio/ListagemModelos.cshtml";
            public readonly string ListagemModelos2 = "~/Areas/EngenhariaProduto/Views/Relatorio/ListagemModelos2.cshtml";
            public readonly string ListagemModelosAprovados = "~/Areas/EngenhariaProduto/Views/Relatorio/ListagemModelosAprovados.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RelatorioController : Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.RelatorioController
    {
        public T4MVC_RelatorioController() : base(Dummy.Instance) { }

        partial void ListagemModelosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult ListagemModelos()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ListagemModelos);
            ListagemModelosOverride(callInfo);
            return callInfo;
        }

        partial void ListagemModelosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloModel model);

        public override System.Web.Mvc.ActionResult ListagemModelos(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ListagemModelos);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ListagemModelosOverride(callInfo, model);
            return callInfo;
        }

        partial void ListagemModelosAprovadosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult ListagemModelosAprovados()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ListagemModelosAprovados);
            ListagemModelosAprovadosOverride(callInfo);
            return callInfo;
        }

        partial void ListagemModelosAprovadosOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ListagemModelosAprovadosModel model);

        public override System.Web.Mvc.JsonResult ListagemModelosAprovados(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ListagemModelosAprovadosModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ListagemModelosAprovados);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ListagemModelosAprovadosOverride(callInfo, model);
            return callInfo;
        }

        partial void ConsumoMaterialColecaoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult ConsumoMaterialColecao()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ConsumoMaterialColecao);
            ConsumoMaterialColecaoOverride(callInfo);
            return callInfo;
        }

        partial void ConsumoMaterialColecaoOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ConsumoMaterialColecaoModel model);

        public override System.Web.Mvc.JsonResult ConsumoMaterialColecao(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ConsumoMaterialColecaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ConsumoMaterialColecao);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ConsumoMaterialColecaoOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
