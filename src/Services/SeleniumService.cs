using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace src.Services
{
    public class SeleniumService
    {
        private string _driverName = "chromedriver.exe";    // Remove ".exe" if operational system is Linux based
        private bool _isHeadless;

        /// <summary>
        /// Constructor with window visibility indicator.
        /// </summary>
        /// <param name="isHeadless">Indicates if browser window will be visible (false) or invisible (true).</param>
        public SeleniumService(bool isHeadless = false)
        {
            _isHeadless = isHeadless;
        }

        public IWebDriver Start()
        {
            ChromeOptions options = new();

            if (_isHeadless)
                options.AddArgument("--headless");

            options.AddArgument("--start-maximized");
            options.AddArgument("--incognito");
            options.AddArgument("--no-sandbox");

            string chromedriverPath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Drivers{Path.DirectorySeparatorChar}";
            
            ChromeDriverService driver = ChromeDriverService.CreateDefaultService(chromedriverPath, _driverName);
            return new ChromeDriver(driver, options, TimeSpan.FromSeconds(120));
        }
    }
}