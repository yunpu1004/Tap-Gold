using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T> 
{
    [SerializeField] public List<T> list = new List<T>();
    public int Count => list.Count;
    public T this[int index] => list[index];
}
