using UnityEngine;
using UnityEngine.UI;
public class BuildButton : MonoBehaviour // 重命名为更有意义的名称
{

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var btn = transform.GetChild(i).GetComponent<Button>();
            if (!btn) continue;

            int index = i; 
            btn.onClick.AddListener(() => OnClick(index));
        }
    }

    private void OnClick(int buttonIndex)
    {
        IndexEvent.GetIndex(buttonIndex);
    }
}