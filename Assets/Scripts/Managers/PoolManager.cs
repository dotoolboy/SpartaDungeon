using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    private GameObject prefab;
    private IObjectPool<GameObject> pool;
    private Transform root;
    private Transform Root
    {
        get
        {
            if (root == null)
            {
                GameObject obj = new() { name = $"[Pool_Root] {prefab.name}" };
                root = obj.transform;
            }
            return root;
        }
    }

    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
        this.pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    /// <summary>
    /// Ǯ���� ������Ʈ�� ������. ������ �� ������Ʈ ����
    /// </summary>
    /// <returns></returns>
    public GameObject Pop()
    {
        return pool.Get();
    }

    /// <summary>
    /// ������Ʈ�� Ǯ�� ��ȯ �� ��Ȱ��ȭ
    /// </summary>
    /// <param name="obj"></param>
    public void Push(GameObject obj)
    {
        pool.Release(obj);
    }
    #region Callbacks

    /// <summary>
    /// ���ο� ������Ʈ �ν��Ͻ��� ����
    /// </summary>
    /// <returns></returns>
    private GameObject OnCreate()
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.SetParent(Root);
        obj.name = prefab.name;
        return obj;
    }
    /// <summary>
    /// Ǯ���� ������Ʈ�� ������
    /// </summary>
    /// <param name="obj"></param>
    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    /// <summary>
    /// ������Ʈ�� Ǯ�� ��ȯ
    /// </summary>
    /// <param name="obj"></param>
    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }
    /// <summary>
    /// ������Ʈ ����
    /// </summary>
    /// <param name="obj"></param>
    private void OnDestroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    #endregion
}

public class PoolManager
{

    private Dictionary<string, Pool> pools = new();

    public GameObject Pop(GameObject prefab)
    {
        // #1. Ǯ�� ������ ���� �����.
        if (pools.ContainsKey(prefab.name) == false) CreatePool(prefab);

        // #2. �ش� Ǯ���� �ϳ��� �����´�.
        return pools[prefab.name].Pop();
    }

    public bool Push(GameObject obj)
    {
        // #1. Ǯ�� �ִ��� Ȯ���Ѵ�. (���� �ִ� ���� ����)
        if (pools.ContainsKey(obj.name) == false) return false;

        // #2. Ǯ�� ���ӿ�����Ʈ�� �����ش�.
        pools[obj.name].Push(obj);

        return true;
    }
    /// <summary>
    /// ���ο� Ǯ�� �����ϰ� Dictionary�� �߰�
    /// </summary>
    /// <param name="prefab"></param>
    private void CreatePool(GameObject prefab)
    {
        Pool pool = new(prefab);
        pools.Add(prefab.name, pool);
    }

    public void Clear()
    {
        pools.Clear();
    }

}