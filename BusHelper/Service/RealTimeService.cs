using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;
using System.Collections;

namespace BusHelper.Service;
//获取实时信息
public class RealTimeService
{
    //添加实时信息
    public static void addRealTime(RealTimeRecord realTimeRecord)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            context.RealTimeRecords.Add(realTimeRecord);
            context.DangerActions.Add(realTimeRecord.DangerAction);
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
}
