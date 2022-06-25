using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;
using System.Collections;

namespace BusHelper.Service;

//实时信息
public class RealTimeService
{
    //添加实时信息
    public static void addRealTime(RealTimeRecord realTimeRecord)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            context.RealTimeRecords.Add(realTimeRecord);
            realTimeRecord.DangerAction.RealTimeRecordId=realTimeRecord.RecordId;
            context.DangerActions.Add(realTimeRecord.DangerAction);
            realTimeRecord.DangerIndex.RealTimeRecordId=realTimeRecord.RecordId;
            context.DangerIndices.Add(realTimeRecord.DangerIndex);
            context.SaveChanges();
        }
    }

    //查询实时信息
    public static ArrayList getRealTime(string busId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            //找到现在的第一条此车记录的记录号
            RealTimeRecord record=context.RealTimeRecords.FirstOrDefault(record=>record.BusId==busId);
            string recordId=record.RecordId;
            //根据记录号去找到相应的五个指标八个状态
            DangerIndex dangerIndex=context.DangerIndices.FirstOrDefault(danger=>danger.RealTimeRecordId==recordId);
            DangerAction dangerAction=context.DangerActions.FirstOrDefault(danger=>danger.RealTimeRecordId==recordId);
            ArrayList list=new ArrayList();
            list.Add(dangerIndex);
            list.Add(dangerAction);
            return list;
        }
    }

    //查询实时司机
    public static dynamic getBusInfo(string busId)
    {
        ArrayList list=new ArrayList();
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            //根据车牌号找到车辆的道路信息
            string roadId=context.Buses.FirstOrDefault(record=>record.BusId==busId).RoadId;
            //根据车牌号加时间查询排班表，找到此时的值班司机ID
            string driverId=context.WorkInfos.FirstOrDefault
            (workInfo=>(DateTime.Compare(workInfo.StartTime,DateTime.Now)<0
            &&DateTime.Compare(workInfo.EndTime,DateTime.Now)>0)).DriverId;
            Driver driver=context.Drivers.FirstOrDefault(driver=>driver.DriverId==driverId);
            //只需要司机的姓名，路线和电话号码，因此构造匿名对象返回
            return new {roadId=roadId,name=driver.Name,phone=driver.PhoneNumber};
        }
    }
}
