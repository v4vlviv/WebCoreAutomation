using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Firefox;
using System;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace WebCoreAutoTests.Tools
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Opera,
        IE
    }
    class TestHelper : DriverInit
    {
        public static readonly int TIMESPAN = 20;
        public static readonly int TIMEWAIT = 10;
        public static readonly string URL = $"http://kil777.westus.azurecontainer.io/";
        protected bool isTestSuccess = true;

        BrowserType _browserType;

        // Reporting
        public static ExtentReports extent;
        public static ExtentTest test;

        public TestHelper(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var fileName = this.GetType().ToString() + ".html";
            var htmlReporter = new ExtentHtmlReporter(dir + fileName);

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        protected virtual void SetUp()
        {
            Initialization(_browserType);
            driver.Navigate().GoToUrl(URL);
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            if (!isTestSuccess)
            {
                TakeScreenshot();
            }
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extent.Flush();
        }

        public void TakeScreenshot()
        {
            var counter = DateTime.Now.Ticks.ToString();
            string projectPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), counter);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile((projectPath + ".jpg").ToString(), OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            Console.WriteLine(projectPath);
        }

        public static void Initialization(BrowserType browserType)
        {
            ChooseDriverInstance(browserType);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TIMESPAN);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEWAIT);            
        }

        private static void ChooseDriverInstance(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        try
                        {
                            DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                            driver = new RemoteWebDriver(new Uri("http://0.0.0.0:4444/wd/hub/"), 
                                capabilities, TimeSpan.FromSeconds(200));
                        }
                        catch (Exception)
                        {
                            // Only for Windows
                            driver = new ChromeDriver(".");
                        }
                        break;

                    }
                case BrowserType.Firefox:
                    {
                        try
                        {
                            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                            driver = new RemoteWebDriver(new Uri("http://0.0.0.0:4444/wd/hub/"), 
                                capabilities, TimeSpan.FromSeconds(200));
                        }
                        catch (Exception)
                        {
                            // Only for Windows
                            driver = new FirefoxDriver(".");
                        }                        
                        break;
                    }
                case BrowserType.Opera:
                    {
                        driver = new OperaDriver();
                        break;
                    }
                case BrowserType.IE:
                    {
                        driver = new InternetExplorerDriver();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong option");
                        break;
                    }
            }
        }
    }
}
