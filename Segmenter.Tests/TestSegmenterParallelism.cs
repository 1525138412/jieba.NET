﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace JiebaNet.Segmenter.Tests
{
    [TestFixture]
    [Ignore]
    public class TestSegmenterParallelism
    {
        private string[] GetTestSentences()
        {
            return File.ReadAllLines(@"Cases\jieba_test.txt");
        }

        [TestCase]
        public void TestCut()
        {
            TestCutFunction((new JiebaSegmenter()).Cut, false, true, @"Cases\accurate_hmm.txt");
        }

        [TestCase]
        public void TestCutAll()
        {
            TestCutFunction((new JiebaSegmenter()).Cut, true, false, @"Cases\cut_all.txt");
        }

        [TestCase]
        public void TestCutWithoutHmm()
        {
            TestCutFunction((new JiebaSegmenter()).Cut, false, false, @"Cases\accurate_no_hmm.txt");
        }

        [TestCase]
        public void TestCutForSearch()
        {
            TestCutSearchFunction((new JiebaSegmenter()).CutForSearch, true, @"Cases\cut_search_hmm.txt");
        }

        [TestCase]
        public void TestCutForSearchWithoutHmm()
        {
            TestCutSearchFunction((new JiebaSegmenter()).CutForSearch, false, @"Cases\cut_search_no_hmm.txt");
        }

        [TestCase]
        public void TestTokenize()
        {
            var seg = new JiebaSegmenter();
            seg.AddWord("机器学习");
            seg.AddWord("自然语言处理");
            foreach (var token in seg.Tokenize("小明最近在学习机器学习、自然语言处理、云计算和大数据"))
            {
                Console.WriteLine(token);
            }

            foreach (var token in seg.Tokenize("小明最近在学习机器学习、自然语言处理、云计算和大数据", TokenizerMode.Search))
            {
                Console.WriteLine(token);
            }
        }

        [TestCase]
        public void TestAddWord()
        {
            var seg = new JiebaSegmenter();
            var s = "小明最近在学习机器学习和自然语言处理";

            var segments = seg.Cut(s);
            Assert.That(segments, Contains.Item("机器"));
            Assert.That(segments, Contains.Item("学习"));

            seg.AddWord("机器学习");
            segments = seg.Cut(s);
            Assert.That(segments, Contains.Item("机器学习"));
            Assert.That(segments, Is.Not.Contains("机器"));
        }

        #region Private Helpers

        private void TestCutFunction(Func<string, bool, bool, IEnumerable<string>> method,
                                     bool cutAll, bool useHmm,
                                     string testResultFile)
        {
            var testCases = GetTestSentences();
            var testResults = File.ReadAllLines(testResultFile);
            Assert.That(testCases.Length, Is.EqualTo(testResults.Length));
            for (int i = 0; i < testCases.Length; i++)
            {
                var testCase = testCases[i];
                var testResult = testResults[i];
                Assert.That(method(testCase, cutAll, useHmm).Join("/ "), Is.EqualTo(testResult));
            }
        }

        private void TestCutSearchFunction(Func<string, bool, IEnumerable<string>> method,
                                     bool useHmm,
                                     string testResultFile)
        {
            var testCases = GetTestSentences();
            var testResults = File.ReadAllLines(testResultFile);
            Assert.That(testCases.Length, Is.EqualTo(testResults.Length));
            for (int i = 0; i < testCases.Length; i++)
            {
                var testCase = testCases[i];
                var testResult = testResults[i];
                Assert.That(method(testCase, useHmm).Join("/ "), Is.EqualTo(testResult));
            }
        }

        #endregion
    }
}