using EasyAutomationFramework;
using javax.lang.model.util;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
            string path = @"C:\Users\artur\Desktop\Projetos\PrevisionMax\PrevisionMax\PrevisionMax\Images\";

            if (driver == null)
                StartBrowser();
            Navigate("https://www.flashscore.com.br/");

            var items = new List<string>();


            IKeyboard teclado = ((IHasInputDevices)driver).Keyboard;
            Actions actions = new Actions(driver);


            
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
            driver.Quit();
        }        
    } 
}
