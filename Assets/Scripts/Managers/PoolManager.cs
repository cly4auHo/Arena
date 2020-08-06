using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, Queue<Reusable>> poolDictionary = new Dictionary<string, Queue<Reusable>>();

    private void OnObjectDeactivate(Reusable objectToPool)
    {
        string key = objectToPool.Key;
        objectToPool.gameObject.SetActive(false);

        if (poolDictionary.ContainsKey(key))
            poolDictionary[key].Enqueue(objectToPool);
        else
            Debug.LogError("Cannot deactivate that object, it was not created by Pool Manager");
    }

    public Reusable Instantiate(Reusable objectFromPool, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        string key = objectFromPool.Key;
        Reusable poolObject;

        if (poolDictionary.ContainsKey(key))
        {
            if (poolDictionary[key].Count != 0)
                poolObject = poolDictionary[key].Dequeue();
            else
            {
                poolObject = Instantiate(objectFromPool);
                poolObject.Reuse += OnObjectDeactivate;
            }
        }
        else
        {
            Queue<Reusable> objectsPool = new Queue<Reusable>();
            poolDictionary.Add(key, objectsPool);
            poolObject = Instantiate(objectFromPool);
            poolObject.Reuse += OnObjectDeactivate;
        }

        poolObject.gameObject.SetActive(true);
        poolObject.transform.position = position;
        poolObject.transform.rotation = rotation;
        poolObject.transform.parent = parent;

        return poolObject;
    }
}
