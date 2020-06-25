﻿using System.Collections;
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
    IPointerEnterHandler,
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
        m_rect.localScale = Vector2.one;
        m_isPush = false;
        m_pushCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //長押しのカウント
        if (m_isPush)
        {
            m_pushCount += Time.deltaTime;
            if (LongClickTime < m_pushCount)
            {
                if (m_onLongClick != null)
                    m_onLongClick.Invoke();
                print("長押し処理");
                m_pushCount = 0;
                m_isPush = false;
            }

        }

    }
    //普通にクリック
    public void OnPointerClick(PointerEventData eventdata)
    {
        if(m_onClick != null)
            m_onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        m_isPush = false;
        Debug.Log("Exit");
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
        m_rect.DOScale(downScale, m_animTime);
    }
    void OutAnim()
    {
        m_rect.DOScale(Vector2.one, m_animTime);
    }
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう
    void Reset()
    {
        m_rect = gameObject.GetComponent<RectTransform>();
    }
#endif
}
