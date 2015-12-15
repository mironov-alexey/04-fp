using System;
using CommandLine;
using Nuclex.Game.Packing;
//using Annytab;
//using Iveonik.Stemmers;

namespace TagsCloudGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*
                1) Распарсить аргументы командной строки
                2) Загрузить настройки
                3) Загрузить список слов
                4) Загрузить чёрный список
                x) Привести в начальную форму?
                5) Отфильтровать слова
                6) Собрать статистику по словам
                7) Сформировать информацию для отрисовки
                8) Отрисовать
                9) Сохранить
            */
            var options = new Options();
            Parser.Default.ParseArguments(args, options);
            var settings = SettingsLoader.LoadFromFile(options.PathToConfig);
            var tags = WordsLoader
                .LoadWords(options.PathToWords)
                .FilterBannedWords(options.PathToBlackList)
                .Calculate(settings)
                .BuildTags(settings, new ArevaloRectanglePacker(int.MaxValue, int.MaxValue).Pack, FontGenerator.GetFont);
            using (var cloud = CloudGenerator.GenerateCloudImage(tags, settings))
            {
                CloudSaver.Save(cloud, options);
            }
        }
    }
}