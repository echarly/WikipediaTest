using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WikipediaTest
{
    public static class WikiTest
    {

        public static void Main(string[] args)
        {

        }

        public class Wiki
        {

            public static void WikiTest() { 

                int waitTime = 3000;
                string URL = "https://www.wikipedia.org/";
                string searchString = "Taylor Swift";
                By searchTextXpath = By.XPath("//input[@id='searchInput']");
                By externalLinksXpath = By.XPath("//span[@class='toctext'][contains(text(),'External links')]");
                By studioAlbumsXpath = By.XPath("//th[contains(text(),'Studio albums')]");
                By studioAlbumsDiscographyXpath = By.XPath("//table[@class='nowraplinks vcard hlist collapsible expanded navbox-inner mw-collapsible mw-made-collapsible']//tbody//tr[3]//td[1]");
                By reputationXpah = By.XPath("//td[@class='navbox-list navbox-odd']//a[@title='Reputation (Taylor Swift album)'][contains(text(),'Reputation')]");
                string reputationXpath2 = "//body[@class='mediawiki ltr sitedir-ltr mw-hide-empty-elt ns-0 ns-subject page-Taylor_Swift rootpage-Taylor_Swift skin-vector action-view']//div[@id='content']//div[@id='bodyContent']//div[@id='mw-content-text']//div[@class='mw-parser-output']//div[@class='navbox']//table[@class='nowraplinks vcard hlist collapsible expanded navbox-inner mw-collapsible mw-made-collapsible']//tbody//tr//td[@class='navbox-list navbox-odd']//div//ul//li[6]//i[1]";
                By popup = By.Id("mwe-popups-mask");


                IWebDriver driver = new ChromeDriver(@"C:\\Chromedriver");
                driver.Manage().Window.Size = new Size(1200, 1020);


                /////////////////////////////////
                // Open URL - Wikipedia.org
                driver.Navigate().GoToUrl(URL);
                Thread.Sleep(8000);

                /////////////////////////////////
                // Enter search text and press enter to search
                driver.FindElement(searchTextXpath).SendKeys(searchString);
                Thread.Sleep(waitTime);
                Console.WriteLine("Search String entered: " + searchString);
                driver.FindElement(searchTextXpath).SendKeys(Keys.Enter);
                Console.WriteLine("Pressed ENTER to search");
                Thread.Sleep(waitTime);

                /////////////////////////////////
                // Naviate to External Links and validate text fields and Pop up
                driver.FindElement(externalLinksXpath).Click();
                Console.WriteLine("External Links CLICKED");
                Thread.Sleep(waitTime);

                string studioAlbumstext = driver.FindElement(studioAlbumsXpath).Text;

                if (studioAlbumstext == "Studio albums")
                {

                    string studioAlbumsDiscography = driver.FindElement(studioAlbumsDiscographyXpath).Text;
                    string albumsCheck = "Taylor Swift Fearless Speak Now Red 1989 Reputation";

                    if (studioAlbumsDiscography == albumsCheck) {

                        string texttemp = driver.FindElement(By.XPath(reputationXpath2)).Text;
                        Console.WriteLine("Reputation Text obtained: " + texttemp);
                        IWebElement hoverElement = driver.FindElement(By.XPath(reputationXpath2));
                        
                        Actions action = new Actions(driver);
                        action.MoveToElement(hoverElement).Perform();

                        bool shown = driver.FindElement(popup).Displayed;

                        if(shown)
                        { Console.WriteLine("Pop Up Shown!"); }
                        else { Console.WriteLine("¨Pop Up NOT Shown!"); }

                    }
                }

                driver.Close();
            }

        }

    }
}
