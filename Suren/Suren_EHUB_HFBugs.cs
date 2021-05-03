using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Suren_nunit.Page;
using Suren_nunit.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace Suren_nunit
{
    [TestFixture]
    public class Suren_EHUB_HFBugs : Driverhelper
    {
        ExtentReports extent = null;

        public string get(string testname, string inp)
        {
            string parentdir = Environment.CurrentDirectory.ToString();
            string filepath = Path.GetFullPath(Path.Combine(parentdir, @"..\..\..\Suren"));
            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = filepath + "\\Suren_EHUB_HF.config" };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            string value = config.AppSettings.Settings[testname + "_" + inp].Value;
            return value;
        }

        [OneTimeSetUp]
        public void Extentstart()
        {
            string parentdir = Environment.CurrentDirectory.ToString();
            string filepath = Path.GetFullPath(Path.Combine(parentdir, @"..\..\..\Suren"));
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@parentdir + "\\Logs\\");
            extent.AttachReporter(htmlReporter);
        }

        [OneTimeTearDown]
        public void ExtentStop()
        {
            extent.Flush();
        }

        [Test]
        public void EHUB_13825()
        {

            //PreReq: Agent 2 should be ondemand Agent

            var testname = "EHUB_13825";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);

            try
            {
                test = extent.CreateTest(testname).Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));

                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");

                //Agent-1 consults to worktype and Agent-2 is located
                MethodsVUE.VUE_Consult_Worktype(Driver1, test, testname, get(testname, "cons_wt"));
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                //Agent-1 presses conference and then transfers the caller to Agent-2 directly (Agent-1 dropped out at this point)
                MethodsVUE.VUE_Conference(Driver, test, testname);
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Logout(Driver, test, testname);

                //Agent-2 consults to worktype (no agent available)
                MethodsVUE.VUE_Consult_Worktype(Driver1, test, testname, get(testname, "wt3"));
                //Agent-2 conferences the caller and the consult call together
                MethodsVUE.VUE_Conference(Driver1, test, testname);
                //Agent-2 releases the caller leaving him with just the consult worktype call (still queuing)
                MethodsVUE.VUE_Active_Line(Driver1, test, testname, "Line1");
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));

                //Agent-2 makes external call on other line
                MethodsVUE.VUE_Manual_Dial(Driver1, test, testname, get(testname, "ext_num"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");

                //Agent-2 releases the caller leaving him with just the consult worktype call (still queuing)
                MethodsVUE.VUE_Active_Line(Driver1, test, testname, "Line1");
                //Agent-2 attempts to transfer the worktype call to the external call - EHUB rejects this and error message  - Invalid agent index is popped up on VUE
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                //In the above step agent able to transfer the call to external number so the below steps are not valid
                //Agent-2 repeats this request multiple times
                //Agent-2 releases the worktype call (leaving him with the external call only)
                //Agent-2 consults to worktype (no agent available)
                //Agent-2 attempts to transfer the worktype call to the external call - EHUB allows this

                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Logout(Driver1, test, testname);

                Driver.Quit();
                Driver1.Quit();
                test.Info("Execution Completed");
                test.Log(Status.Pass);
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }


        }

        [Test]
        public void EHUB_13941()
        {

            //PreReq: Agent 2 should be ondemand Agent

            var testname = "EHUB_13941";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);


            try
            {
                test = extent.CreateTest(testname).Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));

                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Check_for_Audiopath(Driver, test, testname);

                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Check_for_Audiopath(Driver, test, testname);

                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));

                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");

                //worktype consult
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));

                //Agent 1 hold consultation Call C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve call C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 hold call C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 Hold C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 Retrieve C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");

                // OnDemand Agent 2 with audio call became Idle and got the call while the call was held by Agent 1
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                //Agent 2 request NotReady and was in NotReady-Pending State
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                //Caller in C1 dropped and was in Wrap State
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                // Agent 1 retrieve consultation call C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //. Agent 1 disposition the call in Line 1 and back to idle in Line 1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                //Agent 1 hold the call C2 in Line 2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 made external consultation Call C3
                MethodsVUE.VUE_Manual_Dial(Driver, test, testname, get(testname, "ext_num"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
                //Agent 1 hold call C3
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 hold C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C3
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                // Agent 1 transferred the call
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                //Agent 1 back to idle
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Logout(Driver, test, testname);
                //Agent 2 made an external consultation
                MethodsVUE.VUE_Manual_Dial(Driver1, test, testname, get(testname, "ext_num"));
                //Agent 2 transferred the call
                MethodsVUE.VUE_Warm_Transfer(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Logout(Driver1, test, testname);
                test.Log(Status.Pass);
                test.Info("Ececution Completed");
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }


        }

        [Test]
        public void EHUB_19074()
        {

            //PreReq: Agent 2 should be ondemand Agent

            var testname = "EHUB_19074";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);

            try
            {

                test = extent.CreateTest(testname).Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));
                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));

                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");

                //worktype consult
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));

                //Agent 1 hold consultation Call C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve call C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 hold call C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 Hold C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 Retrieve C1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");

                // OnDemand Agent 2 with audio call became Idle and got the call while the call was held by Agent 1
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                //Agent 2 request NotReady and was in NotReady-Pending State
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                //Caller in C1 dropped and was in Wrap State
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                // Agent 1 retrieve consultation call C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 disposition the call in Line 1 and back to idle in Line 1
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                //Agent 1 hold the call C2 in Line 2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 made external consultation Call C3
                MethodsVUE.VUE_Manual_Dial(Driver, test, testname, get(testname, "ext_num"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
                //Agent 1 hold call C3
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                //Agent 1 hold C2
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");
                //Agent 1 retrieve C3
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                // Agent 1 transferred the call
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                //Agent 1 back to idle
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Logout(Driver, test, testname);
                //Agent 2 made an external consultation
                MethodsVUE.VUE_Manual_Dial(Driver1, test, testname, get(testname, "ext_num"));
                //Agent 2 transferred the call
                MethodsVUE.VUE_Warm_Transfer(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Logout(Driver1, test, testname);

            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }



        }

        [Test]
        public void EHUB_18585_1()
        {

            //After  Executing this scenario verify EHUB logs, should not see "ghost CallIndex<>"
            //and verify CC2DCP logs should not see "Duplicate Call"
            // get(testname, "user2") should be an On-Demand Agent

            //Scenario 1 :

            var testname = "EHUB_18585";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);
            try
            {
                test = extent.CreateTest(testname+"_1").Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));

                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Check_for_Audiopath(Driver, test,  testname);
                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
                //worktype consult
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));

                //Login Agent 2 receive WT consultation call
                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_for_Call(Driver1, test, testname, 120);

                //Agent1 conference then transfer the call
                MethodsVUE.VUE_Conference(Driver, test, testname);

                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "INTERNAL", "");

                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                //Agetn 2 hangup
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.VUE_Logout(Driver, test, testname);
                MethodsVUE.VUE_Logout(Driver1, test, testname);
                test.Info("Excecution Completed");
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }

            
        }

        [Test]
        public void EHUb_18585_2()
        {
            //Scenario 2 :
            // for Ondemand Agent

            var testname = "EHUB_18585";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);
            try
            {
                test = extent.CreateTest(testname + "_2").Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));

                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                //Login Agent 2 receive WT consultation call
                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "ondemanduser"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");


                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "IDLE", "");

                //worktype consult
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));

                //Agent2 receivest he call
                MethodsVUE.VUE_Wait_for_Call(Driver1, test, testname, 120);
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "INTERNAL", "");
                MethodsVUE.VUE_Conference(Driver, test, testname);

                //Agent1 conference then transfer the call
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                //Agetn 2 hangup
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.VUE_Logout(Driver, test, testname);
                MethodsVUE.VUE_Logout(Driver1, test, testname);
                test.Info("Execution Completed");
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }

        }

        [Test]
        public void EHUB_18585_3()
        {

               //Sceanario 3: Hold consult call while transfer

                 var testname = "EHUB_18585";
             ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);
                 try
                 {
                     test = extent.CreateTest(testname+"_3").Info("Execution Started");
                     Driver.Navigate().GoToUrl(get(testname, "url"));
                     Driver1.Navigate().GoToUrl(get(testname, "url"));

                     //Agent 1 got inbound call C1 and consult WT 1
                       MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                     MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                     MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
                     //worktype consult
                     MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));
                     MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                     MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");

                     //Login Agent 2 receive WT consultation call
                     MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                     MethodsVUE.VUE_Wait_for_Call(Driver1, test, testname, 120);
                     MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                     MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                     MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                     MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "INTERNAL", "");

                     //Agent2 ends the consult call
                     MethodsVUE.VUE_Hangup(Driver1, test, testname);
                     MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                     MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                     MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                     MethodsVUE.VUE_Hangup(Driver, test, testname);
                     MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                     MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                     MethodsVUE.VUE_Logout(Driver, test, testname);
                     MethodsVUE.VUE_Logout(Driver1, test, testname);
                     test.Info("Execution completed");
                 }
                 catch (Exception e)
                 {
                     var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                     Thread.Sleep(2000);
                     string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                     string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                     test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                     test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                     test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                     throw;
                 }
                 finally
                 {
                     if (Driver != null)
                     {
                         Driver.Quit();
                     }
                     if (Driver != null)
                     {
                         Driver1.Quit();
                     }
                 }
   
        }

        [Test]
        public void EHUB_18585_4()
        {
            //Sceanario 4: Hold consult call while transfer
            var testname = "EHUB_18585";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);

            try
            {
                test = extent.CreateTest(testname + "_4").Info("Execution Started");
                 Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));

                //Agent 1 got inbound call C1 and consult WT 1
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
           
                //Login Agent 2 receive WT consultation call
                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "ondemanduser"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));

                //worktype consult
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "HOLD");

                //Agent2 receives the call
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Wait_for_Call(Driver1, test, testname, 120);
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "INTERNAL", "");
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line2");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
              
                //Agent2 ends the consult call
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hold_Action(Driver, test, testname, "UNHOLD");
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                MethodsVUE.VUE_Logout(Driver, test, testname);
                MethodsVUE.VUE_Logout(Driver1, test, testname);
                test.Info("Execution Completed");


            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }
        }

        [Test]
        public void EHUB_13832()
        {

            //PreReq: Agent 2 should be ondemand Agent

            var testname = "EHUB_13832";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);

           
            try
            {
                test = extent.CreateTest(testname).Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1"), get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");

                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "NOTREADY", get(testname, "nr_rc"));

                //Agent 1 got voice call from Worktype 1 in Line 1
                MethodsVUE.Generate_Voicecalls_prophecy(Driver, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");

                //Agent 1 made consultation to Worktype 2 and queued for Agent
                MethodsVUE.VUE_Consult_Worktype(Driver, test, testname, get(testname, "cons_wt"));
                //Agent 1 conference the call
                MethodsVUE.VUE_Conference(Driver, test, testname);

                //Agent 1 drop the call in Line 1 and was in Wrap state
                MethodsVUE.VUE_Active_Line(Driver, test, testname, "Line1");
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                //Agent 1 disposition the call in Line 1
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                //Agent 1 made external consultation
                MethodsVUE.VUE_Manual_Dial(Driver, test, testname, get(testname, "ext_num"));
                //Agent 1 requested to transfer the call while consultation call was still queuing in Worktype
                MethodsVUE.VUE_Warm_Transfer(Driver, test, testname);
                //Agent 2 became idle in Worktype 2 and got the transferred call
                MethodsVUE.VUE_Change_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");

                MethodsVUE.VUE_Logout(Driver, test, testname);
                MethodsVUE.VUE_Logout(Driver1, test, testname);

                Driver.Quit();
                Driver1.Quit();
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }



        }

        [Test]
        public void EHUB_13819()
        {
            //PreReq: Agent 2 should be ondemand Agent

            var testname = "EHUB-13819";
            ExtentTest test = null;

            var Driver = new Driverhelper().Init(driver);
            var Driver1 = new Driverhelper().Init(driver);

            try
            {
                test = extent.CreateTest(testname).Info("Execution Started");
                Driver.Navigate().GoToUrl(get(testname, "url"));
                Driver1.Navigate().GoToUrl(get(testname, "url"));
                //OnDemand Agent 1 in Park no audio
                MethodsVUE.VUE_Login(Driver, test, testname, get(testname, "user1") + "@" + get(testname, "org") + ".com", get(testname, "password"), get(testname, "station1"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "IDLE", "");
                MethodsVUE.VUE_Change_State(Driver, test, testname, "NOTREADY", get(testname, "nr_rc"));
                MethodsVUE.VUE_Change_State(Driver, test, testname, "PARK", get(testname, "park_rc"));

                //Agent 1 dial external num
                MethodsVUE.VUE_Manual_Dial(Driver, test, testname, get(testname, "ext_num"));
                // MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVEMANUAL", "");
                //Agent 1 dropped the call and back to Park no audio
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "PARK", get(testname, "park_rc"));
                //Agent 2 consulted with Agent
                //Agent 1 timeout and was in NotReady
                //Agent 1 went to Park
                MethodsVUE.VUE_Login(Driver1, test, testname, get(testname, "user2"), get(testname, "password"), get(testname, "station2"), "IDLE");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                MethodsVUE.Generate_Voicecalls_prophecy(Driver1, test, testname, get(testname, "dnis"), get(testname, "org"));
                MethodsVUE.VUE_Accept_Reject(Driver1, test, testname, "Enabled");
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "ACTIVE", "");
                MethodsVUE.VUE_Consult_User(Driver1, test, testname, get(testname, "user1"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "PARK", "Controlled Call Connection Timeout");

                //Agent 2 consult with Agent 1
                MethodsVUE.VUE_Active_Line(Driver1, test, testname, "Line2");
                MethodsVUE.VUE_Hangup(Driver1, test, testname);
                MethodsVUE.VUE_Consult_User(Driver1, test, testname, get(testname, "user1"));
                //Agent 1 accept the call
                MethodsVUE.VUE_Accept_Reject(Driver, test, testname, "Enabled");
                //Agent 2 transfer the call
                MethodsVUE.VUE_Warm_Transfer(Driver1, test, testname);
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "ACTIVE", "");
                //Agent 1 complete the call
                MethodsVUE.VUE_Dispose_Call(Driver1, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver1, test, testname, "IDLE", "");
                //Agent 1 logout
                MethodsVUE.VUE_Logout(Driver1, test, testname);

                //clear calls
                MethodsVUE.VUE_Hangup(Driver, test, testname);
                MethodsVUE.VUE_Dispose_Call(Driver, test, testname, get(testname, "disp"));
                MethodsVUE.VUE_Wait_For_State(Driver, test, testname, "PARK", "Controlled Call Connection Timeout");
                //Agent 1 logout
                MethodsVUE.VUE_Logout(Driver, test, testname);

                Driver.Quit();
                Driver1.Quit();
            }
            catch (Exception e)
            {
                var screenShotName = DateTime.Now.ToString("yyyy-MM-dd h-mm-ss-tt");
                Thread.Sleep(2000);
                string filePath = Custommethods.TakeScreenshot(Driver, screenShotName);
                string filePath1 = Custommethods.TakeScreenshot(Driver1, screenShotName + "1_");
                test.Log(Status.Fail, "" + "<pre>" + e + "</pre>");
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath));
                test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(filePath1));
                throw;
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                }
                if (Driver != null)
                {
                    Driver1.Quit();
                }
            }


        }
    }

}

