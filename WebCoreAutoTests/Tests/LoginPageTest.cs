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
        public void LoginTest()
        {
            string expected = "Hello test@test.com!";
            homePage = loginPage.Login();
            string actual = homePage.VerifyUserName();
            Assert.That(expected, Is.EqualTo(actual), $"Name should be Hello test@test.com! but was {actual}");
        }

        [Test]
        public void VerifyCarouselIsShown()
        {
            bool expected = true;
            homePage = loginPage.Login();
            bool actual = homePage.CarouselIsExist();
            Assert.That(expected, Is.EqualTo(actual), $"Carousel isn't shown");
        }
    }
}
