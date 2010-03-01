﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    public class QQPinyin :  IWordLibraryImport, IWordLibraryExport
    {
        #region IWordLibraryExport 成员

        public string Export(WordLibraryList wlList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wlList.Count; i++)
            {
                sb.Append(wlList[i].GetPinYinString("'", BuildType.None));
                sb.Append(" ");
                sb.Append(wlList[i].Word);
                sb.Append(" 1");
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
        public Encoding Encoding
        {
            get
            {
                return Encoding.Default;
            }
        }
        #endregion

        #region IWordLibraryImport 成员

        public WordLibraryList Import(string str)
        {
            WordLibraryList wlList = new WordLibraryList();
            var lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string py = line.Split(' ')[0];
                string word = line.Split(' ')[1];
                WordLibrary wl = new WordLibrary();
                wl.Word = word;
                wl.Count = 1;
                wl.PinYin = new List<string>(py.Split(new char[] { '\'' }, StringSplitOptions.RemoveEmptyEntries));
                wlList.Add(wl);

            }
            return wlList;
        }
      

        #endregion
    }
}