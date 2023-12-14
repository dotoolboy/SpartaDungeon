using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Equip : UI_Popup
{
    private enum Buttons
    {
        CancleBtn,
        ConfirmBtn
    }


    private void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.CancleBtn).gameObject.BindEvent(CancleBtn);
        GetButton((int)Buttons.ConfirmBtn).gameObject.BindEvent(ConfirmBtn);


        return true;

    }

    public void CancleBtn(PointerEventData data)
    {
        Main.UI.ClosePopup(this);
    }

    public void ConfirmBtn(PointerEventData data)
    {
        Main.UI.ClosePopup(this);

    }
}
