using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WordFinder.Properties;

namespace WordFinder
{
    class Program
    {
        static void Main(string[] args)
        {            
            var wf = new WordFinder(Resources.matrix.Split(",").ToList());

            var result = wf.Find(new List<string>
            {
                "NICE",
                "YOU",
                "UMBRELLA",
                "MY",
                "COMPUTER",
                "NAME",
                "IS",
                "HOUSE",
                "DAMIAN",
                "TO",
                "MEET",
                "HELLO",
                "ALL"
            });

            Console.WriteLine(string.Join(" ", result));
            Console.ReadLine();
        }
    }

    public class WordFinder
    {
        private readonly List<string> matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            this.matrix = matrix.ToList();
        }
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords = new Dictionary<string, int>();

            var verticalLine = new StringBuilder();
            var horizontalLine = new StringBuilder();

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    verticalLine.Append(matrix[j][i]);
                    horizontalLine.Append(matrix[i][j]);
                }
                foreach (var word in wordstream)
                {
                    var wordCount = Regex.Matches(verticalLine.ToString(), word).Count + Regex.Matches(horizontalLine.ToString(), word).Count;
                    if (wordCount != 0)
                        foundWords[word] = !foundWords.ContainsKey(word) ? wordCount : foundWords[word] + wordCount;
                }
                horizontalLine.Clear();
                verticalLine.Clear();
            }

            return foundWords.OrderByDescending(x => x.Value).Select(x => x.Key).Take(10);
        }
    }
}
