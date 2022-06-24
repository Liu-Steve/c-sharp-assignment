using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;

namespace BusHelper.Service;

//获取道路信息
public class RoadService
{
    //查询所有的线路
    public List<Road> getAllRoads(){
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var query= context.Roads;
            return query.ToList();
        }
    }
}