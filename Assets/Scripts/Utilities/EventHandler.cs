using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventHandler
{
    public static event Action<int, int, int, Season> GameMinuteEvent;
    public static void CallGameMinuteEvent(int minute, int hour, int day, Season season)
    {
        GameMinuteEvent?.Invoke(minute, hour, day, season);
    }

    public static event Action<int, Season> GameDayEvent;
    public static void CallGameDayEvent(int day, Season season)
    {
        GameDayEvent?.Invoke(day, season);
    }

    public static event Action<int, int, int, int, Season> GameDateEvent;
    public static void CallGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        GameDateEvent?.Invoke(hour, day, month, year, season);
    }
}
