﻿using System.Linq;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Compras;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Repository;

namespace Fashion.ERP.Testes.Persistencia
{   
    /**
    * Responsável por fornecer objetos persistidos para testes de unidade 
    * Alterações realizadas nesses objetos podem causar bugs nos testes já implementados.
    */
    public class FabricaObjetosPersistidos
    {
        private readonly FabricaObjetos _fabricaObjetos = new FabricaObjetos();

        #region Almoxarifado

        public ReservaMaterial ObtenhaReservaMaterial()
        {
            var reservaMaterial = _fabricaObjetos.ObtenhaReservaMaterial();
            reservaMaterial.Unidade = ObtenhaUnidade();
            reservaMaterial.Requerente = ObtenhaFuncionario();
            reservaMaterial.Colecao = ObtenhaColecao();

            var reservaMaterialItem = _fabricaObjetos.ObtenhaReservaMaterialItem();
            reservaMaterialItem.Material = ObtenhaMaterial();
            reservaMaterial.ReservaMaterialItems.Add(reservaMaterialItem);

            RepositoryFactory.Create<ReservaMaterial>().Save(reservaMaterial);
            return reservaMaterial;
        }

        public void ExcluaReservaMaterial(ReservaMaterial reservaMaterial)
        {
            RepositoryFactory.Create<ReservaMaterial>().Delete(reservaMaterial);
            ExcluaPessoa(reservaMaterial.Unidade);
            ExcluaPessoa(reservaMaterial.Requerente);
            ExcluaColecao(reservaMaterial.Colecao);

            ExcluaMaterial(reservaMaterial.ReservaMaterialItems[0].Material);
        }

        public MovimentacaoEstoqueMaterial ObtenhaMovimentacaoEstoqueMaterial()
        {
            var movimentacaoEstoqueMaterial = _fabricaObjetos.ObtenhaMovimentacaoEstoqueMaterial();
            movimentacaoEstoqueMaterial.EstoqueMaterial = ObtenhaEstoqueMaterial();

            RepositoryFactory.Create<MovimentacaoEstoqueMaterial>().Save(movimentacaoEstoqueMaterial);

            return movimentacaoEstoqueMaterial;
        }

        public void ExcluaMovimentacaoEstoqueMaterial(MovimentacaoEstoqueMaterial movimentacaoEstoqueMaterial)
        {
            RepositoryFactory.Create<MovimentacaoEstoqueMaterial>().Delete(movimentacaoEstoqueMaterial);
            ExcluaEstoqueMaterial(movimentacaoEstoqueMaterial.EstoqueMaterial);
        }

        public EstoqueMaterial ObtenhaEstoqueMaterial()
        {
            var estoqueMaterial = _fabricaObjetos.ObtenhaEstoqueMaterial();
            estoqueMaterial.Material = ObtenhaMaterial();
            estoqueMaterial.DepositoMaterial = ObtenhaDepositoMaterial();

            RepositoryFactory.Create<EstoqueMaterial>().Save(estoqueMaterial);
            
            return estoqueMaterial;
        }

        public void ExcluaEstoqueMaterial(EstoqueMaterial estoqueMaterial)
        {
            RepositoryFactory.Create<EstoqueMaterial>().Delete(estoqueMaterial);
            ExcluaMaterial(estoqueMaterial.Material);
            ExcluaDepositoMaterial(estoqueMaterial.DepositoMaterial);
        }

        public EntradaMaterial ObtenhaEntradaMaterial()
        {
            var entradaMaterial = _fabricaObjetos.ObtenhaEntradaMaterial();
            entradaMaterial.DepositoMaterialDestino = ObtenhaDepositoMaterial();

            var entradaItemMaterial = _fabricaObjetos.ObtenhaEntradaItemMaterial();
            entradaItemMaterial.Material = ObtenhaMaterial();
            entradaItemMaterial.UnidadeMedidaCompra = ObtenhaUnidadeMedida();
            entradaItemMaterial.MovimentacaoEstoqueMaterial = _fabricaObjetos.ObtenhaMovimentacaoEstoqueMaterial();
            entradaItemMaterial.MovimentacaoEstoqueMaterial.EstoqueMaterial = ObtenhaEstoqueMaterial();
            
            entradaMaterial.AddEntradaItemMaterial(entradaItemMaterial);

            RepositoryFactory.Create<EntradaMaterial>().Save(entradaMaterial);

            return entradaMaterial;
        }

        public void ExcluaEntradaMaterial(EntradaMaterial entradaMaterial)
        {
            RepositoryFactory.Create<EntradaMaterial>().Delete(entradaMaterial);
            ExcluaMaterial(entradaMaterial.EntradaItemMateriais.First().Material);
            ExcluaUnidadeMedida(entradaMaterial.EntradaItemMateriais.First().UnidadeMedidaCompra);
            ExcluaEstoqueMaterial(entradaMaterial.EntradaItemMateriais.First().MovimentacaoEstoqueMaterial.EstoqueMaterial);
        }
        
        public ConferenciaEntradaMaterial ObtenhaConferenciaEntradaMaterial()
        {
            var conferenciaEntradaMaterial = _fabricaObjetos.ObtenhaConferenciaEntradaMaterial();
            conferenciaEntradaMaterial.Comprador = ObtenhaFuncionario();
            
            var conferenciaEntradaMaterialItem = _fabricaObjetos.ObtenhaConferenciaEntradaMaterialItem();
            conferenciaEntradaMaterialItem.Material = ObtenhaMaterial();
            conferenciaEntradaMaterialItem.UnidadeMedida = ObtenhaUnidadeMedida();
            conferenciaEntradaMaterial.ConferenciaEntradaMaterialItens.Add(conferenciaEntradaMaterialItem);
            
            RepositoryFactory.Create<ConferenciaEntradaMaterial>().Save(conferenciaEntradaMaterial);
            
            return conferenciaEntradaMaterial;
        }

        public void ExcluaConferenciaEntradaMaterial(ConferenciaEntradaMaterial conferenciaEntradaMaterial)
        {
            RepositoryFactory.Create<ConferenciaEntradaMaterial>().Delete(conferenciaEntradaMaterial);
            ExcluaPessoa(conferenciaEntradaMaterial.Comprador);
            ExcluaMaterial(conferenciaEntradaMaterial.ConferenciaEntradaMaterialItens[0].Material);
            ExcluaUnidadeMedida(conferenciaEntradaMaterial.ConferenciaEntradaMaterialItens[0].UnidadeMedida);
        }

        public DepositoMaterial ObtenhaDepositoMaterial()
        {
            var depositoMaterial = _fabricaObjetos.ObtenhaDepositoMaterial();
            depositoMaterial.Unidade = ObtenhaUnidade();
            RepositoryFactory.Create<DepositoMaterial>().Save(depositoMaterial);
            
            return depositoMaterial;
        }

        public SaidaMaterial ObtenhaSaidaMaterial()
        {
            var saidaMaterial = _fabricaObjetos.ObtenhaSaidaMaterial();
            saidaMaterial.DepositoMaterialDestino = ObtenhaDepositoMaterial();
            saidaMaterial.DepositoMaterialOrigem = ObtenhaDepositoMaterial();
            RepositoryFactory.Create<SaidaMaterial>().Save(saidaMaterial);
            
            return saidaMaterial;
        }

        public void ExcluaSaidaMaterial(SaidaMaterial saidaMaterial)
        {
            RepositoryFactory.Create<SaidaMaterial>().Delete(saidaMaterial);
        }

        public Bordado ObtenhaBordado()
        {
            var bordado = _fabricaObjetos.ObtenhaBordado();

            RepositoryFactory.Create<Bordado>().Save(bordado);

            return bordado;
        }

        public TipoItem ObtenhaTipoItem()
        {
            var tipoItem = _fabricaObjetos.ObtenhaTipoItem();

            RepositoryFactory.Create<TipoItem>().Save(tipoItem);

            return tipoItem;
        }

        public void ExcluaTipoItem(TipoItem tipoItem)
        {
            RepositoryFactory.Create<TipoItem>().Delete(tipoItem);
        }

        public Material ObtenhaMaterial()
        {
            var material = _fabricaObjetos.ObtenhaMaterial();
            material.OrigemSituacaoTributaria = ObtenhaOrigemSituacaoTributaria();
            material.UnidadeMedida = ObtenhaUnidadeMedida();
            material.MarcaMaterial = ObtenhaMarcaMaterial();
            material.Subcategoria = ObtenhaSubCategoria();
            material.Familia = ObtenhaFamilia();
            material.Foto = _fabricaObjetos.ObtenhaArquivo();

            RepositoryFactory.Create<Material>().Save(material);

            return material;
        }
        
        public void ExcluaMaterial(Material material)
        {
            RepositoryFactory.Create<Material>().Delete(material);
            ExcluaOrigemSituacaoTributaria(material.OrigemSituacaoTributaria);
            ExcluaUnidadeMedida(material.UnidadeMedida);
            ExcluaSubcategoria(material.Subcategoria);
            ExcluaMarcaMaterial(material.MarcaMaterial);
            ExcluaFamilia(material.Familia);
        }
        
        public Familia ObtenhaFamilia()
        {
            var familia = _fabricaObjetos.ObtenhaFamilia();

            RepositoryFactory.Create<Familia>().Save(familia);

            return familia;
        }

        public void ExcluaFamilia(Familia familia)
        {
            RepositoryFactory.Create<Familia>().Delete(familia);
        }

        public Subcategoria ObtenhaSubCategoria()
        {
            var subCategoria = _fabricaObjetos.ObtenhaSubcategoria();
            subCategoria.Categoria = ObtenhaCategoria();
            RepositoryFactory.Create<Subcategoria>().Save(subCategoria);
            return subCategoria;
        }

        public void ExcluaSubcategoria(Subcategoria subcategoria)
        {
            RepositoryFactory.Create<Subcategoria>().Delete(subcategoria);
        }

        public MarcaMaterial ObtenhaMarcaMaterial()
        {
            var marcaMaterial = _fabricaObjetos.ObtenhaMarcaMaterial();

            RepositoryFactory.Create<MarcaMaterial>().Save(marcaMaterial);

            return marcaMaterial;
        }

        public void ExcluaMarcaMaterial(MarcaMaterial marcaMaterial)
        {
            RepositoryFactory.Create<MarcaMaterial>().Delete(marcaMaterial);
        }

        public OrigemSituacaoTributaria ObtenhaOrigemSituacaoTributaria()
        {
            var origemSituacaoTributaria = _fabricaObjetos.ObtenhaOrigemSituacaoTributaria();

            RepositoryFactory.Create<OrigemSituacaoTributaria>().Save(origemSituacaoTributaria);

            return origemSituacaoTributaria;
        }

        public void ExcluaOrigemSituacaoTributaria(OrigemSituacaoTributaria origemSituacaoTributaria)
        {
            RepositoryFactory.Create<OrigemSituacaoTributaria>().Delete(origemSituacaoTributaria);
        }

        public UnidadeMedida ObtenhaUnidadeMedida()
        {
            var unidadeMedida = _fabricaObjetos.ObtenhaUnidadeMedida();

            RepositoryFactory.Create<UnidadeMedida>().Save(unidadeMedida);

            return unidadeMedida;
        }

        public PedidoCompraItemCancelado ObtenhaPedidoCompraItemCancelado()
        {
            var pedidoCompraItemCancelado = _fabricaObjetos.ObtenhaPedidoCompraItemCancelado();
            pedidoCompraItemCancelado.MotivoCancelamentoPedidoCompra = ObtenhaMotivoCancelamentoPedidoCompra();

            RepositoryFactory.Create<PedidoCompraItemCancelado>().Save(pedidoCompraItemCancelado);

            return pedidoCompraItemCancelado;
        }

        public void ExcluaPedidoCompraItemCancelado(PedidoCompraItemCancelado pedidoCompraItemCancelado)
        {
            RepositoryFactory.Create<PedidoCompraItemCancelado>().Delete(pedidoCompraItemCancelado);
            ExcluaMotivoCancelamentoPedidoCompra(pedidoCompraItemCancelado.MotivoCancelamentoPedidoCompra);
        }
        
        public void ExcluaUnidadeMedida(UnidadeMedida unidadeMedida)
        {
            RepositoryFactory.Create<UnidadeMedida>().Delete(unidadeMedida);
        }

        public Categoria ObtenhaCategoria()
        {
            var categoria = _fabricaObjetos.ObtenhaCategoria();
            
            RepositoryFactory.Create<Categoria>().Save(categoria);
            
            return categoria;
        }

        public void ExcluaDepositoMaterial(DepositoMaterial depositoMaterial)
        {
            RepositoryFactory.Create<DepositoMaterial>().Delete(depositoMaterial);
            ExcluaPessoa(depositoMaterial.Unidade);
        }

        public ConferenciaEntradaMaterial ObtenhaOrdemEntradaMaterial()
        {
            var ordemEntrada = _fabricaObjetos.ObtenhaConferenciaEntradaMaterial();
            ordemEntrada.Comprador = ObtenhaFuncionario();

            RepositoryFactory.Create<ConferenciaEntradaMaterial>().Save(ordemEntrada);

            return ordemEntrada;
        }

        public void ExcluaOrdemEntradaMaterial(ConferenciaEntradaMaterial conferenciaEntrada)
        {
            RepositoryFactory.Create<ConferenciaEntradaMaterial>().Delete(conferenciaEntrada);
            ExcluaPessoa(conferenciaEntrada.Comprador);
        }
        #endregion
        
        #region Compras
        
        public PedidoCompraItem ObtenhaPedidoCompraItem()
        {
            var pedidoCompraItem = _fabricaObjetos.ObtenhaPedidoCompraItem();
            pedidoCompraItem.Material = ObtenhaMaterial();
            pedidoCompraItem.UnidadeMedida = ObtenhaUnidadeMedida();
            pedidoCompraItem.PedidoCompra = ObtenhaPedidoCompra();
            RepositoryFactory.Create<PedidoCompraItem>().Save(pedidoCompraItem);

            return pedidoCompraItem;
        }

        public void ExcluaPedidoCompraItem(PedidoCompraItem pedidoCompraItem)
        {
            RepositoryFactory.Create<PedidoCompraItem>().Delete(pedidoCompraItem);
            ExcluaMaterial(pedidoCompraItem.Material);
            ExcluaUnidadeMedida(pedidoCompraItem.UnidadeMedida);
        }

        public PedidoCompra ObtenhaPedidoCompra()
        {
            var pedidoCompra = _fabricaObjetos.ObtenhaPedidoCompra();
            pedidoCompra.Fornecedor = ObtenhaFuncionario();
            pedidoCompra.Comprador = ObtenhaFuncionario();
            pedidoCompra.FuncionarioAutorizador = ObtenhaFuncionario();
            pedidoCompra.UnidadeEstocadora = ObtenhaUnidade();
            pedidoCompra.Prazo = ObtenhaPrazo();
            pedidoCompra.MeioPagamento = ObtenhaMeioPagamento();
            
            var pedidoCompraItem = _fabricaObjetos.ObtenhaPedidoCompraItem();
            pedidoCompraItem.Material = ObtenhaMaterial();
            pedidoCompraItem.UnidadeMedida = ObtenhaUnidadeMedida();
            pedidoCompraItem.PedidoCompra = pedidoCompra;

            pedidoCompra.AddPedidoCompraItem(pedidoCompraItem);
            RepositoryFactory.Create<PedidoCompra>().Save(pedidoCompra);

            return pedidoCompra;
        }

        public void ExcluaPedidoCompra(PedidoCompra pedidoCompra)
        {
            RepositoryFactory.Create<PedidoCompra>().Delete(pedidoCompra);
            ExcluaFornecedor(pedidoCompra.Fornecedor);
            ExcluaPessoa(pedidoCompra.Comprador);
            ExcluaPessoa(pedidoCompra.FuncionarioAutorizador);
            ExcluaPessoa(pedidoCompra.UnidadeEstocadora);
            ExcluaPrazo(pedidoCompra.Prazo);
        }

        public MotivoCancelamentoPedidoCompra ObtenhaMotivoCancelamentoPedidoCompra()
        {
            var motivoCancelamentoPedidoCompra = _fabricaObjetos.ObtenhaMotivoCancelamentoPedidoCompra();
            RepositoryFactory.Create<MotivoCancelamentoPedidoCompra>().Save(motivoCancelamentoPedidoCompra);
            return motivoCancelamentoPedidoCompra;
        }

        public void ExcluaMotivoCancelamentoPedidoCompra(MotivoCancelamentoPedidoCompra motivoCancelamentoPedidoCompra)
        {
            RepositoryFactory.Create<MotivoCancelamentoPedidoCompra>().Delete(motivoCancelamentoPedidoCompra);
        }
        #endregion

        #region Engenharia de Produto
        
        public VariacaoModelo ObtenhaVariacaoModelo()
        {
            var variacaoModelo = _fabricaObjetos.ObtenhaVariacaoModelo();
         
            variacaoModelo.Variacao = ObtenhaVariacao();
            variacaoModelo.AddCor(ObtenhaCor());

            RepositoryFactory.Create<VariacaoModelo>().Save(variacaoModelo);

            return variacaoModelo;
        }

        public void ExcluaVariacaoModelo(VariacaoModelo variacaoModelo)
        {
            RepositoryFactory.Create<VariacaoModelo>().Delete(variacaoModelo);
            ExcluaVariacao(variacaoModelo.Variacao);
            ExcluaCor(variacaoModelo.Cores.ToList()[0]);
        }

        public Natureza ObtenhaNatureza()
        {
            var natureza = _fabricaObjetos.ObtenhaNatureza();

            RepositoryFactory.Create<Natureza>().Save(natureza);

            return natureza;
        }

        public Artigo ObtenhaArtigo()
        {
            var artigo = _fabricaObjetos.ObtenhaArtigo();

            RepositoryFactory.Create<Artigo>().Save(artigo);

            return artigo;
        } 

        public Modelo ObtenhaModelo()
        {
            var modelo = _fabricaObjetos.ObtenhaModelo();

            modelo.Grade = ObtenhaGrade();
            modelo.Artigo = ObtenhaArtigo();
            modelo.Classificacao = ObtenhaClassificacao();
            modelo.Barra = ObtenhaBarra();
            modelo.Colecao = ObtenhaColecao();
            modelo.Estilista = ObtenhaFuncionario();
            modelo.Marca = ObtenhaMarca();
            modelo.Natureza = ObtenhaNatureza();

            RepositoryFactory.Create<Modelo>().Save(modelo);

            return modelo;
        }

        public Grade ObtenhaGrade()
        {
            var grade = _fabricaObjetos.ObtenhaGrade();

            RepositoryFactory.Create<Grade>().Save(grade);

            return grade;
        }

        public Comprimento ObtenhaComprimento()
        {
            var comprimento = _fabricaObjetos.ObtenhaComprimento();

            RepositoryFactory.Create<Comprimento>().Save(comprimento);

            return comprimento;
        }

        public void ExcluaComprimento(Comprimento comprimento)
        {
            RepositoryFactory.Create<Comprimento>().Delete(comprimento);
        }

        public ProdutoBase ObtenhaProdutoBase()
        {
            var produtoBase = _fabricaObjetos.ObtenhaProdutoBase();
            
            RepositoryFactory.Create<ProdutoBase>().Save(produtoBase);

            return produtoBase;
        }

        public void ExcluaProdutoBase(ProdutoBase produtoBase)
        {
            RepositoryFactory.Create<ProdutoBase>().Delete(produtoBase);
        }

        public Barra ObtenhaBarra()
        {
            var barra = _fabricaObjetos.ObtenhaBarra();

            RepositoryFactory.Create<Barra>().Save(barra);

            return barra;
        }

        public Segmento ObtenhaSegmento()
        {
            var segmento = _fabricaObjetos.ObtenhaSegmento();

            RepositoryFactory.Create<Segmento>().Save(segmento);

            return segmento;
        }

        public void ExcluaSegmento(Segmento segmento)
        {
            RepositoryFactory.Create<Segmento>().Delete(segmento);
        }

        public Classificacao ObtenhaClassificacao()
        {
            var classificacao = _fabricaObjetos.ObtenhaClassificacao();

            RepositoryFactory.Create<Classificacao>().Save(classificacao);

            return classificacao;
        }

        public void ExcluaModelo(Modelo modelo)
        {
            RepositoryFactory.Create<Modelo>().Delete(modelo);
        }

        public void ExcluaNatureza(Natureza natureza)
        {
            RepositoryFactory.Create<Natureza>().Delete(natureza);
        }

        public void ExcluaBarra(Barra barra)
        {
            RepositoryFactory.Create<Barra>().Delete(barra);
        }

        public void ExcluaClassificacao(Classificacao classificacao)
        {
            RepositoryFactory.Create<Classificacao>().Delete(classificacao);
        }

        public void ExcluaArtigo(Artigo artigo)
        {
            RepositoryFactory.Create<Artigo>().Delete(artigo);
        }

        public void ExcluaGrade(Grade grade)
        {
            RepositoryFactory.Create<Grade>().Delete(grade);
        }

        public SequenciaProducao ObtenhaSequenciaProducao()
        {
            var sequenciaProducao = _fabricaObjetos.ObtenhaSequenciaProducao();
            var setorProducao = ObtenhaSetorProducao();
            sequenciaProducao.SetorProducao = ObtenhaSetorProducao();
            sequenciaProducao.DepartamentoProducao = setorProducao.DepartamentoProducao;

            var modelo = ObtenhaModelo();
            modelo.AddSequenciaProducao(sequenciaProducao);

            RepositoryFactory.Create<SequenciaProducao>().Save(sequenciaProducao);

            return sequenciaProducao;
        }

        public void ExcluaSequenciaProducao(SequenciaProducao sequenciaProducao)
        {
            RepositoryFactory.Create<SequenciaProducao>().Delete(sequenciaProducao);
            ExcluaDepartamentoProducao(sequenciaProducao.DepartamentoProducao);
            ExcluaSetorProducao(sequenciaProducao.SetorProducao);
        }

        public SetorProducao ObtenhaSetorProducao()
        {
            var setorProducao = _fabricaObjetos.ObtenhaSetorProducao();
            setorProducao.DepartamentoProducao = ObtenhaDepartamentoProducao();
            RepositoryFactory.Create<SetorProducao>().Save(setorProducao);
            return setorProducao;
        }

        public void ExcluaSetorProducao(SetorProducao setorProducao)
        {
            RepositoryFactory.Create<SetorProducao>().Delete(setorProducao);
            ExcluaDepartamentoProducao(setorProducao.DepartamentoProducao);
        }

        #endregion

        #region Comun

        public ClassificacaoDificuldade ObtenhaClassificacaoDificuldade()
        {
            var classificacaoDificuldade = _fabricaObjetos.ObtenhaClassificacaoDificuldade();

            RepositoryFactory.Create<ClassificacaoDificuldade>().Save(classificacaoDificuldade);

            return classificacaoDificuldade;
        }

        public void ExcluaClassificacaoDificuldade(ClassificacaoDificuldade classificacaoDificuldade)
        {
            RepositoryFactory.Create<ClassificacaoDificuldade>().Delete(classificacaoDificuldade);
        }

        public Variacao ObtenhaVariacao()
        {
            var variacao = _fabricaObjetos.ObtenhaVariacao();

            RepositoryFactory.Create<Variacao>().Save(variacao);

            return variacao;
        }
        
        public void ExcluaVariacao(Variacao variacao)
        {
            RepositoryFactory.Create<Variacao>().Delete(variacao);
        }

        public CentroCusto ObtenhaCentroCusto()
        {
            var centroCusto = _fabricaObjetos.ObtenhaCentroCusto();

            RepositoryFactory.Create<CentroCusto>().Save(centroCusto);

            return centroCusto;
        }


        public void ExcluaCentroCusto(CentroCusto centroCusto)
        {
            RepositoryFactory.Create<CentroCusto>().Delete(centroCusto);
        }

        public OperacaoProducao ObtenhaOperacaoProducao()
        {
            var operacaoProducao = _fabricaObjetos.ObtenhaOperacaoProducao();
            operacaoProducao.SetorProducao = ObtenhaSetorProducao();

            RepositoryFactory.Create<OperacaoProducao>().Save(operacaoProducao);

            return operacaoProducao;
        }

        public void ExcluaOperacaoProducao(OperacaoProducao operacaoProducao)
        {
            RepositoryFactory.Create<OperacaoProducao>().Delete(operacaoProducao);
            RepositoryFactory.Create<SetorProducao>().Delete(operacaoProducao.SetorProducao);
        }

        public Tamanho ObtenhaTamanho()
        {
            var tamanho = _fabricaObjetos.ObtenhaTamanho();

            RepositoryFactory.Create<Tamanho>().Save(tamanho);

            return tamanho;
        }

        public void ExcluaTamanho(Tamanho tamanho)
        {
            RepositoryFactory.Create<Tamanho>().Delete(tamanho);
        }

        public Cor ObtenhaCor()
        {
            var cor = _fabricaObjetos.ObtenhaCor();
            
            RepositoryFactory.Create<Cor>().Save(cor);

            return cor;
        }

        public void ExcluaCor(Cor cor)
        {
            RepositoryFactory.Create<Cor>().Delete(cor);
        }

        public Arquivo ObtenhaArquivo()
        {
            var arquivo = _fabricaObjetos.ObtenhaArquivo();

            RepositoryFactory.Create<Arquivo>().Save(arquivo);
            
            return arquivo;
        }

        public Prazo ObtenhaPrazo()
        {
            var prazo = _fabricaObjetos.ObtenhaPrazo();

            RepositoryFactory.Create<Prazo>().Save(prazo);

            return prazo;
        }

        public void ExcluaPrazo(Prazo prazo)
        {
            RepositoryFactory.Create<Prazo>().Delete(prazo);
        }

        public MeioPagamento ObtenhaMeioPagamento()
        {
            var meioPagamento = _fabricaObjetos.ObtenhaMeioPagamento();

            RepositoryFactory.Create<MeioPagamento>().Save(meioPagamento);

            return meioPagamento;
        }

        public void ExcluaMeioPagamento(MeioPagamento meioPagamento)
        {
            RepositoryFactory.Create<MeioPagamento>().Delete(meioPagamento);
        }

        public Referencia ObtenhaReferencia()
        {
            var referencia = _fabricaObjetos.ObtenhaReferencia();
            referencia.Cliente = ObtenhaCliente();
            RepositoryFactory.Create<Referencia>().Save(referencia);

            return referencia;
        }
        
        public void ExcluaReferencia(Referencia referencia)
        {
            RepositoryFactory.Create<Referencia>().Delete(referencia);
            ExcluaCliente(referencia.Cliente);
        }
        
        public Pessoa ObtenhaUnidade()
        {
            var pessoa = _fabricaObjetos.ObtenhaUnidade();
            RepositoryFactory.Create<Pessoa>().Save(pessoa);

            return pessoa;
        }

        public TipoFornecedor ObtenhaTipoFornecedor()
        {
            var tipoFornecedor = _fabricaObjetos.ObtenhaTipoFornecedor();
            RepositoryFactory.Create<TipoFornecedor>().Save(tipoFornecedor);
            return tipoFornecedor;
        }
        
        public void ExcluaTipoFornecedor(TipoFornecedor tipoFornecedor)
        {
            RepositoryFactory.Create<TipoFornecedor>().Delete(tipoFornecedor);
        }

        public GrauDependencia ObtenhaGrauDependencia()
        {
            var grauDependencia = _fabricaObjetos.ObtenhaGrauDependencia();
            RepositoryFactory.Create<GrauDependencia>().Save(grauDependencia);
            return grauDependencia;
        }
        
        public void ExcluaGrauDependencia(GrauDependencia grauDependencia)
        {
            RepositoryFactory.Create<GrauDependencia>().Delete(grauDependencia);
        }
        
        public Cliente ObtenhaCliente()
        {
            var cliente = _fabricaObjetos.ObtenhaCliente();
            cliente.AreaInteresse = ObtenhaAreaInteresse();
            RepositoryFactory.Create<Cliente>().Save(cliente);
            return cliente;
        }
        
        public void ExcluaCliente(Cliente cliente)
        {
            RepositoryFactory.Create<Cliente>().Delete(cliente);
            ExcluaAreaInteresse(cliente.AreaInteresse);
        }
        
        public Dependente ObtenhaDependente()
        {
            var dependente = _fabricaObjetos.ObtenhaDependente();
            RepositoryFactory.Create<Dependente>().Save(dependente);
            return dependente;
        }
        
        public void ExcluaDependente(Dependente dependente)
        {
            RepositoryFactory.Create<Dependente>().Delete(dependente);
        }
        
        public AreaInteresse ObtenhaAreaInteresse()
        {
            var areaInteresse = _fabricaObjetos.ObtenhaAreaInteresse();
            RepositoryFactory.Create<AreaInteresse>().Save(areaInteresse);
            return areaInteresse;
        }
        
        public void ExcluaAreaInteresse(AreaInteresse areaInteresse)
        {
            RepositoryFactory.Create<AreaInteresse>().Delete(areaInteresse);
        }
        
        public PerfilDeAcesso ObtenhaPerfilDeAcesso()
        {
            var perfilDeAcesso = _fabricaObjetos.ObtenhaPerfilDeAcesso();
            RepositoryFactory.Create<PerfilDeAcesso>().Save(perfilDeAcesso);
            return perfilDeAcesso;
        }
        
        public void ExcluaPerfilDeAcesso(PerfilDeAcesso perfilDeAcesso)
        {
            RepositoryFactory.Create<PerfilDeAcesso>().Delete(perfilDeAcesso);
        }

        public Permissao ObtenhaPermissao()
        {
            var permissao = _fabricaObjetos.ObtenhaPermissao();

            RepositoryFactory.Create<Permissao>().Save(permissao);

            return permissao;
        }

        public void ExcluaPermissao(Permissao permissao)
        {
            RepositoryFactory.Create<Permissao>().Delete(permissao);
        }

        public Colecao ObtenhaColecao()
        {
            var colecao = _fabricaObjetos.ObtenhaColecao();

            RepositoryFactory.Create<Colecao>().Save(colecao);

            return colecao;
        }

        public RemessaProducao ObtenhaRemessaProducao()
        {
            var remessaProducao = _fabricaObjetos.ObtenhaRemessaProducao();
            remessaProducao.Colecao = ObtenhaColecao();

            RepositoryFactory.Create<RemessaProducao>().Save(remessaProducao);

            return remessaProducao;
        }

        public void ExcluaRemessaProducao(RemessaProducao remessaProducao)
        {
            RepositoryFactory.Create<RemessaProducao>().Delete(remessaProducao);
            RepositoryFactory.Create<Colecao>().Delete(remessaProducao.Colecao);
        }
        
        public void ExcluaColecao(Colecao colecao)
        {
            RepositoryFactory.Create<Colecao>().Delete(colecao);
        }
        
        public Marca ObtenhaMarca()
        {
            var marca = _fabricaObjetos.ObtenhaMarca();

            RepositoryFactory.Create<Marca>().Save(marca);

            return marca;
        }
        
        public void ExcluaMarca(Marca marca)
        {
            RepositoryFactory.Create<Marca>().Delete(marca);
        }

        public Pessoa ObtenhaFuncionario()
        {
            var pessoa = _fabricaObjetos.ObtenhaFuncionario(FuncaoFuncionario.Estilista);

            RepositoryFactory.Create<Pessoa>().Save(pessoa);

            return pessoa;
        }

        public Pessoa ObtenhaFornecedor()
        {
            var pessoa = _fabricaObjetos.ObtenhaFornecedor();
            pessoa.Fornecedor.TipoFornecedor = ObtenhaTipoFornecedor();

            RepositoryFactory.Create<Pessoa>().Save(pessoa);

            return pessoa;
        }

        public void ExcluaFornecedor(Pessoa pessoa)
        {
            RepositoryFactory.Create<Pessoa>().Delete(pessoa);

            if (pessoa.Fornecedor != null)
            {
                ExcluaTipoFornecedor(pessoa.Fornecedor.TipoFornecedor);
            }
        }

        public Pessoa ObtenhaTransportadora()
        {
            var pessoa = _fabricaObjetos.ObtenhaTransportadora();

            RepositoryFactory.Create<Pessoa>().Save(pessoa);

            return pessoa;
        }

        public void ExcluaTransportadora(Pessoa pessoa)
        {
            RepositoryFactory.Create<Pessoa>().Delete(pessoa);
        }

        public DepartamentoProducao ObtenhaDepartamentoProducao()
        {
            var departamentoProducao = _fabricaObjetos.ObtenhaDepartamentoProducao();
            RepositoryFactory.Create<DepartamentoProducao>().Save(departamentoProducao);
            return departamentoProducao;
        }
        
        public void ExcluaDepartamentoProducao(DepartamentoProducao departamento)
        {
            RepositoryFactory.Create<DepartamentoProducao>().Delete(departamento);
        }


        
        public void ExcluaPessoa(Pessoa pessoa)
        {
            RepositoryFactory.Create<Pessoa>().Delete(pessoa);
        }
        #endregion

        #region Produção
        public FichaTecnicaJeans ObtenhaFichaTecnica()
        {
            var fichaTecnicaJeans = _fabricaObjetos.ObtenhaFichaTecnicaJeans();

            fichaTecnicaJeans.Classificacao = ObtenhaClassificacao();
            fichaTecnicaJeans.Colecao = ObtenhaColecao();
            fichaTecnicaJeans.Natureza = ObtenhaNatureza();
            fichaTecnicaJeans.Artigo = ObtenhaArtigo();
            fichaTecnicaJeans.Marca = ObtenhaMarca();
            fichaTecnicaJeans.Barra = ObtenhaBarra();
            fichaTecnicaJeans.Segmento = ObtenhaSegmento();
            fichaTecnicaJeans.ClassificacaoDificuldade = ObtenhaClassificacaoDificuldade();
            fichaTecnicaJeans.ProdutoBase = ObtenhaProdutoBase();
            fichaTecnicaJeans.Comprimento = ObtenhaComprimento();
            fichaTecnicaJeans.Estilista = ObtenhaFuncionario();
            
            var fichaTecnicaVariacaoMatriz = _fabricaObjetos.ObtenhaFichaTecnicaVariacaoMatriz();
            fichaTecnicaVariacaoMatriz.Variacao = ObtenhaVariacao();
            fichaTecnicaVariacaoMatriz.AddCor(ObtenhaCor());

            fichaTecnicaJeans.FichaTecnicaMatriz =  _fabricaObjetos.ObtenhaFichaTecnicaMatriz();
            fichaTecnicaJeans.FichaTecnicaMatriz.Grade = ObtenhaGrade();
            fichaTecnicaJeans.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.Add(fichaTecnicaVariacaoMatriz);

            //fichaTecnicaJeans.FichaTecnicaSequenciaOperacionals.Add(_fichaTecnicaSequenciaOperacional);
            //fichaTecnicaJeans.MateriaisComposicaoCusto.Add(_materialComposicaoCusto);
            //fichaTecnicaJeans.MateriaisConsumo.Add(_materialConsumo);
            
            RepositoryFactory.Create<FichaTecnicaJeans>().Save(fichaTecnicaJeans);

            return fichaTecnicaJeans;
        }

        public void ExcluaFichaTecnicaJeans(FichaTecnicaJeans fichaTecnicaJeans)
        {
            RepositoryFactory.Create<FichaTecnicaJeans>().Delete(fichaTecnicaJeans);
            
            ExcluaArtigo(fichaTecnicaJeans.Artigo);
            ExcluaClassificacao(fichaTecnicaJeans.Classificacao);
            ExcluaClassificacaoDificuldade(fichaTecnicaJeans.ClassificacaoDificuldade);
            ExcluaBarra(fichaTecnicaJeans.Barra);
            ExcluaColecao(fichaTecnicaJeans.Colecao);
            ExcluaPessoa(fichaTecnicaJeans.Estilista);
            ExcluaMarca(fichaTecnicaJeans.Marca);
            ExcluaNatureza(fichaTecnicaJeans.Natureza);
            ExcluaBarra(fichaTecnicaJeans.Barra);
            ExcluaProdutoBase(fichaTecnicaJeans.ProdutoBase);
            ExcluaComprimento(fichaTecnicaJeans.Comprimento);
            ExcluaSegmento(fichaTecnicaJeans.Segmento);
            ExcluaGrade(fichaTecnicaJeans.FichaTecnicaMatriz.Grade);
            ExcluaCor(fichaTecnicaJeans.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs[0].Cores.First());
            ExcluaVariacao(fichaTecnicaJeans.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs[0].Variacao);
        }
        #endregion
    }
}