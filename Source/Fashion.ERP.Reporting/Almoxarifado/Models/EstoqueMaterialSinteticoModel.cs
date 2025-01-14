﻿namespace Fashion.ERP.Reporting.Almoxarifado.Models
{
    public class EstoqueMaterialSinteticoModel
    {
        public long Id { get; set; }
        public string Unidade { get; set; }
        public string DepositoMaterial { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public double QuantidadeEstoque { get; set; }
        public double QuantidadeReservada { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string Familia { get; set; }
        public string Marca { get; set; }
        public string Foto { get; set; }
        
        public double QuantidadeDisponivel
        {
            get { return  QuantidadeEstoque - QuantidadeReservada; }
        }
    }
}