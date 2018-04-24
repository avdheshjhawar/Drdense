using OpenQA.Selenium;

namespace DrDenese.Helpers
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElementWithDelay(this IWebDriver driver, By by )
        {
            return WaitHelper.WaitResult(() => driver.FindElement(by));
        }

        public static IWebElement FindElementWithDelay(this IWebElement root, By by)
        {
            return WaitHelper.WaitResult(() => root.FindElement(by));
        }
    }
}
