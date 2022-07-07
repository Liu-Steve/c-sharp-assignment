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
        catch(Exception)
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
                    .Where(m => m.BusId == busId);
                if (buses.Count() == 0)
                    return null;
                var MaxValue = buses.Max(msg => msg.Time);
                msg = context.ManagerMsgs
                    .Where(m => (m.BusId == busId && m.Time == MaxValue))
                    .FirstOrDefault();
            }
            if (msg == null)
            {
                return null;
            }
            return msg.Content;
        }

    }