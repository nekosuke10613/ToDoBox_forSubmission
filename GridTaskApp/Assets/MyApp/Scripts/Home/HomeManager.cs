using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HomePage
{
    None = 0,
    List = 1,   //一覧
    Add = 2,    //登録
    Option = 3, //
}
public class HomeManager : MonoBehaviour
{
    //[SerializeField,Header("各ページのプレハブを生成する親オブジェクト")]
    //GameObject m_pagesParent;

    //アタッチWindowクラスに変える
    [SerializeField,Header("各ページプレハブ")]
    AppWindow[]  m_appWins = null;

    //現在のページ名
    HomePage m_currentPage = HomePage.List;

    [SerializeField]
    TaskListManager m_taskListMgr;
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
        SetPage(HomePage.List);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(int nextPage)
    {
        //同じページだったらreturn
        if (nextPage == (int)m_currentPage)
            return;
        SetPage((HomePage)nextPage);
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
            case HomePage.List:
                page.Init(()=>  m_taskListMgr.Init());
                break;
            case HomePage.Add:
                page.Init(() => m_addTaskMgr.Init());
                break;
            case HomePage.Option:
                page.Init(() => m_optionMgr.Init());
                break;
        }
        page.gameObject.SetActive(true);

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
