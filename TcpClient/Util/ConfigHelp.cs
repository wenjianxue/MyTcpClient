﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MyTcpClient.Util
{
    public class ConfigHelp
    {
     public string inipath;
     [DllImport("kernel32")]
     private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
     [DllImport("kernel32")]
     private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// 构造方法
     public ConfigHelp(string INIPath)
    }
}