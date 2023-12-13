using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Player Player { get; private set; }
    public void Initialize()
    {
        Player = new("Player", Main.Data.Characters["Warrior"]);
    }

}
