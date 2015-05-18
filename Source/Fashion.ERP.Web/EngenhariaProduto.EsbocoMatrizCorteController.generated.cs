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
    public partial class EsbocoMatrizCorteController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EsbocoMatrizCorteController(Dummy d) { }

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

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ObtenhaListaGridEsbocoMatrizCorteModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridEsbocoMatrizCorteModel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EsbocarCorte()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EsbocarCorte);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EsbocoMatrizCorteController Actions { get { return MVC.EngenhariaProduto.EsbocoMatrizCorte; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "EngenhariaProduto";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "EsbocoMatrizCorte";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "EsbocoMatrizCorte";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string ObtenhaListaGridEsbocoMatrizCorteModel = "ObtenhaListaGridEsbocoMatrizCorteModel";
            public readonly string EsbocarCorte = "EsbocarCorte";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string ObtenhaListaGridEsbocoMatrizCorteModel = "ObtenhaListaGridEsbocoMatrizCorteModel";
            public const string EsbocarCorte = "EsbocarCorte";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ObtenhaListaGridEsbocoMatrizCorteModel s_params_ObtenhaListaGridEsbocoMatrizCorteModel = new ActionParamsClass_ObtenhaListaGridEsbocoMatrizCorteModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ObtenhaListaGridEsbocoMatrizCorteModel ObtenhaListaGridEsbocoMatrizCorteModelParams { get { return s_params_ObtenhaListaGridEsbocoMatrizCorteModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ObtenhaListaGridEsbocoMatrizCorteModel
        {
            public readonly string request = "request";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_EsbocarCorte s_params_EsbocarCorte = new ActionParamsClass_EsbocarCorte();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EsbocarCorte EsbocarCorteParams { get { return s_params_EsbocarCorte; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EsbocarCorte
        {
            public readonly string id = "id";
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
                public readonly string EsbocarCorte = "EsbocarCorte";
                public readonly string Index = "Index";
            }
            public readonly string EsbocarCorte = "~/Areas/EngenhariaProduto/Views/EsbocoMatrizCorte/EsbocarCorte.cshtml";
            public readonly string Index = "~/Areas/EngenhariaProduto/Views/EsbocoMatrizCorte/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EsbocoMatrizCorteController : Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.EsbocoMatrizCorteController
    {
        public T4MVC_EsbocoMatrizCorteController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaEsbocoMatrizCorteModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaEsbocoMatrizCorteModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        partial void ObtenhaListaGridEsbocoMatrizCorteModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaEsbocoMatrizCorteModel model);

        public override System.Web.Mvc.ActionResult ObtenhaListaGridEsbocoMatrizCorteModel(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaEsbocoMatrizCorteModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridEsbocoMatrizCorteModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ObtenhaListaGridEsbocoMatrizCorteModelOverride(callInfo, request, model);
            return callInfo;
        }

        partial void EsbocarCorteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult EsbocarCorte(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EsbocarCorte);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EsbocarCorteOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
