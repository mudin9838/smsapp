using sms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;
using OpenQA.Selenium.DevTools.V109.Audits;

namespace sms.Tests.Controllers
{
    [TestClass]
    public class LoginViewTests
    {
        private WebDriver WebDriver { get; set; } = null;

        private string DriverPath { get; set; } = @"C:\edgedriver_win64";

        private string BaseUrl { get; set; } = "http://localhost:44360/Identity/Account/Login";

        /// <summary>
        /// Setup edge driver
        /// </summary>
        

      
        public void Setup()
        {
            WebDriver = GetEdgeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120); // wait 120 second before testing
            
        }
        public void Quit()
        {
          WebDriver.Quit();

        }

        /// <summary>
        /// Login test using Selenium
        /// </summary>

        [TestMethod]

        public void LoginTest()
        {
            Setup();
            // Navigate to login page
            WebDriver.Navigate().GoToUrl(BaseUrl);

            // Enter EmailAddress
            //Thread.Sleep(5000);
            var input = WebDriver.FindElement(By.Id("form2Example11"));
            input.Clear(); // am clearing the field
            input.SendKeys("admin@gmail.com");

            // Enter Password
            //Thread.Sleep(5000);
            input = WebDriver.FindElement(By.Id("form2Example22"));
            input.Clear();
            input.SendKeys("Zd*0910588372");

            // Click on Login button
            //Thread.Sleep(5000);
            input = WebDriver.FindElement(By.Id("button_login"));
            input.Click();

            // Validate login message
            var startTimestamp = DateTime.Now.Millisecond;
            var endTimstamp = startTimestamp + 22000; //run 15 seconds faster




            while (true)
            {
                try
                {
                    input = WebDriver.FindElement(By.Id("p_welcome_message"));
                    Assert.AreEqual("Welcome, admin", input.Text);
                    break;
                }
                catch
                {
                    var currentTimestamp = DateTime.Now.Millisecond;
                    if (currentTimestamp > endTimstamp)
                    {
                        throw;
                    }
                    Thread.Sleep(2000); // sleep for 2 second and start try again! but if it crosses 15 second throw an exception am not able to loginin
                }

                Quit();
            }
       
        }






        private WebDriver GetEdgeDriver()
        {
            var options = new EdgeOptions();
          //  options.AddArguments("--headless"); uncomment if we want to test without opening browser

            return new EdgeDriver(DriverPath, options, TimeSpan.FromSeconds(300));
        }

        
    }
}
