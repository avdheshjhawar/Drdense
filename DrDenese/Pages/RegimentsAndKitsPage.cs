using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using DrDenese.Helpers;

namespace DrDenese.Pages
{
    class RegimentsAndKitsPage : AbstractPage
    {
        public RegimentsAndKitsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement RegimentsAndKits_Heading
        {
            get { return FindElementByXpath("//h1[text()='Regimens and Kits']"); }
        }
    }
}
