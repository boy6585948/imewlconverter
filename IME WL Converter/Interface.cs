﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Studyzy.IMEWLConverter
{
    interface IWordLibraryImport
    {
        WordLibraryList Import(string str);
    }
    interface IWordLibraryExport
    {
        string Export(WordLibraryList wlList);
        Encoding Encoding { get; }
    }
}