﻿using fiiCrawlerApi.Models;
using PuppeteerSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fiiCrawlerApi.WebScraper
{
    /// <summary>
    /// Classe feita para tratar questões de 
    /// Web Scraping da aplicação.
    /// 
    /// Com o Web Scraping, obtemos informações específicas
    /// dentro da página.
    /// </summary>
    public class Scraper
    {
        #region Variaveis Privadas
        private IBrowser navegador;
        #endregion

        #region Construtor
        public Scraper(ref IBrowser _navegador)
        {
            navegador = _navegador;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Web Scraping do detalhamento de um FII's        
        /// </summary>
        public async Task<FIIDetalhado> ScrapelDadosFII(string codigoFii, IPage paginaWeb)
        {
            /*
           * Neste ponto temos o início do scraping da página.
           * 
           * O processo é feito para extrair informações específicas da página,
           * neste contexto os dados de cada célula da tabela de Fii é lido
           * e utilizado para formar um objeto dentro da aplicação
           * 
           * Estrutura base do elemento HTML            
           */

            try
            {
                #region Scraping - Detalhamento FII
                string jsSeletorNomeFII =
                    @"document.getElementsByClassName('headerTicker__content')[0]?"
                    + @".getElementsByClassName('headerTicker__content__name')[0]?"
                    + @".getElementsByTagName('p')[0]?"
                    + @".innerText;";
                var nomeFii = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorNomeFII);

                string jsSeletorCotacao =
                    @"document.getElementsByClassName('quotations-details')[0]?"
                    + @".getElementsByClassName('item quotation')[0]?"
                    + @".getElementsByTagName('div')[0]?"
                    + @".getElementsByClassName('value')[0]?"
                    + @".innerText";
                var cotacao = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorCotacao);

                string jsSeletorVariacao =
                    @"document.getElementsByClassName('quotations-details')[0]?"
                    + @".getElementsByClassName('item quotation')[0]"
                    + @".getElementsByTagName('div')[0]?"
                    + @".getElementsByClassName('change')[0]?"
                    + @".getElementsByTagName('span')[0]?"
                    + @".innerText";
                var variacao = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorVariacao);

                string jsSeletorSinalVariacao =
                    @"document.getElementsByClassName('quotations-details')[0]?"
                    + @".getElementsByClassName('item quotation')[0]"
                    + @".getElementsByTagName('div')[0]?"
                    + @".getElementsByClassName('change')[0]?"
                    + @".getElementsByTagName('span')[0]?"
                    + @".getAttribute('class')";
                var sinalVariacao = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorSinalVariacao);
                #endregion

                #region Scraping - Dividendos
                string jsSeletorDividendoDataBase =
                       @"Array.from("
                        + @"document.getElementsByClassName('yieldChart__table__body')[0]?"
                        + @".getElementsByClassName('yieldChart__table__bloco'))?"
                    + @".map( div => div.children[0].innerText)";
                var dividendo_dataBase = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSeletorDividendoDataBase);

                string jsSeletorDividendoDataPagamento =
                       @"Array.from("
                        + @"document.getElementsByClassName('yieldChart__table__body')[0]?"
                        + @".getElementsByClassName('yieldChart__table__bloco'))?"
                    + @".map( div => div.children[1].innerText)";
                var dividendo_dataPagamento = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSeletorDividendoDataPagamento);

                string jsSeletorDividendoCotacaoBase =
                       @"Array.from("
                        + @"document.getElementsByClassName('yieldChart__table__body')[0]?"
                        + @".getElementsByClassName('yieldChart__table__bloco'))?"
                    + @".map( div => div.children[2].innerText)";
                var dividendo_cotacaoBase = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSeletorDividendoCotacaoBase);

                string jsSeletorDividendoYeild =
                       @"Array.from("
                        + @"document.getElementsByClassName('yieldChart__table__body')[0]?"
                        + @".getElementsByClassName('yieldChart__table__bloco'))?"
                    + @".map( div => div.children[3].innerText)";
                var dividendo_yeild = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSeletorDividendoYeild);

                string jsSeletorDividendRedimento =
                    // Array.from(document.getElementsByClassName('yieldChart__table__body')[0].getElementsByClassName('yieldChart__table__bloco')).map( div => div.children[4].innerText)
                    @"Array.from("
                        + @"document.getElementsByClassName('yieldChart__table__body')[0]?"
                        + @".getElementsByClassName('yieldChart__table__bloco'))?"
                    + @".map( div => div.children[4].innerText)";
                var dividendo_rendimento = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSeletorDividendRedimento);
                #endregion

                #region Scraping - Informações do Admnistrador 
                string jsSeletorAdministradorRazaoSocial = @"document.getElementsByClassName('informations__adm__name')[0]?.innerText";
                var adm_razaoSocial = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorRazaoSocial);

                string jsSeletorAdministradorTelefone = @"document.getElementsByClassName('informations__contact__tel')[0].getElementsByTagName('p')[0].innerText";
                var adm_telefone = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorTelefone);

                string jsSeletorAdministradorEmail = @"document.getElementsByClassName('informations__contact__email')[0].getElementsByTagName('p')[0].innerText";
                var adm_email = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorEmail);

                string jsSeletorAdministradorSite = @"document.getElementsByClassName('informations__contact__site')[0].getElementsByTagName('p')[0].innerText";
                var adm_site = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorSite);

                string jsSeletorAdministradorCNPJ = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[0].getElementsByTagName('b')[0].innerText";
                var adm_cnpj = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorCNPJ);

                string jsSeletorAdministradorNomePregao = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[1].getElementsByTagName('b')[0].innerText";
                var adm_nomePregao = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorNomePregao);

                string jsSeletorAdministradorNumeroCotas = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[2].getElementsByTagName('b')[0].innerText";
                var adm_numeroCotas = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorNumeroCotas);

                string jsSeletorAdministradorPatrimonio = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[3].getElementsByTagName('b')[0].innerText";
                var adm_patrimonio = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorPatrimonio);

                string jsSeletorAdministradorSegmento = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[6].getElementsByTagName('b')[0].innerText";
                var adm_segmento = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorSegmento);

                string jsSeletorAdministradorTipoGestao = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[7].getElementsByTagName('b')[0].innerText";
                var adm_tipoGestao = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorTipoGestao);

                string jsSeletorAdministradorPublicoAlvo = @"document.getElementsByClassName('moreInfo wrapper')[0].getElementsByTagName('p')[8].getElementsByTagName('b')[0].innerText";
                var adm_publicoAlvo = await paginaWeb.EvaluateExpressionAsync<string>(jsSeletorAdministradorPublicoAlvo);

                #endregion

                var fii = new FIIDetalhado();
                fii.codigoFii = codigoFii;
                fii.nomeCompleto = nomeFii;
                fii.cota = cotacao;
                fii.variacao = sinalVariacao.ToLower() == "down" ? $"-{variacao}" : $"+{variacao}";

                // dividendos
                fii.historicoDividendos = new List<Dividendo>();
                for (int i = 0; i < dividendo_rendimento.Length; ++i)
                    fii.historicoDividendos.Add(
                        new Dividendo
                        {
                            dataBase = dividendo_dataBase[i],
                            dataPagamento = dividendo_dataPagamento[i],
                            cotacaoBase = dividendo_cotacaoBase[i],
                            dividendoYeild = dividendo_yeild[i],
                            rendimento = dividendo_rendimento[i],
                        }
                    );

                // administrador
                fii.administrador = new Administrador();
                fii.administrador.nomeNoPregao = adm_nomePregao;
                fii.administrador.numeroDeCotas = adm_numeroCotas;
                fii.administrador.patrimonio = adm_patrimonio;
                fii.administrador.publicoAlvo = adm_publicoAlvo;
                fii.administrador.segmento = adm_segmento;
                fii.administrador.site = adm_site;
                fii.administrador.telefone = adm_telefone;
                fii.administrador.email = adm_email;
                fii.administrador.tipoGestao = adm_tipoGestao;
                fii.administrador.cnpj = adm_cnpj;

                // fechar o browser toda vez que finalizar
                // o processo de scraping & crawling                                      
                await paginaWeb.CloseAsync();
                await this.navegador.CloseAsync();
                System.Console.WriteLine("Scraped!");

                return fii;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                // garantir que a instância do navegador criada seja fechada
                // Esse processo providencia um ciclo de vida melhor para a aplicação
                // e uso de memória no ambiente em que a api for publicada
                if (this.navegador != null && !this.navegador.IsClosed)
                    await this.navegador.CloseAsync();             
            }
        }

        /// <summary>
        /// Web Scraping da lista de FII's        
        /// </summary>
        public async Task<List<FII>> ScrapeDadosListaFii(IPage paginaWeb)
        {
            /*
            * Neste ponto temos o início do scraping da página.
            * 
            * O processo é feito para extrair informações específicas da página,
            * neste contexto os dados de cada célula da tabela de Fii é lido
            * e utilizado para formar um objeto dentro da aplicação
            * 
            * Estrutura base do elemento HTML            
            */
            try
            {
                #region Scraping Lista de FII
                string jsSelecionarTodosOsNomes = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[0]?.innerText);";
                var nomesFii = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosOsNomes);

                string jsSelecionarTodosOsUltimosRendimentosRs = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[1]?.innerText);";
                var ultimosRendimentosRs = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosOsUltimosRendimentosRs);

                string jsSelecionarTodosOsUltimosRendimentos = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[2]?.innerText);";
                var ultimosRendimentos = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosOsUltimosRendimentos);

                string jsSelecionarTodosAsDatasDePagamento = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[3]?.innerText);";
                var datasDePagamento = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosAsDatasDePagamento);

                string jsSelecionarTodosAsDatasBase = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[4]?.innerText);";
                var datasBase = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosAsDatasBase);

                string jsSelecionarTodosOsRedimentoMedioAnual = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[5]?.innerText);";
                var rendimentoMedioAnual = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosOsRedimentoMedioAnual);

                string jsSelecionarTodosOsPatrimonios = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[6]?.innerText);";
                var patrimonios = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodosOsPatrimonios);

                string jsSelecionarTodasAsCotas = @"Array.from(document.querySelectorAll('tr')).map(tr => tr.getElementsByTagName('td')[7]?.innerText);";
                var cotas = await paginaWeb.EvaluateExpressionAsync<string[]>(jsSelecionarTodasAsCotas);
                #endregion                

                // Criando novos objetos DTO com base nos dados recebidos.
                List<FII> fiis = new List<FII>();
                if (nomesFii.Length > 0)
                {
                    // a primeira linha é o nome das colunas da tabela,
                    // como este dado não será utilizado o mesmo pode ser ignorado
                    for (int i = 1; i < ultimosRendimentos.Length; ++i)
                        fiis.Add(
                            new FII
                            {
                                nome = nomesFii[i],
                                ultimoRedimentoRS = ultimosRendimentosRs[i],
                                ultimosRedimento = ultimosRendimentos[i],
                                dataPagamento = datasDePagamento[i],
                                dataBase = datasBase[i],
                                rendimentoMedioAnual = rendimentoMedioAnual[i],
                                patrimonio = patrimonios[i],
                                cota = cotas[i]
                            }
                        );
                }

                // fechar o browser toda vez que finalizar
                // o processo de scraping & crawling
                await paginaWeb.CloseAsync();
                await this.navegador.CloseAsync();
                System.Console.WriteLine("Scraped!");

                return fiis;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                // garantir que a instância do navegador criada seja fechada
                // Esse processo providencia um ciclo de vida melhor para a aplicação
                // e uso de memória no ambiente em que a api for publicada
                if (this.navegador != null && !this.navegador.IsClosed)
                    await this.navegador.CloseAsync();                
            }
        }
    }
    #endregion
}
