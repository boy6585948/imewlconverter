﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Collections;

namespace Studyzy.IMEWLConverter
{
   public class PinyinHelper
    {
       private PinyinHelper()
       {}

       /// <summary>
       /// 获得单个字的拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static List<string> GetPinYinOfChar(char str)
       {                   
           return PinYinDict[str];
       }
       /// <summary>
       /// 获得一个词的拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>       
       public static List<string> GetPinYinOfString(string str, string split)
       {
           List<string> pinyinList = new List<string>();

           for (int i = 0; i < str.Length; i++)
           {
               var py = GetPinYinOfChar(str[i]);
               if (pinyinList.Count == 0)
               {
                   py.ForEach(delegate(string p)
                   {
                       pinyinList.Add(p + split);
                   });

               }
               else//不是第一个字
               {
                   List<string> newString = new List<string>();
                   foreach (string p in py)
                   {
                       string charPy = p + split;

                       
                       pinyinList.ForEach(delegate(string s)
                       {
                           newString.Add(s + charPy);
                       });                      
                   }
                   pinyinList = newString;
               }

           }
           return pinyinList;
       }

       /// <summary>
       /// 获得一个词的所有拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static List<List<string>> GetPinYinListOfString(string str)
       {
           var pyList= GetPinYinOfString(str, "'");
           List<List<string>> pyArray = new List<List<string>>();
           pyList.ForEach(delegate(string pyString)
           {
               List<string> py = new List<string>(pyString.Split('\''));
               pyArray.Add(py);
           });
           return pyArray;
       }

       /// <summary>
       /// 获得一个词中的每个字的音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static List<List<string>> GetPinYinOfStringEveryChar(string str)
       {
           List<List<string>> pyList = new List<List<string>>();
           for (int i = 0; i < str.Length; i++)
           {
               pyList.Add(GetPinYinOfChar(str[i]));
           }
           return pyList;
       }

       private static Dictionary<char, List<string>> dictionary = new Dictionary<char, List<string>>();
       private static Dictionary<char, List<string>> PinYinDict
       {
           get
           {
               if (dictionary.Count == 0)
               {
                   string[] pyList = PinyinDic.AllPinYin.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                   for (int i = 0; i < pyList.Length; i++)
                   {
                       string[] hzpy = pyList[i].Split(',');
                       char hz =Convert.ToChar( hzpy[0]);
                       string py = hzpy[1];
                       if (dictionary.ContainsKey(hz))
                       {
                           dictionary[hz].Add(py);
                       }
                       else
                       {
                           dictionary.Add(hz,new List<string>(){ py});
                       }
                   }
               }
               return dictionary;
           }
       }

    }
}
