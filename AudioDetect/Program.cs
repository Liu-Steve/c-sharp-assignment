using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AudioDetect
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var AppId = "26537660";
            var AppKey = "KBbd7K4QDN1NHjuepA1mqFuu";
            var SecretKey = "WX0j9pCOwH8StOOPImh2FFo0RgjT3ibv";
            var client = new Baidu.Aip.Speech.Asr(AppId, AppKey, SecretKey);
            var data = File.ReadAllBytes("E:/CsharpFinal/AudioDetect/audio/16k.pcm");

            // 可选参数
            var options = new Dictionary<string, object>
             {
                {"dev_pid", 1537}
             };
            client.Timeout = 120000000; // 若语音较长，建议设置更大的超时时间. ms
            var result = client.Recognize(data, "pcm", 16000, options);
            Console.Write(result);
            Console.ReadLine();

        }
       
    }
}
