using System;
using UnityEngine;

public static class TimeUtil
{
    public static int DAY_INTERVAL = 24 * 3600;

    public static long Now
    {
        get { return GetNowTime(); }
    }

    public static long ToTimeStamp(this DateTime dt)
    {
        return (long)(dt - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    public static DateTime ToDateTime(this long stamp)
    {
        return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(stamp);
    }

    private static long GetNowTime()
    {
        // temp
        return DateTime.Now.ToTimeStamp();
    }

    public static bool IsNewDay(DateTime now, DateTime last)
    {
        return CheckDaily(now, last) != 0;
    }

    public static bool IsNewDay(long now, long last)
    {
        return CheckDaily(now, last) != 0;
    }


    public static int CheckDaily(long now, long last)
    {
        return CheckDaily(now.ToDateTime(), last.ToDateTime());
    }

    /// <summary>
    /// 0 - Logged in today; -1 Login Breaked; 1 Continuous login 
    /// </summary>
    /// <param name="now"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    public static int CheckDaily(DateTime now, DateTime last)
    {
        TimeSpan diff = now - last;
        Debug.Log("[CheckDaily] diff.TotalSeconds = " + diff.TotalSeconds + " ; now.DayOfYear = " + now.DayOfYear + " ; last.DayOfYear = " + last.DayOfYear);
        if (diff.TotalSeconds >= 2 * DAY_INTERVAL)
            return -1;
        else if (now.DayOfYear == last.DayOfYear)
            return 0;
        else
        {
            if (now.DayOfYear - last.DayOfYear > 1)
                return -1;
            else
                return 1;
        }
    }


}
