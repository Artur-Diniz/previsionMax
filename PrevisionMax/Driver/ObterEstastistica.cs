using EasyAutomationFramework;
using java.sql;
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
        public ObterEstastistica(string link)
        {
            //"#detail > div.filterOver.filterOver--indent > div > a:nth-child(2) > button"

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


            var linhas  = GetValue(TypeElement.CssSelector, "#detail > div:nth-child(9)")
               .element.FindElements(By.ClassName("_row_bn1w5_8"));


            casa.NomeTimeCasa = driver.FindElement(By.CssSelector
                ( "#detail > div.duelParticipant > div.duelParticipant__home.duelParticipant--winner >" +
                " div.participant__participantNameWrapper > div.participant__participantName.participant__overflow > a")).Text;

            fora.NomeTimeFora = driver.FindElement(By.CssSelector
                ("#detail > div.duelParticipant > div.duelParticipant__away > div.participant__participantNameWrapper " +
                "> div.participant__participantName.participant__overflow > a")).Text;

            casa.GolsCasa = int.Parse(driver.FindElement(By.CssSelector("#detail > div.duelParticipant > div.duelParticipant__score >" +
                " div > div.detailScore__wrapper > span:nth-child(1)")).Text);

            fora.GolsFora = int.Parse(driver.FindElement(By.CssSelector("#detail > div.duelParticipant > div.duelParticipant__score " +
                "> div > div.detailScore__wrapper > span:nth-child(3)")).Text);
           
            
            int count = 0;

            foreach (var element in linhas)
            {
                count += 1;
                string item = element.FindElement(By.ClassName("_category_hyte3_4")).Text;
                

                switch (item)
                {
                    case "Tentativas de Gol":
                        int TentativasGolsCasa;
                        int TentativasGolsfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out TentativasGolsCasa))
                        {
                            casa.TentativasGolsCasa = TentativasGolsCasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out TentativasGolsfora))
                        {
                            fora.TentativasGolsFora = TentativasGolsfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();
                        break;
                    case "Chutes no Gol":
                        int chutesnogolCasa, chutesnogolFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out chutesnogolCasa))
                        {
                            casa.chutesnoGolsCasa = chutesnogolCasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out chutesnogolFora))
                        {
                            fora.chutesnoGolsFora = chutesnogolFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Chutes para Fora":
                        int ChutesparaForacasa, ChutesparaForaFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out ChutesparaForacasa))
                        {
                            casa.chutespraforaCasa = ChutesparaForacasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out ChutesparaForaFora))
                        {
                            fora.chutespraforaFora = ChutesparaForaFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;

                    case "Escanteios":
                        int EscanteiosCasa, EscanteiosFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out EscanteiosCasa))
                        {
                            casa.escanteiosCasa = EscanteiosCasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out EscanteiosFora))
                        {
                            fora.escanteiosFora = EscanteiosFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;

                    case "Impedimentos":
                        int Impedimentoscasa, ImpedimentosFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out Impedimentoscasa))
                        {
                            casa.InpedimentosCasa = Impedimentoscasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out ImpedimentosFora))
                        {
                            fora.InpedimentosFora = ImpedimentosFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Defesas do Goleiro":
                        int defesasdogoleiroCasa, defesasdogoleiroFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out defesasdogoleiroCasa))
                        {
                            casa.DefesaGoleiroCasa = defesasdogoleiroCasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out defesasdogoleiroFora))
                        {
                            fora.DefesaGoleiroFora = defesasdogoleiroFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Faltas":
                        int Faltascasa, Faltasfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out Faltascasa))
                        {
                            casa.FaltasCasas = Faltascasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out Faltasfora))
                        {
                            fora.FaltasForas = Faltasfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Cartões Vermelhos":
                        int Cartoesvermelhoscasa, Cartoesvermelhosfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out Cartoesvermelhoscasa))
                        {
                            casa.CartoesVermelhosCasa = Cartoesvermelhoscasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out Cartoesvermelhosfora))
                        {
                            fora.CartoesVermelhosFora = Cartoesvermelhosfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Cartões Amarelos":
                        int Cartoesamarelosscasa, CartoesamarelossFora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out Cartoesamarelosscasa))
                        {
                            casa.CartoesAmareloCasa = Cartoesamarelosscasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out CartoesamarelossFora))
                        {
                            fora.CartoesAmareloFora = CartoesamarelossFora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Passes Totais":
                        int PassesTotaiscasa, PassesTotaisfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out PassesTotaiscasa))
                        {
                            casa.PassesTotaisCasa = PassesTotaiscasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out PassesTotaisfora))
                        {
                            fora.PassesTotaisFora = PassesTotaisfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Passes Completados":
                        int PassesCompletadosCasa, PassesCompletadosfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out PassesCompletadosCasa))
                        {
                            casa.PassesCompletosCasa = PassesCompletadosCasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out PassesCompletadosfora))
                        {
                            fora.PassesCompletosFora = PassesCompletadosfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                    case "Ataques Perigosos":
                        int AtaquesPerigososcasa, AtaquesPerigososfora;
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._homeValue_lgd3g_9")).Text, out AtaquesPerigososcasa))
                        {
                            casa.AtaquesperigososCasa = AtaquesPerigososcasa;
                        }
                        if (int.TryParse(element.FindElement(By.CssSelector
                            ($"#detail > div:nth-child(9) > div:nth-child({count}) > div._category_bn1w5_15 > div._value_lgd3g_4._awayValue_lgd3g_13")).Text, out AtaquesPerigososfora))
                        {
                            fora.AtaquesperigososFora = AtaquesPerigososfora;
                        }
                        actions.SendKeys(Keys.ArrowDown).Perform();

                        break;
                }
            }
            driver.Quit();
        }
    }
}
