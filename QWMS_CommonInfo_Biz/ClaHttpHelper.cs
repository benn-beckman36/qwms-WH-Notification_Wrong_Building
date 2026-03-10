using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Common
{
    public class ClaHttpHelper
    {
        public string HttpPostByHttpWebRequest(string Url, object PostData)
        {
            try
            {
                //POST参数
                string strPostData = PostData.ToString().Replace("\r\n", "");
                byte[] bytPostData = Encoding.UTF8.GetBytes(strPostData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 1000000;
                request.ContentType = "application/json";   //"application/x-www-form-urlencoded";
                request.ContentLength = bytPostData.Length;

                //解决IIS配置
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                System.IO.Stream objPostStream = request.GetRequestStream();
                objPostStream.Write(bytPostData, 0, bytPostData.Length);
                objPostStream.Close();
                //获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader objResponseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string strResult = objResponseStreamReader.ReadToEnd();
                objResponseStreamReader.Close();

                return strResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string HttpGetByHttpWebRequest(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 1000000;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader objResponseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string strResult = objResponseStreamReader.ReadToEnd();
                objResponseStreamReader.Close();

                return strResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Task<string> HttpPostAsync(string url, string postData = null, string contentType = "application/json", int timeOut = 30, Dictionary<string, string> headers = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            if (!string.IsNullOrEmpty(contentType))
            {
                request.ContentType = contentType;
            }
            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers[header.Key] = header.Value;
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(postData ?? "");
                using (Stream sendStream = request.GetRequestStream())
                {
                    sendStream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    return streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }

        }
    }
}
