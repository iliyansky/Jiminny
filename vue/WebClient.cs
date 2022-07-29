using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace vue
{
    public class WebClient
    {
        public const string url = "https://todomvc.com/examples/vue/#/all";

        public static IWebDriver Driver = new ChromeDriver(@"C:\Users\ilbo\source\repos\vue\vue\drivers");

        public ToDo_WebPage ToDo_WebPage => new ToDo_WebPage();

        public WebClient()
        {
            
        }
        
        public WebClient StartAndNavigateToPage(string url)
        {
            Driver.Url = url;
            Driver.Navigate();
            Maximize();
            return this;
        }

        public virtual void CloseBrowser()
        {
            Driver.Quit();
            Driver = null;
        }

        public virtual void Maximize()
        {
            Driver.Manage().Window.Maximize();
        }

        public virtual void Resfresh()
        {
            Driver.Navigate().Refresh();
        }
    }
}
