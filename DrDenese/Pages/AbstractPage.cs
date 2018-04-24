using System.Threading;
using DrDenese.Helpers;
using OpenQA.Selenium;

namespace DrDenese.Pages
{
    public abstract class AbstractPage
    {
        protected IWebDriver Driver;

        public void GoTo(string link)
        {
            Driver.Navigate().GoToUrl(link);
        }

        protected AbstractPage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement FindElementByName(string name)
        {
            return Driver.FindElementWithDelay(By.Name(name));
        }

        public bool IsMessagePresent(string message)
        {
            return WaitHelper.WaitUntil(() => FindElementByXpath(string.Format("//span[text()='{0}']", message)).Displayed);
        }

        protected IWebElement FindElementById(string id)
        {
            return Driver.FindElementWithDelay(By.Id(id));
        }

        protected IWebElement FindElementByXpath(string expression, IWebElement parent = null)
        {
            if (parent != null)
                return parent.FindElementWithDelay(By.XPath(expression));
            return Driver.FindElementWithDelay(By.XPath(expression));
        }

        public void ScrollTo(IWebElement element)
        {
            var jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(2000);
        }

        public IWebElement LanguageSelector
        {
            get { return FindElementByXpath("//button[@class='btn btn-default btn-sm dropdown-toggle']"); }
        }

        public IWebElement CurrentLanguage
        {
            get { return LanguageSelector.FindElement(By.XPath("//span[@class='dropdown-label']")); }
        }

        public IWebElement UserName
        {
            get { return FindElementById("menu_UserName"); }
        }

        public void SelectLanguage(string language)
        {
            const string selectorLocator = "//button[@class='btn btn-default btn-sm dropdown-toggle']";
            WaitHelper.WaitUntil(() => FindElementByXpath(selectorLocator).Displayed);
            WaitHelper.WaitUntil(() =>
            {
                FindElementByXpath(selectorLocator).Click();
                return true;
            });
            var el = FindElementByXpath(string.Format("//a[@data-lang='{0}']", language));
            WaitHelper.WaitUntil(() => el.Displayed);
            el.Click();
        }

        public bool VerifyValidationForField(string field, string message)
        {
            return FindElementByXpath(string.Format("//span[@for='{0}'][text()='{1}']", field, message)).Displayed;
        }

        public bool VerifyValidationForField(string field, string attribute, string message)
        {
            return FindElementByXpath(string.Format("//span[@{2}='{0}'][text()='{1}']", field, message, attribute)).Displayed;
        }
    }
}
