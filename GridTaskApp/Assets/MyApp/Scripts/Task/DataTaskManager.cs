using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task
{
    //IDの管理・設定などはTask内で行う
    //タスク自体の識別ID　-1は例外
    public int ID { get; private set; }
    //登録したタスク表の場所
    public int HouseID { get; private set; }
    //登録されてるページID -1は例外
    public int PageID { get; private set; }
    //タスクの名前
    public string Name { get; private set; }
    //ページの名前
    public string PageName { get; private set; }
    //タスクの詳細説明
    public string Description { get; private set; }
    //タスクの登録日
    public string CreateDate { get; private set; }
    //タスクの期限
    public string　Limit { get; private set; }
    //タスクの優先度
    public string Priority { get; private set; }
    //このタスクは完了したものか
    public bool IsFinish { get; private set; }　

    /// <summary>
    /// タスク情報をセット(登録)する
    /// </summary>
    /// <param name="houseID">タスク表の場所</param>
    /// <param name="pageID">ページID</param>
    /// <param name="name">タスクタイトル</param>
    /// <param name="pageName">ページ名前</param>
    /// <param name="desc">タスク説明</param>
    /// <param name="createDate">タスク登録日</param>
    /// <param name="limit">タスク期限</param>
    /// <param name="priority">優先度</param>
    /// <param name="isFinish">タスクが完了したか</param>
    public void SetInfo(int houseID,int pageID, string name, string pageName,string desc,string createDate, string limit, string priority,bool isFinish = false)
    {
        HouseID = houseID;
        PageID = pageID;
        Name = name;
        PageName = pageName;
        Description = desc;
        CreateDate = createDate;
        Limit = limit;
        Priority = priority;
        IsFinish = isFinish;

        CreateID();
    }
    /// <summary>
    /// タスクの終了状態を保存する
    /// </summary>
    /// <param name="isFinish"></param>
    public void SaveIsFinish(bool isFinish)
    {
        IsFinish = isFinish;
    }
    void CreateID()
    {

    }
    //CSVかJsonか何かに全タスク情報＆タスクスペースの情報を保存する
    public void SaveData()
    {

    }
    public void LoadDate()
    {

    }

}
//タスクデータの管理クラス　シングルトンにしたい
public class DataTaskManager : SingletonMonoBehaviour<DataTaskManager>
{
    public readonly Vector2 TaskInitPos = new Vector2(-300, 0);
    public readonly Vector2 TaskSpace = new Vector2(300, 200);

    public int AllTaskNum = 0;

    [SerializeField]
    Transform m_parent;
    [SerializeField, Header("個別のタスク")]
    SingleTask m_singleTask;
    [SerializeField,Header("タスク関係Win生成親")]
    Transform m_taskWinParnet;
    public Transform TaskWinParent{
        get { return m_taskWinParnet; }  }

    //タスク入れ物リスト ページがつくから、あとでDictionaryかリストのリストになる
    public List<SingleTask> SingleTaskList = new List<SingleTask>();

    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Init()
    {
        //最初期は3*5の空白が設定してある
        //TODO: Instanciateしたくない
        for(int i = 0; i < 5; i++)
            CreateThreeEmpty();

        // データ保存してある状態なら各方眼に情報を入れる
    }
    
    /// <summary>
    /// タスクの情報を作成して既存のボックスに代入
    /// </summary>
    public void CreateInfo(Task task)
    {
        //タスク情報入れる先の住所を持ってくる
        var taskAdress = SingleTaskList[task.HouseID];
        //情報を入れる
        taskAdress.SetTask(task);
    }
    //単体方眼をクリエイト
    //TODO : (指定した空白に情報を詰める処理にする)
    void CreateTask(Task task)
    {
        //空のデータを挿入
        task.SetInfo(AllTaskNum,-1,"","","","","","");
        var singleTask = Instantiate(m_singleTask,m_parent);
        singleTask.SetTask(task);
        //SingleTaskをリストに保存(Listのintが住所になる)
        SingleTaskList.Add(singleTask);

        AllTaskNum++;
        singleTask.SetPosition(TaskInitPos,TaskSpace,AllTaskNum-1);
        
    }
    //1行３つのの空白を追加する
    public void CreateThreeEmpty()
    {
        for (int i = 0; i < 3; i++)
        {
            var task = new Task();
            //から情報の初期タスクを作る
            CreateTask(task);
        }
    }

    /// <summary>
    /// タスク行の高さを取得
    /// </summary>
    public float TaskHeight()
    {
        int s = AllTaskNum / 3;
        float height = s *  TaskSpace.y;
        return height;
    }
}
