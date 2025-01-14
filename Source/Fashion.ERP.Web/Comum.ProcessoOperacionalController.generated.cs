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
namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class ProcessoOperacionalController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProcessoOperacionalController(Dummy d) { }

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
        public virtual System.Web.Mvc.JsonResult SalvarNovo()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SalvarNovo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Editar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SalvarAlteracoes()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SalvarAlteracoes);
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
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Detalhar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detalhar);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProcessoOperacionalController Actions { get { return MVC.Comum.ProcessoOperacional; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Comum";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ProcessoOperacional";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ProcessoOperacional";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Novo = "Novo";
            public readonly string SalvarNovo = "SalvarNovo";
            public readonly string Editar = "Editar";
            public readonly string SalvarAlteracoes = "SalvarAlteracoes";
            public readonly string Excluir = "Excluir";
            public readonly string EditarSituacao = "EditarSituacao";
            public readonly string Detalhar = "Detalhar";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Novo = "Novo";
            public const string SalvarNovo = "SalvarNovo";
            public const string Editar = "Editar";
            public const string SalvarAlteracoes = "SalvarAlteracoes";
            public const string Excluir = "Excluir";
            public const string EditarSituacao = "EditarSituacao";
            public const string Detalhar = "Detalhar";
        }


        static readonly ActionParamsClass_Novo s_params_Novo = new ActionParamsClass_Novo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Novo NovoParams { get { return s_params_Novo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Novo
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_SalvarNovo s_params_SalvarNovo = new ActionParamsClass_SalvarNovo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SalvarNovo SalvarNovoParams { get { return s_params_SalvarNovo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SalvarNovo
        {
            public readonly string request = "request";
            public readonly string sequenciaOperacoes = "sequenciaOperacoes";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Editar s_params_Editar = new ActionParamsClass_Editar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Editar EditarParams { get { return s_params_Editar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Editar
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_SalvarAlteracoes s_params_SalvarAlteracoes = new ActionParamsClass_SalvarAlteracoes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SalvarAlteracoes SalvarAlteracoesParams { get { return s_params_SalvarAlteracoes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SalvarAlteracoes
        {
            public readonly string request = "request";
            public readonly string sequenciaOperacoes = "sequenciaOperacoes";
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
        static readonly ActionParamsClass_Detalhar s_params_Detalhar = new ActionParamsClass_Detalhar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Detalhar DetalharParams { get { return s_params_Detalhar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Detalhar
        {
            public readonly string modeloId = "modeloId";
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
                public readonly string _NovoOuEditar = "_NovoOuEditar";
                public readonly string Editar = "Editar";
                public readonly string Index = "Index";
                public readonly string Novo = "Novo";
            }
            public readonly string _NovoOuEditar = "~/Areas/Comum/Views/ProcessoOperacional/_NovoOuEditar.cshtml";
            public readonly string Editar = "~/Areas/Comum/Views/ProcessoOperacional/Editar.cshtml";
            public readonly string Index = "~/Areas/Comum/Views/ProcessoOperacional/Index.cshtml";
            public readonly string Novo = "~/Areas/Comum/Views/ProcessoOperacional/Novo.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProcessoOperacionalController : Fashion.ERP.Web.Areas.Comum.Controllers.ProcessoOperacionalController
    {
        public T4MVC_ProcessoOperacionalController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Novo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            NovoOverride(callInfo);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model);

        public override System.Web.Mvc.ActionResult Novo(Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NovoOverride(callInfo, model);
            return callInfo;
        }

        partial void SalvarNovoOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Comum.Models.SequenciaOperacionalModel> sequenciaOperacoes, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model);

        public override System.Web.Mvc.JsonResult SalvarNovo(Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Comum.Models.SequenciaOperacionalModel> sequenciaOperacoes, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SalvarNovo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sequenciaOperacoes", sequenciaOperacoes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SalvarNovoOverride(callInfo, request, sequenciaOperacoes, model);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult Editar(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditarOverride(callInfo, id);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model);

        public override System.Web.Mvc.ActionResult Editar(Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditarOverride(callInfo, model);
            return callInfo;
        }

        partial void SalvarAlteracoesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Comum.Models.SequenciaOperacionalModel> sequenciaOperacoes, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model);

        public override System.Web.Mvc.JsonResult SalvarAlteracoes(Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Comum.Models.SequenciaOperacionalModel> sequenciaOperacoes, Fashion.ERP.Web.Areas.Comum.Models.ProcessoOperacionalModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SalvarAlteracoes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sequenciaOperacoes", sequenciaOperacoes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SalvarAlteracoesOverride(callInfo, request, sequenciaOperacoes, model);
            return callInfo;
        }

        partial void ExcluirOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        public override System.Web.Mvc.ActionResult Excluir(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Excluir);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ExcluirOverride(callInfo, id);
            return callInfo;
        }

        partial void EditarSituacaoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult EditarSituacao(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditarSituacao);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditarSituacaoOverride(callInfo, id);
            return callInfo;
        }

        partial void DetalharOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long modeloId);

        public override System.Web.Mvc.ActionResult Detalhar(long modeloId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detalhar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "modeloId", modeloId);
            DetalharOverride(callInfo, modeloId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
