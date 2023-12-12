using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ResourceManager
{
    public bool Loaded { get; set; }
    private Dictionary<string, UnityEngine.Object> _resources = new();

    // key(�ּ�)�� �޾� �񵿱�(Async) �ε�
    public void LoadAsync<T>(string key, Action<T> callback = null)
        where T : UnityEngine.Object // T�� UnityEngine.Object�� ���ؾ��Ѵ�.
    {
        // key�� �̹� �ε�� ���ҽ����� Ȯ�� (�ߺ� �ε� ����)
        // �̹� �ε�Ǿ��ִ� ���ҽ���(key���� �̹� Dictionary�� �ִٸ�)
        // �ٽ� �ε����� �ʰ� �ݹ� ȣ��
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            // callback�� null�� �ƴҰ�쿡�� ȣ��
            // ȣ��ɶ��� resource�� T�� ����ȯ�Ͽ� �ݹ鿡 ����
            callback?.Invoke(resource as T);
            return;
        }

        // key�� �޾�, ������ � ���ҽ��� �ε��� ������ ���� 
        string loadKey = key;

        // Sprite�� ��� key�� �״�� �ε��ϸ� Texture2D�� �ε�ǹǷ�,
        // Sprite ������ ��� Ű���� ���� �ε��ؾ� �Ѵ�.
        // Texture2D ������ �����Ϳ��� sprite�� �̾Ƴ��� ���� key�� ����
        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";
        // ".sprite"�� ""�� ġȯ�ؼ� loadKey�� ����
        // ex) resource.sprite -> resource

        // ���ҽ� �񵿱� �ε� ����
        if (key.Contains(".sprite"))
        {
            // Addressables.LoadAssetAsync<T> �� ����
            // AsyncOperationHandle<T> ������ ��ȯ���� asyncOperation�� �Ҵ�
            var asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);

            // �񵿱� �۾��� �Ϸ�� �� ȣ��Ǵ� �̺�Ʈ �ڵ鷯�� ����
            // op �Ű�����: �񵿱� �۾��� ���¿� ����� ��Ÿ��
            // Completed �̺�Ʈ: Action<AsyncOperationHandle<Sprite>> �̺�Ʈ
            // op.Result��: �ε��� ����
            asyncOperation.Completed += op =>
            {
                // Dictionary�� (key, op.Result) �߰�
                _resources.Add(key, op.Result);
                // �ݹ��� �ִٸ� Invoke
                callback?.Invoke(op.Result as T);
            };
        }
        else // sprite�� �ƴҰ��
        {
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += op =>
            {
                _resources.Add(key, op.Result);
                callback?.Invoke(op.Result as T);
            };
        }
    }

    // �ش� label�� ���� ��� ���ҽ��� �񵿱� �ε�
    // �Ϸ�Ǹ� �ݹ�(key, ����ε��, ��ü�ε��) ȣ��
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback)
        where T : UnityEngine.Object
    {
        // Label�� ���� ������ ��ġã��
        // LoadResourceLocationsAsync�� ���� ������ ����ü�� Result����
        // �ش� label�� ���� ��� ������ ��ġ���� ����ִ�.
        // label�� �ش��ϴ� ���ҽ��� ��ġ ������ �ε�
        // operation: AsyncOperationHandle<IList<IResourceLocation>> Ÿ����
        // �񵿱� �۾� �ڵ�
        var operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        // op: AsyncOperationHandle<IList<IResourceLocation>> Ÿ��
        operation.Completed += op =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            // ������ ��ġ ����(result)�� ����
            foreach (IResourceLocation result in op.Result)
            {
                // result.PrimaryKey: ���ҽ� ��ġ�� ���� Ű -> �� �̿��Ͽ� ���ҽ� �ε�
                LoadAsync<T>(result.PrimaryKey, obj =>
                {
                    loadCount++;
                    // callback�� �޾� loadCount / totalCount�� �ε����� ���� ���� �ִ�.
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };

        Loaded = true;
    }

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        // key������ resources�� ���ҽ��� �����ϴ��� Ȯ��
        if (!_resources.TryGetValue(key, out UnityEngine.Object resource))
            return null;

        // �����Ѵٸ� resource�� T�� ����ȯ�ؼ� ��ȯ
        return resource as T;
    }

    public void Unload<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            Addressables.Release(resource);
            _resources.Remove(key);
        }
        else
            Debug.LogError($"Resource Unload {key}");
    }

    // prefab
    //GameObject obj = Thing.Resource.InstantiatePrefab("name.prefab");
    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        if (pooling) return Main.Pool.Pop(prefab);

        GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    // �ش� ������Ʈ�� Ǯ�� �������ų� �ı��Ѵ�.
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        if (Main.Pool.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }
}
