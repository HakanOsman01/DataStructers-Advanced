using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
    class Program
    {
       
        static void Main()
        {
            string[]words=Console.ReadLine()
                .Split(", ")
                .Distinct()
                .ToArray();

            string formatedWords=Console.ReadLine();
            Permution(words, formatedWords,0,words.Length-1);

        }
        private static void Permution(string[] words
            ,string formatedString,int start,int end )
        {
            if (string.Join("", words) == formatedString)
            {
                Console.WriteLine(string.Join(" ",words));
                return;
            }

            Permution(words, formatedString, start + 1, end);
            HashSet<string> uniqueWords = new HashSet<string>{string.Join(" ", words)};
            for (int i = start+1; i <=end; i++)
            {


                if (!uniqueWords.Contains(string.Join(" ",words)))
                {
                    Swap(words, start, i);
                    Permution(words, formatedString, start + 1, end);
                    Swap(words, start, i);

                }
                 
                
               
            }
          

        }
        private static void Swap(string[]words,int firstIndex,int secondIndex)
        {
            var swap = words[firstIndex];
            words[firstIndex] = words[secondIndex];
            words[secondIndex] = swap;
        }
    }
}
