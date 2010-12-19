﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Studyzy.IMEWLConverter
{
   public class PinyinJiaJia:IWordLibraryExport,IWordLibraryImport
    {
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(ExportLine(wlList[i]));
                sb.Append( "\r\n");
            }
            return sb.ToString();
        }

        public Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        #endregion

        #region IWordLibraryImport 成员

        public int CountWord        {            get;            set;        }
        public int CurrentStatus { get; set; }
        SinglePinyin single = new SinglePinyin();
       /// <summary>
        /// 形如：冷血xue动物
        /// 只有多音字才注音，一般的字不注音，就使用默认读音即可
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public WordLibraryList Import(string str)
        {
            
            WordLibraryList wlList = new WordLibraryList();
            string[] words = str.Split(new char[] { '\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
            CountWord = words.Length;
            for (int i = 0; i < words.Length; i++)
            {
                CurrentStatus = i;
                try
                {
                   wlList.AddWordLibraryList(ImportLine(words[i]));
                }
                catch
                {
                }
            }
            return wlList;
        }

        #endregion
        public string ExportLine(WordLibrary wl)
        {
            StringBuilder sb = new StringBuilder();

            string str = wl.Word;
            for (int j = 0; j < str.Length; j++)
            {
                sb.Append(str[j] + wl.PinYin[j]);
            }

            return sb.ToString();
        }


        public WordLibraryList ImportLine(string word)
        {
            string hz = "";
            List<string> py = new List<string>();
            int j;
            for (j = 0; j < word.Length - 1; j++)
            {
                hz += word[j];
                if (word[j + 1] > 'z')//而且后面跟的不是拼音
                {
                    py.Add(single.GetPinYinOfChar(word[j])[0]);
                }
                else//后面跟拼音
                {
                    int k = 1;
                    string py1 = "";
                    while (j + k != word.Length && word[j + k] <= 'z')
                    {
                        py1 += word[j + k];
                        k++;
                    }
                    py.Add(py1);
                    j += k - 1;//减1是因为接下来会运行j++
                }

            }
            if (j == word.Length - 1)//最后一个字是汉字
            {
                hz += word[j];
                py.Add(single.GetPinYinOfChar(word[j])[0]);
            }
            WordLibrary wl = new WordLibrary();
            wl.PinYin = py.ToArray();
            wl.Word = hz;
            WordLibraryList wll = new WordLibraryList();
            wll.Add(wl);
            return wll;

        }
    }
}
