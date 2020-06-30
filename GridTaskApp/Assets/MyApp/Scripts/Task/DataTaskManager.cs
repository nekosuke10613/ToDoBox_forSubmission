using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Task
{
    int id;//タスク自体の識別ID
    int houseID; //登録したタスク表の場所
    string name;　//タスクの名前
    string description;　//タスクの詳細説明
    bool isFinish;　//このタスクは完了したものか
}
//タスクデータの管理クラス　シングルトンにしたい
public class DataTaskManager : MonoBehaviour
{
    public readonly Vector2 TaskInitPos = new Vector2(-300, 400);
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateTask(string name)
    {
        var task = Instantiate(m_singleTask,m_parent);
        task.SetTask(name);
        AllTaskNum++;
        task.SetPosition(TaskInitPos,TaskSpace,AllTaskNum-1);
        
    }
}
