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
        ifWarning(realTimeRecord);
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
    public static dynamic getRealTime(string busId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            //找到现在的时间最近的第一条此车记录的记录号
            DateTime dateTime=context.RealTimeRecords.
                Where(record=>record.BusId==busId).
                Max(record=>record.Time);
            RealTimeRecord record=context.RealTimeRecords.
            FirstOrDefault(record=>record.Time==dateTime);
            string recordId=record.RecordId;
            //根据记录号去找到相应的五个指标八个状态
            DangerIndex dangerIndex=context.DangerIndices.
            FirstOrDefault(danger=>danger.RealTimeRecordId==recordId);
            DangerAction dangerAction=context.DangerActions.
            FirstOrDefault(danger=>danger.RealTimeRecordId==recordId);

            return new{HeartRate=dangerIndex.HeartRate,HighBloodPressure=dangerIndex.HighBloodPressure,
            LowBloodPressure=dangerIndex.LowBloodPressure,Temperature=dangerIndex.Temperature,
            BloodOxygen=dangerIndex.BloodOxygen,Smoke=dangerAction.Smoke,
            Yawn=dangerAction.Yawn,NoSafetyBelt=dangerAction.NoSafetyBelt,
            LeavingSteering=dangerAction.LeavingSteering,CloseEye=dangerAction.CloseEye,
            UsingPhone=dangerAction.UsingPhone,LookAround=dangerAction.LookAround,
            Conflict=dangerAction.Conflict,realPic=record.RealPic};
        }
    }

    //查询所有车辆的实时位置
    public static ArrayList getRealLocation()
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            List<Bus> buses=context.Buses.ToList();
            ArrayList list=new ArrayList();
            foreach(Bus bus in buses)
            {
                //遍历bus中的每一个值，找到每一辆车最近的时间
                DateTime dateTime=context.RealTimeRecords.
                Where(record=>record.BusId==bus.BusId).
                Max(record=>record.Time);
                //找到这个最近时间对应的记录
                RealTimeRecord realTimeRecord=context.RealTimeRecords.
                FirstOrDefault(record=>record.Time==dateTime);
                list.Add(new {busId=bus.BusId,x=realTimeRecord.X,y=realTimeRecord.Y});
            }
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
            &&workInfo.EndTime==null
            &&workInfo.BusId==busId)).DriverId;
            Driver driver=context.Drivers.FirstOrDefault(driver=>driver.DriverId==driverId);
            //只需要司机的姓名，路线和电话号码，因此构造匿名对象返回
            return new {roadId=roadId,name=driver.Name,phone=driver.PhoneNumber};
        }
    }

    //八个行为指标的简单异常阈值判断
    public static void ifWarning(RealTimeRecord realTimeRecord)
    {
        
        int[] warning=new int[8];//累计8个异常指标是否异常的标识
        List<double> threshold=new List<double>{0.25,0.66,0.58,0.3,0.1,0.76,0.53,0.58};//累计8个异常情况阈值
        List<double> index=new List<double>{realTimeRecord.DangerAction.Smoke,realTimeRecord.DangerAction.Yawn
        ,realTimeRecord.DangerAction.NoSafetyBelt,realTimeRecord.DangerAction.LeavingSteering,
        realTimeRecord.DangerAction.CloseEye,realTimeRecord.DangerAction.UsingPhone,
        realTimeRecord.DangerAction.LookAround,realTimeRecord.DangerAction.Conflict};
        bool ifOneWarning=false;//只要出现某一个异常，此项为true，否则为false
        bool ifWarning=false;//临时变量，指示某个指标是否异常
        for(int i=0;i<warning.Length;i++)
        {
            ifWarning=comparer(index[i],threshold[i]);
            if(ifWarning)
            {
                ifOneWarning=true;
                warning[i]=1;
            }
            else
            {
                warning[i]=0;
            }
        }
        if(ifOneWarning)//只要出现一个指标异常，就需要更新异常记录的表格
        {
            using (var context = new BusContext(new DbContextOptions<BusContext>()))
            {
                //根据车牌号加时间查询排班表，找到此时的值班ID
                string workId=context.WorkInfos.FirstOrDefault
                (workInfo=>(DateTime.Compare(workInfo.StartTime,DateTime.Now)<0
                &&workInfo.EndTime==null
                &&workInfo.BusId==realTimeRecord.BusId)).WorkId;
                //查找本次值班的异常记录表，进行修改
                DangerRecord dangerRecord=context.DangerRecords.
                FirstOrDefault(dangerRecord=>dangerRecord.WorkInfoId==workId);
                addWarning(warning,dangerRecord);
                context.SaveChanges();
            }
        }
    } 

    //比较某个指标是否异常,第一个参数为当前指标，第二个为阈值，当前指标大于阈值则认为出现问题
    public static bool comparer(double v1,double v2)
    {
        if(v1>v2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //依次比较八个异常指标值，异常则累计次数加一
    public static void addWarning(int[] warning,DangerRecord dangerRecord)
    {
        if(warning[0]==1)
        {
            dangerRecord.Smoke=dangerRecord.Smoke+1;
        }
        if(warning[1]==1)
        {
            dangerRecord.Yawn=dangerRecord.Yawn+1;
        }
        if(warning[2]==1)
        {
            dangerRecord.SafetyBelt=dangerRecord.SafetyBelt+1;
        }
        if(warning[3]==1)
        {
            dangerRecord.LeavingSteering=dangerRecord.LeavingSteering+1;
        }
        if(warning[4]==1)
        {
            dangerRecord.CloseEye=dangerRecord.CloseEye+1;
        }
        if(warning[5]==1)
        {
            dangerRecord.UsingPhone=dangerRecord.UsingPhone+1;
        }
        if(warning[6]==1)
        {
            dangerRecord.LookAround=dangerRecord.LookAround+1;
        }
        if(warning[7]==1)
        {
            dangerRecord.Conflict=dangerRecord.Conflict+1;
        }
    }

    // 判断5个指标(heartRate, highBloodRate, lowBloodRate, temperature, bloodOxygen)是否异常
    // 异常为1
    public int[] indexIsAbnormal(RealTimeRecord record){
        int heartRate = record.DangerIndex.HeartRate;
        int highBloodPressure = record.DangerIndex.HighBloodPressure;
        int lowBloodPressure = record.DangerIndex.LowBloodPressure;
        float temp = record.DangerIndex.Temperature;
        int bloodOxygen = record.DangerIndex.BloodOxygen;
        int [] result={0, 0, 0, 0, 0};

        // 条件判断
        if(heartRate < 60 || heartRate > 100){
            result[0] = 1;
        }
        if(highBloodPressure < 90 || highBloodPressure > 140){
            result[1] = 1;
        }
        if(lowBloodPressure < 60 || lowBloodPressure > 90){
            result[2] = 1;
        }
        if(temp < 36 || temp > 37){
            result[3] = 1;
        }
        // 正常动脉氧约为75 至100 毫米汞柱(mm Hg)
        if(bloodOxygen < 75 || bloodOxygen > 100){
            result[4] = 1;
        }
        return result;
    }
}
