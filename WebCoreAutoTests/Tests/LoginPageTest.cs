using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebCoreAutoTests.Pages;
using WebCoreAutoTests.Pages.NonLoginUserPages;
using WebCoreAutoTests.Tools;

namespace WebCoreAutoTests.Tests
{
    [TestFixture]
    class LoginPageTest : TestHelper
    {
        LoginPage loginPage;
        HomePage homePage;

        public LoginPageTest() : base(BrowserType.Firefox) { }

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            loginPage = new LoginPage();
        }

        [Test]
        [Parallelizable]
        public void LoginTestFF()
        {
            try
            {
                string expected = "Hello test@testfromcode.com!";
                homePage = loginPage.Login();
                string actual = homePage.VerifyUserName();
                StringAssert.AreEqualIgnoringCase(expected, actual, $"Name should be Hello test@testfromcode.com! but was {actual}");
            }
            catch (Exception ex)
            {
                test.Fail(ex.StackTrace);
                test.Fail(ex.Message);
                isTestSuccess = false;
            }           
        }

        [Test]
        public void VerifyCarouselIsShownFF()
        {
            try
            {
                bool expected = true;
                homePage = loginPage.Login();
                bool actual = homePage.CarouselIsExist();
                Assert.That(actual, Is.EqualTo(expected), $"Carousel isn't shown");
            }
            catch (Exception ex)
            {
                test.Fail(ex.StackTrace);
                test.Fail(ex.Message);
                isTestSuccess = false;
            }
        }
    }
}
