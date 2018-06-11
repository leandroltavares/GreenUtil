using Newtonsoft.Json;
using System;
using System.Net;

namespace GreenUtil.Web
{
    /// <summary>
    /// Classe para lógicas relacionadas a Web
    /// </summary>
    public class WebUtil
    {
        /// <summary>
        /// Realiza uma requisição POST para a URL informada e retorna um objeto populado a partir do JSON retornado
        /// </summary>
        /// <typeparam name="T">Tipo a ser retornado</typeparam>
        /// <param name="url">URL da requisição</param>
        /// <param name="postData">Dados da requisição</param>
        /// <param name="headers">Cabeçalhos</param>
        /// <returns>Instância de um objeto populado a partir do JSON retornado</returns>
        public static T Post<T>(string url, string postData, WebHeaderCollection headers = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            using (var wc = new WebClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        wc.Headers[item] = headers[item];
                    }
                }

                var htmlResult = wc.UploadString(url, "POST", postData);
                return JsonConvert.DeserializeObject<T>(htmlResult);
            }
        }

        /// <summary>
        ///  Realiza uma requisição GET para a URL informada e retorna um objeto populado a partir do JSON retornado
        /// </summary>
        /// <typeparam name="T">Tipo a ser retornado</typeparam>
        /// <param name="url">URL da requisição</param>
        /// <param name="headers">Cabeçalhos</param>
        /// <returns>Instância de um objeto populado a partir do JSON retornado</returns>
        public static T Get<T>(string url, WebHeaderCollection headers = null)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            using (var wc = new WebClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers.AllKeys)
                    {
                        wc.Headers[item] = headers[item];
                    }
                }

                var htmlResult = wc.DownloadString(url);
                return JsonConvert.DeserializeObject<T>(htmlResult);
            }
        }
    }
}
