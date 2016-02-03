﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JiebaNet.Analyser;
using JiebaNet.Segmenter.Common;
using JiebaNet.Segmenter.Spelling;
using NUnit.Framework;

namespace JiebaNet.Segmenter.Tests
{
    [TestFixture]
    public class TestSpellChecker
    {
        // 控制中心，不列，一目了然
        [TestCase]
        public void TestGetEdits1()
        {
            var s = "不列";
            var checker = new SpellChecker();

            var edits1 = checker.GetEdits1(s);
            foreach (var edit in edits1)
            {
                Console.WriteLine(edit);
            }

            //var edits2 = checker.GetKnownEdits2(s);
            //foreach (var e2 in edits2)
            //{
            //    Console.WriteLine(e2);
            //}
            //Console.WriteLine("-----");

            //var sugguests = checker.Suggests(s);
            //foreach (var sugguest in sugguests)
            //{
            //    Console.WriteLine(sugguest);
            //}
        }

        [Test]
        public void TestSuggests()
        {
            var checker = new SpellChecker();
            var word = "振奋心人";
            var suggests = checker.Suggests(word);
            foreach (var suggest in suggests)
            {
                Console.WriteLine(suggest);
            }
        }

        [Test]
        public void TestWordDictToTrie()
        {
            var trie = GetWordDictTrie();
            Console.WriteLine(trie.Count); // 349045 (v.37)
            Console.WriteLine(trie.TotalFrequency); // 60101964 (v.37)

            Assert.That(trie.Contains("不列颠"));

            Assert.That(trie.ContainsPrefix("不列"));
            Assert.That(trie.Contains("不列"), Is.False);
        }

        [Test]
        public void TestNextCharsOf()
        {
            var trie = GetWordDictTrie();

            var chars = trie.ChildChars("天地");
            Console.WriteLine(chars.Count());
            foreach (var c in chars)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        public void TestReplaces()
        {
            var trie = GetWordDictTrie();
            var firstCharDict = GetFirstChars();
            var firstChars = firstCharDict['言'];
            foreach (var firstChar in firstChars)
            {
                Console.WriteLine(firstChar);
            }
        }

        private Trie GetWordDictTrie()
        {
            var wordDict = WordDictionary.Instance;
            var trie = new Trie();
            foreach (var wd in wordDict.Trie)
            {
                if (wd.Value > 0)
                {
                    trie.Insert(wd.Key, wd.Value);
                }
            }
            return trie;
        }

        private Dictionary<char, HashSet<char>> GetFirstChars()
        {
            var wordDict = WordDictionary.Instance;
            var result = new Dictionary<char, HashSet<char>>();
            foreach (var wd in wordDict.Trie)
            {
                if (wd.Value > 0 && wd.Key.Length >= 2)
                {
                    var second = wd.Key[1];
                    var first = wd.Key[0];
                    if (!result.ContainsKey(second))
                    {
                        result[second] = new HashSet<char>();
                    }
                    result[second].Add(first);
                }
            }
            return result;
        }
    }
}