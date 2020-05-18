////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Reflection;
////using System.Text;
////using UnityEngine;
////using Random = System.Random;
////using RandomUnity = UnityEngine.Random;

////public partial class WordRacer
////{
////    internal class Level  {

////        public string word;
////        public char[] wordChars;
////        public bool[] revealed;
////        public List<char> noChars;
////        public List<char> okChars;
////        private int roundIndex = 0;
////        private static Vector2[] ROUNDS = new Vector2[] { 
////            new Vector2(2,3),
////            new Vector2(2,4),
////            new Vector2(1,3),
////            new Vector2(2,5),
////            new Vector2(1,4),
////            new Vector2(2,6),
////            new Vector2(1,5),
////            new Vector2(1,6),
////            new Vector2(2,7),
////            new Vector2(1,7)
////        };

////        public Level (string word) {
            
////            Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));
			
////            this.word = word.ToLower();
////            this.wordChars = word.ToCharArray();
////            revealed = new bool[this.wordChars.Length];

////            noChars = new List<char>();
////            okChars = new List<char>();

////            foreach (var c in TileChar.chars) {
////                if (this.word.IndexOf(c) == -1) 
////                    noChars.Add(c);
////                else 
////                    okChars.Add(c);
////            }
////        }

////        public bool HasChar (char c) {
////            if (okChars.IndexOf(c) == -1) return false;

////            var index = okChars.IndexOf (c);
////            okChars.RemoveAt (index);

////            for (var i = 0; i < wordChars.Length; i++) {
////                if ( wordChars [i] == c ) {
////                    revealed [i] = true;
////                }
////            }
////            return true;
////        }

////        public char[] GetPlayersLetterDeck1()
////        {
////            Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

////            var result = new List<char>();

////            var randomString = getRandomString();

////            foreach (var r in randomString)
////            {
////                result.Add(r);
////            }

////            return result.ToArray();
////        }

////        public string getRandomString()
////        {
////            int length = 7;

////            // creating a StringBuilder object()
////            StringBuilder str_build = new StringBuilder();
////            Random random = new Random();

////            char letter;

////            for (int i = 0; i < length; i++)
////            {
////                double flt = random.NextDouble();
////                int shift = Convert.ToInt32(Math.Floor(25 * flt));
////                letter = Convert.ToChar(shift + 65);
////                str_build.Append(letter.ToString().ToLower());
////            }
////            return str_build.ToString();
////        }

////        public char[] GetPlayersLetterDeck()
////        {

////            Utils.MyLog(string.Format("Method '{0}' called", MethodBase.GetCurrentMethod()));

////            var result = new List<char>();

////            var correct = ROUNDS[roundIndex].x;
////            var total = ROUNDS[roundIndex].y;

////            if (correct > okChars.Count)
////                correct = okChars.Count;

////            var i = 0;
////            while (result.Count < total)
////            {
////                if (result.Count < correct)
////                {
////                    var c = okChars[RandomUnity.Range(0, okChars.Count)];
////                    if (!result.Contains(c))
////                    {
////                        result.Add(c);
////                    }
////                }
////                else
////                {
////                    var c = noChars[RandomUnity.Range(0, noChars.Count)];
////                    if (!result.Contains(c))
////                    {
////                        result.Add(c);
////                    }
////                }
////            }

////            roundIndex++;
////            if (roundIndex == ROUNDS.Length)
////                roundIndex = ROUNDS.Length - 1;

////            // new.. delete if issue.
////            ////result.Clear();
////            ////var chars1 = GetPlayersLetterDeck1();
////            ////foreach (var c in chars1)
////            ////{
////            ////    result.Add(c);
////            ////}
////            // end new

////            return result.ToArray();
////        }
////    }
////}