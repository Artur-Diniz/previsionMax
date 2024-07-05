using EasyAutomationFramework;
using javax.lang.model.util;
using OpenQA.Selenium;
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
    public class Teste : Web 
    {

        public IWebDriver drive;
        public Teste() 
        {

            if (driver == null)
                StartBrowser();
            Navigate("https://www.flashscore.com.br/");

            var items = new List<string>();


            IKeyboard teclado = ((IHasInputDevices)driver).Keyboard;
            Actions actions = new Actions(driver);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement cookieButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#onetrust-accept-btn-handler")));
            cookieButton.Click();


            var jogos_dia = GetValue(TypeElement.Xpath, "//*[@id=\"live-table\"]/section/div/div[1]")
                .element.FindElements(By.CssSelector(".eventRowLink"));

            foreach (var element in jogos_dia)
            {
                string item = element.GetAttribute("href");
                items.Add(item);
            }

            Console.WriteLine("Links encontrados:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            actions.SendKeys(Keys.PageDown).Perform();
            driver.FindElement(By.XPath("//*[@id=\"category-left-menu\"]/div/div[4]")).Click();
            actions.SendKeys(Keys.ArrowDown).Perform();

            IWebElement BundesLiga = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"category-left-menu\"]/div/div[4]/span[1]/span")));
            BundesLiga.Click();


            actions.SendKeys(Keys.PageUp).Perform();

            var Ligas = new List<string>();


            var ligas = GetValue(TypeElement.CssSelector, "#my-leagues-list")
            .element.FindElements(By.ClassName("leftMenu__href"));

            foreach (var element in ligas)
            {
                string item = element.GetAttribute("href");
                  
              
                var nomepart = item.Split("/");
                 string nomecamp = string.Format(nomepart[5].Trim());

                if(nomecamp== "brasileirao-betano" || nomecamp == "serie-b" ||
                    nomecamp == "laliga" || nomecamp == "ligue-1" ||
                    nomecamp == "campeonato-ingles" || nomecamp == "serie-a" ||
                    nomecamp == "bundesliga")
                Ligas.Add(item);
            }
            driver.Quit();

            foreach (var element in Ligas)
            {
                var tabela = new ObtendoTabelaClassificao(element);
            }

        }        
    } 
}
