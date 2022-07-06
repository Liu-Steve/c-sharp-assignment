using BusHelper.Context;
using BusHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace BusHelper.Service;

public class AlertService
{
    public AlertService()
    {
    }

    public static List<Alert> GetUnreadAlerts()
    {
        List<Alert> listAlert;
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var unread = context.Alerts.Where(a => a.IsRead == false);
            listAlert = unread.ToList<Alert>();
        }
        return listAlert;
    }

    public static bool MarkAlertRead(string alertId)
    {
        using (var context = new BusContext(new DbContextOptions<BusContext>()))
        {
            var alert = context.Alerts.FirstOrDefault(a => a.AlertId == alertId);
            if(alert == null)
            {
                return false;
            }
            alert.IsRead = true;
            context.SaveChanges();
        }
        return true;
    }
}