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
    [SerializeField,Header("アニメーションタイプどちらかにチェック")]
    bool m_isSlideAnim = false;
    [SerializeField]
    bool m_isFadeAnim = false;
    [SerializeField]
    CanvasGroup m_group;

    readonly Vector2 m_initPos = new Vector2(0, -2000);
    readonly float m_animSpeed = 0.3f;

    UnityAction m_closeCallback = null;

    public void Init(UnityAction callback,UnityAction closeCallBack = null)
    {
        m_closeCallback = closeCallBack;
        //アニメーション初期位置調整
        if(m_isSlideAnim)
            m_rect.anchoredPosition = m_initPos;
        callback.Invoke();
        gameObject.SetActive(true);
        //ここで共通アニメーション行いたい
        if(m_isSlideAnim)
            m_rect.DOAnchorPosY(0, m_animSpeed);
        if (m_isFadeAnim)
        {
            m_group.alpha = 0;
            m_group.DOFade(1, 0.3f);
        }

    }
    public void OnClose()
    {
        if(m_isSlideAnim)
            //アニメーション後非アクティブにする
            m_rect.DOAnchorPosY(m_initPos.y, m_animSpeed)
                .OnComplete(() => {
                    //アニメーション後にオブジェクト消すとかデータ保存とかの処理を行う
                    if (m_closeCallback != null)
                        m_closeCallback.Invoke();
                    gameObject.SetActive(false);
                    });

        if (m_isFadeAnim)
        {
            m_group.DOFade(0, 0.3f)
                .OnComplete(()=> {
                    //アニメーション後にオブジェクト消すとかデータ保存とかの処理を行う
                    if (m_closeCallback != null)
                        m_closeCallback.Invoke();
                    gameObject.SetActive(false);
                });
        }

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
