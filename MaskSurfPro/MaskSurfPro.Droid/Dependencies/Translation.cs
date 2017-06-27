using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Mono.Data.Sqlite;

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.TranslationOnAndroid))]

namespace MaskSurfPro.Droid
{
    public class TranslationOnAndroid : ITranslate
    {
        public TranslationOnAndroid()
        {
        }

        public string GetEnglish(string StringID)
        {
            string path = @"/data/data/com.thanksoft.masksurfpro/databases/languages.db";
            if (System.IO.File.Exists(path))
            {
                var connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT StringContent FROM [English] WHERE StringID=\"" + StringID + "\"";
                    var reply = command.ExecuteScalar();
                    if (reply != null)
                    {
                        connection.Close();
                        return reply.ToString();
                    }
                }

                connection.Close();
            }
            return "";
        }
        public string GetRussian(string StringID)
        {
            string path = @"/data/data/com.thanksoft.masksurfpro/databases/languages.db";
            if (System.IO.File.Exists(path))
            {
                var connection = new SqliteConnection("Data Source=" + path);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT StringContent FROM [Russian] WHERE StringID=\"" + StringID + "\"";
                    var reply = command.ExecuteScalar();
                    if (reply != null)
                    {
                        connection.Close();
                        return reply.ToString();
                    }
                }

                connection.Close();
            }
            return "";
        }
    }
}