using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Firefox;
using System;

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
        public virtual string URL(string fullUrl) => $"http://kil777.westus.azurecontainer.io/{fullUrl}";
        BrowserType _browserType;

        [OneTimeSetUp]
        protected void OneTimeSetup()
        {

        }

        [SetUp]
        protected virtual void SetUp()
        {
            var browserType = TestContext.Parameters.Get("Browser", "Firefox");
            //Parse the browser Type, since its Enum
            _browserType = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            //Pass it to browser
            Initialization(_browserType);
            driver.Navigate().GoToUrl(URL(""));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }


        public static void Initialization(BrowserType browserType)
        {
            ChooseDriverInstance(browserType);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TIMESPAN);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEWAIT);            
        }

        //do not use
        public static void NavigateToPage(string partURL)
        {
           // driver.Navigate().GoToUrl(URL(partURL));
        }

        private static void ChooseDriverInstance(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        driver = new FirefoxDriver();
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
