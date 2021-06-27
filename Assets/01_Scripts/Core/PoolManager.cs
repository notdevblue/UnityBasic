using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager
{
    public static Dictionary<string, object> pool = new Dictionary<string, object>();
    public static Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    

    public static void CreatePool<T>(GameObject prefab, Transform parent, int count =  5)
    {
        Queue<T> queue = new Queue<T>();
        for(int i = 0; i < count; ++i)
        {
            GameObject g = GameObject.Instantiate(prefab, parent);
            T t = g.GetComponent<T>();
            g.SetActive(false);

            queue.Enqueue(t);

        }

        string key = typeof(T).ToString();
        pool.Add(key, queue);
        prefabDictionary.Add(key, prefab);
    }

    public static T GetItem<T>() where T : MonoBehaviour
    {
        string key = typeof(T).ToString();
        T item = null;

        if(pool.ContainsKey(key))
        {
            Queue<T> queue = (Queue<T>)pool[key];
            T firstItem = queue.Peek();

            if (firstItem.gameObject.activeSelf)
            {
                GameObject prefab = prefabDictionary[key];
                GameObject g = GameObject.Instantiate(prefab, firstItem.transform.parent);
                item = g.GetComponent<T>();
            }
            else
            {
                item = queue.Dequeue();
                item.gameObject.SetActive(true);
            }
            queue.Enqueue(item);
        }

        
        return item;
    }

}
