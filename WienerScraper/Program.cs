using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net;
using log4net.Config;

class Program
{
    // Создаем логгер
    private static readonly ILog log = LogManager.GetLogger(typeof(Program));

    static async Task Main()
    {
        // Настраиваем логгер из конфигурационного файла
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        XmlConfigurator.Configure(LogManager.GetRepository(assembly), new FileInfo("log4net.config"));

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        string fileName = $"wienerborse_{date}.csv";

        try
        {
            log.Info("Старт сбора данных с wienerborse.at");

            using var client = new HttpClient();
            using var writer = new StreamWriter(fileName);

            bool firstPage = true;
            int page = 1;

            while (true)
            {
                string url = $"https://www.wienerborse.at/en/bonds/?page={page}";
                log.Info($"Загружаем страницу {page}: {url}");

                string html = await client.GetStringAsync(url);

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var table = doc.DocumentNode.SelectSingleNode("//table");
                if (table == null)
                {
                    log.Info("Таблица не найдена на странице. Завершаем.");
                    break;
                }

                var headersNodes = table.SelectNodes(".//thead/tr/th");
                if (headersNodes == null)
                {
                    log.Warn("Заголовки таблицы не найдены. Завершаем.");
                    break;
                }

                if (firstPage)
                {
                    var headers = headersNodes.Select(h => h.InnerText.Trim());
                    writer.WriteLine(string.Join("\t", headers));
                    firstPage = false;
                }

                var rows = table.SelectNodes(".//tbody/tr");
                if (rows == null || rows.Count == 0)
                {
                    log.Info("Строки таблицы не найдены на странице. Завершаем.");
                    break;
                }

                foreach (var row in rows)
                {
                    var colsNodes = row.SelectNodes("td");
                    if (colsNodes == null)
                    {
                        log.Warn("Ячейки строки не найдены. Пропускаем строку.");
                        continue;
                    }

                    var cols = colsNodes.Select(td => td.InnerText.Trim());
                    writer.WriteLine(string.Join("\t", cols));
                }

                page++;
            }

            log.Info($"Сбор данных завершен. Результат сохранен в файл: {fileName}");
        }
        catch (Exception ex)
        {
            log.Error("Произошла ошибка во время сбора данных!", ex);
        }
    }
}
