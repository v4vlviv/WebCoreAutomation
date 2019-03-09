using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebCoreAutoTests.Tools;

namespace WebCoreAutoTests.Pages
{
    class HomePage:DriverInit
    {
        IWebElement carousel => driver.FindElement(By.Id("myCarousel"));
        IWebElement hellostring => driver.FindElement(By.XPath("//*[contains(text(),'Hello')]"));

        public bool CarouselIsExist()
        {
            bool isExist = IsElementVisible(carousel);
            return true;
        }

        public bool IsElementVisible(IWebElement element)
        {
            return carousel.Displayed && carousel.Enabled;
        }

        public string VerifyUserName()
        {
            string nameOfUser = hellostring.Text;
            return nameOfUser;
        }
    }
}
