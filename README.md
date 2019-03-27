# Web scraper using .NET framework

# Scraping car ads from Macedonian website reklama5.mk using ASP.NET

# Short demo
// To do

# Getting the data
```
var url = "http://reklama5.mk/Search?q=&city=&sell=0&sell=1&buy=0&buy=1&trade=0&trade=1&includeOld=0&includeOld=1&includeNew=0&includeNew=1&f31=&priceFrom=&priceTo=&f33_from=&f33_to=&f36_from=&f36_to=&f35=&f37=&f138=&f10016_from=&f10016_to=&private=0&company=0&page=" + 1 + "&SortByPrice=0&zz=1&cat=24";
if (id.HasValue)
                url = "http://reklama5.mk/Search?q=&city=&sell=0&sell=1&buy=0&buy=1&trade=0&trade=1&includeOld=0&includeOld=1&includeNew=0&includeNew=1&f31=&priceFrom=&priceTo=&f33_from=&f33_to=&f36_from=&f36_to=&f35=&f37=&f138=&f10016_from=&f10016_to=&private=0&company=0&page=" + id.ToString() + "&SortByPrice=0&zz=1&cat=24";
```
