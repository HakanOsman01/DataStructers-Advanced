using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
    class Program
    {
       
        static void Main()
        {
           var syllables=Console.ReadLine()
           .Split(", ",StringSplitOptions.RemoveEmptyEntries);
            var targetWord=Console.ReadLine();
            var cruncher=new Cruncher(syllables, targetWord);
        }
      
    }
    class Cruncher
    {
        private class Node
        {
            public Node(string syllable,List<Node>nextSyllables)
            {
                this.Syllable = syllable;
                this.NextSyllables = nextSyllables;
            }
            public string Syllable { get; set; }
            public List<Node> NextSyllables { get; set; }
        }
        
      
        private List<Node> syllableGroups { get; set; }
        public Cruncher(string[] syllables, string targetWord)
        {
           this.syllableGroups=this.GenerateSyllablesGroups(syllables, targetWord);
        }

        private List<Node> GenerateSyllablesGroups(string[] syllables, string targetWord)
        {
           if(string.IsNullOrEmpty(targetWord) || syllables.Length == 0)
           {
                return null;
           }
           var resultValues=new List<Node>();
            for (int i = 0; i < syllables.Length; i++)
            {
                var syllable = syllables[i];

                if (targetWord.StartsWith(syllable))
                {
                    var nextSyllable = this.GenerateSyllablesGroups();
                    resultValues.Add(new Node(syllable, nextSyllable));

                }



            }
        }
    }
}
