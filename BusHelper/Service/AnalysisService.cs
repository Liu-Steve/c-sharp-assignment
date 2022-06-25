using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;
using BusHelper.Models;
using Newtonsoft.Json.Linq;

namespace BusHelper.Service;

//上传相应的API进行行为分析并获取返回的json结果
public class DriverBehaviorAnalysis
{
    // AK
    private static String clientId = "vGQlxGEKG8hjzZ95qbSIk3F1";
    // SK
    private static String clientSecret = "NTR31Ct6FH6Hh65q84bQGUk8vGxVoSLH";

    //获得token
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

        JObject json = (JObject)JsonConvert.DeserializeObject(getAccessToken());
        string token=(string)json["access_token"];
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

    //图片类型转换
    public static String getFileBase64(String fileName) {
        FileStream filestream = new FileStream(fileName, FileMode.Open);
        byte[] arr = new byte[filestream.Length];
        filestream.Read(arr, 0, (int)filestream.Length);
        string baser64 = Convert.ToBase64String(arr);
        filestream.Close();
        return baser64;
    }

    //解析json的结果
    public static void parseJson(RealTimeRecord realTimeRecord,JObject json){
        realTimeRecord.DangerAction.Smoke=(float)json["person_info"][0]["attributes"]["smoke"]["score"];
        realTimeRecord.DangerAction.Yawn=(float)json["person_info"][0]["attributes"]["yawning"]["score"];
        realTimeRecord.DangerAction.NoSafetyBelt=(float)json["person_info"][0]["attributes"]["not_buckling_up"]["score"];
        realTimeRecord.DangerAction.LeavingSteering=(float)json["person_info"][0]["attributes"]["both_hands_leaving_wheel"]["score"];
        realTimeRecord.DangerAction.CloseEye=(float)json["person_info"][0]["attributes"]["eyes_closed"]["score"];
        realTimeRecord.DangerAction.UsingPhone=(float)json["person_info"][0]["attributes"]["cellphone"]["score"];
        realTimeRecord.DangerAction.LookAround=(float)json["person_info"][0]["attributes"]["not_facing_front"]["score"];
        realTimeRecord.DangerAction.Conflict=(float)json["person_info"][0]["attributes"]["head_lowered"]["score"];
    }
}

