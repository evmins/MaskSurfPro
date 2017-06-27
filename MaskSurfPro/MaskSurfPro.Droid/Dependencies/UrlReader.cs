using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Java.Net;
using Java.IO;

using MaskSurfPro.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MaskSurfPro.Droid.UrlReader))]

namespace MaskSurfPro.Droid
{
        public class UrlReader : IGetUrl
    {
        public UrlReader()
        {
        }
        public string GetUrl()
        {
            URL ipurl = new URL("https://showtheip.net/scripts/getip.php");
            Java.Net.Proxy proxy = new Java.Net.Proxy(Java.Net.Proxy.Type.Http, new InetSocketAddress("127.0.0.1", Settings.GetInt("Polipo port", 8000)));
            BufferedReader reader = new BufferedReader(new InputStreamReader(ipurl.OpenConnection(proxy).InputStream));
            System.Text.StringBuilder reply = new System.Text.StringBuilder();

            System.String inputLine;
            while ((inputLine = reader.ReadLine()) != null)
            {
                reply.Append(inputLine);
            }
            reader.Close();

            return reply.ToString();
        }
        public string PostToUrl(string IPsToSend)
        {
            System.Text.ASCIIEncoding a = new System.Text.ASCIIEncoding();  //формируем параметры и конвертируем их в POST байткод
            string postData = string.Format("IPs={0}", IPsToSend);
            byte[] bytePostdata = a.GetBytes(postData);
            string url;  //ссылка на скрипт

            switch (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
            {
                case "ru":
                    {
                        url = "https://showtheip.net/scripts/getcities-ru.php";
                        break;
                    }
                case "en":
                default:
                    {
                        url = "https://showtheip.net/scripts/getcities.php";
                        break;
                    }
            }


            URL scripturl = new URL(url);
            HttpURLConnection connection = null;
            connection = (HttpURLConnection)scripturl.OpenConnection();
            connection.RequestMethod = "POST";
            connection.SetRequestProperty("Content-Type", "application/x-www-form-urlencoded");

            //Send request
            DataOutputStream wr = new DataOutputStream(connection.OutputStream);
            wr.Write(bytePostdata);
            wr.Close();

            //Get Response  
            Stream ins = connection.InputStream;
            BufferedReader rd = new BufferedReader(new InputStreamReader(ins));
            System.Text.StringBuilder reply = new System.Text.StringBuilder(); // or StringBuffer if Java version 5+
            System.String line;
            while ((line = rd.ReadLine()) != null)
            {
                reply.AppendLine(line);
            }
            rd.Close();
            connection.Disconnect();
            return reply.ToString();
        }
    }
}