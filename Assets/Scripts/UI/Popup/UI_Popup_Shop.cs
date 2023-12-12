using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Shop : UI_Popup
{
    #region Enums

    private enum Texts
    {
        GoldText,
    }

    private enum Buttons
    {
        CloseBtn,
    }

    private enum GameObjects
    {
        Content,
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

        GetButton((int)Buttons.CloseBtn).gameObject.BindEvent(OnBtnClose);


        return true;

    }
    #endregion

    public void OnBtnClose(PointerEventData data)
    {
        Main.UI.ClosePopup(this);
    }
}
