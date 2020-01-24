using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System;
using System.IO;
using System.Threading;

namespace UberAcceptApp {

    internal class Program {

        private static void Main(string[] args) {
            Console.Title = "Uber";
            string path = @"C:\inetpub\www\uber\rides.txt";

            ChromeOptions options = new ChromeOptions();

            // profile
            options.AddArgument(@"user-data-dir=C:\\Users\\Administrator\\AppData\\Local\\Google\\Chrome\\User Data\\");
            options.AddArgument("--profile-directory=Profile 1");

            // options
            //options.AddArgument("headless");
            options.AddArgument("log-level=3");

            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://vsdispatch.uber.com");

            while (true) {
                //IWebElement button = driver.FindElement(By.CssSelector("#wrapper > div:nth-child(3) > div.jss22.jss26.jss23.jss16 > div.jss53.jss55.jss54 > div > button"));
                IWebElement button = driver.FindElement(By.XPath("/html/body/div/div/div[3]/div[1]/div[1]/div/button"));
                bool status = button.Enabled;

                if (status) {
                    button.Click();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    using (StreamWriter w = File.AppendText(path)) {
                        Log(w);
                        Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " | Ride accepted");
                    }
                    Console.ResetColor();
                    Thread.Sleep(5000);
                }
                //driver.Navigate().Refresh();
                Thread.Sleep(500);
            }
        }

        private static void Log(TextWriter w) {
            w.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " | Ride accepted");
        }
    }
}