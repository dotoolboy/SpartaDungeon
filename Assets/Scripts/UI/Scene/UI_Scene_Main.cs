using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Main : UI_Scene
{
    #region Enums

    enum Texts
    {
        ClassText,
        PlayerNameText,
        PlayerLevelText,
        ClassDescriptionText
    }
    enum Images
    {
        PlayerSprite
    }


    #endregion


    void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));


        SetPlayerData();

        return true;
    }

    private void SetPlayerData()
    {
        GetText((int)Texts.PlayerNameText).text = Main.Game.Player.UserName;
        GetText((int)Texts.ClassText).text = Main.Game.Player.ClassName;
        GetText((int)Texts.ClassDescriptionText).text = Main.Game.Player.ClassDescription;
    }

}
