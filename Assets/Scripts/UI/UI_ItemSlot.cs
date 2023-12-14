using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : UI_Base
{

    private enum Buttons
    {
        BtnEquip
    }


    private void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.BtnEquip).gameObject.BindEvent(OnBtnEquip);


        return true;

    }

    public void OnBtnEquip(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Equip>();
    }
}
