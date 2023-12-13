using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public string key;
    public string name;
    public string description;
    public float Hp;     
    public float Atk;
    public float Def;
    public float Cri;
    public int Gold;
}

[Serializable]
public class CharacterDataLoader : ILoader<string, CharacterData>
{
    public List<CharacterData> characters = new();
    public Dictionary<string, CharacterData> MakeDictionary()
    {
        Dictionary<string, CharacterData> dictionary = new();
        foreach (CharacterData character in characters)
        {
            dictionary.Add(character.key, character);
        }
        return dictionary;
    }
}
