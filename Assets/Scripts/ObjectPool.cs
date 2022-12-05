using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject target;
    private Queue<GameObject> pool = new Queue<GameObject>();
    public int initcount = 0;
    

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < initcount; i++)
        {
            pool.Enqueue(CreateObject());
        }
    }

    public GameObject CreateObject()
    {
        var obj = Instantiate(target);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }


    public GameObject GetObject()
    {
        if(pool.Count>0)
        {
            var obj = pool.Dequeue();
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            var obj = Instantiate(target);
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }
}
