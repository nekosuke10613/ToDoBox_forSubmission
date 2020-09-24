using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlertManager : SingletonMonoBehaviour<AlertManager>
{
    [SerializeField, Header("AlertWinプレハブ")]
    AlertWin m_alert;
    
    /// <summary>
    /// アラートWinを出す
    /// </summary>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    /// <param name="callback"></param>
    public void Alert(string title,string desc,UnityAction callback = null)
    {
        m_alert.Init(title,desc, callback);
    }
}
