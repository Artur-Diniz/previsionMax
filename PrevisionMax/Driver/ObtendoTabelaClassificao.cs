using EasyAutomationFramework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sun.awt.image.ImageWatched;
using OpenQA.Selenium.Interactions;
using PrevisionMax.Models;

namespace PrevisionMax.Driver
{
    public class ObtendoTabelaClassificao:Web
    {
        public ObtendoTabelaClassificao(string URl)
        {

            if (driver == null)
                StartBrowser();

            Navigate(URl);

            IKeyboard teclado = ((IHasInputDevices)driver).Keyboard;
            Actions actions = new Actions(driver);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cookieButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#onetrust-accept-btn-handler")));
            cookieButton.Click();

            for (int i = 0; i < 3; i++)
            {
                actions.SendKeys(Keys.ArrowDown).Perform();

            }         

            driver.FindElement(By.CssSelector("#li4")).Click();

            actions.SendKeys(Keys.PageDown).Perform();


            var TimesTotal = GetValue(TypeElement.CssSelector, "#tournament-table-tabs-and-content > div:nth-child(3) > div:nth-child(1) > div > div")
             .element.FindElements(By.ClassName("ui-table__row"));

            List<Classificacao> TabelaTotal = new List<Classificacao>();

            int poisicao = 0;
            foreach (var elem in TimesTotal)
            {
                Classificacao classificacaoTotal = new Classificacao();


                poisicao += 1;
                classificacaoTotal.PosicaoTime = poisicao;
                classificacaoTotal.NomeTime = elem.FindElement(By.ClassName("tableCellParticipant__name")).Text;
                classificacaoTotal.numJogos = int.Parse(elem.FindElement(By.CssSelector("span:nth-child(3)")).Text);
                classificacaoTotal.numVitorias = int.Parse(elem.FindElement(By.CssSelector("span:nth-child(4)")).Text);
                classificacaoTotal.numEmpates = int.Parse(elem.FindElement(By.CssSelector("span:nth-child(5)")).Text);
                classificacaoTotal.numDerrotas = int.Parse(elem.FindElement(By.CssSelector("span:nth-child(6)")).Text);
                classificacaoTotal.Pontos = int.Parse(elem.FindElement(By.CssSelector("span:nth-child(9)")).Text);
 
                for (int i = 1; i <= 6; i++)
                {               
                    try
                    {
                        var selector = $"div.table__cell.table__cell--form > div:nth-child({i})";
                        var element = elem.FindElement(By.CssSelector(selector));
                        if (element != null && !string.IsNullOrEmpty(element.Text))
                        {
                            switch (i)
                            {
                                case 2:
                                    classificacaoTotal.ultiomojogos1 = element.Text;
                                    break;
                                case 3:
                                    classificacaoTotal.ultiomojogos2 = element.Text;
                                    break;
                                case 4:
                                    classificacaoTotal.ultiomojogos3 = element.Text;
                                    break;
                                case 5:
                                    classificacaoTotal.ultiomojogos4 = element.Text;
                                    break;
                                case 6:
                                    classificacaoTotal.ultiomojogos5 = element.Text;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch { }
                }
                
                
               
                var golsText = elem.FindElement(By.ClassName("table__cell--score")).Text;
                var golsParts = golsText.Split(':');
                if (golsParts.Length == 2)
                {
                    classificacaoTotal.GolsFeitos = int.Parse(golsParts[0].Trim());
                    classificacaoTotal.GolsSofridos = int.Parse(golsParts[1].Trim());
                } 
                TabelaTotal.Add(classificacaoTotal);
                if (TabelaTotal.Count >= 1 && classificacaoTotal.Pontos <= 1)
                {
                    break; 
                }
                   
            }

            driver.Quit();

        }
    }
}
