using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AddTaskManager : MonoBehaviour
{
    [SerializeField,Header("入力したテキスト")]
    Text m_inputText;

    
    AppWindow m_appWin;

    public void Init(UnityAction callBack = null)
    {
        if (callBack != null)
            callBack.Invoke();
    }

    public void OnSave()
    {
        //入力したタスクを登録する
        var text = m_inputText.text;
        Debug.Log(text);
    }
    #region アタッチ自動化
#if UNITY_EDITOR
    //スクリプト追加時に毎回自動でアタッチしてもらう
    void Reset()
    {
        m_appWin = gameObject.GetComponent<AppWindow>();
    }
#endif
    #endregion
}
