using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Suren_nunit.Utility
{
    class Custommethods
    {
        public static string TakeScreenshot(IWebDriver driver, string screenShotName)
        {
            string parentdir = Environment.CurrentDirectory.ToString();
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot ss = ts.GetScreenshot();
            var filePath = @parentdir + "\\Logs\\" + screenShotName + ".png";
            ss.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            return filePath;
        }
        public static void EnterText(IWebDriver driver, string testname, string element, string value, string elementtype)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            if (elementtype == "Id")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(element)))).SendKeys(value);
            if (elementtype == "Name")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name(element)))).SendKeys(value);
            if (elementtype == "XPath")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(element)))).SendKeys(value);

        }

        public static void Click(IWebDriver driver, string testname, string element, string elementtype)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            if (elementtype == "Id")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(element)))).Click();
            if (elementtype == "Name")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name(element)))).Click();
            if (elementtype == "XPath")
                (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(element)))).Click();

        }

        public static void Selectdropdown(IWebDriver driver, string testname, string element, string value, string elementtype)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            if (elementtype == "Id")
                new SelectElement(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(element)))).SelectByText(value);
            if (elementtype == "Name")
                new SelectElement(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name(element)))).SelectByText(value);
            if (elementtype == "XPath")
                new SelectElement(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(element)))).SelectByText(value);

        }


        public static string Gettext(IWebDriver driver, string testname, string element, string elementtype)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            if (elementtype == "Id")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(element))).Text;
            if (elementtype == "Name")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(element))).Text;
            if (elementtype == "XPath")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(element))).Text;
            else return String.Empty;
        }

        public static string Getattribute (IWebDriver driver, string testname, string element, string value, string elementtype)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            if (elementtype == "Id")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(element))).GetAttribute(value);
            if (elementtype == "Name")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(element))).GetAttribute(value);
            if (elementtype == "XPath")
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(element))).GetAttribute(value);
            else return String.Empty;
        }



    }
}
