namespace TagsCloudGenerator
{
    internal class Word
    {
        public string Value{ get; }
        public int Frequency{ get; }

        public Word(string value, int frequency)
        {
            Value = value;
            Frequency = frequency;
        }
    }
}