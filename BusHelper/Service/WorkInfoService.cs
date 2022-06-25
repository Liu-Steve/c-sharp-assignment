using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;

namespace BusHelper.Service;

//道路信息
public class WorkInfoService
{
    //司机打卡，插入上班信息表，创建累计风险指标表
    public static void addWorkInfo(WorkInfo workInfo){
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            workInfo.DangerRecord.WorkInfoId=workInfo.WorkId;
            context.WorkInfos.Add(workInfo);
            context.DangerRecords.Add(workInfo.DangerRecord);
            context.SaveChanges();
        }
    }
}