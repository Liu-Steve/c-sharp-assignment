using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;

//获取实时信息
public class RealTimeService
{
    //添加实时信息
    public void addRealTime(RealTimeRecord realTimeRecord)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            context.RealTimeRecords.Add(realTimeRecord);
            context.DangerActions.Add(realTimeRecord.DangerAction);
            context.DangerIndices.Add(realTimeRecord.DangerIndex);
            context.SaveChanges();
        }
    }
}