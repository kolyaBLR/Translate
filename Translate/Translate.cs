using System.Collections.Generic;
using System.Linq;

namespace Translate
{
    abstract class Translate
    {
        protected List<string> listUi = new List<string>(); // список сокращённых кодировок ui
        protected List<string> listNameLang = new List<string>(); // список зыков

        /// <summary>
        /// корректировка текста для Web запроса, расставляем плюсы вместо пробелов
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string parse(string text)
        {
            while (text.IndexOf(" ") >= 0)
                text = text.Replace(" ", "+");
            return text;
        }

        /// <summary>
        /// проверка введено ли число
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool isNumber(string text)
        {
            // из-за того что я посылаю число на стороye сервера, сервер не может мне вернуть язык для перевода
            // поэтому и существует эта проверка на стороне клиента, ввёл ли он число
            int num;
            return text == "" || int.TryParse(text, out num);
        }

        /// <summary>
        /// переводим текст
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public abstract string translate(string text, string lang);

        /// <summary>
        /// получение языка для переводимого текста
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string getLang(string text);

        /// <summary>
        /// получение списка поддерживаемых языков для перевода
        /// </summary>
        /// <param name="ui"></param>
        public abstract void listLang(string ui);
    }
}
