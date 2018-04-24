using System;
using TechTalk.SpecFlow;
using DrDenese.Helpers;
using DrDenese.Pages;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;



namespace DrDenese.StepDefinition
{
    [Binding]
    public class DR_1HomePageVerificationSteps
    {
        [Given(@"I open Home Page")]
        public void GivenIOpenHomePage()
        {
            WaitHelper.WaitUntil(() => PageStorage.GetPage<HomePage>().HomePageDisplay.Displayed);
           
        }
        
        [Then(@"Home Page should display")]
        public void ThenHomePageShouldDisplay()
        {
            Assert.IsTrue(PageStorage.GetPage<HomePage>().HomePageTitle.Displayed);
        }

        [Given(@"user is present on home page")]
        public void GivenUserIsPresentOnHomePage()
        {
            WaitHelper.WaitUntil(() => PageStorage.GetPage<HomePage>().HomePageDisplay.Displayed);
        }

        [When(@"user mouse hover on shop products link")]
        public void WhenUserMouseHoverOnShopProductsLink()
        {
            WaitHelper.WaitUntil(() => PageStorage.GetPage<HomePage>().Shop_Products_Header_Link.Displayed);
            Actions actions = new Actions(PageStorage.Driver);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Shop_Products_Header_Link).Perform();                          
        }

        [Then(@"all sublinks of shop products should display")]
        public void ThenAllSublinksOfShopProductsShouldDisplay()
        {
            WaitHelper.WaitUntil(() => PageStorage.GetPage<HomePage>().Sub_Menu_Regimens_and_Kits_Link.Displayed);
            Actions actions = new Actions(PageStorage.Driver);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Regimens_and_Kits_Link).Perform();
            Thread.Sleep(1000);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Moisturizers_Link).Perform();
            Thread.Sleep(1000);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Anti_Aging_Treatments_Link).Perform();
            Thread.Sleep(1000);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Cleansers_Toners_Link).Perform();
            Thread.Sleep(1000);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Serums_Link).Perform();
            Thread.Sleep(1000);
            actions.MoveToElement(PageStorage.GetPage<HomePage>().Sub_Menu_Eyes_Lips_Link).Perform();
            Thread.Sleep(1000);
        }

        [When(@"user click on Regiments and Kit sub-link of shop products link")]
        public void WhenUserClickOnRegimentsAndKitSub_LinkOfShopProductsLink()
        {
            PageStorage.GetPage<HomePage>().Sub_Menu_Regimens_and_Kits_Link.Click();
        }

        [Then(@"user should redirected to desired page")]
        public void ThenUserShouldRedirectedToDesiredPage()
        {
            WaitHelper.WaitUntil(() => PageStorage.GetPage<RegimentsAndKitsPage>().RegimentsAndKits_Heading.Displayed);
            //Assert.IsTrue(PageStorage.GetPage<RegimentsAndKitsPage>().RegimentsAndKits_Heading.Displayed);
        }

    }
}
