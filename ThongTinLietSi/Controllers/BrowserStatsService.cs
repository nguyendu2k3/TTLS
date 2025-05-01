using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ThongTinLietSi.Models;
public class BrowserStatsService
{
    private readonly ApplicationDbContext _context;

    public BrowserStatsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void LogBrowserVisit(HttpContext context)
    {
        var userAgent = context.Request.Headers["User-Agent"].ToString();
        var browserName = GetBrowserName(userAgent);

        var today = DateTime.Today;

        var existingRecord = _context.BrowserVisits
            .FirstOrDefault(b => b.BrowserName == browserName && b.Date == today);

        if (existingRecord != null)
        {
            existingRecord.VisitCount++;
        }
        else
        {
            _context.BrowserVisits.Add(new BrowserVisit
            {
                BrowserName = browserName,
                VisitCount = 1,
                Date = today
            });
        }

        _context.SaveChanges();
    }

    public void TrackVisit(string browserName)
    {
        var browserStat = _context.BrowserVisits.FirstOrDefault(b => b.BrowserName == browserName);
        if (browserStat != null)
        {
            browserStat.VisitCount++;
        }
        else
        {
            _context.BrowserVisits.Add(new BrowserVisit { BrowserName = browserName, VisitCount = 1 });
        }
        _context.SaveChanges();

    }

    private string GetBrowserName(string userAgent)
    {
        if (userAgent.Contains("Chrome"))
            return "Chrome";
        else if (userAgent.Contains("Firefox"))
            return "Firefox";
        else if (userAgent.Contains("Edge"))
            return "Edge";
        else if (userAgent.Contains("Safari"))
            return "Safari";
        else if (userAgent.Contains("MSIE"))
            return "Internet Explorer";
        else
            return "Other";
    }
}
