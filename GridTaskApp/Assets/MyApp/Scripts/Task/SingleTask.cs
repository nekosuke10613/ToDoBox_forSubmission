using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SingleTask : MonoBehaviour
{
    [SerializeField]
    RectTransform m_rect;
    [SerializeField, Header("タスク詳細Win")]
    TaskDetailWindow m_detailMnr;
    [SerializeField]
    AppWindow m_detailWindow;
    
    [SerializeField]
    Text m_taskTitle;
    [SerializeField]
    Text m_taskDescription;
    [SerializeField]
    Text m_taskLimit;
    [SerializeField]
    Text m_taskPriority;
    [SerializeField, Header("チェックマークImage")]
    Image m_checkImage;

    [SerializeField]
    Image m_buttonBG;

    //このスペースのタスク情報
    Task m_task;
    //詳細Win生成親
    Transform m_winParent;

    float m_finishAlpha = 0.5f;

    
    /// <summary>
    /// タスク(Taskクラス)をセットする
    /// </summary>
    /// <param name="task"></param>
    public void SetTask(Task task)
    {
        m_task = task;
        m_taskTitle.text = task.Name;
        m_taskDescription.text = task.Description;
        m_taskLimit.text = task.Limit;
        m_taskPriority.text = task.Priority;
        m_checkImage.gameObject.SetActive(task.IsFinish);
    }
    public Task GetTask()
    {//追加するかもしれないからとりあえず関数
        return m_task;
    }
    /// <summary>
    /// タスク詳細ウィンドウを出す
    /// </summary>
    public void OnOpenDetail()
    {
        var parent = DataTaskManager.Instance.TaskWinParent;
        var win = Instantiate(m_detailWindow,parent);
        win.Init(
            ()=>m_detailMnr.Init(m_task,this),
            ()=>m_detailMnr.OnClose(win.gameObject));
    }

    public void OnFinish()
    {
        //タップで完了
        m_task.SaveIsFinish(true);
        m_checkImage.gameObject.SetActive(true);
        m_buttonBG.DOFade(0.5f,0.5f);
    }
  
    public void SetPosition(Vector2 pos, Vector2 space,int taskNum)
    {
        var setPos = Vector2.zero;
        setPos.x = pos.x + space.x * (taskNum % 3);
        setPos.y = pos.y +(- space.y * (taskNum / 3));
        m_rect.anchoredPosition = setPos;
    }
    /// <summary>
    /// 外からSingleTaskの表示を空白にするためだけの関数
    /// </summary>
    public void SetEmpty()
    {
        m_taskTitle.text = "";
        m_taskDescription.text = "";
        m_taskLimit.text = "";
        m_taskPriority.text = "";
        m_checkImage.gameObject.SetActive(false);
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
