using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    [Header("参数设置")]
    public int panelIndex; //按钮索引
    public float duration; //持续时间
    public float moveDistance; //移动距离

    [Header("不用参数设置")]
    public float originYPos;
    public bool isSelected;

    private void Start()
    {
        isSelected = false;
        originYPos = transform.position.y;
    }

    /// <summary>
    /// 面板动画播放
    /// </summary>
    public void PanelPlayAnim()
    {
        if (isSelected)
        {
            transform.DOMoveY(transform.position.y + moveDistance, duration).SetEase(Ease.InOutQuad);
        }
        else
        {
            transform.DOMoveY(originYPos, duration).SetEase(Ease.InOutQuad);
        }
    }
}
