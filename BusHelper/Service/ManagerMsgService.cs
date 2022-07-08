using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using BusHelper.Models;

namespace BusHelper.Service;

public class ManagerMsgService
{
    public static bool AddManagerMsg(ManagerMsg msg)
    {
        try
        {
            using (var context = new BusContext(new DbContextOptions<BusContext>()))
            {
                context.ManagerMsgs.Add(msg);
                context.SaveChanges();
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string? GetLatestMsg(string busId)
    {
        ManagerMsg? msg;
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var buses = context.ManagerMsgs
                .Where(m => (m.BusId == busId && m.IsRead == false));
            if (buses.Count() == 0)
                return null;
            var MaxValue = buses.Min(msg => msg.Time);
            msg = context.ManagerMsgs
                .Where(m => (
                    m.BusId == busId && 
                    m.Time == MaxValue && 
                    m.IsRead == false))
                .FirstOrDefault();
            if (msg == null)
            {
                return null;
            }
            msg.IsRead = true;
            context.SaveChanges();
        }
        return msg.Content;
    }

}