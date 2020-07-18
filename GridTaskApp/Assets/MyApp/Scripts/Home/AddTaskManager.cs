using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AddTaskManager : MonoBehaviour
{
#region *** SerializeField ***
    [SerializeField]
    RectTransform m_rect;
    [SerializeField, Header("AddSingleTaskプレハブ")]
    AddSingleTask m_addSingleTask;

    [SerializeField,Header("タイトル")]
    Text m_titleInput;
    [SerializeField, Header("詳細説明")]
    Text m_desctiptionInput;
    [SerializeField, Header("期限(仮にフィールド、--:--で入力)")]
    Text m_scheduleInput;
    [SerializeField, Header("優先度ラベル")]
    Text m_priorityLabel;

    [SerializeField]
    Dropdown m_priorityDropdown;
    [SerializeField, Header("SingleTaskのスクロールバーのコンテンツ")]
    RectTransform m_content;

    [SerializeField, Header("仮タスクアドレス入力")]
    Text m_testAdressText;

#endregion


    AppWindow m_appWin;
    //Add用ボックスの一覧リスト。ページ選択するたびに情報クリア＆数調整するからDicにはしない。
    List<AddSingleTask> m_addTaskList = new List<AddSingleTask>();

    Vector2 m_defaultPos = Vector2.zero;
    Vector2 m_initPos = new Vector2(0, -2000);
    float m_scrollHeight = 500;

    //OpenとInitで分けたほうがいいのでは？
    public void Init(UnityAction callBack = null)
    {
        ListClear();

        m_rect.anchoredPosition = m_initPos;
        if (callBack != null)
            callBack.Invoke();
        m_rect.DOAnchorPos(m_defaultPos, 0.5f);

        
        //最初に初期選択状態の方眼の数分AddSingleTaskを生成してリストに保存
        //とりあえず１５
        for(int i = 0; i < 15; i++)
        {
            var adTask= Instantiate(m_addSingleTask,m_content);
            m_addTaskList.Add(adTask);
            adTask.Init();
            //位置調整をする
            adTask.SetPosition(new Vector2(-300, 0), new Vector2(300, 200), i);

        }

        //Open();
        
    }
    public void Open(UnityAction callBack = null)
    {
        if (callBack != null)
            callBack.Invoke();
        //タスクのデータを持ってくる
        var stLis = DataTaskManager.Instance.SingleTaskList;
        for(int i = 0; i < stLis.Count;i++)
        {
            //Instantiate(single); //一応オブジェクト持ってこれるけどなんかナンセンス
            var task = stLis[i].GetTask();
            string test = task.Name;
            Debug.Log(i);
            //ここでAddSingleTaskのInitorSetInfoを呼びたい
            m_addTaskList[i].SetInfo(task,i);

            //AddSingletaskの数調整。多ければ消し足りなければ生成する
            //.m_addSingleTask[].SetInfo(task);
            //
            //Debug.Log(test);
        }
        //Contentサイズの調整
        float h = DataTaskManager.Instance.TaskHeight();
        //5行以上ならスクロールバーの長さ調整
        if (h > m_scrollHeight)
            m_content.sizeDelta = new Vector2(
                m_content.sizeDelta.x,
                DataTaskManager.Instance.TaskHeight());
        
    }
    /// <summary>
    /// 保存ボタンでタスクを保存する
    /// </summary>
    public void OnSave()
    {
        //入力したタスクを登録する
        var testAdress = int.Parse(m_testAdressText.text);

        var task = new Task();
        task.SetInfo(
            testAdress,
            m_titleInput.text,
            m_desctiptionInput.text,
            m_scheduleInput.text,
            m_priorityLabel.text
            );
        DataTaskManager.Instance.CreateInfo(task);

        //このページで選択中のボックスに情報入れる
        Clear();
    }
    //タスク保存後の入力値リセット
    void Clear()
    {
        m_titleInput.text = "";
        m_desctiptionInput.text = "";
        m_scheduleInput.text = "00:00";
        //Debug.Log(m_priorityDropdown.options[0]);
        m_priorityLabel.text = m_priorityDropdown.options[0].text;
    }
    /// <summary>
    ///行の追加ボタン
    /// </summary>
    public void OnAddLine()
    {
        //ページListのほうに追加する
        DataTaskManager.Instance.CreateThreeEmpty();
        //このページのラインを追加する
    }
    void ListClear()
    {
        foreach(AddSingleTask adTsk in m_addTaskList)
        {
            Destroy(adTsk.gameObject);
        }
        m_addTaskList.Clear();
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
