using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Translate
{
    class YandexAPI : Translate
    {
        private string key = "trnsl.1.1.20160901T062551Z.3b7d9c3b3f9d703a.cb6fdea8fb17248adc9b90373cdccc5ffadbf8f0";

        public List<string> getListUi()
        {
            return listUi;
        }
        public List<string> getListNameLang()
        {
            return listNameLang;
        }

        /// <summary>
        /// получение списка поддерживаемых языков для перевода
        /// </summary>
        /// <param name="ui"></param>
        public override void listLang(string ui)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/getLangs?" + "key=" + key + "&ui=" + ui);
                WebResponse response = request.GetResponse();   // ждём ответ
                XmlTextReader sx = new XmlTextReader(response.GetResponseStream());
                while (sx.Read())
                {
                    if (sx.Name == "Item")
                    {
                        listNameLang.Add(sx.GetAttribute("value"));
                        listUi.Add(sx.GetAttribute("key"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
        }

        /// <summary>
        /// переводим текст
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public override string translate(string text, string lang)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/translate?" + "key=" + key + "&text=" + text + "&lang=" + lang);
                WebResponse response = request.GetResponse();   // ждём ответ
                XmlDocument d = new XmlDocument();
                d.Load(response.GetResponseStream());   // записываю в документ полученные данные с сервера
                XmlNode node = d.SelectSingleNode("//text");    // читаю информацию внутри тэга <text>
                return node.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ошибка.";
            }
        }

        /// <summary>
        /// получение языка для переводимого текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string getLang(string text)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr/detect?" + "key=" + key + "&text=" + text);
                WebResponse response = request.GetResponse();   // ждём ответ
                XmlTextReader sx = new XmlTextReader(response.GetResponseStream());
                while (sx.Read())
                {
                    if (sx.Name == "DetectedLang")
                        return sx.GetAttribute("lang");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Ошибка.";
        }
    }
}
