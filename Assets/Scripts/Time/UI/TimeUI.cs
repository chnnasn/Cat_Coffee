using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public RectTransform dayNightImage;
    public RectTransform clockParent;
    public Image seasonImage;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;
    public Sprite[] seasonSprites;

    public List<GameObject> clockBlocks = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < clockParent.childCount; i++)
        {
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
        EventHandler.GameDateEvent += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }

    /// <summary>
    /// 设置时间正确显示
    /// </summary>
    /// <param name="minute"></param>
    /// <param name="hour"></param>
    private void OnGameMinuteEvent(int minute, int hour, int day, Season season)
    {
        timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    }   

    /// <summary>
    /// 设置UI跟日期跟季节正确显示
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <param name="season"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        dateText.text = year + "年" + month.ToString("00") + "月" + day.ToString("00") + "日";
        seasonImage.sprite = seasonSprites[(int)season];

        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }

    /// <summary>
    /// 根据小时切换时间轮盘UI的正确显示
    /// </summary>
    /// <param name="hour"></param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;
        if(index == 0)
        {
            foreach(var item in clockBlocks)
            {
                item.SetActive(false);
            }
        }
        else
        {
            for(int i = 0; i < clockBlocks.Count; i++)
            {
                if(i < index + 1)
                    clockBlocks[i].SetActive(true);
                else
                    clockBlocks[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 旋转时间的图标
    /// </summary>
    /// <param name="hour"></param>
    private void DayNightImageRotate(int hour)
    {
        //24小时，每个小时15度
        var target = new Vector3(0, 0, hour * 15 - 90);
        dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }
}
