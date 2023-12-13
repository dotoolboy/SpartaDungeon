using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public UI_Scene UI { get; protected set; }

    private bool _Initialized = false;

    void Start()
    {
        if (Main.Resource.Loaded)
        {
            //Main.Data.Initialize();
            //Main.Game.Initialize();
            Initialize();
        }
        else
        {
            Main.Resource.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) =>
            {
                // Debug.Log($"[GameScene] Load asset {key} ({count}/{totalCount})");
                if (count >= totalCount)
                {
                    Main.Resource.Loaded = true;
                    //Main.Data.Initialize();
                    //Main.Game.Initialize();
                    Initialize();
                }
            });
        }
    }

    protected virtual bool Initialize()
    {
        if (_Initialized) return false;

        Main.Scene.CurrentScene = this;

        Object obj = GameObject.FindObjectOfType<EventSystem>();
        if (obj == null) Main.Resource.Instantiate("EventSystem.prefab").name = "@EventSystem";

        _Initialized = true;
        return true;
    }
}
