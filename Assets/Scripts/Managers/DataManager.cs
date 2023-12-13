using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static CharacterData;

public class DataManager
{
    public Dictionary<string, ItemData> Items = new();
    public Dictionary<string, CharacterData> Characters = new();

    public void Initialize()
    {
        Items = LoadJson<ItemDataLoader, string, ItemData>("ItemData").MakeDictionary();
        Characters = LoadJson<CharacterDataLoader, string, CharacterData>("CharacterData").MakeDictionary();
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Main.Resource.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }

}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDictionary();
}
