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
    public bool IsFinish = false;　//このタスクは完了したものか

    public void SetInfo(int houseID,string name, string desc, string limit, string priority)
    {
        HouseID = houseID;
        Name = name;
        Description = desc;
        Limit = limit;
        Priority = priority;
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

    //タスク入れ物リスト
    List<SingleTask> m_singleTaskList = new List<SingleTask>();

    
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
        var taskAdress = m_singleTaskList[task.HouseID];
        //情報を入れる
        taskAdress.SetTask(task.Name, task.Description, task.Limit, task.Priority);
    }
    //単体方眼をクリエイト
    //TODO : (指定した空白に情報を詰める処理にする)
    void CreateTask(string title,string desctiption,string limit,string priority)
    {
        var task = Instantiate(m_singleTask,m_parent);
        task.SetTask(title,desctiption,limit,priority);
        //SingleTaskをリストに保存(Listのintが住所になる)
        m_singleTaskList.Add(task);

        AllTaskNum++;
        task.SetPosition(TaskInitPos,TaskSpace,AllTaskNum-1);
        
    }
    //1行３つのの空白を追加する
    public void CreateThreeEmpty()
    {
        CreateTask("","","","");
        CreateTask("","","","");
        CreateTask("","","","");
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
