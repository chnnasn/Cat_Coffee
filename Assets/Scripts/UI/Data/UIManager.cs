using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("按钮获取")]
    public ButtonAnim[] buttons;

    [Header("面板获取")]
    public GameObject panelsFather;
    public PanelAnim[] panels; //面板，索引按照按钮的顺序

    /// <summary>
    /// 切换面板
    /// </summary>
    /// <param name="index"></param>
    public void SwitchPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
            {
                //通过SetAsLastSibling()来将指定的panels移到transform的最下面
                panels[i].transform.SetAsLastSibling();
            }
        }
    }

    /// <summary>
    /// 按钮触发打开面板
    /// </summary>
    public void ButtonOpenPanel(int index)
    {
        #region 按钮
        foreach(ButtonAnim button in buttons)
        {
            if (button.buttonIndex != index)
            {
                button.isSelected = false;
                button.ButtonPlayAnim();
            }
        }

        foreach(ButtonAnim button in buttons)
        {
            if (button.buttonIndex != index)
                continue;
            button.isSelected = !button.isSelected;
            button.ButtonPlayAnim();
        }
        #endregion

        #region 面板
        foreach (PanelAnim panel in panels)
        {
            if (panel.panelIndex != index)
            {
                panel.isSelected = false;
                panel.PanelPlayAnim();
            }
        }

        foreach(PanelAnim panel in panels)
        {
            if (panel.panelIndex != index)
                continue;
            panel.isSelected = !panel.isSelected;
            panel.PanelPlayAnim();
        }
        #endregion
    }
}
