using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AlertWin : MonoBehaviour
{
    [SerializeField]
    Text m_title;
    [SerializeField]
    Text m_desctiprion;

    [SerializeField]
    CanvasGroup m_group;

    UnityAction m_callBack;

    
    //HomeManagerなどから呼び出す
    public void Init(string title,string description,UnityAction callback = null)
    {
        m_group.alpha = 0;
        m_callBack = callback;
        
        //アニメーション開始
        gameObject.SetActive(true);
        m_group.DOFade(1, 0.3f);

    }
    public void OnClose()
    {
        //閉じるアニメーション
        m_group.DOFade(0, 0.3f).OnComplete(() =>
        {
            m_callBack.Invoke();
            gameObject.SetActive(false);
        });
    }
}
