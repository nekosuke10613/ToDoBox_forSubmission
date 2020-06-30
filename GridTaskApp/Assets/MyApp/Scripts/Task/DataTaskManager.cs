using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task
{
    public int ID;//タスク自体の識別ID
    public int HouseID; //登録したタスク表の場所
    public string Name;　//タスクの名前
    public string Description;　//タスクの詳細説明
    public bool IsFinish;　//このタスクは完了したものか


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

    List<Task> m_taskDic = new List<Task>();

    
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
        
    }
    //単体方眼をクリエイト
    //TODO : (指定した空白に情報を詰める処理にする)
    public void CreateTask(string name)
    {
        var task = Instantiate(m_singleTask,m_parent);
        task.SetTask(name);
        AllTaskNum++;
        task.SetPosition(TaskInitPos,TaskSpace,AllTaskNum-1);
        
    }
    //1行３つのの空白を追加する
    public void CreateThreeEmpty()
    {
        CreateTask("");
        CreateTask("");
        CreateTask("");
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
