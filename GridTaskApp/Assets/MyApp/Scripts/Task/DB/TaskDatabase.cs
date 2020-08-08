using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDatabase : MonoBehaviour
{
    public Text OwnerObject;
    private List<string> Charactors = new List<string>();
    public Task TestTask = new Task();
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            var fileName = "user_task_data.db";
            var db = new SqliteDatabase(fileName);
            var query = db.ExecuteQuery("SELECT * FROM TaskData");

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
                    $"Name:{name},\n"+
                    $"PageName:{page_name},\n" +
                    $"Description:{description},\n" +
                    $"CreateDate:{create_date},\n" +
                    $"Limit:{limit},\n" +
                    $"Proiority:{priority},\n" +
                    $"is_finish:{is_finish},\n" 
                    ;

                Charactors.Add(text);
                var test = $"{name}";
                Debug.Log(test);
                //テスト
                TestTask.SetInfo(int.Parse($"{id}"), int.Parse($"{house_id}"), int.Parse($"{page_id}"),
                    $"{name}", $"{page_name}", $"{description}",
                    $"{create_date}", $"{limit}", $"{priority}", true);
            }

        }
        catch (Exception ex)
        {
            Charactors.Add(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OwnerObject != null && Charactors.Count > 0)
        {
            string text = "";

            if (Charactors.Count > 0)
            {
                text = string.Join("\r\n", Charactors);
            }
            else
            {
                text = "キャラクターが存在しません";
            }

            OwnerObject.text = text;
        }
    }
}