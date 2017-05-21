using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shell
{
    class AppConfig
    {
        public static string WebService;
        public static bool UseLocalAppFolder;
        public static string LocalAppFolder;
        public static string Application;


        static AppConfig()
        {
            WebService = ReadString("WebService");
            UseLocalAppFolder = ReadBoolean("UseLocalAppFolder");
            LocalAppFolder = ReadString("LocalAppFolder");
            Application = ReadString("Application");
        }


        static bool ReadBoolean(string name)
        {
            var value = ReadString(name).ToLower();
            return value == "y" || value == "true" || value == "1";
        }


        static double ReadDouble(string name)
        {
            var value = ReadString(name);
            return Convert.ToDouble(value);
        }


        static int ReadInt32(string name)
        {
            var value = ReadString(name);
            return Convert.ToInt32(value);
        }


        static string ReadString(string name)
        {
            var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            var value = appConfig.AppSettings.Settings[name].Value;
            return "" + value;
        }
    }
}
