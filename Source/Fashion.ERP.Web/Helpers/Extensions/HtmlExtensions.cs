﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Fashion.Framework.Common.Validators;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Newtonsoft.Json;
using Kendo.Mvc.UI.Html;
using System.Web.Mvc.Html;
using NHibernate.Mapping.ByCode.Impl;

namespace Fashion.ERP.Web.Helpers.Extensions
{
    public static class HtmlExtensions
    {
        public static bool PossuiPermissao(this HtmlHelper helper, ActionResult action)
        {
            var result = action.GetT4MVCResult();
            var actionName = result.Action;
            var controllerName = result.Controller;
            var areaName = GetAreaName(action.GetRouteValueDictionary());

            return PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName);
        }

        #region SubmitButtonAuth
        public static MvcHtmlString SubmitButtonAuth(this HtmlHelper helper, ActionResult action, string id = "btnSubmit", string text = "Salvar", object htmlAttributes = null, bool? enabled = null)
        {
            var result = action.GetT4MVCResult();
            var actionName = result.Action;
            var controllerName = result.Controller;
            var areaName = GetAreaName(action.GetRouteValueDictionary());

            if ((enabled ?? true) == false || PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName) == false)
                return new MvcHtmlString(string.Empty);
            
            return BuildSubmitAuthButton(id, text, htmlAttributes);
        }
        #endregion

        #region ActionLinkAuth
        public static MvcHtmlString ActionLinkAuth(this HtmlHelper helper, string text, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var routes = new RouteValueDictionary(routeValues);
            var areaName = GetAreaName(routes);

            if (!PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName))
                return new MvcHtmlString(string.Empty);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            return BuildLinkAuthButton(url, text, htmlAttributes);
        }
        #endregion

        #region ActionLinkAuth

        public static MvcHtmlString ActionLinkAuth(this HtmlHelper helper, string text, ActionResult action, object htmlAttributes = null, bool? enabled = null)
        {
            var result = action.GetT4MVCResult();
            var actionName = result.Action;
            var controllerName = result.Controller;
            var areaName = GetAreaName(action.GetRouteValueDictionary());

            if ((enabled ?? true) == false || PermissaoHelper.PossuiPermissao(actionName, controllerName, areaName) == false)
                return new MvcHtmlString(string.Empty);

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(action);

            return BuildLinkAuthButton(url, text, htmlAttributes);
        }
        #endregion

        public static MvcHtmlString FormActionAuth(this HtmlHelper helper, bool isEditar, ActionResult actionExcluir)
        {
            var resultado = @"<div class='row'>
                <div class='col-sm-6'>
                    <div class='form-group form-group-sm'>
                        <div class='col-sm-offset-4 col-md-offset-3 col-sm-8 col-md-9'>
                            <button id='btnSubmit' class='btn btn-primary' type='submit' data-loading-text='Aguarde...'>Salvar</button>
                                " + (isEditar ? ExcluirAuth(helper, actionExcluir).ToString() : "") + @"                                                            
                        </div>
                    </div>
                </div>     
            </div>";

            return new MvcHtmlString(resultado);
        }

        #region EditarAuth
        public static MvcHtmlString EditarAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "Editar", action, new { @class = "btn btn-primary" });
        }
        #endregion
        
        #region ExcluirAuth
        public static MvcHtmlString ExcluirAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "Excluir", action, new { @class = "delete btn btn-default" });
        }
        #endregion

        #region CancelaAuth
        public static MvcHtmlString CancelaAuth(this HtmlHelper helper, ActionResult action)
        {
            return ActionLinkAuth(helper, "Cancelar", action, new { @class = "btn btn-small btn-primary" });
        }
        #endregion

        #region EditarSituacaoAuth
        public static MvcHtmlString EditarSituacaoAuth(this HtmlHelper helper, ActionResult action, bool ativo)
        {
            // Gerar o botão de acordo com a situação
            return ativo
                    ? ActionLinkAuth(helper, "Inativar", action, new { @class = "btn btn-primary display-block btn-editar-situacao" })
                    : ActionLinkAuth(helper, "Ativar", action, new { @class = "btn btn-danger display-block btn-editar-situacao" });
        }

        public static MvcHtmlString EditarSituacaoAuth(this HtmlHelper helper, string text, string actionName,
            string controllerName, object routeValues, string ativo)
        {
            // Gerar o botão de acordo com a situação
            return ativo == "false"
                    ? ActionLinkAuth(helper, "Inativar", actionName, controllerName, routeValues, new { @class = "btn btn-primary display-block btn-editar-situacao" })
                    : ActionLinkAuth(helper, "Ativar", actionName, controllerName, routeValues, new { @class = "btn btn-danger display-block btn-editar-situacao" });
        }

        #endregion

        #region GetAreaName
        private static string GetAreaName(RouteValueDictionary routeValueDictionary)
        {
            return routeValueDictionary["area"] as string;
        }
        #endregion

        #region AreaName
        public static string AreaName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");

            var rd = page.Context.Request.RequestContext.RouteData;
            return (rd.Values["area"] ?? rd.DataTokens["area"]) as string;
        }
        #endregion

        #region ControllerName
        public static string ControllerName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");
            return page.ViewContext.Controller.ValueProvider.GetValue("controller").AttemptedValue;
        }

        #endregion

        #region ActionName
        public static string ActionName(this WebViewPage page)
        {
            if (page == null) throw new ArgumentNullException("page");
            return page.ViewContext.Controller.ValueProvider.GetValue("action").AttemptedValue;
        }
        #endregion

        #region IsAction
        public static bool IsAction(this WebViewPage page, string actionName)
        {
            if (page == null) throw new ArgumentNullException("page");

            return page.ViewContext.Controller.ValueProvider.GetValue("action").AttemptedValue == actionName;
        }
        #endregion

        #region IsEditar
        public static bool IsEditar(this WebViewPage page)
        {
            return IsAction(page, "Editar");
        }
        #endregion

        #region IsIncluir
        public static bool IsNovo(this WebViewPage page)
        {
            return IsAction(page, "Novo");
        }
        #endregion

        #region BuildLinkAuthButton
        private static MvcHtmlString BuildLinkAuthButton(string url, string text, object htmlAttributes)
        {
            var buttonBuilder = new TagBuilder("a");
            buttonBuilder.Attributes.Add("href", url);
            buttonBuilder.SetInnerText(text);
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            buttonBuilder.MergeAttributes(attrs);

            return new MvcHtmlString(buttonBuilder.ToString());
        }
        #endregion
        
        #region BuildLinkAuthButton
        private static MvcHtmlString BuildSubmitAuthButton(string id, string text, object htmlAttributes)
        {
            var buttonBuilder = new TagBuilder("button");
            buttonBuilder.Attributes.Add("id", id);
            buttonBuilder.Attributes.Add("class", "btn btn-primary");
            buttonBuilder.Attributes.Add("data-loading-text", "Aguarde...");
            buttonBuilder.SetInnerText(text);
            buttonBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return new MvcHtmlString(buttonBuilder.ToString());
        }
        #endregion


        #region BeginForm
        /// <summary>
        /// Inicia um form para uso com o bootstrap modal.
        /// </summary>
        /// <remarks>
        /// Depende das funções onAjaxFormSucess e onAjaxFormComplete definidas no utils.js.
        /// </remarks>
        /// <param name="ajaxHelper">Helper.</param>
        /// <param name="result">ActionResult.</param>
        /// <param name="modalName">Id do div que contém o bootstrap modal.</param>
        public static MvcForm BeginForm(this AjaxHelper ajaxHelper, ActionResult result, string modalName)
        {
            var callInfo = result.GetT4MVCResult();
            var ajaxOptions = new AjaxOptions { OnSuccess = "onAjaxFormSucess('" + modalName + "')", OnComplete = "onAjaxFormComplete", HttpMethod = "POST" };
            var htmlAttributes = new Dictionary<string, object> { { "class", "form-horizontal" } };
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }
        
        public static MvcForm BeginForm(this AjaxHelper ajaxHelper, ActionResult result, string modalName, string id)
        {
            var callInfo = result.GetT4MVCResult();
            var ajaxOptions = new AjaxOptions { OnSuccess = "onAjaxFormSucess('" + modalName + "')", OnComplete = "onAjaxFormComplete", HttpMethod = "POST" };
            var htmlAttributes = new Dictionary<string, object> { { "class", "form-horizontal" }, {"id", id} };
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }
        #endregion

        #region BeginSearchForm
        /// <summary>
        /// Inicia um form para a pesquisa modal.
        /// </summary>
        /// <param name="ajaxHelper">Ajax helper.</param>
        /// <param name="result">ActionResult.</param>
        /// <param name="nomePesquisa">Nome da pesquisa.</param>
        public static MvcForm BeginSearchForm(this AjaxHelper ajaxHelper, ActionResult result, string nomePesquisa)
        {
            var callInfo = result.GetT4MVCResult();
            var ajaxOptions = new AjaxOptions { UpdateTargetId = "modal-body-" + nomePesquisa };
            var htmlAttributes = new Dictionary<string, object> { { "class", "form-inline" } };
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }
        #endregion

        #region LabelForRequired
        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "", object htmlAttributes = null)
        {
            return LabelHelper(html,
                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression), labelText,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelHelper(html,
                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression),
                null,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        #endregion

        #region LabelHelper
        private static MvcHtmlString LabelHelper(HtmlHelper html,
            ModelMetadata metadata, string htmlFieldName, string labelText,
            IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(labelText))
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (string.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            bool isRequired = false;

            if (metadata.ContainerType != null && metadata.PropertyName != null)
                isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)
                                 .GetCustomAttributes(typeof(RequiredAttribute), false)
                                 .Length == 1 ||
                             metadata.ContainerType.GetProperty(metadata.PropertyName)
                                 .GetCustomAttributes(typeof(RequiredIfAttribute), false)
                                 .Length >= 1;

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.Attributes.Add("class", "control-label col-sm-4 col-md-3");

            if (isRequired)
                tag.Attributes["class"] += " required-label";

            tag.SetInnerText(labelText);
            tag.MergeAttributes(htmlAttributes);

            var output = tag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(output);
        }
        #endregion

        #region UneditableTextBoxFor
        public static MvcHtmlString UneditableTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;

            var tag = new TagBuilder("span");
            tag.SetInnerText(html.FormatValue(model, null));
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);

            if (tag.Attributes.ContainsKey("class"))
                tag.Attributes["class"] += " uneditable-input";
            else
                tag.Attributes.Add("class", "uneditable-input");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region UneditableTextBoxFor
        public static MvcHtmlString UneditableTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;

            var tag = new TagBuilder("div");
            tag.SetInnerText(html.FormatValue(model, null));
            tag.Attributes.Add("rows", "3");
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);

            if (tag.Attributes.ContainsKey("class"))
                tag.Attributes["class"] += " input-large uneditable-textarea";
            else
                tag.Attributes.Add("class", "input-large uneditable-textarea");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region GetUrl
        /// <summary>
        /// Retorna uma url relativa à página.
        /// </summary>
        public static string GetUrl(this HtmlHelper helper, ActionResult action)
        {
            return new UrlHelper(helper.ViewContext.RequestContext).Action(action);
        }
        #endregion

        #region ToJson
        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            var scriptSerializer = JsonSerializer.Create();

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, obj);
                return MvcHtmlString.Create(sw.ToString());
            }
        }
        #endregion

        #region CustomKendoComboBoxFornecedor
        public static MvcHtmlString CustomKendoComboBoxForFornecedor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var kendoComponenteHtml = html.Kendo().ComboBoxFor(expression);

            return ObtenhaEstruturaCustomKendoComboBoxFornecedor(kendoComponenteHtml);
        }

        public static MvcHtmlString CustomKendoComboBoxFornecedor(this HtmlHelper html, String nome = "Fornecedor", object htmlAttributes = null)
        {
            var kendoComponenteHtml = html.Kendo().ComboBox().Name(nome);

            return ObtenhaEstruturaCustomKendoComboBoxFornecedor(kendoComponenteHtml);
        }

        private static MvcHtmlString ObtenhaEstruturaCustomKendoComboBoxFornecedor(ComboBoxBuilder kendoComponenteHtml)
        {
            kendoComponenteHtml = kendoComponenteHtml
                .Placeholder("-- Selecione --")
                .DataTextField("CodigoNome")
                .DataValueField("Id")
                .Template("<span data-toggle='tooltip' title='#=Tooltip#' style='width:100%'>#=CodigoNome#</span>")
                .AutoBind(false)
                .Filter("contains")
                .DataSource(source => source.Custom()
                    .ServerFiltering(true)
                    .ServerPaging(true)
                    .PageSize(80)
                    .Type("aspnetmvc-ajax")
                    .Transport(transport =>
                        transport.Read("VirtualizationComboBox_Read", "Fornecedor", new {Area = "Comum"}))
                    .Schema(schema =>
                        schema.Data("Data").Total("Total")))
                .Virtual(v => v.ItemHeight(26).ValueMapper("valueMapper"));

            var divInputGroup = new TagBuilder("div");
            divInputGroup.AddCssClass("input-group");
            divInputGroup.Attributes.Add("style", "width:100%;");

            //<span class="input-group-btn">
            var spanInputGroupBtn = new TagBuilder("span");
            spanInputGroupBtn.AddCssClass("input-group-btn");

            //<button id="pesquisar-fornecedor" class="btn btn-default btn-sm " type="button" data-toggle="modal" data-target="#modal-fornecedor">
            var button = new TagBuilder("button");
            button.Attributes.Add("id", "pesquisar-fornecedor");
            button.AddCssClass("btn btn-default btn-sm");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-toggle", "modal");
            button.Attributes.Add("data-target", "#modal-fornecedor");

            //<span class="glyphicon glyphicon-search"></span>
            var spanIconPesquisa = new TagBuilder("span");
            spanIconPesquisa.AddCssClass("glyphicon glyphicon-search");

            button.InnerHtml += spanIconPesquisa.ToString();
            spanInputGroupBtn.InnerHtml += button.ToString();
            divInputGroup.InnerHtml += kendoComponenteHtml;
            divInputGroup.InnerHtml += spanInputGroupBtn.ToString();

            var htmlFinal = divInputGroup.ToString(TagRenderMode.Normal);

            var builder = new StringBuilder();
            builder.AppendLine(@"<script>");

            builder.AppendLine(@"function valueMapper(options) {
                $.ajax({
                    url: '/Comum/Fornecedor/Fornecedores_ValueMapper', 
                    data: convertValues(options.value),
                    success: function (data) {
                        options.success(data);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }");

            builder.AppendLine(@"function convertValues(value) {
                var data = {};

                value = $.isArray(value) ? value : [value];

                for (var idx = 0; idx < value.length; idx++) {
                    data['values[' + idx + ']'] = value[idx];
                }

                return data;
            }");

            builder.AppendLine(@"$(document).ready(function () {
                $('#Fornecedor').data('kendoComboBox').value($('#Fornecedor').val());
            });");

            builder.AppendLine(@"</script>");

            return MvcHtmlString.Create(htmlFinal + builder);
        }
        #endregion

        #region CustomKendoComboBoxFuncionario
        public static MvcHtmlString CustomKendoComboBoxForFuncionario<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, String funcoes = "", object htmlAttributes = null)
        {
            var kendoComponenteHtml = html.Kendo().ComboBoxFor(expression);

            return ObtenhaEstruturaCustomKendoComboBoxFuncionario(kendoComponenteHtml, funcoes);
        }

        public static MvcHtmlString CustomKendoComboBoxFuncionario(this HtmlHelper html, String funcoes = "", object htmlAttributes = null)
        {
            var kendoComponenteHtml = html.Kendo().ComboBox().Name("Funcionario");

            return ObtenhaEstruturaCustomKendoComboBoxFuncionario(kendoComponenteHtml, funcoes);
        }

        private static MvcHtmlString ObtenhaEstruturaCustomKendoComboBoxFuncionario(ComboBoxBuilder kendoComponenteHtml, string funcoes)
        {
            kendoComponenteHtml = kendoComponenteHtml
                .Placeholder("-- Selecione --")
                .DataTextField("CodigoNome")
                .DataValueField("Id")
                .Template("<span data-toggle='tooltip' title='#=Tooltip#' style='width:100%'>#=CodigoNome#</span>")
                .AutoBind(false)
                .Filter("contains")
                .DataSource(source => source.Custom()
                    .ServerFiltering(true)
                    .ServerPaging(true)
                    .PageSize(80)
                    .Type("aspnetmvc-ajax")
                    .Transport(transport =>
                        transport.Read("VirtualizationComboBox_Read", "Funcionario", new { Area = "Comum", funcoes }))
                    .Schema(schema =>
                        schema.Data("Data").Total("Total")))
                .Virtual(v => v.ItemHeight(26).ValueMapper("valueMapperFuncionario"));

            var divInputGroup = new TagBuilder("div");
            divInputGroup.AddCssClass("input-group");
            divInputGroup.Attributes.Add("style", "width:100%;");
            
            var spanInputGroupBtn = new TagBuilder("span");
            spanInputGroupBtn.AddCssClass("input-group-btn");
            
            var button = new TagBuilder("button");
            button.Attributes.Add("id", "pesquisar-funcionario");
            button.AddCssClass("btn btn-default btn-sm");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-toggle", "modal");
            button.Attributes.Add("data-target", "#modal-funcionario");
            
            var spanIconPesquisa = new TagBuilder("span");
            spanIconPesquisa.AddCssClass("glyphicon glyphicon-search");

            button.InnerHtml += spanIconPesquisa.ToString();
            spanInputGroupBtn.InnerHtml += button.ToString();
            divInputGroup.InnerHtml += kendoComponenteHtml;
            divInputGroup.InnerHtml += spanInputGroupBtn.ToString();

            var htmlFinal = divInputGroup.ToString(TagRenderMode.Normal);

            var builder = new StringBuilder();
            builder.AppendLine(@"<script>");

            builder.AppendLine(@"function valueMapperFuncionario(options) {
                $.ajax({
                    url: '/Comum/Funcionario/Funcionarios_ValueMapper', 
                    data: convertValues(options.value),
                    success: function (data) {
                        options.success(data);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }");

            builder.AppendLine(@"function convertValues(value) {
                var data = {};

                value = $.isArray(value) ? value : [value];

                for (var idx = 0; idx < value.length; idx++) {
                    data['values[' + idx + ']'] = value[idx];
                }

                return data;
            }");

            builder.AppendLine(@"$(document).ready(function () {
                $('#Funcionario').data('kendoComboBox').value($('#Funcionario').val());
            });");

            builder.AppendLine(@"</script>");

            return MvcHtmlString.Create(htmlFinal + builder);
        }
        #endregion
    }
}