using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventHandler
{
    public static event Action<int, int, int> GameMinuteEvent;
    public static void CallGameMinuteEvent(int minute, int hour, int day)
    {
        GameMinuteEvent?.Invoke(minute, hour, day);
    }

    public static event Action<int> GameDayEvent;
    public static void CallGameDayEvent(int day)
    {
        GameDayEvent?.Invoke(day);
    }

    public static event Action<int, int, int, int> GameDateEvent;
    public static void CallGameDateEvent(int hour, int day, int month, int year)
    {
        GameDateEvent?.Invoke(hour, day, month, year);
    }
}
