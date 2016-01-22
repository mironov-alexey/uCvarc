using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using CVARC.V2;

namespace Assets.Tools
{
    public static class HttpWorker
    {
        public static void SendGameResults(string leftTag, string rightTag, int leftScore, int rightScore, string logGuid)
        {
            if (!CheckForForbiddenSymbols(leftTag) || !CheckForForbiddenSymbols(rightTag))
                return;
            var request = (HttpWebRequest) WebRequest.Create(string.Format(
                "http://{0}:{1}/{2}?password={3}&leftTag={4}&rightTag={5}&leftScore={6}&rightScore={7}&logFileName={8}",
                UnityConstants.WebIp, UnityConstants.WebPort, UnityConstants.Method, UnityConstants.PasswordToWeb,
                leftTag, rightTag, leftScore, rightScore, logGuid));

            // как-то запихнуть лог-файл.

            var response = request.GetResponse();
            var responseString = Encoding.Default.GetString(response.GetResponseStream().ReadToEnd());
            Debugger.Log(DebuggerMessageType.Unity, "Game result sent. answer: " + responseString);

            var nvc = new NameValueCollection();
            nvc.Add("password", UnityConstants.PasswordToWeb);
            var a = HttpUploadFile(string.Format(
                "http://{0}:{1}/{2}", UnityConstants.WebIp, UnityConstants.WebPort, "Rules/PushLog"), 
                UnityConstants.LogFolderRoot + logGuid, "file", "multipart/form-data", nvc);

            Debugger.Log(DebuggerMessageType.Unity, a);

        }

        private static bool CheckForForbiddenSymbols(string value)
        {
            return value.All(ch => char.IsLetterOrDigit(ch) || ch == '-');
        }

        public static string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp;
            wresp = wr.GetResponse();
            return Encoding.Default.GetString(wresp.GetResponseStream().ReadToEnd());
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);

            return "keek";
        }
    }
}
