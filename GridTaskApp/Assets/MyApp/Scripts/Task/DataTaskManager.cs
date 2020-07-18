using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task
{
    //IDの管理・設定などはTask内で行う
    public int ID { get; private set; }//タスク自体の識別ID
    public int HouseID { get; private set; } //登録したタスク表の場所
    public string Name { get; private set; }　//タスクの名前
    public string Description { get; private set; }　//タスクの詳細説明
    public string　Limit { get; private set; }　//タスクの期限
    public string Priority { get; private set; }　//タスクの優先度

    public bool IsFinish { get; private set; }　//このタスクは完了したものか

    public void SetInfo(int houseID,string name, string desc, string limit, string priority)
    {
        HouseID = houseID;
        Name = name;
        Description = desc;
        Limit = limit;
        Priority = priority;
        IsFinish = false;

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
    void SaveData()
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
        task.SetInfo(AllTaskNum,"","","","");
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
