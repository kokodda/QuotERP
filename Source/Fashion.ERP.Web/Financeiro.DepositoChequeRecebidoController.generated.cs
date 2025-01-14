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
    public partial class DepositoChequeRecebidoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DepositoChequeRecebidoController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult LerCheques()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LerCheques);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisaCheque()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisaCheque);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult PesquisarAgenciaContaPorBanco()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarAgenciaContaPorBanco);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DepositoChequeRecebidoController Actions { get { return MVC.Financeiro.DepositoChequeRecebido; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Financeiro";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "DepositoChequeRecebido";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "DepositoChequeRecebido";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string LerCheques = "LerCheques";
            public readonly string PesquisaCheque = "PesquisaCheque";
            public readonly string PesquisarAgenciaContaPorBanco = "PesquisarAgenciaContaPorBanco";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string LerCheques = "LerCheques";
            public const string PesquisaCheque = "PesquisaCheque";
            public const string PesquisarAgenciaContaPorBanco = "PesquisarAgenciaContaPorBanco";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_LerCheques s_params_LerCheques = new ActionParamsClass_LerCheques();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_LerCheques LerChequesParams { get { return s_params_LerCheques; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_LerCheques
        {
            public readonly string request = "request";
        }
        static readonly ActionParamsClass_PesquisaCheque s_params_PesquisaCheque = new ActionParamsClass_PesquisaCheque();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisaCheque PesquisaChequeParams { get { return s_params_PesquisaCheque; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisaCheque
        {
            public readonly string unidade = "unidade";
            public readonly string vencimentoDe = "vencimentoDe";
            public readonly string vencimentoAte = "vencimentoAte";
            public readonly string naoDepositado = "naoDepositado";
            public readonly string devolvido = "devolvido";
            public readonly string custodia = "custodia";
        }
        static readonly ActionParamsClass_PesquisarAgenciaContaPorBanco s_params_PesquisarAgenciaContaPorBanco = new ActionParamsClass_PesquisarAgenciaContaPorBanco();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_PesquisarAgenciaContaPorBanco PesquisarAgenciaContaPorBancoParams { get { return s_params_PesquisarAgenciaContaPorBanco; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_PesquisarAgenciaContaPorBanco
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
                public readonly string Index = "Index";
            }
            public readonly string Index = "~/Areas/Financeiro/Views/DepositoChequeRecebido/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_DepositoChequeRecebidoController : Fashion.ERP.Web.Areas.Financeiro.Controllers.DepositoChequeRecebidoController
    {
        public T4MVC_DepositoChequeRecebidoController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Financeiro.Models.DepositoChequeRecebidoModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.Financeiro.Models.DepositoChequeRecebidoModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        partial void LerChequesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request);

        public override System.Web.Mvc.ActionResult LerCheques(Kendo.Mvc.UI.DataSourceRequest request)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LerCheques);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            LerChequesOverride(callInfo, request);
            return callInfo;
        }

        partial void PesquisaChequeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? unidade, System.DateTime? vencimentoDe, System.DateTime? vencimentoAte, Fashion.ERP.Domain.Financeiro.ChequeSituacao? naoDepositado, Fashion.ERP.Domain.Financeiro.ChequeSituacao? devolvido, Fashion.ERP.Domain.Financeiro.ChequeSituacao? custodia);

        public override System.Web.Mvc.ActionResult PesquisaCheque(long? unidade, System.DateTime? vencimentoDe, System.DateTime? vencimentoAte, Fashion.ERP.Domain.Financeiro.ChequeSituacao? naoDepositado, Fashion.ERP.Domain.Financeiro.ChequeSituacao? devolvido, Fashion.ERP.Domain.Financeiro.ChequeSituacao? custodia)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisaCheque);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "unidade", unidade);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vencimentoDe", vencimentoDe);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "vencimentoAte", vencimentoAte);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "naoDepositado", naoDepositado);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "devolvido", devolvido);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "custodia", custodia);
            PesquisaChequeOverride(callInfo, unidade, vencimentoDe, vencimentoAte, naoDepositado, devolvido, custodia);
            return callInfo;
        }

        partial void PesquisarAgenciaContaPorBancoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        public override System.Web.Mvc.ActionResult PesquisarAgenciaContaPorBanco(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PesquisarAgenciaContaPorBanco);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PesquisarAgenciaContaPorBancoOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
