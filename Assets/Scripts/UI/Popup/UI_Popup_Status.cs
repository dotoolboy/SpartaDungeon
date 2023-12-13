using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Status : UI_Popup
{
    #region Enums
    enum Buttons
    {
        CloseBtn
    }
    enum Images
    {
        AtkImege,
        DefImege,
        HpImege,
        CriImege
    }
    enum Texts
    {
        AtkText,
        DefText,
        HpText,
        CriText
    }
    #endregion

    #region MonoVehaviours
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region Initialize
    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.CloseBtn).gameObject.BindEvent(OnBtnClose);


        return true;

    }
    #endregion

    public void OnBtnClose(PointerEventData data)
    {
        Main.UI.ClosePopup(this);

    }

}
