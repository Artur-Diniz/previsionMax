using EasyAutomationFramework;
using javax.lang.model.util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PrevisionMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Driver
{
    public class ObterUltimos_jogos:Web
    {

        public ObterUltimos_jogos(string url)
        {
            string path = @"C:\Users\artur\Desktop\Projetos\PrevisionMax\PrevisionMax\PrevisionMax\Images\";

            if (driver == null)
                StartBrowser();

            Navigate(url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cookieButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#onetrust-accept-btn-handler")));
            cookieButton.Click();

            Partidas partida = new Partidas();

            partida.NomeTimeCasa = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[4]/div[2]/div[3]/div[2]/a")).Text;
            partida.NomeTimeFora = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[4]/div[4]/div[3]/div[1]/a")).Text;
            partida.data = DateTime.Today;
            string nome = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[3]/div/span[3]/a")).Text;
            var nomepart = nome.Split(" - ");
            partida.Campeonato = string.Format(nomepart[0].Trim());
            partida.PartidaAnalise = true;




            driver.FindElement(By.CssSelector("#detail > div.detailOver > div > a:nth-child(3) > button")).Click();


            var items = new List<string>();

            var ultimoscasa = new List<Partidas>();
            var ultimosfora = new List<Partidas>();
            var confrontoDireto = new List<Partidas>();
            var casacasa = new List<Partidas>();
            var forafora = new List<Partidas>();

            IKeyboard teclado = ((IHasInputDevices)driver).Keyboard;
            Actions actions = new Actions(driver);

            int contador = 0;
            int count = 0;

            IWebElement botaocasa = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#detail > div.h2hSection > div.filterOver.filterOver--indent > div > a:nth-child(2) > button")));

            IWebElement botaofora = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#detail > div.h2hSection > div.filterOver.filterOver--indent > div > a:nth-child(3) > button")));

            for (int i = 0; i <= 3; i++)
            {
                var h2h = GetValue(TypeElement.CssSelector, "#detail > div.h2hSection")
                .element.FindElements(By.ClassName("rows"));
                contador += 1;

                if (contador > 3 && contador<8)
                {

                    count = 0;
                    if (contador == 4)  
                    {        
                        actions.SendKeys(Keys.PageUp).Perform();                        
                        botaocasa.Click();
                    }
                    else
                    {
                        actions.SendKeys(Keys.PageUp).Perform();
                        botaofora.Click();
                    }
                }
                contador += 2;

                int voltas = 0;
                foreach (var element in h2h)
                {
                    count++;

                    if (count >= 2 && contador >=4)
                        break;

                    string cssSelector = string.Format($"#detail > div.h2hSection > div.h2h > div:nth-child({count}) > div.rows");
                    var historico = GetValue(TypeElement.CssSelector, cssSelector)
                        .element.FindElements(By.ClassName("h2h__row "));


                    string originalWindow = driver.CurrentWindowHandle;
                 
                    foreach (var link in historico)
                    {
                        voltas = voltas+1;
                        if(voltas<11)
                        {
                            DateTime datajogo = DateTime.Parse(link.FindElement(By.ClassName("h2h__date")).Text);
                            DateTime hoje = DateTime.Now;
                            DateTime MargemData = hoje.AddMonths(-3);

                            if (datajogo >= MargemData)
                                link.Click();
                            else
                                break;
                        }
                        else
                        link.Click();

                        // Espera até que uma nova aba seja aberta
                        wait.Until(driver => driver.WindowHandles.Count > 1);

                        // Alterna para a nova aba
                        foreach (string window in driver.WindowHandles)
                        {
                            if (window != originalWindow)
                            {
                                driver.SwitchTo().Window(window);
                                break;
                            }
                        }


                        string currentUrl = driver.Url;                        
                        items.Add(currentUrl);


                        Partidas partidaAnterior = new Partidas();

                        partidaAnterior.NomeTimeCasa = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[4]/div[2]/div[3]/div[2]/a")).Text;
                        partidaAnterior.NomeTimeFora = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[4]/div[4]/div[3]/div[1]/a")).Text;
                        partidaAnterior.data = DateTime.Today;
                        string name = driver.FindElement(By.XPath("//*[@id=\"detail\"]/div[3]/div/span[3]/a")).Text;
                        var part = name.Split(" - ");
                        partidaAnterior.Campeonato = string.Format(part[0].Trim());
                        partidaAnterior.PartidaAnalise = false;

                        if (contador == 3 )
                            if( count == 1)
                            ultimoscasa.Add(partidaAnterior);
                            else if(count == 2)
                            ultimosfora.Add(partidaAnterior);
                            else 
                            confrontoDireto.Add(partidaAnterior);
                        else if (contador == 6)
                            casacasa.Add(partidaAnterior);
                        else if (contador == 9)
                            forafora.Add(partidaAnterior);


                        driver.Close();
                        driver.SwitchTo().Window(originalWindow);
                    }

                    foreach (var item in items)
                    {
                        Console.WriteLine("URL capturada: " + item);
                    }
                }

                Console.WriteLine("Links encontrados:");
                foreach (var item in items)
                {


                    Console.WriteLine(item);
                }
            }

            //parte reservada para anotar todas as estátiscas do jogo um por um 

            driver.Quit();
        }


    }
}
