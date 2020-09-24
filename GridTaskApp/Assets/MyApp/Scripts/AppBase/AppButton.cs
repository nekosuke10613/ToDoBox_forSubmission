using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

//*-- 　ボタンの共通機能  --*
[RequireComponent(typeof(RectTransform))]
public class AppButton : MonoBehaviour,
    IPointerClickHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    //ボタン長押し判定時間
    public readonly float LongClickTime = 2;

    readonly Vector2 downScale = new Vector2(0.8f, 0.8f);
    readonly float m_animTime = 0.2f;

    [SerializeField]
    RectTransform m_rect;

    //--通常SerializeFieldはここより上に書く-----------------------
    [SerializeField, Header("アニメーションするか")]
    bool m_isAnime = true;
    
    //イベントエディタ拡張
    [SerializeField,Header("クリック時処理")]
    UnityEvent m_onClick;
    [SerializeField, Header("一定時間長押した時の処理")]
    UnityEvent m_onLongClick;


    //-------------------------------------------
    
    bool m_isPush = false;
    float m_pushCount = 0;

    //後で消す
    void Start()
    {
        Init();
    }
    public void Init()
    {
        //m_rect.localScale = Vector2.one;
        m_isPush = false;
        m_pushCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //長押しのカウント
        if (!m_isPush) return;
        
        m_pushCount += Time.deltaTime;

        if (LongClickTime >= m_pushCount) return;

        if (m_onLongClick != null)
            m_onLongClick.Invoke();
        m_pushCount = 0;
        m_isPush = false;

    }
    //普通にクリック
    public void OnPointerClick(PointerEventData eventdata)
    {
        if(m_onClick != null)
            m_onClick.Invoke();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        m_isPush = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        m_isPush = true;
        m_pushCount = 0;
        InAnim();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        m_isPush = false;
        OutAnim();
    }
    
    //アニメーション関連
    void InAnim()
    {
        if(m_isAnime)
            m_rect.DOScale(downScale, m_animTime);
    }
    void OutAnim()
    {
        if (m_isAnime)
            m_rect.DOScale(Vector2.one, m_animTime);
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
