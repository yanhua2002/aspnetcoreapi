using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTestWeb.Controllers
{
    public class WxController : ApiBaseController
    {
        // GET api/wx
        [HttpGet("")]
        public string Get()
        {
            if (Request.Query.ContainsKey("signature") &&
                Request.Query.ContainsKey("timestamp") &&
                Request.Query.ContainsKey("nonce") &&
                Request.Query.ContainsKey("echostr"))
            {
                var _token = "XXXXXXXXXXXX";

                var _signature = Request.Query["signature"];
                var _timestamp = Request.Query["timestamp"];
                var _nonce = Request.Query["nonce"];
                var _echostr = Request.Query["echostr"];

                var queryArr = new string[] { _token, _timestamp, _nonce }.OrderBy(z => z).ToArray();
                var queryValueStr = string.Join("", queryArr);

                var sha1Str = GetSha1(queryValueStr);

                if (string.Equals(_signature, sha1Str))
                    return _echostr;
                //return Content(_echostr);
                else
                    return "Sorry, but who is it!";
            }
            else
                return "Hey, U do know what I need, right?";

        }

        public static string GetSha1(string sourceStr)
        {
            var sha1Code = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
            StringBuilder sb = new StringBuilder();
            foreach (var b in sha1Code)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

    }
}