using src.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.Json;

namespace src.App
{
    public class Scraper
    {
        private IWebDriver _driver;
        
        public Scraper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Start()
        {
            Console.WriteLine("## Navigating to scraping page...");
            _driver.Navigate().GoToUrl("https://www.investing.com/indices/major-indices");

            Console.WriteLine("## Accepting OneTrust alert...");
            OneTrustAccept();

            Console.WriteLine("## Scraping page data...");
            List<Indice> indices = ReadTable();

            Console.WriteLine("## Printing JSON result...");
            PrintResult(indices);
        }

        private void OneTrustAccept()
        {
            // Try-catch is necessary because if the element id does not exist an exception will be shown
            // In this case the exception is not a bad thing :)
            // ** Remember to disable the option to break all exceptions while debugging

            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
                wait.Until(x => x.FindElement(By.Id("onetrust-accept-btn-handler"))).Click();
            }
            catch {}
        }

        private List<Indice> ReadTable()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            try
            {
                // Search for all rows on prices table
                ICollection<IWebElement> rowsElements = wait.Until(x => x.FindElement(By.ClassName("table-browser_table-browser-wrapper__2ynbE"))
                                                                         .FindElements(By.ClassName("datatable_row__2vgJl")));

                List<Indice> indices = new();

                foreach (IWebElement row in rowsElements)
                {
                    Indice newIndice = ReadRow(row);

                    if (newIndice == null)
                        continue;

                    indices.Add(newIndice);
                }

                return indices;
            }
            catch (Exception ex)
            {
                _driver.Quit(); // Close Google Chrome
                Console.WriteLine($"!! ERROR READING DATA FROM WEBPAGE: {ex.Message}");
                return new List<Indice>();
            }
        }

        private Indice ReadRow(IWebElement row)
        {
            List<string> readData = new();

            try
            {
                ICollection<IWebElement> columns = row.FindElements(By.TagName("td"));

                if (columns.Count <= 0)
                    return null;

                foreach (var data in columns)
                {
                    if (string.IsNullOrWhiteSpace(data.Text))
                        continue;

                    readData.Add(data.Text);
                }

                Indice indice = new()
                {
                    Name = readData[0],
                    Last = readData[1],
                    High = readData[2],
                    Low = readData[3],
                    Change = readData[4],
                    PercentageChange = readData[5],
                    Time = readData[6]
                };

                return indice;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!! ERROR READING INDEX {readData[0]}: {ex.Message}");
                return null;
            }
        }

        private void PrintResult(List<Indice> indices)
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            string resultJson = JsonSerializer.Serialize(indices, jso);
            Console.WriteLine(resultJson);
        }
    }
}