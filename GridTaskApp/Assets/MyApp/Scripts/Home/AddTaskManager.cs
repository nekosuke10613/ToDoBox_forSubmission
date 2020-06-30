using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AddTaskManager : MonoBehaviour
{
    [SerializeField]
    RectTransform m_rect;
    [SerializeField,Header("入力したテキスト")]
    Text m_inputText;
    

    
    AppWindow m_appWin;
    

    Vector2 m_defaultPos = Vector2.zero;
    Vector2 m_initPos = new Vector2(0, -2000);

    public void Init(UnityAction callBack = null)
    {
        m_rect.anchoredPosition = m_initPos;
        if (callBack != null)
            callBack.Invoke();
        m_rect.DOAnchorPos(m_defaultPos, 0.5f);
    }

    public void OnSave()
    {
        //入力したタスクを登録する
        var text = m_inputText.text;
        //TODO 
        FindObjectOfType<DataTaskManager>().CreateTask(text);
    }
    #region アタッチ自動化
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう 途中から追加した場合は自分で追加
    void Reset()
    {
        m_appWin = gameObject.GetComponent<AppWindow>();
        m_rect = gameObject.GetComponent<RectTransform>();
    }
#endif
    #endregion
}
