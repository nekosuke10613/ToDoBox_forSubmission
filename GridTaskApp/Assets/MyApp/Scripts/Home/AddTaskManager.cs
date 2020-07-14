using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class AddTaskManager : MonoBehaviour
{
#region *** SerializeField ***
    [SerializeField]
    RectTransform m_rect;
    [SerializeField,Header("タイトル")]
    Text m_titleInput;
    [SerializeField, Header("詳細説明")]
    Text m_desctiptionInput;
    [SerializeField, Header("期限(仮にフィールド、--:--で入力)")]
    Text m_scheduleInput;
    [SerializeField, Header("優先度ラベル")]
    Text m_priorityLabel;

    [SerializeField]
    Dropdown m_priorityDropdown;

    [SerializeField, Header("仮タスクアドレス入力")]
    Text m_testAdressText;

#endregion

    AppWindow m_appWin;
    

    Vector2 m_defaultPos = Vector2.zero;
    Vector2 m_initPos = new Vector2(0, -2000);

    public void Init(UnityAction callBack = null)
    {
        m_rect.anchoredPosition = m_initPos;
        if (callBack != null)
            callBack.Invoke();
        m_rect.DOAnchorPos(m_defaultPos, 0.5f);
    }
    /// <summary>
    /// 保存ボタンでタスクを保存する
    /// </summary>
    public void OnSave()
    {
        //入力したタスクを登録する
        var testAdress = int.Parse(m_testAdressText.text);

        var task = new Task();
        task.SetInfo(
            testAdress,
            m_titleInput.text,
            m_desctiptionInput.text,
            m_scheduleInput.text,
            m_priorityLabel.text
            );
        DataTaskManager.Instance.CreateInfo(task);
        Clear();
    }
    //タスク保存後の入力値リセット
    void Clear()
    {
        m_titleInput.text = "";
        m_desctiptionInput.text = "";
        m_scheduleInput.text = "00:00";
        //Debug.Log(m_priorityDropdown.options[0]);
        m_priorityLabel.text = m_priorityDropdown.options[0].text;
    }
    /// <summary>
    ///行の追加ボタン
    /// </summary>
    public void OnAddLine()
    {
        DataTaskManager.Instance.CreateThreeEmpty();
    }

    #region アタッチ自動化
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう 途中から追加した場合は自分で追加
    void Reset()
    {
        m_appWin = gameObject.GetComponent<AppWindow>();
        m_rect = gameObject.GetComponent<RectTransform>();
    }
#endif
    #endregion
}
