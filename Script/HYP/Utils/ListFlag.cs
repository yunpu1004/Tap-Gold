using System.Collections;
using System.Collections.Generic;

public class ListFlag<T> : IEnumerable<T>
{
    private List<T> list;
    public bool isChanged;
    public int Count => list.Count;

    public T this[int index]
    {
        get => list[index];
        set
        {
            if(list[index].Equals(value)) return;
            list[index] = value;
            isChanged = true;
        }
    }
    
    public ListFlag()
    {
        list = new List<T>();
        isChanged = false;
    }

    public void Add(T item)
    {
        list.Add(item);
        isChanged = true;
    }

    public void Remove(T item)
    {
        bool removed = list.Remove(item);
        if(removed) isChanged = true;
    }

    public void RemoveAt(int index)
    {
        if(index < 0 || index >= list.Count) throw new System.IndexOutOfRangeException();
        list.RemoveAt(index);
        isChanged = true;
    }

    public void Clear()
    {
        if(list.Count == 0) return;
        list.Clear();
        isChanged = true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}