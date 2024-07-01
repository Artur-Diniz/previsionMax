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

            driver.FindElement(By.CssSelector("#detail > div.detailOver > div > a:nth-child(3) > button")).Click();




            var items = new List<string>();

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

                        driver.Close();
                        driver.SwitchTo().Window(originalWindow);
                    }

                    // Imprime as URLs capturadas
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
