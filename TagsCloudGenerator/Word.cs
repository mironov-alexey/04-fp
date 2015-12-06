namespace TagsCloudGenerator
{
    internal class Word
    {
        public Word(string value, int frequency)
        {
            Value = value;
            Frequency = frequency;
        }

        public string Value{ get; }
        public int Frequency{ get; }
    }
}