using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SqlTester
{
    private static string _fileName = "sample.db";

    public static void Insert()
    {
        try
        {
            var db = new SqliteDatabase(_fileName);

            var rand = UnityEngine.Random.Range(2, 100);
            var name = $"athena No.{rand}";
            var data1 = rand;
            var data2 = "arrow";

            db.ExecuteQuery($"INSERT INTO sample (name, data1, data2) VALUES ('{name}', {data1}, '{data2}')");
        }
        catch
        {
            throw;
        }
    }

    public static void Update()
    {
        try
        {
            var db = new SqliteDatabase(_fileName);

            var power = UnityEngine.Random.Range(100, 1000);

            db.ExecuteQuery($"UPDATE sample SET data1 = {power} WHERE id = 1");
        }
        catch
        {
            throw;
        }
    }

    public static IEnumerable<string> Select()
    {
        var charactors = new List<string>();

        try
        {
            var db = new SqliteDatabase(_fileName);
            var query = db.ExecuteQuery("SELECT * FROM sample");

            foreach (var row in query.Rows)
            {
                var id = row["id"];
                var name = row["name"];
                var data1 = row["data1"];
                var data2 = row["data2"];

                var text = $"ID:{id}, Name:{name}, DATA1:{data1}, DATA2:{data2}";

                charactors.Add(text);
            }

        }
        catch
        {
            throw;
        }

        return charactors;
    }
}
public class Test : MonoBehaviour
{
    public Text OwnerObject;
    private List<string> Charactors = new List<string>();
    private string ExceptionText = "";
    public event EventHandler<IEnumerable<string>> OccurredSelect;

    // Start is called before the first frame update
    void Start()
    {
       

        try
        {
            var charactors = SqlTester.Select();

            AddCharactors(charactors, true);
        }
        catch (Exception ex)
        {
            ExceptionText = ex.Message;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OwnerObject == null) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Insert Called.");
            SqlTester.Insert();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Select Called.");
            var characters = SqlTester.Select();
            OccurredSelect.Invoke(this, characters);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Update Called.");
            SqlTester.Update();
        }

        string text = "";

        if (ExceptionText == "")
        {
            if (Charactors.Count > 0)
            {
                text = string.Join("\r\n", Charactors);
            }
            else
            {
                text = "キャラクターが存在しません";
            }
        }
        else
        {
            text = ExceptionText;
        }

        OwnerObject.text = text;

    }

    private void AddCharactors(IEnumerable<string> charactors, bool isReset)
    {
        if (isReset)
        {
            Charactors.Clear();
        }

        foreach (var chara in charactors)
        {
            Charactors.Add(chara);
        }
    }
}
