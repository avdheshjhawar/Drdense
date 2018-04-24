using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using DrDenese.Helpers;

namespace DrDenese.Pages
{
    class HomePage : AbstractPage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement HomePageDisplay
        {
            get { return FindElementByXpath("//a[contains(text (),'1-800-123-4567')]"); }
        }

        public IWebElement HomePageTitle
        {
            get { return FindElementByXpath("//img[@id='logo']"); }
        }

        public IWebElement Shop_Products_Header_Link
        {
            get { return FindElementByXpath("//html//li[@id='mega-menu-item-257']/a[1]"); }
        }

        public IWebElement Sub_Menu_Regimens_and_Kits_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-341']"); }
        }

        public IWebElement Sub_Menu_Moisturizers_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-344']"); }
        }

        public IWebElement Sub_Menu_Cleansers_Toners_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-342']"); }
        }

        public IWebElement Sub_Menu_Anti_Aging_Treatments_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-1085']"); }
        }

        public IWebElement Sub_Menu_Serums_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-343']"); }
        }

        public IWebElement Sub_Menu_Eyes_Lips_Link
        {
            get { return FindElementByXpath("//li[@id='mega-menu-item-345']"); }
        }

       
    }
}
