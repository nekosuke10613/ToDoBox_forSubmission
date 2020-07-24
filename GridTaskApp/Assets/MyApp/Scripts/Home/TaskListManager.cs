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
    [SerializeField, Header("ページ切り替えボタン")]
    GameObject m_pageButton;

    bool m_isDaily = true;

    public void Init( UnityAction callback = null)
    {
        if (callback != null)
            callback.Invoke();

        

        
    }

    public void Open(bool isDaily,UnityAction callback = null)
    {
        //false：デイリー　true：それ以外
        m_isDaily = isDaily;

        //デイリーならページ切り替えボタンを表示しない
        m_pageButton.SetActive(m_isDaily);
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
