using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionManager : MonoBehaviour
{
    public void Init(UnityAction callback = null)
    {
        if (callback != null)
            callback.Invoke();
    }
    
    public void Open(UnityAction callback = null)
    {
        

    }
}
