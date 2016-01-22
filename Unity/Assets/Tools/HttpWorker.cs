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
            Debugger.Log(DebuggerMessageType.Unity, "Http got...");
            Debugger.Log(DebuggerMessageType.Unity, leftTag + leftScore + rightTag + rightScore);
            var request = (HttpWebRequest) WebRequest.Create(string.Format(
                "http://{0}:{1}/{2}?password={3}&leftTag={4}&rightTag={5}&leftScore={6}&rightScore={7}",
                UnityConstants.WebIp, UnityConstants.WebPort, UnityConstants.Method, UnityConstants.PasswordToWeb,
                leftTag, rightTag, leftScore, rightScore));
            var response = request.GetResponse();
            Debugger.Log(DebuggerMessageType.Unity, "Sent");
            var responseString = Encoding.Default.GetString(response.GetResponseStream().ReadToEnd());
            Debugger.Log(DebuggerMessageType.Unity, "Game result sent. answer: " + responseString);
        }

        private static bool CheckForForbiddenSymbols(string value)
        {
            return value.All(ch => char.IsLetterOrDigit(ch) || ch == '-');
        }
    }
}
