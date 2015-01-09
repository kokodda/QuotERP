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
namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class RequisicaoMaterialController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RequisicaoMaterialController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult EditingInline_Read()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Read);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditingInline_Create()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Create);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditingInline_Update()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Update);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditingInline_Destroy()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Destroy);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RequisicaoMaterialController Actions { get { return MVC.Almoxarifado.RequisicaoMaterial; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Almoxarifado";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "RequisicaoMaterial";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "RequisicaoMaterial";

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
            public readonly string EditingInline_Read = "EditingInline_Read";
            public readonly string EditingInline_Create = "EditingInline_Create";
            public readonly string EditingInline_Update = "EditingInline_Update";
            public readonly string EditingInline_Destroy = "EditingInline_Destroy";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Novo = "Novo";
            public const string Editar = "Editar";
            public const string Excluir = "Excluir";
            public const string EditingInline_Read = "EditingInline_Read";
            public const string EditingInline_Create = "EditingInline_Create";
            public const string EditingInline_Update = "EditingInline_Update";
            public const string EditingInline_Destroy = "EditingInline_Destroy";
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
        static readonly ActionParamsClass_EditingInline_Read s_params_EditingInline_Read = new ActionParamsClass_EditingInline_Read();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditingInline_Read EditingInline_ReadParams { get { return s_params_EditingInline_Read; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditingInline_Read
        {
            public readonly string request = "request";
        }
        static readonly ActionParamsClass_EditingInline_Create s_params_EditingInline_Create = new ActionParamsClass_EditingInline_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditingInline_Create EditingInline_CreateParams { get { return s_params_EditingInline_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditingInline_Create
        {
            public readonly string request = "request";
            public readonly string reservaMaterialItemModel = "reservaMaterialItemModel";
        }
        static readonly ActionParamsClass_EditingInline_Update s_params_EditingInline_Update = new ActionParamsClass_EditingInline_Update();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditingInline_Update EditingInline_UpdateParams { get { return s_params_EditingInline_Update; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditingInline_Update
        {
            public readonly string request = "request";
            public readonly string reservaMaterialItemModel = "reservaMaterialItemModel";
        }
        static readonly ActionParamsClass_EditingInline_Destroy s_params_EditingInline_Destroy = new ActionParamsClass_EditingInline_Destroy();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditingInline_Destroy EditingInline_DestroyParams { get { return s_params_EditingInline_Destroy; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditingInline_Destroy
        {
            public readonly string request = "request";
            public readonly string reservaMaterialItemModel = "reservaMaterialItemModel";
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
            public readonly string _NovoOuEditar = "~/Areas/Almoxarifado/Views/RequisicaoMaterial/_NovoOuEditar.cshtml";
            public readonly string Editar = "~/Areas/Almoxarifado/Views/RequisicaoMaterial/Editar.cshtml";
            public readonly string Index = "~/Areas/Almoxarifado/Views/RequisicaoMaterial/Index.cshtml";
            public readonly string Novo = "~/Areas/Almoxarifado/Views/RequisicaoMaterial/Novo.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RequisicaoMaterialController : Fashion.ERP.Web.Areas.Almoxarifado.Controllers.RequisicaoMaterialController
    {
        public T4MVC_RequisicaoMaterialController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Almoxarifado.Models.PesquisaReservaMaterialModel model);

        public override System.Web.Mvc.ActionResult Index(Fashion.ERP.Web.Areas.Almoxarifado.Models.PesquisaReservaMaterialModel model)
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

        partial void NovoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Almoxarifado.Models.RequisicaoMaterialModel model);

        public override System.Web.Mvc.ActionResult Novo(Fashion.ERP.Web.Areas.Almoxarifado.Models.RequisicaoMaterialModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Novo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            NovoOverride(callInfo, model);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        public override System.Web.Mvc.ActionResult Editar(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Editar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditarOverride(callInfo, id);
            return callInfo;
        }

        partial void EditarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Fashion.ERP.Web.Areas.Almoxarifado.Models.RequisicaoMaterialModel model);

        public override System.Web.Mvc.ActionResult Editar(Fashion.ERP.Web.Areas.Almoxarifado.Models.RequisicaoMaterialModel model)
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

        partial void EditingInline_ReadOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request);

        public override System.Web.Mvc.ActionResult EditingInline_Read(Kendo.Mvc.UI.DataSourceRequest request)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Read);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            EditingInline_ReadOverride(callInfo, request);
            return callInfo;
        }

        partial void EditingInline_CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel);

        public override System.Web.Mvc.ActionResult EditingInline_Create(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "reservaMaterialItemModel", reservaMaterialItemModel);
            EditingInline_CreateOverride(callInfo, request, reservaMaterialItemModel);
            return callInfo;
        }

        partial void EditingInline_UpdateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel);

        public override System.Web.Mvc.ActionResult EditingInline_Update(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Update);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "reservaMaterialItemModel", reservaMaterialItemModel);
            EditingInline_UpdateOverride(callInfo, request, reservaMaterialItemModel);
            return callInfo;
        }

        partial void EditingInline_DestroyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel);

        public override System.Web.Mvc.ActionResult EditingInline_Destroy(Kendo.Mvc.UI.DataSourceRequest request, Fashion.ERP.Web.Areas.Almoxarifado.Models.ReservaMaterialItemModel reservaMaterialItemModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditingInline_Destroy);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "reservaMaterialItemModel", reservaMaterialItemModel);
            EditingInline_DestroyOverride(callInfo, request, reservaMaterialItemModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
