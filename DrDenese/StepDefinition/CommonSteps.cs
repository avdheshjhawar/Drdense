using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using DrDenese.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using System.Drawing.Imaging;

namespace BeeptifyAutomation.StepDefinition
{
    public enum Browser
    {
        Firefox,
        Chrome
    }

    [Binding]
    public class CommonSteps
    {

        #region Broser specific settings

        static IWebDriver _driver;

        private void IntitDriver(Browser browser)
        {
            if (_driver == null)
            {
                switch (browser)
                {
                    
                    case Browser.Chrome:
                       // System.Environment.SetEnvironmentVariable("webdriver.chrome.driver","E:/BeeptifyAutomation/BeeptifyAutomation/chromedriver.exe");
                        String path = System.IO.Directory.GetCurrentDirectory(); 
                       _driver = new ChromeDriver(@path);
                        
                        break;

                    case Browser.Firefox:
                        _driver = new FirefoxDriver();
                        break;

                    default:
                        throw new Exception("This browser is not supported yet.");
                }
            }
            /*else
            {*/
                _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["Url"]);
                _driver.Manage().Cookies.DeleteAllCookies();
            //}

            PageStorage.Initialize(_driver);
            _driver.Manage().Window.Maximize();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browserSetting = ConfigurationManager.AppSettings["Browser"];
            Console.WriteLine("Browser: {0}", browserSetting);
            Browser browser;
            Enum.TryParse(browserSetting, out browser);
            IntitDriver(browser);
        }

        #endregion

        #region pre & post conditions

        [AfterScenario]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var path = ConfigurationManager.AppSettings["PathToScreenshots"];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var postFix = string.Format("{0:MM_dd_yy_H_mm_ss}", DateTime.Now);
                ITakesScreenshot screenshotDriver = _driver as ITakesScreenshot;
                var pathToUploadImage = Path.Combine(path, ScenarioContext.Current.ScenarioInfo.Title + postFix + ".png");

                try
                {
                    if (screenshotDriver != null)
                        screenshotDriver.GetScreenshot().SaveAsFile(pathToUploadImage,OpenQA.Selenium.ScreenshotImageFormat.Png);
                    else Console.WriteLine("Screenshot cannot be created.");
                    Console.WriteLine("Screenshot saved: " + pathToUploadImage);
                }
                finally
                {
                    PageStorage.Clear();
                    ScenarioContext.Current.Clear();
                }
            }
        }

        [AfterTestRun]
        public static void AfterAll()
        {
            if (_driver != null)
            {
                _driver.Quit();
                KillAllDrivers();
                _driver = null;
            }
        }

        public static void KillAllDrivers()
        {
            var drivers = Process.GetProcessesByName("chromedriver");
            foreach (var driver in drivers)
                driver.Kill();
        }

        #endregion
    }
}
