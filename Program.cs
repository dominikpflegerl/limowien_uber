using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System;
using System.IO;
using System.Threading;

namespace UberAcceptApp {


    internal class Program {

        private static void Main() {
            Console.Title = "Uber";

            ChromeDriver driver = ChromeLaunch();

            //driver.Navigate().GoToUrl("https://vsdispatch.uber.com");
            driver.Navigate().GoToUrl("https://www.computerbase.de/");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);
            ConsoleStartTime();

            while (true) {
                //UberAcceptButton(driver);
                TestSample(driver);
            }
        }

        private static ChromeDriver ChromeLaunch() {
            ChromeOptions options = new ChromeOptions();
            // profile
            options.AddArgument(@"user-data-dir=.\\GoogleChromePortable\\Data\\profile");
            options.AddArgument("--profile-directory=Default");
            // options
            //options.AddArgument("headless");
            options.AddArgument("log-level=3");
            options.BinaryLocation = @".\\GoogleChromePortable\\App\\Chrome-bin\\chrome.exe";
            ChromeDriver driver = new ChromeDriver(options);
            return driver;
        }

        private static void UberAcceptButton(ChromeDriver driver) {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement button = driver.FindElement(By.XPath("/html/body/div/div/div[3]/div[1]/div[1]/div/button"));
            bool status = button.Enabled;
            if (status) {
                button.Click();
                ConsoleWriteAccept();
                Thread.Sleep(5000);
            }
        }        
        
        private static void TestSample(ChromeDriver driver) { // computerbase.de
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement button = driver.FindElement(By.XPath("/html/body/main/div[3]/div/div[1]/div[1]/a"));
            bool status = button.Enabled;
            if (status) {
                button.Click();
                ConsoleWriteAccept();
                Thread.Sleep(5000);
            }
        }

        private static void ConsoleWriteAccept() {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            //using (StreamWriter w = File.AppendText(@"C:\inetpub\www\uber\rides.txt")) {
            using (StreamWriter w = File.AppendText(@".\\rides.txt")) {
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