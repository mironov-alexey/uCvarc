using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CVARC.V2;

namespace Assets.Tools
{
    public static class HttpWorker
    {
        public static void SendGameResults(string leftTag, string rightTag, int leftScore, int rightScore)
        {
            if (!CheckForForbiddenSymbols(leftTag) || !CheckForForbiddenSymbols(rightTag))
                return;
            var request = (HttpWebRequest) WebRequest.Create(string.Format(
                "{0}:{1}/{2}?leftTag={3}&rightTag={4}&leftScore={5}&rightScore={6}",
                UnityConstants.WebIp, UnityConstants.WebPort, UnityConstants.Method, 
                leftTag, rightScore, leftScore, rightScore));
            var response = request.GetResponse();
            var responseString = response.GetResponseStream().ReadToEnd();
            Debugger.Log("Game result sent. answer: " + DebuggerMessageType.Unity, responseString);
        }

        private static bool CheckForForbiddenSymbols(string value)
        {
            return value.All(ch => char.IsLetterOrDigit(ch) || ch == '-');
        }
    }
}
