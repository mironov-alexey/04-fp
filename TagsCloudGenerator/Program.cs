using CommandLine;
using Nuclex.Game.Packing;

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
                5) Собрать статистику по словам
                6) Сформировать информацию для отрисовки
                7) Отрисовать
                8) Сохранить
            */

            var options = new Options();
            Parser.Default.ParseArguments(args, options);
            var settings = SettingsLoader.LoadFromFile(options.PathToConfig);
            var words = WordsLoader.LoadWordsFromTxt(options.PathToWords);
            var blackList = WordsLoader.LoadBlackListFromTxt(options.PathToBlackList);
            var statistic = Statistic.Calculate(words, blackList, settings);
            var packer = new ArevaloRectanglePacker(int.MaxValue, int.MaxValue);
            var tags = TagsGenerator.BuildTags(statistic, settings, packer.Pack, FontGenerator.GetFont);
            using (var cloud = CloudGenerator.GenerateCloudImage(tags, settings))
            {
                CloudSaver.Save(cloud, options);
            }
        }
    }
}