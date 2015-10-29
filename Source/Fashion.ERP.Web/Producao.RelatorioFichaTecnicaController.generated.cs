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
namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioFichaTecnicaController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RelatorioFichaTecnicaController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult FichaTecnica()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FichaTecnica);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RelatorioFichaTecnicaController Actions { get { return MVC.Producao.RelatorioFichaTecnica; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Producao";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "RelatorioFichaTecnica";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "RelatorioFichaTecnica";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string FichaTecnica = "FichaTecnica";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string FichaTecnica = "FichaTecnica";
        }


        static readonly ActionParamsClass_FichaTecnica s_params_FichaTecnica = new ActionParamsClass_FichaTecnica();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_FichaTecnica FichaTecnicaParams { get { return s_params_FichaTecnica; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_FichaTecnica
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RelatorioFichaTecnicaController : Fashion.ERP.Web.Areas.Producao.Controllers.RelatorioFichaTecnicaController
    {
        public T4MVC_RelatorioFichaTecnicaController() : base(Dummy.Instance) { }

        partial void FichaTecnicaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult FichaTecnica(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FichaTecnica);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            FichaTecnicaOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591