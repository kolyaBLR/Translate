using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Текст для перевода:");
                string text = Console.ReadLine();
                YandexAPI yandex = new YandexAPI();
                if (!yandex.isNumber(text))
                {
                    string inLang = yandex.getLang(yandex.parse(text)), outLang = ""; // in - входной язык, out - выходной язык
                    yandex.listLang(inLang);
                    for (int i = 0; i < yandex.getListNameLang().Count; i++)    // вывожу список доступных языков
                        Console.WriteLine((i + 1) + ")" + yandex.getListNameLang()[i] + "(" + yandex.getListUi()[i] + ")");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    outLang = yandex.getListUi()[index];
                    Console.WriteLine("Переведёный текст:" + yandex.translate(yandex.parse(text), inLang + "-" + outLang));
                }
                else
                {
                    if (text != "")
                        Console.WriteLine("Переведёный текст:" + text);
                    else
                        Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}