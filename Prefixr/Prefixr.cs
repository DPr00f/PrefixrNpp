using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Prefixr
{
    public static class Prefixr 
    {
        private static string getFromweb(this string website, string data)
        {
            WebRequest webRequest = WebRequest.Create(website);
           webRequest.ContentType = "application/x-www-form-urlencoded";
           webRequest.Method = "POST";
           byte[] bytes = Encoding.ASCII.GetBytes(data);
           Stream os = null;
           try
           {
              webRequest.ContentLength = bytes.Length;   //Count bytes to send
              os = webRequest.GetRequestStream();
              os.Write (bytes, 0, bytes.Length);         //Send it
           }
           catch (WebException ex)
           {
           }
           finally
           {
              if (os != null)
              {
                 os.Close();
              }
           }
 
           try
           { // get the response
              WebResponse webResponse = webRequest.GetResponse();
              if (webResponse == null) 
                 { return null; }
              StreamReader sr = new StreamReader (webResponse.GetResponseStream());
              return sr.ReadToEnd ().Trim();
           }
           catch (WebException ex)
           {
           }
           return null;
        }
        private static string Encode(this string str)
        {
            var charClass = String.Format("0-9a-zA-Z{0}", Regex.Escape("-_.!~*'()"));
            return Regex.Replace(str,
                String.Format("[^{0}]", charClass),
                new MatchEvaluator(EncodeEvaluator));
        }
        private static string EncodeEvaluator(Match match)
        {
            return (match.Value == " ") ? "+" : String.Format("%{0:X2}", Convert.ToInt32(match.Value[0]));
        }
        public static string getCss(this string css)
        {
            css = "http://prefixr.com/api/index.php".getFromweb("css=" + css.Encode());
            return css;
        }
    }
}
