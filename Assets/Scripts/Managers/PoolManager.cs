using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<int, GameObject> poolDictionary;

    #region Singleton

    private static PoolManager instance;

    public PoolManager Instance => instance;

    private PoolManager() { }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<int, GameObject>();
    }


}
