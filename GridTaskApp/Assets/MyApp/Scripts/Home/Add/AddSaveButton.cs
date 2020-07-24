using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AddSaveButton : MonoBehaviour
{
    public bool CanSave { get; private set; }

    [SerializeField]
    Image m_saveButton;

    float m_minAlpha = 0.3f;

    public void Init()
    {
        CanSave = true;
        BanSave(true);
    }
    public void OnValueChange(string text)
    {
        if (text == "")
        {
            CanSave = false;
            BanSave(true);
        }
        else
        {
            CanSave = true;
            BanSave(false);
        }
    }
    void BanSave(bool ban)
    {
        if(ban)
            m_saveButton.DOFade(m_minAlpha, 0.5f);
        else
            m_saveButton.DOFade(1, 0.5f);

    }
}
