﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Extensions;
using Fashion.ERP.Reporting.Almoxarifado;
using Telerik.Reporting.Expressions;
using System.Web;

namespace Fashion.ERP.Reporting.Helpers
{
    public static class UserFunctions
    {
        #region Variáveis

        private static readonly string FotoPadrao;

        #endregion

        static UserFunctions()
        {
            FotoPadrao = HttpContext.Current.Server.MapPath(@"~\Content\images\no_image.jpg");//no_image_report.png
        }

        #region CTipoPrestadorServico
        [Function(Category = "Conversion", Namespace = "FashionErp", Description = "Cria uma lista de de tipo de prestadores de serviço, separados por vírgula.")]
        public static string CTipoPrestadorServico(IEnumerable<TipoPrestadorServico> list)
        {
            if (list == null)
                return string.Empty;

            var converter = TypeDescriptor.GetConverter(typeof(TipoPrestadorServico));
            return string.Join(", ", list.Select(p => converter.ConvertTo(p, typeof(string))));
        }
        #endregion

        #region ConvertToString
        [Function(Category = "Conversion", Namespace = "FashionErp", Description = "Converte um objeto utilizando TypeConverter.")]
        public static string ConvertToString(object obj)
        {
            return TypeDescriptor.GetConverter(obj.GetType()).ConvertToString(obj);
        }
        #endregion

        #region EnumerableToString
        [Function(Category = "Conversion", Namespace = "FashionErp", Description = "Converte uma lista de strings em apenas uma, com itens separados por vírgula.")]
        public static string EnumerableToString(IEnumerable<string> list)
        {
            return string.Join(", ", list);
        }
        #endregion

        #region EnumerableToString
        [Function(Category = "List", Namespace = "FashionErp", Description = "Conta os elementos de uma lista")]
        public static long CountList(IEnumerable list)
        {
            if (list == null)
                return 0;

            return list.Count();
        }
        
        public static int Count(this IEnumerable source)
        {
            return Enumerable.Count(source.Cast<object>());
        }

        #endregion

        #region CoresToString
        [Function(Category = "Conversion", Namespace = "FashionErp", Description = "Converte uma lista de strings em apenas uma, com itens separados por vírgula.")]
        public static string CoresToString(IEnumerable<Cor> cores)
        {
            return cores == null 
                ? string.Empty 
                : EnumerableToString(cores.Select(c => c.Nome));
        }

        #endregion

        #region Any
        [Function(Category = "Aggregates", Namespace = "FashionErp", Description = "Verifica se uma lista possui pelo menos um item.")]
        public static bool Any(IEnumerable list)
        {
            return list != null && list.GetEnumerator().MoveNext();
        }
        #endregion

        #region EnderecoFoto
        [Function(Category = "Text", Namespace = "FashionErp", Description = "Retorna o endereço completo para a imagem.")]
        public static string EnderecoFoto(string nomeFoto)
        {
            var foto = HttpContext.Current.Server.MapPath(@"~\" + nomeFoto);

            return File.Exists(foto) ? foto : FotoPadrao;
        }
        #endregion

        #region EnderecoFoto
        [Function(Category = "Text", Namespace = "FashionErp", Description = "Retorna o endereço completo para a imagem.")]
        public static string EnderecoCompletoFoto(string nomeFoto)
        {
            var foto = HttpContext.Current.Server.MapPath(@"~\Uploads\Files\" + nomeFoto);

            return File.Exists(foto) ? foto : FotoPadrao;
        }
        #endregion

        #region ModeloFotoPadrao
        [Function(Category = "Conversion", Namespace = "FashionErp", Description = "Retorna apenas a foto padrão para impressão.")]
        public static string ModeloFotoPadrao(IEnumerable<ModeloFoto> fotos)
        {
            if (fotos == null)
                return FotoPadrao;

            var modeloFoto = fotos.FirstOrDefault(p => p.Padrao && p.Impressao);

            return modeloFoto != null
                ? EnderecoFoto(modeloFoto.Foto.Nome.GetFileUrlFotoModelo())
                : FotoPadrao;
        }

        public static string GetFileUrlFotoModelo(this string filename)
        {
            return VirtualPathUtility.ToAbsolute("~/Uploads/Files/") + Path.GetFileName(filename);
        }
        #endregion

        #region FormateCpfCnpj
        [Function(Category = "Format", Namespace = "FashionErp", Description = "Formata o Cpf e o Cnpj.")]
        public static string FormateCpfCnpj(string cpfCnpj)
        {
            return cpfCnpj.FormateCpfCnpj();
        }
        #endregion

        [Function(Category = "Fomart", Namespace = "FashionErp", Description = "Ajuste os valores de data se necessário.")]
        public static object AjusteValores(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is DateTime)
            {
                var datetime = (DateTime) value;
                return datetime.Date.ToString("dd/MM/yyyy");
            }

            return value;
        }
    }
}