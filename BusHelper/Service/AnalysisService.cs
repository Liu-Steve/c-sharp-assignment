using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using BusHelper.Models;
using Newtonsoft.Json.Linq;

namespace BusHelper.Service;
public class DriverBehaviorAnalysis
{
    // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
    // 返回token示例
    public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

    // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
    private static String clientId = "vGQlxGEKG8hjzZ95qbSIk3F1";
    // 百度云中开通对应服务应用的 Secret Key
    private static String clientSecret = "NTR31Ct6FH6Hh65q84bQGUk8vGxVoSLH";

    public static String getAccessToken() {
        String authHost = "https://aip.baidubce.com/oauth/2.0/token";
        HttpClient client = new HttpClient();
        List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
        paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
        paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
        paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

        HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
        String result = response.Content.ReadAsStringAsync().Result;
        return result;
    }
    // 驾驶行为分析
    public static string driver_behavior(String fileName)
    {

        string json = getAccessToken();
        RootObject rb=JsonConvert.DeserializeObject<RootObject>(json);
        string token=rb.access_token;
        string host = "https://aip.baidubce.com/rest/2.0/image-classify/v1/driver_behavior?access_token=" + token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "post";
        request.KeepAlive = true;
        // 图片的base64编码
        string base64 = getFileBase64(fileName);
        String str = "image=" + HttpUtility.UrlEncode(base64);
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        return result;
    }

    public static String getFileBase64(String fileName) {
        FileStream filestream = new FileStream(fileName, FileMode.Open);
        byte[] arr = new byte[filestream.Length];
        filestream.Read(arr, 0, (int)filestream.Length);
        string baser64 = Convert.ToBase64String(arr);
        filestream.Close();
        return baser64;
    }

    public static void parseJson(RealTimeRecord realTimeRecord,JObject json){
        realTimeRecord.DangerAction.Smoke=(float)json["person_info"][0]["attributes"]["smoke"]["score"];
        realTimeRecord.DangerAction.Yawn=(float)json["person_info"][0]["attributes"]["smoke"]["yawning"];
        realTimeRecord.DangerAction.NoSafetyBelt=(float)json["person_info"][0]["attributes"]["smoke"]["not_buckling_up"];
        realTimeRecord.DangerAction.LeavingSteering=(float)json["person_info"][0]["attributes"]["smoke"]["both_hands_leaving_wheel"];
        realTimeRecord.DangerAction.CloseEye=(float)json["person_info"][0]["attributes"]["smoke"]["eyes_closed"];
        realTimeRecord.DangerAction.UsingPhone=(float)json["person_info"][0]["attributes"]["smoke"]["cellphone"];
        realTimeRecord.DangerAction.LookAround=(float)json["person_info"][0]["attributes"]["smoke"]["not_facing_front"];
        realTimeRecord.DangerAction.Conflict=(float)json["person_info"][0]["attributes"]["smoke"]["head_lowered"];
    }
}

public class RootObject {
	public string refresh_token  { get; set; }
	public string expires_in  { get; set; }
	public string scope  { get; set; }
	public string session_key  { get; set; }
	public string access_token  { get; set; }
	public string session_secret  { get; set; }
}
