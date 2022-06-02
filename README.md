# About the project

This project implements selenium webdriver for Google Chrome and run a basic scraping of a webpage as application example.

If you want tou use Selenium in your projects, feel free to use the class **SeleniumService.cs**. The example below will spawn Google Chrome browser:

```csharp
new SeleniumService().Start()
```

Then you be able to use the Webdriver navigation and all its features. Take a look at the scraping example implemented! :)

```csharp
new Scraper(seleniumDriver).Start();
```

### **Important:**
```
To use Selenium you MUST download chromedriver.exe. See useful links below this document. After the download, move the executable to the path described in SeleniumService class. 

MAKE SURE YOU DOWNLOAD THE CORRECT VERSION OF CHROMEDRIVER ACCORDING TO THE INSTALLED GOOGLE CHROME VERSION!
```


## Why Selenium?

Selenium is the most popularly used freeware and open source automation tool. The benefits of Selenium for Test Automation are immense. Importantly, it enables record and playback for testing web applications and can run multiple scripts across various browsers. The benefits of Selenium Test Automation hold relevance across diverse business segments.

[See more in Cigniti.](https://www.cigniti.com/blog/10-benefits-selenium-test-automation-publishing/)


## Useful links

 - [Selenium official documentation](https://www.selenium.dev/documentation/)
 - [ChromeDriver download](https://chromedriver.chromium.org/downloads)