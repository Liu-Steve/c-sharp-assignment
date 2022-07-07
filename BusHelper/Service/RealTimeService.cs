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
        IfWarning(realTimeRecord);
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            context.RealTimeRecords.Add(realTimeRecord);
            realTimeRecord.DangerAction.RealTimeRecordId = realTimeRecord.RecordId;
            context.DangerActions.Add(realTimeRecord.DangerAction);
            realTimeRecord.DangerIndex.RealTimeRecordId = realTimeRecord.RecordId;
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
            DateTime dateTime = context.RealTimeRecords.
                Where(record => record.BusId == busId).
                Max(record => record.Time);
            RealTimeRecord record = context.RealTimeRecords.
            FirstOrDefault(record => record.Time == dateTime);
            string recordId = record.RecordId;
            //根据记录号去找到相应的五个指标八个状态
            DangerIndex dangerIndex = context.DangerIndices.
            FirstOrDefault(danger => danger.RealTimeRecordId == recordId);
            DangerAction dangerAction = context.DangerActions.
            FirstOrDefault(danger => danger.RealTimeRecordId == recordId);

            return new
            {
                HeartRate = dangerIndex.HeartRate,
                HighBloodPressure = dangerIndex.HighBloodPressure,
                LowBloodPressure = dangerIndex.LowBloodPressure,
                Temperature = dangerIndex.Temperature,
                Alcohol = dangerIndex.Alcohol,
                BloodOxygen = dangerIndex.BloodOxygen,
                Smoke = dangerAction.Smoke,
                Yawn = dangerAction.Yawn,
                NoSafetyBelt = dangerAction.NoSafetyBelt,
                LeavingSteering = dangerAction.LeavingSteering,
                CloseEye = dangerAction.CloseEye,
                UsingPhone = dangerAction.UsingPhone,
                LookAround = dangerAction.LookAround,
                Conflict = dangerAction.Conflict,
                realPic = record.RealPic
            };
        }
    }

    //查询所有车辆的实时位置
    public static ArrayList getRealLocation()
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            List<Bus> buses = context.Buses.ToList();
            ArrayList list = new ArrayList();
            foreach (Bus bus in buses)
            {
                //遍历bus中的每一个值，找到每一辆车最近的时间
                DateTime dateTime = context.RealTimeRecords.
                Where(record => record.BusId == bus.BusId).
                Max(record => record.Time);
                //找到这个最近时间对应的记录
                RealTimeRecord realTimeRecord = context.RealTimeRecords.
                FirstOrDefault(record => record.Time == dateTime);
                list.Add(new { busId = bus.BusId, x = realTimeRecord.X, y = realTimeRecord.Y });
            }
            return list;
        }
    }

    //查询实时司机的信息
    public static dynamic getBusInfo(string busId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            //根据车牌号找到车辆的道路信息
            string roadId = context.Buses.FirstOrDefault(record => record.BusId == busId).RoadId;
            //根据车牌号加时间查询排班表，找到此时的值班信息司机ID
            WorkInfo workInfo = context.WorkInfos.FirstOrDefault
            (workInfo => (DateTime.Compare(workInfo.StartTime, DateTime.Now) < 0
            && workInfo.EndTime == null
            && workInfo.BusId == busId));
            //根据值班信息获得司机ID
            string driverId = workInfo.DriverId;
            //根据司机ID获得司机的信息
            Driver driver = context.Drivers.FirstOrDefault(driver => driver.DriverId == driverId);
            //只需要司机的姓名，路线和电话号码，因此构造匿名对象返回
            return new { roadId = roadId, name = driver.Name, phone = driver.PhoneNumber };
        }
    }

    //查询累计信息
    public static dynamic getRecord(string busId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            //根据车牌号找到车辆的道路信息
            string roadId = context.Buses.FirstOrDefault(record => record.BusId == busId).RoadId;
            //根据车牌号加时间查询排班表，找到此时的值班信息司机ID
            WorkInfo workInfo = context.WorkInfos.FirstOrDefault
            (workInfo => (DateTime.Compare(workInfo.StartTime, DateTime.Now) < 0
            && workInfo.EndTime == null
            && workInfo.BusId == busId));
            //根据值班信息获得司机的工作信息ID
            string workId = workInfo.WorkId;
            //根据工作信息ID查询累计行为记录
            DangerRecord dangerRecord = context.DangerRecords.
            FirstOrDefault(record => record.WorkInfoId == workId);
            return new
            {
                Smoke = dangerRecord.Smoke,
                Yawn = dangerRecord.Yawn,
                NoSafetyBelt = dangerRecord.SafetyBelt,
                LeavingSteering = dangerRecord.LeavingSteering,
                CloseEye = dangerRecord.CloseEye,
                UsingPhone = dangerRecord.UsingPhone,
                LookAround = dangerRecord.LookAround,
                Conflict = dangerRecord.Conflict
            };
        }
    }

    //八个行为指标的简单异常阈值判断
    public static void IfWarning(RealTimeRecord realTimeRecord)
    {
        int[] warning = new int[8];//累计8个异常指标是否异常的标识
        List<double> threshold = new List<double>
        { 0.25, 0.66, 0.58, 0.3, 0.1, 0.76, 0.53, 0.58 };//累计8个异常情况阈值
        List<double> index = new List<double>
        {
            realTimeRecord.DangerAction.Smoke,
            realTimeRecord.DangerAction.Yawn,
            realTimeRecord.DangerAction.NoSafetyBelt,
            realTimeRecord.DangerAction.LeavingSteering,
            realTimeRecord.DangerAction.CloseEye,
            realTimeRecord.DangerAction.UsingPhone,
            realTimeRecord.DangerAction.LookAround,
            realTimeRecord.DangerAction.Conflict
        };
        bool ifOneWarning = false;//只要出现某一个异常，此项为true，否则为false
        for (int i = 0; i < warning.Length; i++)
        {
            bool ifWarning = comparer(index[i], threshold[i]);
            warning[i] = ifWarning ? 1 : 0;
            if (ifWarning)
            {
                ifOneWarning = true;
            }
        }
        if (ifOneWarning)//只要出现一个指标异常，就需要更新异常记录的表格
        {
            using (var context = new BusContext(new DbContextOptions<BusContext>()))
            {
                //根据车牌号加时间查询排班表，找到此时的值班ID
                string? workId = context.WorkInfos.FirstOrDefault(
                    workInfo => (
                        DateTime.Compare(workInfo.StartTime, DateTime.Now) < 0 &&
                        workInfo.EndTime == null &&
                        workInfo.BusId == realTimeRecord.BusId
                    )
                )?.WorkId;
                //查找本次值班的异常记录表，进行修改
                DangerRecord? dangerRecord = context.DangerRecords.FirstOrDefault(
                    dangerRecord => dangerRecord.WorkInfoId == workId
                );
                if (dangerRecord != null)
                    addWarning(warning, dangerRecord);
                context.SaveChanges();
            }
        }
    }

    //比较某个指标是否异常,第一个参数为当前指标，第二个为阈值，当前指标大于阈值则认为出现问题
    public static bool comparer(double v1, double v2)
    {
        return v1 > v2;
    }

    //依次比较八个异常指标值，异常则累计次数加一
    public static void addWarning(int[] warning, DangerRecord dangerRecord)
    {
        dangerRecord.Smoke += warning[0];
        dangerRecord.Yawn += warning[1];
        dangerRecord.SafetyBelt += warning[2];
        dangerRecord.LeavingSteering += warning[3];
        dangerRecord.CloseEye += warning[4];
        dangerRecord.UsingPhone += warning[5];
        dangerRecord.LookAround += warning[6];
        dangerRecord.Conflict += warning[7];
    }

    //添加异常表信息，现阶段全部都当作低级警告
    public static void addAlert(DangerRecord dangerRecord)
    {
        //如果不需要建立异常表
        if(!ifHasAlert(dangerRecord))
        {
            return;
        }
        using(var context=new BusContext(new DbContextOptions<BusContext>()))
        {
            //没有警告表时建立警告表
            if(context.Alerts.FirstOrDefault(a=>a.WorkInfoId==dangerRecord.WorkInfoId)==null)
            {
                if(dangerRecord.Smoke>=10)
                {
                    
                }
            }
        }
    }

    //判断异常表是否需要建立，如果有指标大于10，就可能需要建立
    public static bool ifHasAlert(DangerRecord dangerRecord)
    {
        if(dangerRecord.Smoke>=10)
        {
            return true;
        }
        else if(dangerRecord.Yawn>=10)
        {
            return true;
        }
        else if(dangerRecord.SafetyBelt>=10)
        {
            return true;
        }
        else if(dangerRecord.LeavingSteering>=10)
        {
            return true;
        }
        else if(dangerRecord.CloseEye>=10)
        {
            return true;
        }
        else if(dangerRecord.UsingPhone>=10)
        {
            return true;
        }
        else if(dangerRecord.LookAround>=10)
        {
            return true;
        }
        else if(dangerRecord.Conflict>=10)
        {
            return true;
        }
        return false;
    }

    // 判断5个指标(heartRate, highBloodRate, lowBloodRate, temperature, bloodOxygen)是否异常
    // 异常为1
    public static int[] IndexIsAbnormal(RealTimeRecord record)
    {
        int heartRate = record.DangerIndex.HeartRate;
        int highBloodPressure = record.DangerIndex.HighBloodPressure;
        int lowBloodPressure = record.DangerIndex.LowBloodPressure;
        float temp = record.DangerIndex.Temperature;
        int bloodOxygen = record.DangerIndex.BloodOxygen;
        int[] result = { 0, 0, 0, 0, 0 };

        // 条件判断
        if (heartRate < 60 || heartRate > 100)
        {
            result[0] = 1;
        }
        if (highBloodPressure < 90 || highBloodPressure > 140)
        {
            result[1] = 1;
        }
        if (lowBloodPressure < 60 || lowBloodPressure > 90)
        {
            result[2] = 1;
        }
        if (temp < 36 || temp > 37)
        {
            result[3] = 1;
        }
        if (bloodOxygen < 96)
        {
            result[4] = 1;
        }
        return result;
    }
}
