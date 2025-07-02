# WienerScraper

1. Скачаем HTML-страницу с сайта [wienerborse.at](https://www.wienerborse.at/en/bonds/)
2. Извлекаем таблицу
3. Сохраняем данные в файле CSV
4. Логируем события через log4net

## Установка и подготовка

5. Установил [.NET SDK](https://dotnet.microsoft.com/download)  
6. Создаю новую директорию и проект:
   ```bash
   mkdir WienerScraper
   cd WienerScraper
   dotnet new console --framework net9.0
7. установка пакетов
    ```bash 
    dotnet add package HtmlAgilityPack
    dotnet add package log4net
8.Создаю файл log4net

9.Начинаю обновлять файл program
## Руководствуюсь
[code-maze.com](https://code-maze.com/html-agility-pack-csharp/?utm_source=chatgpt.com)
[c-sharpcorner.com](https://www.c-sharpcorner.com/blogs/efficient-error-logging-in-c-sharpnet-using-csv-files?utm_source=chatgpt.com)
[blog.elmah.io](https://blog.elmah.io/log4net-tutorial-the-complete-guide-for-beginners-and-pros/)
[brightdata.com](https://brightdata.com/blog/how-tos/web-scraping-with-c-sharp?utm_source=chatgpt.com)
Книга:
C# in Depth
