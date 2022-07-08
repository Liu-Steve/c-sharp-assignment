using BusHelper.Context;
using BusHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace BusHelper.Service;

public class AlertService
{

    public static List<AlertUnread> GetUnreadAlerts()
    {
        List<AlertUnread> listAlert=new List<AlertUnread>();
        try{
        using(var context=new BusContext(new DbContextOptions<BusContext>()))
        {
            
            List<Alert> list=context.Alerts.Where(l=>l.IsRead==false).ToList();
            for(int i=0;i<list.Count;i++)
            {
                String workInfoId=list[i].WorkInfoId;
                WorkInfo workInfo=context.WorkInfos.FirstOrDefault(w=>w.WorkId==workInfoId);
                String busId=workInfo.BusId;
                Bus bus=context.Buses.FirstOrDefault(w=>w.BusId==busId);
                Driver driver=context.Drivers.FirstOrDefault(d=>d.DriverId==workInfo.DriverId);
                AlertUnread audioUnread= new AlertUnread(driver.Name,
                bus.RoadId,workInfo.BusId,list[i].Content);
                listAlert.Add(audioUnread);
            }
            return listAlert;
        }
        }
        //开始的时候还没有录音信息，返回空
        catch(InvalidOperationException ex)
        {
            return listAlert;
        }
    }


    public static bool MarkAlertRead(string alertId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var alert = context.Alerts.FirstOrDefault(a => a.AlertId == alertId);
            if(alert == null)
            {
                return false;
            }
            alert.IsRead = true;
            context.SaveChanges();
        }
        return true;
    }
}


public class AlertUnread{
    public string name{get;set;}
    public string busNo{get;set;}
    public string plateNum{get;set;}
    public string info{get;set;}

    public AlertUnread(string name,string busNo,string plateNum,string info)
    {
        this.name=name;
        this.busNo=busNo;
        this.plateNum=plateNum;
        this.info=info;
    }
}