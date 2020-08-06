using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HomePage
{
    None = 0,
    Daily = 1,   //一覧(デイリー) 
    Other = 2,    //一覧(それ以外)
    Option = 3, //　設定
    Add = 4,
}
public class HomeManager : MonoBehaviour
{
    //[SerializeField,Header("各ページのプレハブを生成する親オブジェクト")]
    //GameObject m_pagesParent;

    //アタッチWindowクラスに変える
    [SerializeField,Header("各ページプレハブ")]
    AppWindow[]  m_appWins = null;

    //現在のページ名
    HomePage m_currentPage = HomePage.Daily;

    [SerializeField]
    TaskListManager m_dailyTaskListMgr;
    [SerializeField]
    TaskListManager m_otherTaskListMgr;
    [SerializeField]
    AddTaskManager m_addTaskMgr;
    [SerializeField]
    OptionManager m_optionMgr;

    

    

    //TODO 後で消す　テスト用
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        //全てのページを非アクティブに
        foreach (AppWindow page in m_appWins)
        {
            page.gameObject.SetActive(false);
        }
        //各ページの初期化処理を行う

        m_addTaskMgr.Init();
        m_dailyTaskListMgr.Init();
        m_otherTaskListMgr.Init();
        m_optionMgr.Init();

        SetPage(HomePage.Daily);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Home内のScreen管理
    public void OnClick(int nextPage)
    {
        //同じページだったらreturn
        if (nextPage == (int)m_currentPage)
            return;
        if (nextPage == (int)HomePage.Add)
        {
            OpenAddPage();
            return;
        }
        SetPage((HomePage)nextPage);
    }

    void OpenAddPage()
    {
        m_appWins[(int)HomePage.Add-1].Init(
            () => m_addTaskMgr.Open(),
            ()=>m_addTaskMgr.OnClose());
        
    }
    void SetPage(HomePage nextPage)
    {
        //*--開いてるページを閉じる--*/
        m_appWins[(int)m_currentPage - 1].gameObject.SetActive(false) ;

        //noneの分＋１で調整
        m_currentPage = nextPage;

        //*-- 新しいページを開く --*/
        var page = m_appWins[(int)m_currentPage - 1];
        
        //ページの初期化を呼ぶ(Window)
        switch (nextPage)
        {
            case HomePage.Daily:
                page.Init(()=>  m_dailyTaskListMgr.Open(false));
                break;
            case HomePage.Other:
                page.Init(() => m_dailyTaskListMgr.Open(true));
                break;
            case HomePage.Option:
                page.Init(() => m_optionMgr.Open());
                break;
        }
        

    }

    #region アタッチ自動化
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう
    void Reset()
    {
        
    }
#endif
    #endregion
}
