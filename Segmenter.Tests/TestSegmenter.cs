﻿using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace JiebaNet.Segmenter.Tests
{
    [TestFixture]
    public class TestSegmenter
    {
        [TestCase]
        public void TestCutKnownWords()
        {
            JiebaSegmenter segmenter = new JiebaSegmenter();

            var sentence = "同学们看到了良好的测试结果，这些都是极好的";
            //var sentence = "I am a programmer.";
            var result = segmenter.process(sentence, JiebaSegmenter.SegMode.INDEX);
            Console.WriteLine(result.Count);
            foreach (var token in result)
            {
                Console.WriteLine(sentence.Sub(token.startOffset, token.endOffset));
            }

            result = segmenter.process(sentence, JiebaSegmenter.SegMode.SEARCH);
            Console.WriteLine(result.Count);
            foreach (var token in result)
            {
                Console.WriteLine(sentence.Sub(token.startOffset, token.endOffset));
            }
        }

        [TestCase]
        public void TestCutUnknownWords()
        {
            JiebaSegmenter segmenter = new JiebaSegmenter();

            var sentence = "小明不知何许人也，最近才学习机器学习知识";
            //var sentence = "I am a programmer.";
            var result = segmenter.process(sentence, JiebaSegmenter.SegMode.INDEX);
            Console.WriteLine(result.Count);
            foreach (var token in result)
            {
                Console.WriteLine(sentence.Sub(token.startOffset, token.endOffset));
            }

            result = segmenter.process(sentence, JiebaSegmenter.SegMode.SEARCH);
            Console.WriteLine(result.Count);
            foreach (var token in result)
            {
                Console.WriteLine(sentence.Sub(token.startOffset, token.endOffset));
            }
        }

        [TestCase]
        public void TestCutComplex()
        {
            JiebaSegmenter segmenter = new JiebaSegmenter();

            var sentence = "工信处女干事每月经过下属科室都要亲口交代24口交换机等技术性器件的安装工作";
            var result = segmenter.cut(sentence, JiebaSegmenter.SegMode.INDEX);
            Console.WriteLine(result.Count);
            Console.WriteLine(string.Join(" / ", result));

            result = segmenter.cut(sentence);
            Console.WriteLine(result.Count);
            Console.WriteLine(string.Join(" / ", result));
        }
    }
}