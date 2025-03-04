using System;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;

    private int monthInSeason = 3;

    public bool gameClockPause;
    private float tikTime;

    private float timeDifference;

    public TimeSpan GameTime => new TimeSpan(gameHour, gameMinute, gameSecond);


    private void Start()
    {
        gameClockPause = false;
        //EventHandler.CallGameDateEvent(gameHour, gameDay, gameMonth, gameYear, gameSeason);
        //EventHandler.CallGameMinuteEvent(gameMinute, gameHour, gameDay, gameSeason);

        //EventHandler.CallLightShiftChangeEvent(gameSeason, GetCurrentLightShift(), timeDifference);
    }

    private void Update()
    {
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;

            if(tikTime > Settings.secondThreshold)
            {
                tikTime -= Settings.secondThreshold;
                UpdateGameTime();
            }
        }

        //作弊加快时间
        if(Input.GetKey(KeyCode.T))
        {
            for (int i = 0; i < 60; i++)
            {
                UpdateGameTime();
            }
        }

        //作弊更新日期
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameDay++;
        }
    }

    private void OnUpdateGameStateEvent(GameState gameState)
    {
        gameClockPause = gameState == GameState.Pause;
    }

    private void OnAfterSceneLoadedEvent()
    {
        //加载时将时钟暂停，加载完后关闭暂停
        gameClockPause = false;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        gameClockPause = true;
    }

    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 7;
        gameDay = 1;  
        gameMonth = 1;
        gameYear = 2022;
    }

    private void UpdateGameTime()
    {
        gameSecond++;

        //根据设定的时间来设置一个时间系统
        if(gameSecond > Settings.secondHold)
        {
            gameMinute++;
            gameSecond = 0;

            if(gameMinute > Settings.minuteHold)
            {
                gameHour++;
                gameMinute = 0;

                if(gameHour > Settings.hourHold)
                {
                    gameDay++;
                    gameHour = 0;

                    if(gameDay > Settings.dayHold)
                    {
                        gameMonth++;
                        gameDay = 1;

                        if (gameMonth > 12)
                            gameMonth = 1;
                        
                        //当三个月过去了季节就会更换
                        monthInSeason--;
                        if(monthInSeason == 0)
                        {
                            monthInSeason = 3;

                            if(gameYear > 9999)
                            {
                                gameYear = 2022;
                            }
                        }
                    }
                }
            }
        }

    }
}
