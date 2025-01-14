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
namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class TituloReceberController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TituloReceberController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult Editar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Excluir()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Excluir);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Baixar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Baixar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SalveBaixaTitulo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SalveBaixaTitulo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AtualizeBaixaTitulo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AtualizeBaixaTitulo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ExcluaBaixaTitulo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExcluaBaixaTitulo);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TituloReceberController Actions { get { return MVC.Financeiro.TituloReceber; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Financeiro";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "TituloReceber";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "TituloReceber";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Novo = "Novo";
            public readonly string Editar = "Editar";
            public readonly string Excluir = "Excluir";
            public readonly string Baixar = "Baixar";
            public readonly string SalveBaixaTitulo = "SalveBaixaTitulo";
            public readonly string AtualizeBaixaTitulo = "AtualizeBaixaTitulo";
            public readonly string ExcluaBaixaTitulo = "ExcluaBaixaTitulo";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Novo = "Novo";
            public const string Editar = "Editar";
            public const string Excluir = "Excluir";
            public const string Baixar = "Baixar";
            public const string SalveBaixaTitulo = "SalveBaixaTitulo";
            public const string AtualizeBaixaTitulo = "AtualizeBaixaTitulo";
            public const string ExcluaBaixaTitulo = "ExcluaBaixaTitulo";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Novo s_params_Novo = new ActionParamsClass_Novo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Novo NovoParams { get { return s_params_Novo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Novo
        {
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
        static readonly ActionParamsClass_Excluir s_params_Excluir = new ActionParamsClass_Excluir();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Excluir ExcluirParams { get { return s_params_Excluir; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Excluir
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Baixar s_params_Baixar = new ActionParamsClass_Baixar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Baixar BaixarParams { get { return s_params_Baixar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Baixar
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SalveBaixaTitulo s_params_SalveBaixaTitulo = new ActionParamsClass_SalveBaixaTitulo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SalveBaixaTitulo SalveBaixaTituloParams { get { return s_params_SalveBaixaTitulo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SalveBaixaTitulo
        {
            public readonly string request = "request";
            public readonly string baixaTitulos = "models";
        }
        static readonly ActionParamsClass_AtualizeBaixaTitulo s_params_AtualizeBaixaTitulo = new ActionParamsClass_AtualizeBaixaTitulo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AtualizeBaixaTitulo AtualizeBaixaTituloParams { get { return s_params_AtualizeBaixaTitulo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AtualizeBaixaTitulo
        {
            public readonly string request = "request";
            public readonly string baixaTitulos = "models";
        }
        static readonly ActionParamsClass_ExcluaBaixaTitulo s_params_ExcluaBaixaTitulo = new ActionParamsClass_ExcluaBaixaTitulo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExcluaBaixaTitulo ExcluaBaixaTituloParams { get { return s_params_ExcluaBaixaTitulo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExcluaBaixaTitulo
        {
            public readonly string request = "request";
            public readonly string baixaTitulos = "models";
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
                public readonly string Baixar = "Baixar";
                public readonly string Editar = "Editar";
                public readonly string Index = "Index";
                public readonly string Novo = "Novo";
            }
            public readonly string _NovoOuEditar = "~/Areas/Financeiro/Views/TituloReceber/_NovoOuEditar.cshtml";
            public readonly string Baixar = "~/Areas/Financeiro/Views/TituloReceber/Baixar.cshtml";
            public readonly string Editar = "~/Areas/Financeiro/Views/TituloReceber/Editar.cshtml";
            public readonly string Index = "~/Areas/Financeiro/Views/TituloReceber/Index.cshtml";
            public readonly string Novo = "~/Areas/Financeiro/Views/TituloReceber/Novo.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_TituloReceberController : Fashion.ERP.Web.Areas.Financeiro.Controllers.TituloReceberController
    {
        public T4MVC_TituloReceberController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Financeiro.Models.PesquisaTituloReceberModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.Financeiro.Models.PesquisaTituloReceberModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Novo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            NovoOverride(callInfo);
            return callInfo;
        }

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Financeiro.Models.TituloReceberModel model);

        public override System.Web.Mvc.ActionResult Novo(Fashion.ERP.Web.Areas.Financeiro.Models.TituloReceberModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NovoOverride(callInfo, model);
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

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Financeiro.Models.TituloReceberModel model);

        public override System.Web.Mvc.ActionResult Editar(Fashion.ERP.Web.Areas.Financeiro.Models.TituloReceberModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditarOverride(callInfo, model);
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

        partial void BaixarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult Baixar(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Baixar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            BaixarOverride(callInfo, id);
            return callInfo;
        }

        partial void SalveBaixaTituloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos);

        public override System.Web.Mvc.ActionResult SalveBaixaTitulo(Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SalveBaixaTitulo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "models", baixaTitulos);
            SalveBaixaTituloOverride(callInfo, request, baixaTitulos);
            return callInfo;
        }

        partial void AtualizeBaixaTituloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos);

        public override System.Web.Mvc.ActionResult AtualizeBaixaTitulo(Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AtualizeBaixaTitulo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "models", baixaTitulos);
            AtualizeBaixaTituloOverride(callInfo, request, baixaTitulos);
            return callInfo;
        }

        partial void ExcluaBaixaTituloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos);

        public override System.Web.Mvc.ActionResult ExcluaBaixaTitulo(Kendo.Mvc.UI.DataSourceRequest request, System.Collections.Generic.IEnumerable<Fashion.ERP.Web.Areas.Financeiro.Models.BaixaItemTituloReceberModel> baixaTitulos)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExcluaBaixaTitulo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "models", baixaTitulos);
            ExcluaBaixaTituloOverride(callInfo, request, baixaTitulos);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
