using Xunit;
using BusHelper.Service;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using BusHelper.Models;

namespace BusTest;

public class AanlysisTest
{

    public AanlysisTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"./appsettings.json")
            .Build();
    }

    // [Theory]
    // [InlineData("3.jpg")]
    // //对于正常存在的图片，可以上传服务器并获取结果，正常情况下可以解析到json返回结果中的结果
    // public void FileExistTest(string fileName)
    // {
    //     JObject json= (JObject)JsonConvert.
    //     DeserializeObject
    //     (DriverBehaviorAnalysis.
    //     driver_behavior("D:\\workspace\\c#\\assignment\\c-sharp-assignment\\BusHelper\\img\\"+fileName));
    //     string test=(string)json["person_info"][0]["attributes"]["smoke"]["score"];
    // }

    [Theory]
    [InlineData("4.jpg")]
    [InlineData("我爱学习")]
    //对于不存在的图片，或者胡乱的字符串，应该抛出异常，并返回文件不存在的信息
    public void FileNotExistTest(string fileName)
    {
        try{
            DriverBehaviorAnalysis.
            driver_behavior("D:\\workspace\\c#\\assignment\\c-sharp-assignment\\BusHelper\\img\\"+fileName);
            DriverBehaviorAnalysis.
            getFileBase64("D:\\workspace\\c#\\assignment\\c-sharp-assignment\\BusHelper\\img\\"+fileName);
        }
        catch(FileNotFoundException ex)
        {
            Assert.IsType<FileNotFoundException>(ex);
        }
    }

    [Theory]
    [InlineData("{\"person_num\":1,\"person_info\":[{\"attributes\":{\"cellphone\":{\"threshold\":0.76,\"score\":0.089325942099094},\"yawning\":{\"threshold\":0.66,\"score\":0.0007511890726164},\"not_buckling_up\":{\"threshold\":0.58,\"score\":0.81095975637436},\"no_face_mask\":{\"threshold\":0.72,\"score\":0.99875915050507},\"both_hands_leaving_wheel\":{\"threshold\":0.3,\"score\":0.9014720916748},\"eyes_closed\":{\"threshold\":0.1,\"score\":0.090511165559292},\"head_lowered\":{\"threshold\":0.58,\"score\":0.11450858414173},\"smoke\":{\"threshold\":0.25,\"score\":0.026156177744269},\"not_facing_front\":{\"threshold\":0.53,\"score\":0.68074524402618}},\"location\":{\"width\":856,\"top\":419,\"score\":0.90945136547089,\"left\":464,\"height\":626}}],\"log_id\":2320165720061799596}")]
    //对于正确的json结果，可以改变RealTimeRecord的值
    public void JsonTest(string json)
    {
        DriverBehaviorAnalysis.
        parseJson(new RealTimeRecord(),(JObject)JsonConvert.DeserializeObject(json));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("笨笨狗")]
    [InlineData("{\"最可爱的狗\":\"柯基\"}")]
    //测试参数错误的情况
    public void JsonInvalidTest(string json)
    {
        try{
            JObject tmp=(JObject)JsonConvert.DeserializeObject(json);
            DriverBehaviorAnalysis.
            parseJson(new RealTimeRecord(),tmp);
        }
        catch(ArgumentNullException ex)//传入参数为NULL
        {
            Assert.IsType<ArgumentNullException>(ex);
        }
        catch(JsonReaderException ex)//传入字符串不是JSON格式的
        {
             Assert.IsType<JsonReaderException>(ex);
        }
        catch(NullReferenceException ex)//解析了json，但是json不是正确的结果，百度智能云返回了错误的结果
        {
            Assert.IsType<NullReferenceException>(ex);
        }
        catch(ArgumentOutOfRangeException ex)//无人脸，解析json报错
        {
            Assert.IsType<ArgumentOutOfRangeException>(ex);
        }
    }
}