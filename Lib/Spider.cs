using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace OneSale.Lib
{
    class Spider
    {
        public string GetInformation(string Url)
        {
            string result;
            string strResponse = GetPageData(Url);
            string strRef = @"(href|HREF|src|SRC|action|ACTION|Action)[ ]*=[ ]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(strResponse);
            Match TitleMatch = Regex.Match(strResponse, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            result = TitleMatch.Groups[1].Value;
            return result;
        }
        private string GetUrlToHtml(string Url, Encoding encode)
        {
            try
            {
                //构造httpwebrequest对象，注意，这里要用Create而不是new
                HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(Url);

                //伪造浏览器数据，避免被防采集程序过滤
                wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215; CrazyCoder.cn;www.jd.com)";

                //注意，为了更全面，可以加上如下一行，避开ASP常用的POST检查              

                wReq.Referer = "http://www.jd.com/";//您可以将这里替换成您要采集页面的主页
                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;
                // 获取输入流
                System.IO.Stream respStream = wResp.GetResponseStream();

                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, encode);
                string content = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                return content;
            }
            catch (System.Exception ex)
            {

            }
            return "";
        }
        private static string GetPageData(string url)
        {
            if (url == null || url.Trim() == "")
                return null;
            WebClient wc = new WebClient(); // 定义
            wc.Credentials = CredentialCache.DefaultCredentials;
            byte[] pageData = wc.DownloadData(url);
            return Encoding.Default.GetString(pageData);//.ASCII.GetString
        }
        private string GetPageData(string url, string charSet)
        {
            string strWebData = string.Empty;
            if (url != null || url.Trim() != "")
            {
                WebClient myWebClient = new WebClient();
                //创建WebClient实例myWebClient
                // 需要注意的：
                //有的网页可能下不下来，有种种原因比如需要cookie,编码问题等等
                //这是就要具体问题具体分析比如在头部加入cookie
                // webclient.Headers.Add("Cookie", cookie);
                //这样可能需要一些重载方法。根据需要写就可以了
                //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。
                myWebClient.Credentials = CredentialCache.DefaultCredentials;
                //如果服务器要验证用户名,密码
                //NetworkCredential mycred = new NetworkCredential(struser, strpassword);
                //myWebClient.Credentials = mycred;
                //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号）
                byte[] myDataBuffer = myWebClient.DownloadData(url);
                strWebData = Encoding.Default.GetString(myDataBuffer);
                //获取网页字符编码描述信息
                Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string webCharSet = charSetMatch.Groups[2].Value;
                if (charSet == null || charSet == "")
                {
                    //如果未获取到编码，则设置默认编码
                    if (webCharSet == null || webCharSet == "")
                    {
                        charSet = "UTF-8";
                    }
                    else
                    {
                        charSet = webCharSet;
                    }
                }
                if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
                {
                    strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
                }
            }
            return strWebData;
        }
    }
}
