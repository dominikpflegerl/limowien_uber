using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System;
using System.IO;
using System.Threading;

namespace UberAcceptApp {

    internal class Program {
        public string path = @"C:\inetpub\www\uber\rides.txt";

        private static void Main() {
            Console.Title = "Uber";
            

            ChromeOptions options = new ChromeOptions();
            // profile
            options.AddArgument(@"user-data-dir=C:\\Users\\Administrator\\AppData\\Local\\Google\\Chrome\\User Data\\");
            options.AddArgument("--profile-directory=Profile 1");
            // options
            //options.AddArgument("headless");
            options.AddArgument("log-level=3");
            IWebDriver driver = new ChromeDriver(options);


            driver.Navigate().GoToUrl("https://vsdispatch.uber.com");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

            ConsoleStartTime();

            while (true) {
                //IWebElement button = driver.FindElement(By.CssSelector("#wrapper > div:nth-child(3) > div.jss22.jss26.jss23.jss16 > div.jss53.jss55.jss54 > div > button"));
                IWebElement button = driver.FindElement(By.XPath("/html/body/div/div/div[3]/div[1]/div[1]/div/button"));
                bool status = button.Enabled;
                if (status) {
                    button.Click();
                    ConsoleWriteAccept();

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().Refresh();
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                } 
            }
        }

        private static void ConsoleWriteAccept() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            using (StreamWriter w = File.AppendText(@"C:\inetpub\www\uber\rides.txt")) {
                Log(w);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " | Ride accepted");
            }
            Console.ResetColor();
        }

        private static void ConsoleStartTime() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Start Time: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            Console.ResetColor();
        }

        private static void Log(TextWriter w) {
            w.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " | Ride accepted");
        }
    }
}