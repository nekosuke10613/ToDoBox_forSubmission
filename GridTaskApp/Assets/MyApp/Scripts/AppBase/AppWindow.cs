using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class AppWindow : MonoBehaviour
{
    [SerializeField]
    RectTransform m_rect;

    public void Init(UnityAction callback)
    {
        callback.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    #region アタッチ自動化
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう
    void Reset()
    {
        m_rect = gameObject.GetComponent<RectTransform>();
    }
#endif
    #endregion
}
