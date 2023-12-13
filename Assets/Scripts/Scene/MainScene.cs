using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // ==================================== �� ���� �� ó�� ====================================

        // �� ���� �� ó�� �۾�!
        UI = Main.UI.ShowSceneUI<UI_Scene_Main>();
        Main.UI.ShowPopupUI<UI_Popup_SelectMenu>();
       

        // =========================================================================================

        return true;
    }
}
