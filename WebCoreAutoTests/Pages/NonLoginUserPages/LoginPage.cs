﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebCoreAutoTests.Tools;

namespace WebCoreAutoTests.Pages.NonLoginUserPages
{
    class LoginPage : DriverInit
    {
        IWebElement title => driver.FindElement(By.XPath("//h2[text()='Log in']"));
        IWebElement login => driver.FindElement(By.Id("Input_Email"));
        IWebElement password => driver.FindElement(By.Id("Input_Password"));
        IWebElement buttonLogin => driver.FindElement(By.Id("//button[text()='Log in']"));


        public HomePage Login()
        {
            login.SendKeys("test@test.com");
            password.SendKeys("Test1234$");
            buttonLogin.Click();
            return new HomePage();
        }
    }
}
