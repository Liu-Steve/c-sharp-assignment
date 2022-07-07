// 用于识别司机语音，并转为文字
using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;


namespace BusHelper.Service;

public class LeavingMessageService{
    public static void addLeavingMsg(LeavingMsg msg)
    {
        using(var context=new BusContext(new DbContextOptions<BusContext>()))
        {
            context.LeavingMsgs.Add(msg);
            context.SaveChanges();
        }
    }

    public static List<AudioUnread>  getUnreadAudio()
    {
        List<AudioUnread> audioUnreads=new List<AudioUnread>();
        try{
        using(var context=new BusContext(new DbContextOptions<BusContext>()))
        {
            
            List<LeavingMsg> list=context.LeavingMsgs.Where(l=>l.IsRead==false).ToList();
            for(int i=0;i<list.Count;i++)
            {
                String workInfoId=list[i].WorkInfoId;
                WorkInfo workInfo=context.WorkInfos.FirstOrDefault(w=>w.WorkId==workInfoId);
                String busId=workInfo.BusId;
                Bus bus=context.Buses.FirstOrDefault(w=>w.BusId==busId);
                Driver driver=context.Drivers.FirstOrDefault(d=>d.DriverId==workInfo.DriverId);
                AudioUnread audioUnread=new  AudioUnread(driver.Name,
                bus.RoadId,workInfo.BusId,list[i].Content);
                audioUnreads.Add(audioUnread);
            }
            return audioUnreads;
        }
        }
        //开始的时候还没有录音信息，返回空
        catch(InvalidOperationException ex)
        {
            return audioUnreads;
        }

    }
}

public class AudioUnread{
    public string name{get;set;}
    public string busNo{get;set;}
    public string plateNum{get;set;}
    public string audioUrl{get;set;}

    public AudioUnread(string name,string busNo,string plateNum,string audioUrl)
    {
        this.name=name;
        this.busNo=busNo;
        this.plateNum=plateNum;
        this.audioUrl=audioUrl;
    }
}
