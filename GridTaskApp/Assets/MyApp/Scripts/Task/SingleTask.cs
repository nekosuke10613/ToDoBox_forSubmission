using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SingleTask : MonoBehaviour
{
    [SerializeField]
    RectTransform m_rect;
    [SerializeField]
    Text m_taskName;
    [SerializeField, Header("チェックマークImage")]
    Image m_checkImage;

    [SerializeField]
    Image m_buttonBG;

    float m_finishAlpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_checkImage.gameObject.SetActive(false);
    }
    public void OnFinish()
    {
        //タップで完了
        m_checkImage.gameObject.SetActive(true);
        m_buttonBG.DOFade(0.5f,0.5f);
    }

    public void SetTask(string text)
    {
        m_taskName.text = text;
    }
    public void SetPosition(Vector2 pos, Vector2 space,int taskNum)
    {
        var setPos = Vector2.zero;
        setPos.x = pos.x + space.x * (taskNum % 3);
        setPos.y = pos.y +(- space.y * (taskNum / 3));
        m_rect.anchoredPosition = setPos;
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
