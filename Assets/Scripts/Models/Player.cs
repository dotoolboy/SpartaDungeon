using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string UserName { get; private set; }
    public CharacterData Data { get; private set; }
    public string ClassName { get; private set; }
    public string ClassDescription { get; private set; }


    public Status Status { get; private set; } = new();


    public event Action OnPlayerDataUpdated;

    public Player(string name, CharacterData data)
    {
        this.UserName = name;
        this.Data = data;
        this.ClassName = data.name;
        this.ClassDescription = data.description;
       
        Status = new();
    }
}