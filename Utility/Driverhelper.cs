using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suren_nunit.Utility
{
    public class Driverhelper
    {
        public static IWebDriver driver { get; set; }

        public IWebDriver Init(IWebDriver driver)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-type");
            options.AddArguments("start-maximized");
            options.AddArguments("--enable-automation");
            options.AddArguments("test-type=browser");
            options.AddArguments("--disable-infobars");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--no-proxy-server");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            return driver;
        }
    }


}
