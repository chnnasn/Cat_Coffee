using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Header("参数设置")]
    public int buttonIndex; //按钮索引
    public float targetScale; //放大大小
    public float duration; //持续时间
    public float moveDistance; //移动距离

    [Header("不用参数设置")]
    public float originXPos;
    public bool isSelected;

    private void Start()
    {
        isSelected = false;
        originXPos = transform.position.x;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.ButtonOpenPanel(buttonIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(targetScale, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, duration);
    }

    /// <summary>
    /// 按钮动画播放
    /// </summary>
    public void ButtonPlayAnim()
    {
        if (isSelected)
        {
            transform.DOMoveX(transform.position.x + moveDistance, duration).SetEase(Ease.InOutQuad);
        }
        else
        {
            transform.DOMoveX(originXPos, duration).SetEase(Ease.InOutQuad);
        }
    }
}
