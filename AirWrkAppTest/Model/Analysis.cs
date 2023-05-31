namespace AirWrkAppTest.Model
{
    public class Analysis
    {
        public int charCount { get; set; }
        public int wordCount { get; set; }
        public int sentenceCount { get; set; }
        public Mostfrequentword mostFrequentWord { get; set; }
        public Longestword longestWord { get; set; }
    }

    public class Mostfrequentword
    {
        public string word { get; set; }
        public int frequency { get; set; }
    }

    public class Longestword
    {
        public string word { get; set; }
        public int length { get; set; }
    }


}
