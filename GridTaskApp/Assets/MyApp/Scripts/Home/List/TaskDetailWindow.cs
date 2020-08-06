using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TaskDetailWindow : MonoBehaviour
{
    #region --  SerializeField  --
    [SerializeField]
    RectTransform m_rect;
    [SerializeField,Header("タスクタイトル")]
    InputField m_titleText;
    [SerializeField, Header("完了状態画像")]
    Image m_finishStateImage;
    [SerializeField, Header("登録日")]
    Text m_dateText;
    [SerializeField, Header("ページカテゴリ")]
    Text m_pageCategory;
    [SerializeField, Header("優先度")]
    Image m_yusendoImage;
    [SerializeField, Header("期限テキスト")]
    InputField m_limitText;
    [SerializeField, Header("説明詳細")]
    InputField m_descriptionText;

    #endregion
    AppWindow m_appWin;
    Task m_currentTask;
    SingleTask m_singleTask;

    public void Init(Task task,SingleTask single,UnityAction callback = null)
    {
        //タスクデータを各場所に入れる
        //TODO :画像の項目
        m_titleText.text = task.Name;
        //m_finishStateImage
        m_dateText.text = task.CreateDate;
        m_pageCategory.text = task.PageName;
        //m_yusendoImage
        m_limitText.text = task.Limit;
        m_descriptionText.text = task.Description;
        m_currentTask = task;
        m_singleTask = single;

    }
    public void OnClose(GameObject thisObj)
    {
        //SingleTaskに変更したデータを反映(IDはそのままにしたい)
        m_currentTask.SetInfo(m_currentTask.HouseID,
            m_currentTask.PageID,//
            m_titleText.text,
            m_pageCategory.text,
            m_descriptionText.text,
            m_currentTask.CreateDate,//
            m_limitText.text,
            m_currentTask.Priority,//
            m_currentTask.IsFinish);//

        
        m_singleTask.SetTask(m_currentTask);
        //指定したID内の情報を上書きする
        //データを保存してWindowを閉じる
        Destroy(thisObj);
    }
    /// <summary>
    /// 今選択している方眼のタスク情報を削除して空白にする
    /// </summary>
    void DeleteTask()
    {

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
