using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;

namespace BusHelper.Service;

//道路信息
public class WorkInfoService
{
    //司机打卡
    public List<Road> getAllRoads(){
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var query= context.Roads;
            return query.ToList();
        }
    }
}