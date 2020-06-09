# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using static TimeSpan;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== FormatTimeString ====\n");

        var time = TimeSpan.FromMinutes(10);
        Console.WriteLine(ToReadableString(time));

        var unixTime = UnixTime.FromTicks(DateTime.Now.Ticks);
        Console.WriteLine($"unix time: {unixTime}");

    }


    public static string ToDisplayTimeElapsed(long unixTime) {
        // var secondsSince = unixTime - (long)DateTime.Now.ToT

    }




    public static string ToReadableString(TimeSpan span) {
        string formatted = string.Format("{0}{1}{2}{3}",
                                         span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                                         span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                                         span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                                         span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

        if (formatted.EndsWith(", ")) {
            formatted = formatted.Substring(0, formatted.Length - 2);
        }
        if (string.IsNullOrEmpty(formatted)) {
            formatted = "0 seconds";
        }

        return formatted;
    }



public static class UnixTime {
    private const long DaysPerWeek = 7L;
    private const long TicksPerWeek = TicksPerDay * DaysPerWeek;

    private static DateTime EPOCH => GeneralConst.EPOCH_STATIC;

    public static long ToTicks(DateTime dt)        => (dt - EPOCH).Ticks;
    public static long ToMilliseconds(DateTime dt) => ToTicks(dt) / TicksPerMillisecond;
    public static long ToSeconds(DateTime dt)      => ToTicks(dt) / TicksPerSecond;
    public static long ToMinutes(DateTime dt)      => ToTicks(dt) / TicksPerMinute;
    public static long ToHours(DateTime dt)        => ToTicks(dt) / TicksPerHour;
    public static long ToDays(DateTime dt)         => ToTicks(dt) / TicksPerDay;
    public static long ToWeeks(DateTime dt)        => ToTicks(dt) / TicksPerWeek;

    public static DateTime FromTicks(long unixTicks)               => EPOCH.AddTicks(unixTicks);
    public static DateTime FromMilliseconds(long unixMilliseconds) => EPOCH.PlusMilliseconds(unixMilliseconds);
    public static DateTime FromSeconds(long unixSeconds)           => EPOCH.PlusSeconds(unixSeconds);
    public static DateTime FromMinutes(long unixMinutes)           => EPOCH.PlusMinutes(unixMinutes);
    public static DateTime FromHours(long unixHours)               => EPOCH.PlusHours(unixHours);
    public static DateTime FromDays(long unixDays)                 => EPOCH.PlusDays(unixDays);
    public static DateTime FromWeeks(long unixWeeks)               => EPOCH.PlusDays(unixWeeks * DaysPerWeek);
}
}
# endif