using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TaskListManager : MonoBehaviour
{
    [SerializeField]
    RectTransform m_content;
    [SerializeField]
    Scrollbar m_bar;

    public void Init(UnityAction callback = null)
    {
        if (callback != null)
            callback.Invoke();
        
    }

    public void Open(UnityAction callback = null)
    {
        //Scrollバーのコンテンツサイズを調整する

        float h = DataTaskManager.Instance.TaskHeight();
        //5行以上ならスクロールバーの長さ調整
        if (h > 1000)
            m_content.sizeDelta = new Vector2(
                m_content.sizeDelta.x,
                DataTaskManager.Instance.TaskHeight());

        m_content.anchoredPosition = new Vector2(-480, 0);
    }
}
