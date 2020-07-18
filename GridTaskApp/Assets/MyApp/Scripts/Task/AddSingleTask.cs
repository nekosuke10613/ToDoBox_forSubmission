using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddSingleTask : MonoBehaviour
{
    [SerializeField]
    RectTransform m_rect;
    [SerializeField]
    SingleTask m_singleTask;

    [SerializeField]
    Image m_stateImage;
   [Header("完了、選択中、埋まってる画像")]
    [SerializeField]
    Sprite m_checkSpr, m_currentSpr, m_buriedSpr;

    public int ID { get; private set; }
    Task m_task = null;
    

    //こちらは方眼生成時
    public void Init(UnityAction callBack = null)
    {
        if(callBack != null)
            callBack.Invoke();
        //テキストは一回空白にする
        m_singleTask.SetEmpty();
        //

    }
    //Add画面開いた時に情報をセットする
    public void SetInfo(Task task,int id)
    {
        m_task = task;
        m_singleTask.SetTask(task);
        ID = id;
    }
    /// <summary>
    /// 情報をセットする場所をタップで指定
    /// </summary>
    public void OnSetPlace()
    {
        //完了してるなら

        //情報が入ってるか(最低限名前)

        //一番早い番号をみつけて選択中画像を入れる
    }
    public void SetPosition(Vector2 pos, Vector2 space, int taskNum)
    {
        var setPos = Vector2.zero;
        setPos.x = pos.x + space.x * (taskNum % 3);
        setPos.y = pos.y + (-space.y * (taskNum / 3));
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
