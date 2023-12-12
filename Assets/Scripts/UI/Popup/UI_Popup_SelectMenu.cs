using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_SelectMenu : UI_Popup
{
    #region Enums
    enum Buttons
    {
        StatusBtn,
        InventoryBtn,
        ShopBtn
    }
    #endregion

    void Start()
    {
        Initialize(); 
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StatusBtn).gameObject.BindEvent(OnBtnStatus);
        GetButton((int)Buttons.InventoryBtn).gameObject.BindEvent(OnBtnInventory);
        GetButton((int)Buttons.ShopBtn).gameObject.BindEvent(OnBtnShop);

        return true;
    }

    public void OnBtnStatus(PointerEventData data)
    {
        Main.UI.ClosePopup(this);
        Main.UI.ShowPopupUI<UI_Popup_Status>();
    }

    public void OnBtnInventory(PointerEventData data)
    {
        Main.UI.ClosePopup(this);
        Main.UI.ShowPopupUI<UI_Popup_Inventory>();
    }

    public void OnBtnShop(PointerEventData data)
    {
        Main.UI.ClosePopup(this);
        Main.UI.ShowPopupUI<UI_Popup_Shop>();
    }
}
