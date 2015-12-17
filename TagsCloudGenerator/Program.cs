using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Microsoft.Xna.Framework;
using Nuclex.Game.Packing;

namespace TagsCloudGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArguments(args, options);
            var settings = SettingsLoader.LoadFromFile(options.PathToConfig);
            var wordsWithFrequency = WordsLoader
                .LoadWords(options.PathToWords)
                .FilterBannedWords(options.PathToBlackList)
                .GetWordsWithFrequency(settings);
            var tags = GetTags(settings, wordsWithFrequency, new ArevaloRectanglePacker(int.MaxValue, int.MaxValue).Pack);
            using (var cloud = CloudGenerator.GenerateCloudImage(tags, settings))
            {
                CloudSaver.Save(cloud, options);
            }
        }

        private static IEnumerable<Tag> GetTags(Settings settings, IReadOnlyList<Word> wordsWithFrequency, Func<int, int, Point> pack)
        {
            Random random = new Random();
            return wordsWithFrequency
                .Select(w => new
                {
                    Word = w,
                    Font = FontGenerator.GetFont(
                        settings,
                        wordsWithFrequency.Max(wf => wf.Frequency),
                        wordsWithFrequency.Min(wf => wf.Frequency), w)
                })
                .Select(pair => new {pair.Word, pair.Font, Size = TagsGenerator.GetTagSize(pair.Word, pair.Font)})
                .Select(three => new Tag(
                    three.Word,
                    pack((int) three.Size.Width, (int) three.Size.Height),
                    three.Font,
                    TagsGenerator.GetRandomColor(settings.Colors, random)));
        }
    }
}