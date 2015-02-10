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
namespace T4MVC
{
    public class SharedController
    {

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
                public readonly string _Confirm = "_Confirm";
                public readonly string _ConfirmDelete = "_ConfirmDelete";
                public readonly string _Default = "_Default";
                public readonly string _Layout = "_Layout";
                public readonly string _Message = "_Message";
                public readonly string _MessagePopup = "_MessagePopup";
                public readonly string _RecortarImagem = "_RecortarImagem";
                public readonly string Error = "Error";
            }
            public readonly string _Confirm = "~/Views/Shared/_Confirm.cshtml";
            public readonly string _ConfirmDelete = "~/Views/Shared/_ConfirmDelete.cshtml";
            public readonly string _Default = "~/Views/Shared/_Default.cshtml";
            public readonly string _Layout = "~/Views/Shared/_Layout.cshtml";
            public readonly string _Message = "~/Views/Shared/_Message.cshtml";
            public readonly string _MessagePopup = "~/Views/Shared/_MessagePopup.cshtml";
            public readonly string _RecortarImagem = "~/Views/Shared/_RecortarImagem.cshtml";
            public readonly string Error = "~/Views/Shared/Error.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                public readonly string CanonicalHelperModel = "CanonicalHelperModel";
                public readonly string MenuHelperModel = "MenuHelperModel";
                public readonly string MetaRobotsHelperModel = "MetaRobotsHelperModel";
                public readonly string SiteMapHelperModel = "SiteMapHelperModel";
                public readonly string SiteMapNodeModel = "SiteMapNodeModel";
                public readonly string SiteMapNodeModelList = "SiteMapNodeModelList";
                public readonly string SiteMapPathHelperModel = "SiteMapPathHelperModel";
                public readonly string SiteMapPathNodeModel = "SiteMapPathNodeModel";
                public readonly string SiteMapTitleHelperModel = "SiteMapTitleHelperModel";
            }
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string Boolean = "Boolean";
                public readonly string BotaoPesquisaMaterialGrid = "BotaoPesquisaMaterialGrid";
                public readonly string CentroCustoList = "CentroCustoList";
                public readonly string Cep = "Cep";
                public readonly string Cnpj = "Cnpj";
                public readonly string Cpf = "Cpf";
                public readonly string currency4casasdecimais = "currency4casasdecimais";
                public readonly string currency5casasdecimais = "currency5casasdecimais";
                public readonly string Date = "Date";
                public readonly string DateTime = "DateTime";
                public readonly string DepartamentosList = "DepartamentosList";
                public readonly string DespesaReceitaList = "DespesaReceitaList";
                public readonly string @double = "double";
                public readonly string Enum = "Enum";
                public readonly string EnumList = "EnumList";
                public readonly string GridForeignKey = "GridForeignKey";
                public readonly string @int = "int";
                public readonly string @long = "long";
                public readonly string numeric = "numeric";
                public readonly string numeric4casasdecimais = "numeric4casasdecimais";
                public readonly string numeric5casasdecimais = "numeric5casasdecimais";
                public readonly string OperacaoProducaoList = "OperacaoProducaoList";
                public readonly string percent = "percent";
                public readonly string ReadonlyDouble = "ReadonlyDouble";
                public readonly string SetorProducaoList = "SetorProducaoList";
                public readonly string SituacaoTitulo = "SituacaoTitulo";
                public readonly string UnidadeList = "UnidadeList";
                public readonly string UnidadeMedidaList = "UnidadeMedidaList";
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591
