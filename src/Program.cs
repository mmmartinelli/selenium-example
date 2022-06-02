using src.App;
using src.Services;

/*
    To use Selenium with Google chrome just instantiate the class SeleniumService.cs
*/
Console.WriteLine("# Start chrome driver...");
var seleniumDriver = new SeleniumService().Start();

/*
    Here is an example of scraping usage.
    The processment below will scrape the major indices in Investing.com website.
*/
Console.WriteLine("# Start scraping...");
new Scraper(seleniumDriver).Start();

seleniumDriver.Quit();