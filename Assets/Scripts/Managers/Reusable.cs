using System;
using UnityEngine;

public abstract class Reusable : MonoBehaviour
{
    public Action<Reusable> Reuse;
    [SerializeField] protected string key;

    public string Key => key;
}
