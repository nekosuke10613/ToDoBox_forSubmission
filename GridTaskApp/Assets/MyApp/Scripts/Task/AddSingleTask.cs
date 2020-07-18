using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddSingleTask : MonoBehaviour
{
    #region ** SerializeField **
    [SerializeField]
    RectTransform m_rect;
    [SerializeField]
    SingleTask m_singleTask;

    [SerializeField]
    Image m_stateImage;
   [Header("完了、選択中、埋まってる画像")]
    [SerializeField]
    Sprite m_checkSpr, m_currentSpr, m_buriedSpr;

    #endregion
    public int ID { get; private set; }

    Task m_task = null;
    AddTaskManager m_manager = null;

    //ボックス内が空か(クリックできるか)
    bool m_isSet = false;

    //こちらは方眼生成時
    public void Init(AddTaskManager manager,UnityAction callBack = null)
    {
        m_manager = manager;
        if(callBack != null)
            callBack.Invoke();
        //テキストは一回空白にする
        m_singleTask.SetEmpty();

        m_stateImage.gameObject.SetActive(false);
;

    }
    //Add画面開いた時に情報をセットする
    public void SetInfo(Task task,int id)
    {
        m_task = task;
        m_singleTask.SetTask(task);
        ID = id;
        //タスクの完了状況によってImageセット
        SetImageState();
        
        //一番早い番号をみつけて選択中画像を入れる
    }
    /// <summary>
    /// 情報をセットする場所をタップで指定
    /// </summary>
    public void OnSetPlace()
    {
        if (!m_isSet) return;
        //まずAddTaskManagerにIDの変更を通知
        m_manager.SetCurrentBox(ID,m_stateImage.gameObject);
        m_stateImage.sprite = m_currentSpr;
        m_stateImage.gameObject.SetActive(true);
        Debug.Log("選択");
    }
    void SetImageState()
    {
        //完了してるなら
        if (m_task.IsFinish)
        {
            m_stateImage.sprite = m_checkSpr;
            m_stateImage.gameObject.SetActive(true);
            return;
        }
        
        //情報が入ってるか(最低限名前)
        if (m_task.Name != "")
        {
            m_stateImage.sprite = m_buriedSpr;
            m_stateImage.gameObject.SetActive(true);
            return;
        }
        
        //それ以外はクリックをオンにする
        m_isSet = true;
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
