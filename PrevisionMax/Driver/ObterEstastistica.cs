﻿using EasyAutomationFramework;
using java.sql;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using PrevisionMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Driver
{
    public class ObterEstastistica: Web
    {
        public ObterEstastistica(string link, string tipoPartida)
        {try
            {


                if (driver == null)
                    StartBrowser();

                Navigate(link);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement cookieButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#onetrust-accept-btn-handler")));
                cookieButton.Click();

                IWebElement estatisticaButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#detail > div.filterOver.filterOver--indent > div > a:nth-child(2) > button")));
                estatisticaButton.Click();



                var items = new List<string>();

                IKeyboard teclado = ((IHasInputDevices)driver).Keyboard;
                Actions actions = new Actions(driver);
                EstatisticaTimesCasa casa = new EstatisticaTimesCasa();
                EstatisticaTimesFora fora = new EstatisticaTimesFora();

                IWebElement esperar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#detail > div.subFilterOver.subFilterOver--indent.subFilterOver--radius > div > a.active > button")));
                esperar.Click();





                casa.NomeTimeCasa = driver.FindElement(By.CssSelector
                    ("#detail > div.duelParticipant > div.duelParticipant__home > div.participant__participantNameWrapper > " +
                    "div.participant__participantName.participant__overflow > a")).Text;

                fora.NomeTimeFora = driver.FindElement(By.CssSelector
                    ("#detail > div.duelParticipant > div.duelParticipant__away > div.participant__participantNameWrapper > " +
                    "div.participant__participantName.participant__overflow > a")).Text;

                casa.GolsCasa = int.Parse(driver.FindElement(By.CssSelector("#detail > div.duelParticipant > div.duelParticipant__score > div > " +
                    "div.detailScore__wrapper > span:nth-child(1)")).Text);

                fora.GolsFora = int.Parse(driver.FindElement(By.CssSelector("#detail > div.duelParticipant > div.duelParticipant__score > " +
                    "div > div.detailScore__wrapper > span:nth-child(3)")).Text);

                string penaltis = string.Empty;
                try
                {
                    penaltis = driver.FindElement(By.CssSelector("#detail > div.duelParticipant > " +
                    "div.duelParticipant__score > div > div.detailScore__fullTime >" +
                    " span.detailScore__divider")).Text;
                }
                catch { }

                if (penaltis != "")
                {
                    if (casa.GolsCasa > fora.GolsFora)
                        casa.GolsCasa = casa.GolsCasa - 1;
                    else
                        fora.GolsFora = fora.GolsFora - 1;
                }

                casa.AdversarioFora = fora.NomeTimeFora;
                casa.GolsSofridosCasa = fora.GolsFora;
                fora.AdversarioCasa = casa.NomeTimeCasa;
                fora.GolsSofridosFora = casa.GolsCasa;

                Partidas partida = new Partidas();

                partida.NomeTimeFora = fora.NomeTimeFora;
                partida.NomeTimeCasa = casa.NomeTimeCasa;
                partida.data = DateTime.Parse(driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[4]/div[1]/div")).Text);
                string nome = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[3]/div/span[3]/a")).Text;
                var nomepart = nome.Split(" - ");
                partida.Campeonato = string.Format(nomepart[0].Trim());
                if (nomepart[1].Trim() == "PLAYOFFS")
                { partida.Campeonato = partida.Campeonato + " - PLAYOFFS"; }
                partida.TipoPartida = tipoPartida;

                if (tipoPartida == "PartidaAnalise")
                    partida.PartidaAnalise = true;
                else
                    partida.PartidaAnalise = false;

                int count = 0;

                IWebElement espera = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#detail > div.subFilterOver.subFilterOver--indent.subFilterOver--radius > div > a.active > button")));
                espera.Click();
                var linhas = GetValue(TypeElement.CssSelector, "#detail > div:nth-child(9)")
                  .element.FindElements(By.ClassName("_row_1nw75_8"));
                int jogodeIda = 9;
                if (linhas.Count == 0)
                {
                    linhas = GetValue(TypeElement.CssSelector, "#detail > div:nth-child(10)")
                                  .element.FindElements(By.ClassName("_row_1nw75_8"));
                    jogodeIda = 10;
                }

                foreach (var element in linhas)
                {
                    count += 1;
                    string item = element.FindElement(By.ClassName("_category_1ague_4")).Text;


                    switch (item)
                    {
                        case "Tentativas de Gol":
                            int TentativasGolsCasa;
                            int TentativasGolsfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out TentativasGolsCasa))
                            {
                                casa.TentativasGolsCasa = TentativasGolsCasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out TentativasGolsfora))
                            {
                                fora.TentativasGolsFora = TentativasGolsfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();
                            break;
                        case "Chutes no Gol":
                            int chutesnogolCasa, chutesnogolFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out chutesnogolCasa))
                            {
                                casa.chutesnoGolsCasa = chutesnogolCasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out chutesnogolFora))
                            {
                                fora.chutesnoGolsFora = chutesnogolFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Chutes para Fora":
                            int ChutesparaForacasa, ChutesparaForaFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out ChutesparaForacasa))
                            {
                                casa.chutespraforaCasa = ChutesparaForacasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out ChutesparaForaFora))
                            {
                                fora.chutespraforaFora = ChutesparaForaFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;

                        case "Escanteios":
                            int EscanteiosCasa, EscanteiosFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out EscanteiosCasa))
                            {
                                casa.escanteiosCasa = EscanteiosCasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out EscanteiosFora))
                            {
                                fora.escanteiosFora = EscanteiosFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;

                        case "Impedimentos":
                            int Impedimentoscasa, ImpedimentosFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out Impedimentoscasa))
                            {
                                casa.InpedimentosCasa = Impedimentoscasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out ImpedimentosFora))
                            {
                                fora.InpedimentosFora = ImpedimentosFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Defesas do Goleiro":
                            int defesasdogoleiroCasa, defesasdogoleiroFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out defesasdogoleiroCasa))
                            {
                                casa.DefesaGoleiroCasa = defesasdogoleiroCasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out defesasdogoleiroFora))
                            {
                                fora.DefesaGoleiroFora = defesasdogoleiroFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Faltas":
                            int Faltascasa, Faltasfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out Faltascasa))
                            {
                                casa.FaltasCasas = Faltascasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out Faltasfora))
                            {
                                fora.FaltasForas = Faltasfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Cartões Vermelhos":
                            int Cartoesvermelhoscasa, Cartoesvermelhosfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out Cartoesvermelhoscasa))
                            {
                                casa.CartoesVermelhosCasa = Cartoesvermelhoscasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out Cartoesvermelhosfora))
                            {
                                fora.CartoesVermelhosFora = Cartoesvermelhosfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Cartões Amarelos":
                            int Cartoesamarelosscasa, CartoesamarelossFora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out Cartoesamarelosscasa))
                            {
                                casa.CartoesAmareloCasa = Cartoesamarelosscasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out CartoesamarelossFora))
                            {
                                fora.CartoesAmareloFora = CartoesamarelossFora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Passes Totais":
                            int PassesTotaiscasa, PassesTotaisfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out PassesTotaiscasa))
                            {
                                casa.PassesTotaisCasa = PassesTotaiscasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out PassesTotaisfora))
                            {
                                fora.PassesTotaisFora = PassesTotaisfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Passes Completados":
                            int PassesCompletadosCasa, PassesCompletadosfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out PassesCompletadosCasa))
                            {
                                casa.PassesCompletosCasa = PassesCompletadosCasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out PassesCompletadosfora))
                            {
                                fora.PassesCompletosFora = PassesCompletadosfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                        case "Ataques Perigosos":
                            int AtaquesPerigososcasa, AtaquesPerigososfora;
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._homeValue_1jbkc_9")).Text, out AtaquesPerigososcasa))
                            {
                                casa.AtaquesperigososCasa = AtaquesPerigososcasa;
                            }
                            if (int.TryParse(element.FindElement(By.CssSelector
                                ($"#detail > div:nth-child({jogodeIda}) > div:nth-child({count}) > div._category_1nw75_15 > div._value_1jbkc_4._awayValue_1jbkc_13")).Text, out AtaquesPerigososfora))
                            {
                                fora.AtaquesperigososFora = AtaquesPerigososfora;
                            }
                            actions.SendKeys(Keys.ArrowDown).Perform();

                            break;
                    }
                }

                var partidaComEstatisticaDTO = new PartidaComEstatisticaDTO
                {
                    Partida = partida,
                    Casa = casa,
                    Fora = fora
                };

                Transicao.EnviarPartida.EnviarDadosAsync(partidaComEstatisticaDTO);

                driver.Quit();
            }
            catch { driver.Quit(); }
        }
    }
}
