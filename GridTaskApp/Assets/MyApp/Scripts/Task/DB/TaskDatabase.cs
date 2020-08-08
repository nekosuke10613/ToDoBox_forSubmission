using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDatabase : MonoBehaviour
{
    static readonly string m_fileName = "user_task_data.db";
    static readonly string m_tableName = "TaskData";


    public List<Task> m_user_task_data = new List<Task>();
    //public Task TestTask = new Task();
    // Start is called before the first frame update
    void Start()
    {
        Select();
    }
    //単体
    public void Select()
    {
        try
        {
            
            var db = new SqliteDatabase(m_fileName);
            var query = db.ExecuteQuery("SELECT * FROM "+ m_tableName);

            foreach (var row in query.Rows)
            {
                var id = row["id"];
                var house_id = row["house_id"];
                var page_id = row["page_id"];
                var name = row["name"];
                var page_name = row["page_name"];
                var description = row["description"];
                var create_date = row["create_date"];
                var limit = row["limit"];
                var priority = row["priority"];
                var is_finish = row["is_finish"];

                var text = $"ID:{id},\n" +
                    $"HouseID:{house_id},\n" +
                    $"PageID:{page_id}, \n" +
                    $"Name:{name},\n" +
                    $"PageName:{page_name},\n" +
                    $"Description:{description},\n" +
                    $"CreateDate:{create_date},\n" +
                    $"Limit:{limit},\n" +
                    $"Proiority:{priority},\n" +
                    $"is_finish:{is_finish},\n"
                    ;


                //テスト
                Task newtask = new Task();
                newtask.SetInfo(int.Parse($"{id}"), int.Parse($"{house_id}"), int.Parse($"{page_id}"),
                    $"{name}", $"{page_name}", $"{description}",
                    $"{create_date}", $"{limit}", $"{priority}", true);
                m_user_task_data.Add(newtask);
            }

        }
        catch (Exception ex)
        {
            Debug.LogError("タスクデータ読み込みに失敗した");
            
        }
    }
    //単体
    public static void Insert(Task task)
    {
        try
        {
            var db = new SqliteDatabase(m_fileName);

            var query = db.ExecuteQuery("INSERT INTO " + m_tableName +
                " (id, house_id, page_id,name,page_name,description,create_date,priority,is_finish)" +
                $" VALUES ('{task.ID}', '{task.HouseID}', '{task.PageID}','{task.Name}','{task.PageName}','{task.Description}','{task.CreateDate}','{task.Priority}','{task.IsFinish}')");
        }
        catch
        {
            throw;
        }
    }
    public void InfoUpdate()
    {

    }
    
}