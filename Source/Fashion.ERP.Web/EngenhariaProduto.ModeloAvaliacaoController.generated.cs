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
    public partial class ModeloAvaliacaoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ModeloAvaliacaoController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult ObtenhaListaGridModeloAvaliacaoModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridModeloAvaliacaoModel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Avaliar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Avaliar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Excluir()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Excluir);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditarSituacao()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditarSituacao);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ModeloAvaliacaoController Actions { get { return MVC.EngenhariaProduto.ModeloAvaliacao; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "EngenhariaProduto";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ModeloAvaliacao";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ModeloAvaliacao";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string ObtenhaListaGridModeloAvaliacaoModel = "ObtenhaListaGridModeloAvaliacaoModel";
            public readonly string Avaliar = "Avaliar";
            public readonly string Excluir = "Excluir";
            public readonly string EditarSituacao = "EditarSituacao";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string ObtenhaListaGridModeloAvaliacaoModel = "ObtenhaListaGridModeloAvaliacaoModel";
            public const string Avaliar = "Avaliar";
            public const string Excluir = "Excluir";
            public const string EditarSituacao = "EditarSituacao";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ObtenhaListaGridModeloAvaliacaoModel s_params_ObtenhaListaGridModeloAvaliacaoModel = new ActionParamsClass_ObtenhaListaGridModeloAvaliacaoModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ObtenhaListaGridModeloAvaliacaoModel ObtenhaListaGridModeloAvaliacaoModelParams { get { return s_params_ObtenhaListaGridModeloAvaliacaoModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ObtenhaListaGridModeloAvaliacaoModel
        {
            public readonly string request = "request";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Avaliar s_params_Avaliar = new ActionParamsClass_Avaliar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Avaliar AvaliarParams { get { return s_params_Avaliar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Avaliar
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Excluir s_params_Excluir = new ActionParamsClass_Excluir();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Excluir ExcluirParams { get { return s_params_Excluir; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Excluir
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_EditarSituacao s_params_EditarSituacao = new ActionParamsClass_EditarSituacao();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditarSituacao EditarSituacaoParams { get { return s_params_EditarSituacao; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditarSituacao
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
                public readonly string Avaliar = "Avaliar";
                public readonly string Index = "Index";
            }
            public readonly string Avaliar = "~/Areas/EngenhariaProduto/Views/ModeloAvaliacao/Avaliar.cshtml";
            public readonly string Index = "~/Areas/EngenhariaProduto/Views/ModeloAvaliacao/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ModeloAvaliacaoController : Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers.ModeloAvaliacaoController
    {
        public T4MVC_ModeloAvaliacaoController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAvaliacaoModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAvaliacaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        partial void ObtenhaListaGridModeloAvaliacaoModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAvaliacaoModel model);

        public override System.Web.Mvc.ActionResult ObtenhaListaGridModeloAvaliacaoModel(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.PesquisaModeloAvaliacaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ObtenhaListaGridModeloAvaliacaoModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ObtenhaListaGridModeloAvaliacaoModelOverride(callInfo, request, model);
            return callInfo;
        }

        partial void AvaliarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult Avaliar(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Avaliar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AvaliarOverride(callInfo, id);
            return callInfo;
        }

        partial void AvaliarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ModeloAvaliacaoModel model);

        public override System.Web.Mvc.ActionResult Avaliar(Fashion.ERP.Web.Areas.EngenhariaProduto.Models.ModeloAvaliacaoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Avaliar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AvaliarOverride(callInfo, model);
            return callInfo;
        }
    }
}

#endregion T4MVC
#pragma warning restore 1591
