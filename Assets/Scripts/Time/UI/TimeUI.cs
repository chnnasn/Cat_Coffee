using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public float startMinute, startHour;
    public RectTransform hourHand;   // 时针的UI组件
    public RectTransform minuteHand; // 分针的UI组件

    private void Update()
    {
        // 更新时针、分针和秒针的位置
        UpdateClockHands();
    }

    private void UpdateClockHands()
    {
        // 分针的旋转，分针每分钟转动360度，所以每分钟转动6度
        float minuteRotation = TimeManager.Instance.gameMinute * 6 + TimeManager.Instance.gameSecond * 0.1f + startMinute * 6; // 每秒1/10度
        minuteHand.rotation = Quaternion.Euler(0, 0, -minuteRotation);  // 负值使得顺时针旋转

        // 时针的旋转，时针每小时转动30度
        float hourRotation = (TimeManager.Instance.gameHour % 12) * 30 + TimeManager.Instance.gameMinute * 0.5f + (startHour % 12) * 30 + startMinute * 0.5f; // 每分钟额外转动0.5度
        hourHand.rotation = Quaternion.Euler(0, 0, -hourRotation);  // 负值使得顺时针旋转
    }
}
