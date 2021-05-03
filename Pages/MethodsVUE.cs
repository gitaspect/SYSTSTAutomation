using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using Suren_nunit.Utility;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AventStack.ExtentReports;

namespace Suren_nunit.Page 
{
    public class MethodsVUE : Driverhelper

    {
        public static int chattab;
        public static int smstab;


        public static int Loop_Count()
        {
            int x = 1;
            return x;
        }


        public static void VUE_Login(IWebDriver driver, ExtentTest test, string testname, string username, string password, string stationid, string initialstate)
        {

            test.Info("VUE_Login");
            Custommethods.EnterText(driver, testname, "email-input", username, "Id");
            Custommethods.EnterText(driver, testname, "password-input", password, "Id");
            Custommethods.Click(driver, testname, "//button[@type='button']", "XPath");
            Custommethods.EnterText(driver, testname, "login-stationField", Keys.Control + "a", "Id");
            Custommethods.EnterText(driver, testname, "login-stationField", stationid, "Id");
            Custommethods.Click(driver, testname, "login-btnMyAspect", "Id");
            String state = initialstate;

            if (state == "IDLE")
            {
                Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");
                Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Custommethods.Click(driver, testname, "navbar-menuIdle", "Id");
            }

            else if (state == "OFFLINE")
            {
                Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");

            }
            else if (state == "NOTREADY")
            {
                Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");
                Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Actions hovernr = new Actions(driver);
                IWebElement notready = driver.FindElement(By.Id("navbar-menuNotReady"));
                hovernr.MoveToElement(notready).Perform();
                //Thread.Sleep(500);
                hovernr.Click().Build().Perform();
                Custommethods.Click(driver, testname, "//*[@id='navbar-hrefCollapseNotReady']/div/ul/li/a[1]", "XPath");
            }
            else if (state == "PARK")
            {
                Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");
                Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Custommethods.Click(driver, testname, "navbar-menuPark", "Id");
            }
            else if (state == "CONNECTED")
            {

                Custommethods.Click(driver, testname, "//*[@title='connected ']", "XPath");

            }
            else
            {
                Custommethods.TakeScreenshot(driver, DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt"));
                throw new ArgumentException("INVALID CHANGE STATE");
            }

        }

        public static void VUE_Change_State(IWebDriver driver, ExtentTest test, string testname, string cs, string rc)
        {

            test.Info("VUE_Change_State");
            if (cs == "IDLE")
            {
               Custommethods.Click(driver, testname, "navbar-menuState", "Id");
               Custommethods.Click(driver, testname, "navbar-menuIdle", "Id");
            }
            else if (cs == "NOTREADY")
            {

               Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Actions hovernr = new Actions(driver);
                IWebElement notready = driver.FindElement(By.Id("navbar-menuNotReady"));
                hovernr.MoveToElement(notready).Perform();
                hovernr.Click().Build().Perform();
               Custommethods.Click(driver, testname, "//*[@title='" + rc + "']", "XPath");
            }
            else if (cs == "PARK")
            {

               Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Actions hovernr = new Actions(driver);
                IWebElement notready = driver.FindElement(By.Id("navbar-menuPark"));
                hovernr.MoveToElement(notready).Perform();
                hovernr.Click().Build().Perform();
               Custommethods.Click(driver, testname, "//*[@title='" + rc + "']", "XPath");
            }
            else if (cs == "OFFLINE")
            {
               Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                Actions hovernr = new Actions(driver);
                IWebElement notready = driver.FindElement(By.Id("navbar-menuLogout"));
                hovernr.MoveToElement(notready).Perform();
                hovernr.Click().Build().Perform();
               Custommethods.Click(driver, testname, "//*[@title='" + rc + "']", "XPath");
            }

        }

        public static void VUE_Wait_For_State(IWebDriver driver, ExtentTest test, string testname, string state, string rc)
        {

            test.Info("VUE_Wait_For_State");
            string wfe = state;

            if (wfe == "IDLE")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Idle ']", "XPath");
            }

            else if (wfe == "ACTIVE")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Active ']", "XPath");
            }

            else if (wfe == "HELD")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Held ']", "XPath");
            }
            else if (wfe == "WRAP")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Wrap ']", "XPath");
            }
            else if (wfe == "OFFLINE")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");
            }
            else if (wfe == "CONNECTED")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Connected ']", "XPath");
            }
            else if (wfe == "INTERNAL")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Internal ']", "XPath");
            }
            else if (wfe == "RINGING")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Ringing ']", "XPath");
            }
            else if (wfe == "INCOMING")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Incoming ']", "XPath");
            }
            else if (wfe == "ACTIVEMANUAL")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Active Manual ']", "XPath");
            }
            else if (wfe == "INTERNALMANUAL")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Internal Manual ']", "XPath");
            }
            else if (wfe == "PREVIEW")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Preview ']", "XPath");
            }
            else if (wfe == "NOTREADY")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Not Ready (" + rc + ")']", "XPath");
            }
            else if (wfe == "PARK")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Park (" + rc + ")']", "XPath");
            }
            else if (wfe == "FEATURE")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Feature ']", "XPath");
            }
            else
            {
                Custommethods.TakeScreenshot(driver, DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt"));
                throw new ArgumentException("INVALID EVENT");
            }


        }

        public static void VUE_Wait_for_Call(IWebDriver driver, ExtentTest test, string testname, int timeinsec)
        {

            test.Info("VUE_Wait_for_Call");
            //int wt = 30;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeinsec));
            (wait.Until(x => x.FindElement(By.XPath("//*[@id='navbar-dispCurrentState' and @title='Incoming '] | //*[@id='navbar-dispCurrentState' and @title='Internal '] | //*[@id='navbar-dispCurrentState' and @title='Active '] | //*[@id='navbar-dispCurrentState' and @title='Preview ']")))).Click();
            Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Incoming '] | //*[@id='navbar-dispCurrentState' and @title='Internal '] | //*[@id='navbar-dispCurrentState' and @title='Active '] | //*[@id='navbar-dispCurrentState' and @title='Preview ']", "XPath");
  
        }

        public static void VUE_EngagementCenter(IWebDriver driver, ExtentTest test, string testname)

        {

            test.Info("VUE_EngagementCente");
            Custommethods.Click(driver, testname, "navbarToggleLink", "Id");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");

        }

        public static void VUE_Manual_Dial(IWebDriver driver, ExtentTest test, string testname, string mobnum)
        {

            test.Info("VUE_Manual_Dial");
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2]", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@title='Enter a phone number or select an entity']", Keys.Control + "a", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@title='Enter a phone number or select an entity']", mobnum, "XPath");
           Custommethods.Click(driver, testname, "manual-contact-control-options", "Id");
           Custommethods.Click(driver, testname, "//button[@title='Call']", "XPath");
        }

        public static void Enable_Chat_Worktype(IWebDriver driver, ExtentTest test, string testname, string chatwtname)
        {

            test.Info("Enable_Chat_Worktype");
            Custommethods.Click(driver, testname, "LaunchSetup", "Id");
           Custommethods.Click(driver, testname, "//*[@id='modalSetup-body']/div/div[1]/ul/li[2]/a", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='modalSetup-body']/div/div[1]/div/div[2]/div[1]/button", "XPath");
           Custommethods.Click(driver, testname, "//*[contains(text(),'" + chatwtname + "')]/../../div[3]/label/input", "XPath");
           Custommethods.Click(driver, testname, "buttonSaveChanges", "Id");
            Thread.Sleep(5000);
            driver.Quit();


        }

        public static void Webchat_Chat(IWebDriver driver, ExtentTest test, string testname, string org, string chatwtname)
        {

            test.Info("Webchat_Chat");
            //chattab = driver.WindowHandles.Count;
            //IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("window.open('" + chaturl + "', 'tab2');");
            //driver.SwitchTo().Window(driver.WindowHandles[chattab]);
            driver.Navigate().GoToUrl("https://" + org + ".brooklyn.aspect-cloud.net/WebChatDemo/");
           Custommethods.Click(driver, testname, "chatLongPolling", "Id");

            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='overlay']")));
           Custommethods.EnterText(driver, testname, "CustNameField", "SCTAutomation", "Id");
           Custommethods.EnterText(driver, testname, "CustEmailField", "SCTAutomation@via.bm.aspect.com", "Id");
           Custommethods.Click(driver, testname, "UserSelectableServicesMenu", "Id");
            Actions schatwt = new Actions(driver);
            IWebElement selectchatwt = driver.FindElement(By.Id("UserSelectableServicesMenu"));
            schatwt.MoveToElement(selectchatwt);
            schatwt.SendKeys(chatwtname).Perform();
            schatwt.SendKeys(Keys.Enter).Perform();
           Custommethods.EnterText(driver, testname, "FirstChatField", "SCTAutomation", "Id");
           Custommethods.Click(driver, testname, "buttonChatFormSubmit", "Id");
            //driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public static void VUE_Chat_Reply(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_Chat_Reply");
            Custommethods.EnterText(driver, testname, "//*[@class='ng-scope k-content k-state-active']/div/div[2]/textarea", "SCTAutomation", "XPath");
           Custommethods.EnterText(driver, testname, "//*[@class='ng-scope k-content k-state-active']/div/div[2]/textarea", Keys.Enter, "XPath");

        }

        public static void Webchat_Chat_Reply(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("Webchat_Chat_Reply");
            //driver.SwitchTo().Window(driver.WindowHandles[chattab]);
            //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='overlay']")));
            Custommethods.EnterText(driver, testname, "ChatInput", "SCTAutomation", "Id");
           Custommethods.EnterText(driver, testname, "ChatInput", Keys.Enter, "Id");
            //driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public static void Veriy_VUE_Chat_msg(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("Veriy_VUE_Chat_msg");
            string asdf = driver.FindElement(By.XPath("//*[@id='newCallWidget-tabstripCallInfo-1']")).Text;
           Custommethods.Click(driver, testname, "//*[contains(text(),'SCTAutomation')]", "XPath");

        }

        public static void Google_VoiceCall_hangout(IWebDriver driver, ExtentTest test, string testname, string dnis)
        {

            test.Info("Google_VoiceCall_hangout");
            int voicetab = driver.WindowHandles.Count;
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://hangouts.google.com/");
            driver.FindElement(By.XPath("//a[contains(text(),'Sign in')]")).Click();
            driver.FindElement(By.Id("identifierId")).SendKeys("aspectsoftware2");
            driver.FindElement(By.XPath("//span[contains(text(),'Next')]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Asp3ct123$");
            driver.FindElement(By.XPath("//span[contains(text(),'Next')]")).Click();
            driver.FindElement(By.XPath("//*[@id='yDmH0d']/div[4]/div[4]/div/div/ul/li[2]/div[1]")).Click();
            driver.SwitchTo().Frame(0);
            driver.FindElement(By.XPath("//*[contains(text(),'New conversation')]")).Click();
            driver.FindElement(By.XPath("//table//tbody//tr//td//input")).SendKeys("+1" + dnis);
            driver.FindElement(By.XPath("//table//tbody//tr//td//input")).SendKeys(Keys.Enter);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public static void Generate_VoiceCall(IWebDriver driver, ExtentTest test, string testname, string DNIS, string activetime)
        {

            test.Info("Generate_VoiceCall");
            string parentdir = Environment.CurrentDirectory.ToString();
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = @parentdir + "\\InputData\\HWPOP_StarTrinityAPI.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.Arguments = "-DNIS " + DNIS + " -ACTIVETIME " + activetime + " -NUMOFCALLS 1";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }

            catch
            {
                // Log error
            }


        }

        public static void Google_Generate_SMSCall(IWebDriver driver, ExtentTest test, string testname, string dnis)
        {

            test.Info("Google_Generate_SMSCall");
            //smstab = driver.WindowHandles.Count;
            //Login google voice
            //IWebElement element = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("window.open('https://voice.google.com/messages', 'tab3');");
            //driver.SwitchTo().Window(driver.WindowHandles[smstab]);
            driver.Navigate().GoToUrl("https://voice.google.com/messages");
           Custommethods.Click(driver, testname, "//*[@id='getVoiceToggle']/button/span", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='getVoiceToggle']/div/button[3]/span", "XPath");
           Custommethods.EnterText(driver, testname, "identifierId", "aspectsoftware2", "Id");
           Custommethods.Click(driver, testname, "identifierNext", "Id");
           Custommethods.EnterText(driver, testname, "//input[@type='password']", "Asp3ct123$", "XPath");
           Custommethods.Click(driver, testname, "passwordNext", "Id");
            //send sms call
           Custommethods.Click(driver, testname, "//*[@id='gvPageRoot']/div[2]/div[2]/gv-side-nav/div/div/gmat-nav-list/a[2]/div/div/mat-icon", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='messaging-view']/div/md-content/div/div/div/div", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@type='search']", dnis + Keys.Enter, "XPath");
           Custommethods.EnterText(driver, testname, "input_2", "Automation Message", "Id");
            Thread.Sleep(2000);
           Custommethods.EnterText(driver, testname, "input_2", Keys.Enter, "Id");
            //driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public static void Generate_SMSCall(IWebDriver driver, ExtentTest test, string testname, string dnis)
        {

            test.Info("Generate_SMSCall");
            string parentdir = Environment.CurrentDirectory.ToString();
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @parentdir + "\\InputData\\SMSAPI.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-DNIS " + dnis;
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error
            }
        }

        public static void VUE_SMS_Reply(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_SMS_Reply");
            Custommethods.EnterText(driver, testname, "//*[@class='ng-scope k-content k-state-active']/div/div[2]/textarea", "BM Automation Agent Reply", "XPath");
           Custommethods.EnterText(driver, testname, "//*[@class='ng-scope k-content k-state-active']/div/div[2]/textarea", Keys.Enter, "XPath");
        }

        public static void Google_SMS_Reply(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("Google_SMS_Reply");
            driver.SwitchTo().Window(driver.WindowHandles[smstab]);
           Custommethods.EnterText(driver, testname, "input_2", "BM Automation Customer Reply", "Id");
           Custommethods.EnterText(driver, testname, "input_2", Keys.Enter, "Id");
            driver.SwitchTo().Window(driver.WindowHandles[0]);
          }

        public static void Generate_EmailCall(IWebDriver driver, ExtentTest test, string testname, string to)
        {

            test.Info("Generate_EmailCall");
            string parentdir = Environment.CurrentDirectory.ToString();
            string sub = "SCTAutomation";
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = @parentdir + "\\InputData\\EmailAPI.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-SENDEMAIL " + to + " -SUB " + sub;
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error
            }
        }

        public static void VUE_Accept_Reject(IWebDriver driver, ExtentTest test, string testname, string status)
        {

            test.Info("VUE_Accept_Reject");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            if (status == "Enabled")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Ringing '] | //*[@id='navbar-dispCurrentState' and @title='Incoming ']", "XPath");
               Custommethods.Click(driver, testname, "newCallWidget-btnAccept", "Id");
            }
            else if (status == "Disabled")
            {

            }
            else if (status == "Reject")
            {
               Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Ringing '] | //*[@id='navbar-dispCurrentState' and @title='Incoming ']", "XPath");
               Custommethods.Click(driver, testname, "newCallWidget-btnReject", "Id");
            }
 
        }

        public static void VUE_Sleep_Duration(IWebDriver driver, ExtentTest test, string testname, int seconds)
        {

            test.Info("VUE_Sleep_Duration");
            int sleep = Convert.ToInt32(seconds);
            Thread.Sleep(sleep * 1000);
        }

        public static void VUE_Hold_Action(IWebDriver driver, ExtentTest test, string testname, string holdaction)
        {

            test.Info("VUE_Hold_Action");
            if (holdaction == "HOLD")
            {
               Custommethods.Click(driver, testname, "//*[@title='Hold']", "XPath");
            }
            else if (holdaction == "UNHOLD")
            {
               Custommethods.Click(driver, testname, "//*[@title='Retrieve Hold']", "XPath");
            }
        }

        public static void VUE_Recording_Action(IWebDriver driver, ExtentTest test, string testname, string recaction)
        {

            test.Info("VUE_Recording_Action");
            if (recaction == "Pause_Recording")
            {
               Custommethods.Click(driver, testname, "//*[@title='Pause Recording']", "XPath");
            }
            else if (recaction == "Resume_Recording")
            {
               Custommethods.Click(driver, testname, "//*[@title='Resume Recording']", "XPath");
            }
        }

        public static void VUE_Agent_Live_Assit(IWebDriver driver, ExtentTest test, string testname, string mon, string team, string username)
        {

            test.Info(" VUE_Agent_Live_Assit");
            //string userFname = username.Substring(0, username.IndexOf('@'));
            string userFname = username;


            if (mon == "Silent")
            {
               Custommethods.Click(driver, testname, "navbarToggleLink", "Id");
               Custommethods.Click(driver, testname, "dashboard-id-agentLiveAssist", "Id");
               Custommethods.Click(driver, testname, "//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]", "XPath");
                Actions tm = new Actions(driver);
                IWebElement tm1 = driver.FindElement(By.XPath("//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]"));
                tm.MoveToElement(tm1).Perform();
                tm.SendKeys(team).Build().Perform();
                tm.Click(tm1).Build().Perform();
                Thread.Sleep(60000);
               Custommethods.EnterText(driver, testname, "//*[@id='enterpriseMonitor-txtSearch']", userFname, "XPath");
                //Click(driver, testname, "//*[contains(text(),'" + username + "')]", "XPath");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-silent", "Id");
            }
            else if (mon == "Coach")
            {

               Custommethods.Click(driver, testname, "navbarToggleLink", "Id");
               Custommethods.Click(driver, testname, "dashboard-id-agentLiveAssist", "Id");
               Custommethods.Click(driver, testname, "//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]", "XPath");
                Actions tm = new Actions(driver);
                IWebElement tm1 = driver.FindElement(By.XPath("//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]"));
                tm.MoveToElement(tm1).Perform();
                tm.SendKeys(team).Build().Perform();
                tm.Click(tm1).Build().Perform();
                Thread.Sleep(60000);
               Custommethods.EnterText(driver, testname, "//*[@id='enterpriseMonitor-txtSearch']", userFname, "XPath");
                //Click(driver, testname, "//*[contains(text(),'" + username + "')]", "XPath");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-coach", "Id");
            }
            else if (mon == "Barge-in")
            {
               Custommethods.Click(driver, testname, "navbarToggleLink", "Id");
               Custommethods.Click(driver, testname, "dashboard-id-agentLiveAssist", "Id");
               Custommethods.Click(driver, testname, "//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]", "XPath");
                Actions tm = new Actions(driver);
                IWebElement tm1 = driver.FindElement(By.XPath("//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]"));
                tm.MoveToElement(tm1).Perform();
                tm.SendKeys(team).Build().Perform();
                tm.Click(tm1).Build().Perform();
                Thread.Sleep(60000);
               Custommethods.EnterText(driver, testname, "//*[@id='enterpriseMonitor-txtSearch']", userFname, "XPath");
                //Click(driver, testname, "//*[contains(text(),'" + username + "')]", "XPath");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-bargein", "Id");
            }
            else if (mon == "All")
            {
               Custommethods.Click(driver, testname, "navbarToggleLink", "Id");
               Custommethods.Click(driver, testname, "dashboard-id-agentLiveAssist", "Id");
               Custommethods.Click(driver, testname, "//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]", "XPath");
                Actions tm = new Actions(driver);
                IWebElement tm1 = driver.FindElement(By.XPath("//*[@id='enterpriseMonitor-agentstatusdiv']/div/form/div[1]/span/span/span[2]"));
                tm.MoveToElement(tm1).Perform();
                tm.SendKeys(team).Build().Perform();
                tm.Click(tm1).Build().Perform();
                Thread.Sleep(30000);
               Custommethods.EnterText(driver, testname, "//*[@id='enterpriseMonitor-txtSearch']", userFname, "XPath");
                //Click(driver, testname, "//*[contains(text(),'" + username + "')]", "XPath");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-silent", "Id");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-coach", "Id");
                Thread.Sleep(20000);
               Custommethods.Click(driver, testname, "enterpriseMonitor-bargein", "Id");
                Thread.Sleep(20000);
            }
            else if (mon == "Stop")
            {
               Custommethods.Click(driver, testname, "enterpriseMonitor-stopall", "Id");
            }

        }

        public static void VUE_Hangup(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_Hangup");
            Custommethods.Click(driver, testname, "//button[@title='Hang Up'] | //button[@title='Close']", "XPath");
        }

        public static void VUE_Dispose_Call(IWebDriver driver, ExtentTest test, string testname, string disp)
        {

            test.Info("VUE_Dispose_Cal");
            Custommethods.Click(driver, testname, "//span[@title='" + disp + "']/following::td/button | //*[@id='newCallWidget-tabstripCallInfo-2']/div/div/div/div[3]/div/button", "XPath");
        }

        public static void VUE_Consult_User(IWebDriver driver, ExtentTest test, string testname, string userfirstname)
        {

            test.Info("VUE_Consult_User");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2] | //*[@id='tabstrip']/ul/li[2]/span[2]", "XPath");
            //Search user first name
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", Keys.Control + "a", "Id");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", userfirstname, "Id");
            //click on speed dial
           Custommethods.Click(driver, testname, "//*[@title='Speed Dials']", "XPath");
           Custommethods.Click(driver, testname, "//*[@title='Agents']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuAgents']/ul/li/span/ul/li/button/i", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='contactsPanelBar_pb_active']/span/ul/li/div/div[2]/div/ul/li[1]/button/i", "XPath");
        }

        public static void VUE_Active_Line(IWebDriver driver, ExtentTest test, string testname, string al)
        {

            test.Info("VUE_Active_Line");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            if (al == "Line1")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-1']/div[1]", "XPath");
            }
            else if (al == "Line2")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-2']/div[1]", "XPath");
            }
            else if (al == "Line3")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-3']/div[1]", "XPath");
            }
            else if (al == "Line4")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-4']/div[1]", "XPath");
            }
            else if (al == "Line5")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-5']/div[1]", "XPath");
            }
            else if (al == "Line6")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-6']/div[1]", "XPath");
            }
            else if (al == "Line7")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-7']/div[1]", "XPath");
            }
            else if (al == "Line8")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-8']/div[1]", "XPath");
            }
            else if (al == "Line9")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-9']/div[1]", "XPath");
            }
            else if (al == "Line10")
            {
               Custommethods.Click(driver, testname, "//*[@id='callLine-10']/div[1]", "XPath");
            }

        }

        public static void VUE_Blind_Transfer_User(IWebDriver driver, ExtentTest test, string testname, string userfirstname)
        {

            test.Info("VUE_Blind_Transfer_User");
            Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2]", "XPath");
            //Search user first name
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", Keys.Control + "a", "Id");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", userfirstname, "Id");
           Custommethods.Click(driver, testname, "//span[contains(text(),'Agents')]", "XPath");
            try
            {
                bool resulte = driver.FindElements(By.XPath("//button[@name='contactOptions']//i[@class='icon-cog']")).Count > 0 && driver.FindElement(By.XPath("//button[@name='contactOptions']//i[@class='icon-cog']")).Displayed;
                if (resulte)
                {
                   Custommethods.Click(driver, testname, "//button[@name='contactOptions']//i[@class='icon-cog']", "XPath");
                }
                else
                {
                   Custommethods.Click(driver, testname, "//span[contains(text(),'Agents')]", "XPath");
                   Custommethods.Click(driver, testname, "//button[@name='contactOptions']//i[@class='icon-cog']", "XPath");
                }
               Custommethods.Click(driver, testname, "//i[@class='glyph-transfer']", "XPath");
            }
            catch { }

            // MethodsVUE.VUE_Dispose_Call(driver, testname, "BM");

            /*Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/span/span[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuAgents']/span/span[2] | //*[@id='sideBar-menuAgents']/span/span[@class='k-icon k-i-arrow-s k-panelbar-expand']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuAgents']/ul/li/span/ul/li/button/i | //*[@id='sideBar-menuAgents']/ul/li/span/ul/li/button/i", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='contactsPanelBar_pb_active']/span/ul/li/div/div[2]/div/ul/li[2]/button/i | //*[@id='contactsPanelBar_pb_active']/span/ul/li/div/div[2]/div/ul/li[2]/button/i", "XPath");
        */
        }

        public static void VUE_Conference(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_Conference");
            //Click on conferencebutton
            Custommethods.Click(driver, testname, "btnStartConference", "Id");
        }

        public static void VUE_Warm_Transfer(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_Warm_Transfer");
            //Click on Transfer button
            Custommethods.Click(driver, testname, "btnTransferToConsultedParty", "Id");
        }

        public static void VUE_End_Conference(IWebDriver driver, ExtentTest test, string testname)
        {

            test.Info("VUE_End_Conference");
            //Click on Transfer button
            Custommethods.Click(driver, testname, "btnEndConference", "Id");
        }

        public static void VUE_Blind_Transfer_External(IWebDriver driver, ExtentTest test, string testname, long mobnum)
        {

            test.Info("VUE_Blind_Transfer_External");
            string mobnumber = Convert.ToString(mobnum);
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2] | //*[@id='tabstrip']/ul/li[2]/span[2] ", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@title='Enter a phone number or select an entity']", Keys.Control + "a", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@title='Enter a phone number or select an entity']", mobnumber, "XPath");
           Custommethods.Click(driver, testname, "manual-contact-control-options", "Id");
           Custommethods.Click(driver, testname, "//button[@title='Transfer']", "XPath");
        }

        public static void VUE_Consult_Worktype(IWebDriver driver, ExtentTest test, string testname, string worktypename)
        {

            test.Info("VUE_Consult_Worktype");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2] | //*[@id='tabstrip']/ul/li[2]/span[2] ", "XPath");
            //Search worktype name
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", Keys.Control + "a", "Id");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", worktypename, "Id");
           Custommethods.Click(driver, testname, "//*[@title='Speed Dials']", "XPath");
            //Consult
           Custommethods.Click(driver, testname, "//*[@title='Work Types']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuServices']/ul/li/span/ul/li/button/i", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='contactsPanelBar_pb_active']/span/ul/li/div/div[2]/div/ul/li[1]/button/i", "XPath");
        }

        public static void VUE_Blind_Transfer_Worktype(IWebDriver driver, ExtentTest test, string testname, string worktypename)
        {
            test.Info("VUE_Blind_Transfer_Worktype");
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2] | //*[@id='tabstrip']/ul/li[2]/span[2] ", "XPath");
            //Search user first name
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", Keys.Control + "a", "Id");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", worktypename, "Id");
           Custommethods.Click(driver, testname, "//span[contains(text(),'Work Types')]", "XPath");
            try
            {
                bool resulte = driver.FindElements(By.XPath("//button[@name='contactOptions']//i[@class='icon-cog']")).Count > 0 && driver.FindElement(By.XPath("//button[@name='contactOptions']//i[@class='icon-cog']")).Displayed;
                if (resulte)
                {
                   Custommethods.Click(driver, testname, "//button[@name='contactOptions']//i[@class='icon-cog']", "XPath");
                }
                else
                {
                   Custommethods.Click(driver, testname, "//span[contains(text(),'Work Types')]", "XPath");
                   Custommethods.Click(driver, testname, "//button[@name='contactOptions']//i[@class='icon-cog']", "XPath");
                }
               Custommethods.Click(driver, testname, "//i[@class='glyph-transfer']", "XPath");
            }
            catch { }

            //MethodsVUE.VUE_Dispose_Call(driver, testname, "BM");

            /*Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/span/span[2]", "XPath");
            //Consult
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuServices']/span/span[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuServices']/ul/li/span/ul/li/button/i", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='contactsPanelBar_pb_active']/span/ul/li/div/div[2]/div/ul/li[2]/button/i", "XPath");*/
        }

        public static void VUE_Send_Agent_Voicemail(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Send_Agent_Voicemail");
            Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[@title='Show Dial Pad']", "XPath");
           Custommethods.Click(driver, testname, "dialPad-btn1", "Id");
        }

        public static void VUE_Play_AudioMessgage(IWebDriver driver, ExtentTest test, string testname, string audiomsg)
        {
            test.Info("VUE_Play_AudioMessgage");
            //Play disclaimer Message
            Custommethods.Click(driver, testname, "//*[@title='Audio Messages']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='audioMessagesGrid']/div[2]/table/tbody//td[contains(text(),'" + audiomsg + "')]", "XPath");
           Custommethods.Click(driver, testname, "//*[@title='Play']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='newCallWidget-tabstripCallInfo-3']/div/div/div[1]/div/div/span/span", "XPath");
            string sdf = driver.FindElement(By.XPath("//*[@id='newCallWidget-tabstripCallInfo-3']/div/div/div[1]/div/div/span/span")).Text;
           Custommethods.Click(driver, testname, "//*[@id='newCallWidget-tabstripCallInfo-3']/div/div/div[1]/div/div/span/span[contains(text(),'00:00:00')]", "XPath");
        }

        public static void VUE_Play_Voicemail(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Play_Voicemail");
            Custommethods.Click(driver, testname, "//*[@id='main']/div/div/div/div[1]/ng-view/div/div/div[4]/div/div/div[2]/div/a/div[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='messageViewerGrid']/div[2]/table/tbody/tr[1]/td[1]/input", "XPath");
           Custommethods.Click(driver, testname, "playTrack", "Id");
            string rundur = driver.FindElement(By.XPath("//*[@id='main']/div/div/div/div[1]/ng-view/div/div/div[1]/div/div/div[1]/div[3]/div/div[1]/ul[2]/li[1]/span")).Text;
            Console.WriteLine(rundur);
            for (int i = 0; i <= 10;)
            {
                rundur = driver.FindElement(By.XPath("//*[@id='main']/div/div/div/div[1]/ng-view/div/div/div[1]/div/div/div[1]/div[3]/div/div[1]/ul[2]/li[1]/span")).Text;
                if (rundur == "00:00")
                {
                    Console.WriteLine("runduration is  = " + rundur);
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("BREAK");
                    break;
                }
            }
            string rundur1 = driver.FindElement(By.XPath("//*[@id='main']/div/div/div/div[1]/ng-view/div/div/div[1]/div/div/div[1]/div[3]/div/div[1]/ul[2]/li[1]/span")).Text;
            Console.WriteLine(rundur1);
            string currdur = driver.FindElement(By.Id("currDur")).Text;
            Console.WriteLine(currdur);
           Custommethods.Click(driver, testname, "//*[@id='main']/div/div/div/div[1]/ng-view/div/div/div[1]/div/div/div[1]/div[3]/div/div[1]/ul[2]/li[1]/span[contains(text(),'" + currdur + "')]", "XPath");
            Console.WriteLine("PASS");
        }

        public static void VUE_Create_SystemExternals(IWebDriver driver, ExtentTest test, string testname, string sysextname, string sysextno)
        {
            test.Info("VUE_Create_SystemExternals");
            driver.SwitchTo().Window(driver.WindowHandles[0]);
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2]", "XPath");
            //Create
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuAgents']/span", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/span/span[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[@title='Add New Speed Dial']", "XPath");
           Custommethods.EnterText(driver, testname, "speedDialNameNew", sysextname, "Id");
           Custommethods.EnterText(driver, testname, "speedDialNumberNew", sysextno, "Id");
           Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
        }

        public static void VUE_Delete_SystemExternals(IWebDriver driver, ExtentTest test, string testname, string sysextname)
        {
            test.Info("VUE_Delete_SystemExternals");
            driver.SwitchTo().Window(driver.WindowHandles[0]);
           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-engagementCenter", "Id");
           Custommethods.Click(driver, testname, "//*[@id='tabstripSideBar']/ul/li[2]/span[2]", "XPath");
            //Delete
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuAgents']/span", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/span/span[2]", "XPath");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", Keys.Control + "a", "Id");
           Custommethods.EnterText(driver, testname, "sideBar-inpContactsSearch", sysextname, "Id");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/ul/li/span/ul/li/button/i", "XPath");
           Custommethods.Click(driver, testname, "//*[@title='Delete']", "XPath");
           Custommethods.Click(driver, testname, "//*[@id='sideBar-menuSpeedDials']/ul/li/span/div/div[1]/button[2]", "XPath");
           Custommethods.Click(driver, testname, "//*[contains(text(),'OK')]", "XPath");
        }

        public static void VUE_Callback_SameAgent_Snooze(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Callback_SameAgent_Snooze");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'No Snooze')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.Tab).Perform();
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToMe']", "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Same Agent - Snooze 5 Minutes", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Callback_SameAgent_ScheduleTime(IWebDriver driver, ExtentTest test, string testname)
        {
            //11/28/2019 4:13 PM
            test.Info("VUE_Callback_SameAgent_ScheduleTime");
            string Schedule = DateTime.UtcNow.AddMinutes(10).ToString("M/d/yyyy h:mm tt");

           Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dtpCallbackDateTime']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dtpCallbackDateTime']", Schedule, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToMe']", "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Same Agent - Scheduled to next 10 minutes UTC TZ", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Callback_SameAgent_Alternate(IWebDriver driver, ExtentTest test, string testname, long num)
        {
            test.Info("VUE_Callback_SameAgent_Alternate");
            string number = Convert.ToString(num);
           Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'No Snooze')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.Tab).Perform();
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToMe']", "XPath");
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackAtAlternate']", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtCallbackNumber']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtCallbackNumber']", number, "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Same Agent - Snooze 5 Minutes", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Callback_WorkType_Snooze(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Callback_WorkType_Snooze");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
            Actions filter101 = new Actions(driver);

            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'No Snooze')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.Tab).Perform();
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToService']", "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Work Type - Snooze 5 Minutes", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Callback_WorkType_ScheduleTime(IWebDriver driver, ExtentTest test, string testname)
        {
            //11/28/2019 4:13 PM
            test.Info("VUE_Callback_WorkType_ScheduleTime");
            string Schedule = DateTime.UtcNow.AddMinutes(10).ToString("M/d/yyyy h:mm tt");

           Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dtpCallbackDateTime']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dtpCallbackDateTime']", Schedule, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToService']", "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Work Type - Scheduled to next 10 minutes UTC TZ", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Callback_WorkType_Alternate(IWebDriver driver, ExtentTest test, string testname, long num)
        {
            test.Info("VUE_Callback_WorkType_Alternate");
            string number = Convert.ToString(num);
           Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnCallback']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'No Snooze')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.Tab).Perform();
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackToService']", "XPath");
           Custommethods.Click(driver, testname, "//input[@id='rdoCallbackAtAlternate']", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtCallbackNumber']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtCallbackNumber']", number, "XPath");
           Custommethods.EnterText(driver, testname, "//textarea[@id='txtCallbackMemo']", "BM CBK Call Center Scenario - Work Type - Snooze 5 Minutes", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Exclusion_SSN(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Exclusion_SSN");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnExclusion']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'Phone Number')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowUp + Keys.ArrowUp + Keys.Tab).Perform();
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long number = LongRandom(888800001, 888899999, new Random());
            string snumber = Convert.ToString(number);
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", snumber, "XPath");
            //11/29/2019
            string StDt = DateTime.UtcNow.ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", StDt, "XPath");
            //11/30/2019
            string EnDt = DateTime.UtcNow.AddDays(1).ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", EnDt, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Exclusion_Account(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Exclusion_Account");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnExclusion']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'Phone Number')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowUp + Keys.Tab).Perform();
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long number = LongRandom(8888000001, 8888999999, new Random());
            string snumber = Convert.ToString(number);
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", snumber, "XPath");
            //11/29/2019
            string StDt = DateTime.UtcNow.ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", StDt, "XPath");
            //11/30/2019
            string EnDt = DateTime.UtcNow.AddDays(1).ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", EnDt, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Exclusion_Phone(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Exclusion_Phone");
           Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnExclusion']", "XPath");
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long number = LongRandom(9999000001, 9999999999, new Random());
            string snumber = Convert.ToString(number);
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", snumber, "XPath");
            //11/29/2019
            string StDt = DateTime.UtcNow.ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", StDt, "XPath");
            //11/30/2019
            string EnDt = DateTime.UtcNow.AddDays(1).ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", EnDt, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Exclusion_SMS(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Exclusion_SMS");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnExclusion']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'Phone Number')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.Tab).Perform();
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long number = LongRandom(7777700001, 7777799999, new Random());
            string snumber1 = Convert.ToString(number);
            string snumber = snumber1.Replace("-", "");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", snumber, "XPath");
            //11/29/2019
            string StDt = DateTime.UtcNow.ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", StDt, "XPath");
            //11/30/2019
            string EnDt = DateTime.UtcNow.AddDays(1).ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", EnDt, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Exclusion_Email(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Exclusion_Email");
            Custommethods.Click(driver, testname, "//i[@class='glyph-dispositions ng-scope']", "XPath");
           Custommethods.Click(driver, testname, "//button[@class='dispositions-grid-button btnExclusion']", "XPath");
            Actions filter101 = new Actions(driver);
            IWebElement filterr101 = driver.FindElement(By.XPath("//span[@class='k-input ng-binding ng-scope'][contains(text(),'Phone Number')]"));
            filter101.MoveToElement(filterr101).Perform();
            filter101.Click(filterr101).Perform();
            filter101.SendKeys(Keys.ArrowDown + Keys.ArrowDown + Keys.Tab).Perform();
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long number = LongRandom(6666600001, 6666699999, new Random());
            string snumber1 = Convert.ToString(number);
            string snumber = snumber1.Replace("-", "");
            string email = snumber + "@gmail.com";
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='txtExclusionValue']", email, "XPath");
            //11/29/2019
            string StDt = DateTime.UtcNow.ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionStartDate']", StDt, "XPath");
            //11/30/2019
            string EnDt = DateTime.UtcNow.AddDays(1).ToString("M/d/yyyy");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", Keys.Control + "a" + Keys.Delete, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='dpkExclusionEndDate']", EnDt, "XPath");
           Custommethods.EnterText(driver, testname, "//input[@placeholder='None']", "(UTC) Coordinated Universal Time", "XPath");
           Custommethods.Click(driver, testname, "//i[@class='glyph-release']", "XPath");
            Thread.Sleep(2000);
           Custommethods.Click(driver, testname, "//button[@class='submit-disposition button-primary ng-binding ng-scope']", "XPath");
        }

        public static void VUE_Logout(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Logout");
            driver.SwitchTo().Window(driver.WindowHandles[0]);
           Custommethods.Click(driver, testname, "navbar-menuOptions", "Id");
           Custommethods.Click(driver, testname, "navbar-menuSignOut", "Id");
        }

        public static void VUE_Relogin_On_Warn(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Relogin_On_Warn");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                string warn = (wait.Until(x => x.FindElement(By.XPath("/html/body")))).Text;
                if (warn.Contains("Warning"))
                {
                   Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                }
                bool result = driver.FindElements(By.XPath("//span[@class='badge badge-notify ng-binding']")).Count > 0 && driver.FindElement(By.XPath("//span[@class='badge badge-notify ng-binding']")).Displayed;
                if (result)
                {
                    bool result2 = driver.FindElements(By.XPath("//span[contains(text(),'NOTIFICATIONS')]")).Count > 0 && driver.FindElement(By.XPath("//span[contains(text(),'NOTIFICATIONS')]")).Displayed;
                    if (result2)
                    {
                       Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                    }
                    else
                    {
                       Custommethods.Click(driver, testname, "//i[@class='glyph-notification']", "XPath");
                       Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                    }
                }

                Thread.Sleep(10000);
            }
            catch { }

        }

        public static void VUE_Relogin_On_Warn_Chat(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Relogin_On_Warn_Chat");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                string warn = (wait.Until(x => x.FindElement(By.XPath("/html/body")))).Text;
                if (warn.Contains("Warning"))
                {
                   Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                }
                bool result = driver.FindElements(By.XPath("//span[@class='badge badge-notify ng-binding']")).Count > 0 && driver.FindElement(By.XPath("//span[@class='badge badge-notify ng-binding']")).Displayed;
                if (result)
                {
                    bool result2 = driver.FindElements(By.XPath("//span[contains(text(),'NOTIFICATIONS')]")).Count > 0 && driver.FindElement(By.XPath("//span[contains(text(),'NOTIFICATIONS')]")).Displayed;
                    if (result2)
                    {
                       Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                    }
                    else
                    {
                       Custommethods.Click(driver, testname, "//i[@class='glyph-notification']", "XPath");
                       Custommethods.Click(driver, testname, "//button[@class='button-primary ng-binding']", "XPath");
                    }
                }

                Thread.Sleep(10000);
            }
            catch { }
        }

        public static void VUE_Relogin_On_No_Audio(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Relogin_On_No_Audio");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                bool result = driver.FindElements(By.XPath("//i[@class='glyph-audio-path-not-connected']")).Count > 0 && driver.FindElement(By.XPath("//i[@class='glyph-audio-path-not-connected']")).Displayed;
                if (result)
                {
                   Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                   Custommethods.Click(driver, testname, "navbar-menuLogout", "Id");
                   Custommethods.Click(driver, testname, "//a[contains(text(),'BMAPI_Logout')]", "XPath");
                    Thread.Sleep(5000);
                   Custommethods.Click(driver, testname, "//*[@id='navbar-dispCurrentState' and @title='Offline ']", "XPath");
                   Custommethods.Click(driver, testname, "navbar-menuState", "Id");
                   Custommethods.Click(driver, testname, "navbar-menuIdle", "Id");
                }
            }
            catch { }
        }

        public static void Start_List(string org, ExtentTest test, string testname, string listname, string worktypename)
        {
            test.Info("Start_List");
            //Report
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-type");
            options.AddArguments("start-maximized");
            options.AddArguments("--enable-automation");
            options.AddArguments("test-type=browser");
            options.AddArguments("--disable-infobars");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--no-proxy-server");
            IWebDriver driver = new ChromeDriver(options);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("https:/" + org + ".brooklyn.aspect-cloud.net/lmsrv/controller.php?do=monitor&cid=sm");
           Custommethods.EnterText(driver, testname, "email-input", "st.surenmgr11@stars.com", "Id");
           Custommethods.EnterText(driver, testname, "password-input", "Forgerock1", "Id");
           Custommethods.Click(driver, testname, "//button[@type='button']", "XPath");
            Thread.Sleep(5000);

           Custommethods.Click(driver, testname, "//button[contains(text(),'Execution')]", "XPath");
            //Click on Execution Tab and loaded page

           Custommethods.Click(driver, testname, "//*[@id='execution_cl']", "XPath");

           Custommethods.EnterText(driver, testname, "//input[@name='basicValue']", listname + Keys.Enter, "XPath");
            //Provided Search information and Performed Search

           Custommethods.Click(driver, testname, "//*[@id='divTableCont1']/table/tbody/tr[2]/td[1]/input", "XPath");
            //Select List to Start

            string currentWindow18 = driver.CurrentWindowHandle;

           Custommethods.Click(driver, testname, "//input[@name='buttonStart']", "XPath");
            //Clisk on Start 

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            // Find the checkbox or radio button element by Name
            IList<IWebElement> oCheckBox = driver.FindElements(By.Name("cblist[]"));

            // This will tell you the number of checkboxes are present
            int Size = oCheckBox.Count;

            //Report

            // Start the loop from first checkbox to last checkboxe
            for (int i = 1; i <= Size; i++)
            {

                // Store the checkbox name to the string variable, using 'Value' attribute
                String Value = driver.FindElement(By.XPath($"//*[@id='divTableCont1']/table/tbody/tr[{i}]/td[2]")).Text;

                // Select the checkbox it the value of the checkbox is same what you are looking for
                if (Value.Equals(worktypename))
                {
                    int j = i - 1;
                    oCheckBox.ElementAt(j).Click();
                    // This will take the execution out of for loop

                    //Report

                    break;

                }
            }

            //Select WorkType
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("buttonOk")))).Click();
            //Click button ok

            driver.SwitchTo().Window(currentWindow18);

            //Thread.Sleep(30000);

            //Report

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            //                                                                         Logout From ALM
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
           Custommethods.Click(driver, testname, "//*[@id='header']/div/a", "XPath");

            driver.Close();
            driver.Quit();
        }

        public static void Stop_List(string org, ExtentTest test, string testname, string listname, string worktypename)
        {
            test.Info("Stop_List");
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-type");
            options.AddArguments("start-maximized");
            options.AddArguments("--enable-automation");
            options.AddArguments("test-type=browser");
            options.AddArguments("--disable-infobars");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--no-proxy-server");
            IWebDriver driver = new ChromeDriver(options);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("https:/" + org + ".brooklyn.aspect-cloud.net/lmsrv/controller.php?do=monitor&cid=sm");
           Custommethods.EnterText(driver, testname, "email-input", "st.surenmgr11@stars.com", "Id");
           Custommethods.EnterText(driver, testname, "password-input", "Forgerock1", "Id");
           Custommethods.Click(driver, testname, "//button[@type='button']", "XPath");
            Thread.Sleep(5000);

            //Report

           Custommethods.Click(driver, testname, "//button[contains(text(),'Execution')]", "XPath");
            //Click on Execution Tab and loaded page

           Custommethods.Click(driver, testname, "//*[@id='execution_cl']", "XPath");


           Custommethods.EnterText(driver, testname, "//input[@name='basicValue']", listname + Keys.Enter, "XPath");
            //Provided Search information and Performed Search

           Custommethods.Click(driver, testname, "//*[@id='divTableCont1']/table/tbody/tr[2]/td[1]/input", "XPath");
            //Select List to Start

            string currentWindow18 = driver.CurrentWindowHandle;

           Custommethods.Click(driver, testname, "//input[@name='buttonStop']", "XPath");
            //Clisk on Stop 

            driver.SwitchTo().Window(driver.WindowHandles.Last());

            // Find the checkbox or radio button element by Name
            IList<IWebElement> oCheckBox = driver.FindElements(By.Name("cblist[]"));

            // This will tell you the number of checkboxes are present
            int Size = oCheckBox.Count;

            //Report

            // Start the loop from first checkbox to last checkboxe
            for (int i = 1; i <= Size; i++)
            {

                // Store the checkbox name to the string variable, using 'Value' attribute
                String Value = driver.FindElement(By.XPath($"//*[@id='divTableCont1']/table/tbody/tr[{i}]/td[2]")).Text;

                // Select the checkbox it the value of the checkbox is same what you are looking for
                if (Value.Equals(worktypename))
                {
                    int j = i - 1;
                    oCheckBox.ElementAt(j).Click();
                    // This will take the execution out of for loop

                    //Report

                    break;

                }
            }

            //Select WorkType
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("buttonOk")))).Click();
            //Click button ok

            driver.SwitchTo().Window(currentWindow18);

            //Wait until List stopped
            int count = 1;
            while (true)
            {
               Custommethods.Click(driver, testname, "//div[@id='searchButton']//img", "XPath");
                try
                {
                    count++;
                    bool resulte = driver.FindElements(By.XPath("//td[contains(text(),'" + worktypename + "(Inactive)')]")).Count > 0 && driver.FindElement(By.XPath("//td[contains(text(),'" + worktypename + "(Inactive)')]")).Displayed;
                    bool resulte2 = driver.FindElements(By.XPath("//td[contains(text(),'" + worktypename + "(Active)')]")).Count > 0 && driver.FindElement(By.XPath("//td[contains(text(),'" + worktypename + "(Active)')]")).Displayed;
                    bool resulte3 = driver.FindElements(By.XPath("//td[contains(text(),'" + worktypename + "(Dialing)')]")).Count > 0 && driver.FindElement(By.XPath("//td[contains(text(),'" + worktypename + "(Dialing)')]")).Displayed;
                    bool resulte4 = driver.FindElements(By.XPath("//td[contains(text(),'" + worktypename + "(Unmanned)')]")).Count > 0 && driver.FindElement(By.XPath("//td[contains(text(),'" + worktypename + "(Unmanned)')]")).Displayed;
                    if (resulte)
                    {
                        Thread.Sleep(10000);
                    }
                    if (resulte2)
                    {
                        Thread.Sleep(10000);
                    }
                    if (resulte3)
                    {
                        Thread.Sleep(10000);
                    }
                    if (resulte4)
                    {
                        Thread.Sleep(10000);
                    }
                    if (count > 5)
                    {

                       Custommethods.Click(driver, testname, "//*[@id='divTableCont1']/table/tbody/tr[2]/td[1]/input", "XPath");
                        //Select List to Start

                        string currentWindow180 = driver.CurrentWindowHandle;

                       Custommethods.Click(driver, testname, "//input[@name='buttonStop']", "XPath");
                        //Clisk on Stop 

                        driver.SwitchTo().Window(driver.WindowHandles.Last());

                        // Find the checkbox or radio button element by Name
                        IList<IWebElement> oCheckBox0 = driver.FindElements(By.Name("cblist[]"));

                        // This will tell you the number of checkboxes are present
                        int Size0 = oCheckBox0.Count;

                        //Report


                        // Start the loop from first checkbox to last checkboxe
                        for (int i = 1; i <= Size0; i++)
                        {

                            // Store the checkbox name to the string variable, using 'Value' attribute
                            String Value = driver.FindElement(By.XPath($"//*[@id='divTableCont1']/table/tbody/tr[{i}]/td[2]")).Text;

                            // Select the checkbox it the value of the checkbox is same what you are looking for
                            if (Value.Equals(worktypename))
                            {
                                int j = i - 1;
                                oCheckBox0.ElementAt(j).Click();
                                // This will take the execution out of for loop

                                //Report


                                break;

                            }
                        }

            //Select WorkType

            (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("buttonOk")))).Click();
                        //Click button ok

                        driver.SwitchTo().Window(currentWindow180);

                        break;
                    }
                    else { break; }
                }
                catch (Exception e)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(e.ToString());
                }
                Thread.Sleep(100);
            }

            //Report

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            //                                                                         Logout From ALM
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
           Custommethods.Click(driver, testname, "//*[@id='header']/div/a", "XPath");

            driver.Close();
            driver.Quit();

        }

        public static void Generate_Voicecalls_prophecy(IWebDriver driver, ExtentTest test, string testname, string DNIS, string org)
        {
            test.Info("Generate_Voicecalls_prophecy");
            string url = "http://10.200.252.4:9999/SessionControl/CCXML10.start?tokenid=5Minutes&callerid=sip:9999%4010.200.252.4&calledid=sip:%2B1" + DNIS + "%40sip." + org + ".brooklyn.aspect-cloud.net:7777";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //optional
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
        }

        public static void VUE_Create_VoiceWT(IWebDriver driver, ExtentTest test, string testname, string name, string dispplan, string sch, string tz, string user, string dnis, string cxdeploy, string cxtemp, string arr, string srr, string projectname, string projectver, string servicename)
        {

            test.Info("VUE_Create_VoiceWT");
            //double viaver = Convert.ToDouble(viaversion);

           Custommethods.Click(driver, testname, "//*[@id='navbarToggleLink']/i", "XPath");
           Custommethods.Click(driver, testname, "dashboard-id-myDashboard", "Id");
           Custommethods.Click(driver, testname, "//a[@href='/manux/dashboard/inboundWorkType']", "XPath");

            //Select CAllTYPE

           Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[1]/span/span/span[1]", "XPath");
            Actions ctdd = new Actions(driver);
            IWebElement calltypedd = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[1]/span/span/span[1]"));
            ctdd.MoveToElement(calltypedd).Perform();
            ctdd.SendKeys(Keys.Enter).Perform();
            ctdd.SendKeys(Keys.ArrowDown).Perform();

            //enter Worktype Name
           Custommethods.Click(driver, testname, "inbound-worktype", "Id");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(x => x.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[@class='cg-busy cg-busy-backdrop cg-busy-backdrop-animation ng-scope ng-hide']")));
           Custommethods.EnterText(driver, testname, "//input[@id='worktypeName']?//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[3]/span/span/span[1]", Keys.Control + "a", "XPath");
           Custommethods.EnterText(driver, testname, "//input[@id='worktypeName']?//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[3]/span/span/span[1]", name, "XPath");

            //Select Disposition Plan
           Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[4]/span/span/span[1]", "XPath");
            Actions dropdown1 = new Actions(driver);
            IWebElement drp = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[4]/span/span/span[1]"));
            dropdown1.MoveToElement(drp).Perform();
            dropdown1.SendKeys(dispplan).Perform();

            //Select Schedule
           Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[5]/span/span/span[1]", "XPath");
            Actions dropdown2 = new Actions(driver);
            IWebElement schedule = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[1]/div[5]/span/span/span[1]"));
            dropdown2.MoveToElement(schedule).Perform();
            dropdown2.SendKeys(sch).Perform();

            //Select TimeZone

           Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[1]", "XPath");
            Actions dropdown3 = new Actions(driver);
            IWebElement timezone = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[1]"));
            dropdown3.MoveToElement(timezone).Perform();
            Thread.Sleep(200);
            dropdown3.SendKeys(tz).Perform();
            Thread.Sleep(200);


            Actions dropdown3a = new Actions(driver);
            IWebElement timezonea = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[1]"));
            dropdown3a.MoveToElement(timezonea).Perform();
            Thread.Sleep(200);
            dropdown3a.SendKeys(Keys.Enter).Perform();


            //Select CX Deployment Option
            if (cxdeploy == "Deploy CX Template")
            {
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[2]/span/span/span[1]", "XPath");
                Actions dropdown4 = new Actions(driver);
                IWebElement cxdo = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[2]/span/span/span[1]"));
                dropdown4.MoveToElement(cxdo).Perform();
                Thread.Sleep(10);
                dropdown4.SendKeys(Keys.Enter).Perform();
                wait.Until(x => x.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[@class='cg-busy cg-busy-backdrop cg-busy-backdrop-animation ng-scope ng-hide']")));

               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[5]/span/span/span[1]", "XPath");
                Actions dropdown5 = new Actions(driver);
                IWebElement cxt = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[5]/span/span/span[1]"));
                dropdown5.MoveToElement(cxt).Perform();
                if (cxtemp == "Basic Routing Voice")
                {
                    ///Select CX Template Name - Basic routing voice
                    dropdown5.SendKeys("b").Perform();
                }
                else if (cxtemp == "Auto-Attendant Menu Voice")
                {
                    ///Select CX Template Name - Auto attendant

                    dropdown5.SendKeys("a").Perform();
                }
                else
                {
                    Custommethods.TakeScreenshot(driver, DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt") + ".png");

                }

                //Select Audio Only Recording Rule
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[1]", "XPath");
                Actions cxtaordd = new Actions(driver);
                IWebElement cxtaor = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[1]"));
                cxtaordd.MoveToElement(cxtaor).Perform();
                cxtaordd.SendKeys(arr).Perform();

                //Select Audio screen Recording Rule
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']//table//tr//th[4]//a[1]", "XPath");
                Actions cxtasrdd = new Actions(driver);
                IWebElement cxtasr = driver.FindElement(By.XPath("//*[@id='inboundWorktype-availableAgents']//table//tr//th[4]//a[1]"));
                cxtasrdd.MoveToElement(cxtasr).Perform();
                cxtasrdd.SendKeys(srr).Perform();

                //User Selection
               Custommethods.Click(driver, testname, "//*[contains(text(),'Is equal to')] | //*[contains(text(),'Contains')]", "XPath");
               Custommethods.Click(driver, testname, "//*[contains(text(),'Show items with value that:')]/../input[1]", "XPath");
                Actions contain = new Actions(driver);
                IWebElement containdd = driver.FindElement(By.XPath("//*[contains(text(),'Show items with value that:')]/../input[1]"));
                contain.MoveToElement(containdd).Perform();
                contain.SendKeys("c").Perform();
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../div[2]/button[1]", Keys.Control + "a", "XPath");
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../div[2]/button[1]", user, "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[1]/td/p/a[@class='k-icon k-i-expand']", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[2]/td[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='agents']/div[2]/button[2]/i", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='availableNumbers']//div[1]//div//table//tr//th[2]//a[1]", "XPath");

                //DNIS Selection
                //Clear Cache
               Custommethods.Click(driver, testname, "/html/body/div[48]/form/div[1]/div[2]/button[2]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='availableNumbers']/div[2]/table/tbody", "XPath");
                //Custommethods.Click(driver, testname,  "//*[contains(text(),'Clear')]", "XPath");

                string adnis = driver.FindElement(By.XPath("//*[@id='availableNumbers']//div[1]//div//table//tr//th[2]//a[1]")).Text;
                if (adnis.Contains(dnis))
                {
                   Custommethods.Click(driver, testname, "/html/body/div[48]/form/div[1]/input[1]", "XPath");
                   Custommethods.EnterText(driver, testname, "/html/body/div[48]/form/div[1]/div[2]/button[1]", Keys.Control + "a", "XPath");
                   Custommethods.EnterText(driver, testname, "/html/body/div[48]/form/div[1]/div[2]/button[1]", dnis, "XPath");
                   Custommethods.Click(driver, testname, "//*[@id='availableNumbers']/div[2]/table/tbody/tr/td[1]/input", "XPath");
                   Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[1]/button[2]", "XPath");
                }


            }
            else if (cxdeploy == "Deploy CX Project")
            {
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[2]", "XPath");
                Actions dropdown4 = new Actions(driver);
                IWebElement cxdo = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[2]"));
                dropdown4.MoveToElement(cxdo).Perform();
                dropdown4.SendKeys(Keys.ArrowDown).Perform();
                dropdown4.SendKeys(Keys.Enter).Perform();
                wait.Until(x => x.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[@class='cg-busy cg-busy-animation ng-scope ng-hide']")));

                //select project
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[3]/span/span/span[2]/span", "XPath");
                Actions pdd = new Actions(driver);
                IWebElement pro = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[3]/span/span/span[2]/span"));
                pdd.MoveToElement(pro).Perform();
                pdd.SendKeys(projectname).Perform();
                pdd.SendKeys(Keys.Enter).Perform();
                wait.Until(x => x.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[@class='cg-busy cg-busy-animation ng-scope ng-hide']")));

                //select version
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[4]/span/span/span[2]/span", "XPath");
                Actions pversion = new Actions(driver);
                IWebElement pver = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[4]/span/span/span[2]/span"));
                pversion.MoveToElement(pver).Perform();
                pversion.SendKeys(projectver).Perform();



                //Select Audio Only Recording Rule
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[5]/span/span/span[2]/span", "XPath");
                Actions cxpaordd = new Actions(driver);
                IWebElement cxpaor = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[5]/span/span/span[2]/span"));
                cxpaordd.MoveToElement(cxpaor).Perform();
                cxpaordd.SendKeys(arr).Perform();

                //Select Audio screen Recording Rule
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[2]/span", "XPath");
                Actions cxpasrdd = new Actions(driver);
                IWebElement cxpasr = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[2]/span"));
                cxpasrdd.MoveToElement(cxpasr).Perform();
                cxpasrdd.SendKeys(srr).Perform();

                //User Selection
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']//table//tr//th[4]//a[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[contains(text(),'Is equal to')] | //*[contains(text(),'Contains')]", "XPath");
                Actions contain = new Actions(driver);
                IWebElement containdd = driver.FindElement(By.XPath("//*[contains(text(),'Is equal to')] | //*[contains(text(),'Contains')]"));
                contain.MoveToElement(containdd).Perform();
                contain.SendKeys("c").Perform();
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../input[1]", Keys.Control + "a", "XPath");
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../input[1]", user, "XPath");
               Custommethods.Click(driver, testname, "//*[contains(text(),'Show items with value that:')]/../div[2]/button[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[1]/td/p/a[@class='k-icon k-i-expand']", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[2]/td[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='agents']/div[2]/button[2]/i", "XPath");

                if (dnis == "")
                {

                }
                else
                {
                    string adnis = driver.FindElement(By.XPath("//*[@id='availableNumbers']/div[2]/table/tbody")).Text;
                    if (adnis.Contains(dnis))
                    {
                        //DNIS Selection
                       Custommethods.Click(driver, testname, "//*[@id='availableNumbers']//div[1]//div//table//tr//th[2]//a[1]", "XPath");
                       Custommethods.EnterText(driver, testname, "/html/body/div[48]/form/div[1]/input[1]", Keys.Control + "a", "XPath");
                       Custommethods.EnterText(driver, testname, "/html/body/div[48]/form/div[1]/input[1]", dnis, "XPath");
                       Custommethods.Click(driver, testname, "/html/body/div[48]/form/div[1]/div[2]/button[1]", "XPath");
                       Custommethods.Click(driver, testname, "//*[@id='availableNumbers']/div[2]/table/tbody/tr/td[1]/input", "XPath");
                    }
                }


            }
            else if (cxdeploy == "Map Pre-Deployed CX Service")
            {

               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[2]", "XPath");
                Actions cxdodd = new Actions(driver);
                IWebElement cxdo = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[1]/span/span/span[2]"));
                cxdodd.MoveToElement(cxdo).Perform();
                cxdodd.SendKeys(Keys.ArrowDown).Perform();
                cxdodd.SendKeys(Keys.ArrowDown).Perform();
                cxdodd.SendKeys(Keys.Enter).Perform();
                wait.Until(x => x.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[@class='cg-busy cg-busy-animation ng-scope ng-hide']")));

                //Select CX Service
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[4]/span/span/span[2]", "XPath");
                Actions service = new Actions(driver);
                IWebElement sname = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[4]/span/span/span[2]"));
                service.MoveToElement(sname).Perform();
                service.SendKeys(servicename).Perform();


                //Select Audio Only Recording Rule

               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[2]", "XPath");
                Actions cxsaordd = new Actions(driver);
                IWebElement cxsaor = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[6]/span/span/span[2]"));
                cxsaordd.MoveToElement(cxsaor).Perform();
                cxsaordd.SendKeys(arr).Perform();

                //Select Audio screen Recording Rule
               Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[7]/span/span/span[2]", "XPath");
                Actions cxsasrdd = new Actions(driver);
                IWebElement cxsasr = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/form/div/div[2]/div[1]/ng-include/div/div/div[2]/div[7]/span/span/span[2]"));
                cxsasrdd.MoveToElement(cxsasr).Perform();
                cxsasrdd.SendKeys(srr).Perform();

                //User Selection

               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']//table//tr//th[4]//a[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[contains(text(),'Is equal to')] | //*[contains(text(),'Contains')]", "XPath");
                Actions contain = new Actions(driver);
                IWebElement containdd = driver.FindElement(By.XPath("//*[contains(text(),'Is equal to')] | //*[contains(text(),'Contains')]"));
                contain.MoveToElement(containdd).Perform();
                contain.SendKeys("c").Perform();
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../input[1]", Keys.Control + "a", "XPath");
               Custommethods.EnterText(driver, testname, "//*[contains(text(),'Show items with value that:')]/../input[1]", user, "XPath");
               Custommethods.Click(driver, testname, "//*[contains(text(),'Show items with value that:')]/../div[2]/button[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[1]/td/p/a[@class='k-icon k-i-expand']", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='inboundWorktype-availableAgents']/div[2]/table/tbody/tr[2]/td[1]", "XPath");
               Custommethods.Click(driver, testname, "//*[@id='agents']/div[2]/button[2]/i", "XPath");

                // DNIS Selection

                if (dnis == "")
                {

                }
                else
                {
                    //Clear Cache
                   Custommethods.Click(driver, testname, "/html/body/div[48]/form/div[1]/div[2]/button[2]", "XPath");
                   Custommethods.Click(driver, testname, "/html/body/div[52]/form/div[1]/div[2]/button[2]", "XPath");

                    string adnis = driver.FindElement(By.XPath("//*[@id='availableNumbers']/div[2]/table/tbody")).Text;
                    if (adnis.Contains(dnis))
                    {
                        //DNIS Selection
                       Custommethods.Click(driver, testname, "//*[@id='availableNumbers']//div[1]//div//table//tr//th[2]//a[1]", "XPath");
                       Custommethods.EnterText(driver, testname, "/html/body/div[52]/form/div[1]/input[1]", Keys.Control + "a", "XPath");
                       Custommethods.EnterText(driver, testname, "/html/body/div[52]/form/div[1]/input[1]", dnis, "XPath");
                       Custommethods.Click(driver, testname, "/html/body/div[52]/form/div[1]/div[2]/button[1]", "XPath");
                       Custommethods.Click(driver, testname, "//*[@id='availableNumbers']/div[2]/table/tbody/tr/td[1]/input", "XPath");
                    }
                }

            }
            else
            {
                //Console.WriteLine("Invalid CX Deployment Option");
            }

            //Deploy
           Custommethods.Click(driver, testname, "//*[@id='inbound-worktype']/form/div/div[1]/button[2]", "XPath");


            //Finish

            WebDriverWait finish = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            finish.Until(drv => drv.FindElement(By.XPath("//*[@id='inbound-worktype']/div/div[2]/div/div[3]/button")));
            string deploymsg = driver.FindElement(By.XPath("//*[@id='inbound-worktype']/div/div[2]/div")).Text;

            //verification Point - Verify the succedd messge after deploy the worktype
            if (deploymsg.Contains("success"))
            {
                //Custommethods.Click(driver, testname,  "//*[@id='ngdialog1']/div[2]/div/div[3]/button", "XPath");
               Custommethods.Click(driver, testname, "//button[contains(text(),'Finish')]", "XPath");

            }
            else
            {
                //Console.WriteLine(deploymsg);
                Custommethods.TakeScreenshot(driver, DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt"));
               Custommethods.Click(driver, testname, "//button[contains(text(),'OK')]", "XPath");
            }

        }

        public static void VUE_Preview_Dial(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Preview_Dial");
            Custommethods.Click(driver, testname, "newCallWidget-btnDial", "Id");
        }

        public static void VUE_Wait_for_Audiopath(IWebDriver driver, ExtentTest test, string testname, int timeinsec)
        {
            test.Info("VUE_Wait_for_Audiopath");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeinsec));
            (wait.Until(x => x.FindElement(By.XPath("//div[@title='Audio Path Connected']")))).Click();
            Custommethods.Click(driver, testname, "//*[@id='sipInfoModal']/div/div[3]/button", "XPath");
        }

        public static void VUE_Check_for_Audiopath(IWebDriver driver, ExtentTest test, string testname)
        {
            test.Info("VUE_Check_for_Audiopath");
            Custommethods.Click(driver, testname, "//div[@title='Audio Path Connected']", "XPath");
            Custommethods.Click(driver, testname, "//*[@id='sipInfoModal']/div/div[3]/button", "XPath");
        }




    }
}
