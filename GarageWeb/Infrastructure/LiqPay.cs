using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security;
using System.Text;
using System.Web;
using System.Net.Http;

namespace GarageWeb.Infrastructure
{
    public class LiqPay
    {
        private string publicKey;
        private readonly string privateKey;

        public LiqPay(string public_key, string private_key)
        {
            publicKey = public_key;
            privateKey = private_key;
        }

        public string GetSignature(string data)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            sha1.Initialize();
            return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(privateKey + data + privateKey)));
        }

        public string GetData(string json) => Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        private async Task<string> SendRequest(string data, string signature)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("data", data),
                    new KeyValuePair<string, string>("signature", signature)
                });
                //System.Diagnostics.Debug.WriteLine(await content.ReadAsStringAsync());
                var response = await client.PostAsync("https://www.liqpay.com/api/request", content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PayTest(string phone, double pay_amount, string order_id, string card, string exp_month, string exp_year, string cvv, string user_ip)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                version = 3,
                public_key = publicKey,
                action = "pay",
                phone = phone,
                amount = pay_amount,
                currency = "UAH",
                description = "Оплата замовлення в кафе-барі 'Гараж'",
                order_id = order_id,
                card = card,
                card_exp_month = exp_month,
                card_exp_year = exp_year,
                card_cvv = cvv,
                ip = user_ip,
                sandbox = "1"
            });
            string data = GetData(json);
            string signature = GetSignature(data);
            return await SendRequest(data, signature);
        }

        public async Task<string> Pay(string phone, double pay_amount, string order_id, string card, string exp_month, string exp_year, string cvv, string user_ip)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                version = 3,
                public_key = publicKey,
                action = "pay",
                phone = phone,
                amount = pay_amount,
                currency = "UAH",
                description = "Оплата замовлення в кафе-барі 'Гараж'",
                order_id = order_id,
                card = card,
                card_exp_month = exp_month,
                card_exp_year = exp_year,
                card_cvv = cvv,
                ip = user_ip
            });
            string data = GetData(json);
            string signature = GetSignature(data);
            return await SendRequest(data, signature);
        }
    }
}