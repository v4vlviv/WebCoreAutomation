using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebCoreAutoTests.Pages;
using WebCoreAutoTests.Tools;

namespace WebCoreAutoTests.Tests
{
    [TestFixture]
    class HomePageNonLoginUser : TestHelper
    {
        HomePage homePage; 

        public HomePageNonLoginUser() : base(BrowserType.Chrome) { }

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();
            homePage = new HomePage();
        }

        [Test]
        [Parallelizable]
        public void VerifyCarouselIsShownCR()
        {
            bool expected = true;
            bool actual = homePage.CarouselIsExist();
            Assert.That(expected, Is.EqualTo(actual), $"Carousel isn't shown");
        }
    }
}
