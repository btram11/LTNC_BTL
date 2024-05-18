using HtmlAgilityPack;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class FuelPriceService : IFuelPriceService
    {
        private readonly FuelModelingHttpClient _httpClient;
        private string htmlContent;

        public FuelPriceService(FuelModelingHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ObservableCollection<string>> GetPrice()
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                htmlContent = await _httpClient.GetAsync("");
            }
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);
            ObservableCollection<string> nameGasPrice = new ObservableCollection<string>();
            for (int i = 1; i <= 4; i++)
            {
                var name = htmlDocument.DocumentNode.SelectNodes($"//*[@id=\"admwrapper\"]/main/div[3]/div[2]/section/div/div[1]/div[2]/table/tbody/tr[{i}]/td[2]")?.First().InnerText;
                var price = htmlDocument.DocumentNode.SelectNodes($"//*[@id=\"admwrapper\"]/main/div[3]/div[2]/section/div/div[1]/div[2]/table/tbody/tr[{i}]/td[3]")?.First().InnerText;
                var dif = htmlDocument.DocumentNode.SelectNodes($"//*[@id=\"admwrapper\"]/main/div[3]/div[2]/section/div/div[1]/div[2]/table/tbody/tr[{i}]/td[4]/strong").First().InnerText;
                if (name == null || string.IsNullOrEmpty(price))
                    name = null;
                if (price == null || string.IsNullOrEmpty(price))
                    price = null;
                if (dif == null || string.IsNullOrEmpty(dif))
                    dif = null;
                nameGasPrice.Add(name);
                nameGasPrice.Add(price);
                nameGasPrice.Add(dif);
            }
            var date = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"admwrapper\"]/main/div[3]/div[2]/section/div/div[1]/div[1]/form/select/option[1]").First().InnerText;
            if (date == null || string.IsNullOrEmpty(date))
                date = null;
            nameGasPrice.Add(date);
            return nameGasPrice;
        }
    }
}
