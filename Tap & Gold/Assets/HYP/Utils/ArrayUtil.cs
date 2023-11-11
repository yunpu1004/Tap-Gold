using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using Random = System.Random;

public static class ArrayUtil {

    public static string ArgsToString<T>(T[] array)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[ ");
        for(int i = 0; i < array.Length; i++)
        {
            sb.Append(array.ElementAt(i).ToString());
            if(i < array.Length - 1)
            {
                sb.Append(", ");
            }
        }
        sb.Append(" ]");
        return sb.ToString();
    }

    public static void Shuffle<T>(T[] array) {
        int n = array.Length;
        Random random = new Random();

        for(int i = 0; i < n; i++)
        {
            int r = i + (int)(random.NextDouble() * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    public static T GetRandom<T>(T[] array)
    {
        Random random = new Random();
        int rand = random.Next(0, array.Length);
        return array[rand];
    }

    public static void ForEach<T>(T[] array, Action<T> action)
    {
        foreach(var item in array)
        {
            action(item);
        }
    }

    public static (T value, int idx) Min<T>(T[] array) where T : IComparable<T>
    {
        T value = array[0];
        int idx = 0;
        for(int i = 1; i < array.Length; i++)
        {
            if(array[i].CompareTo(value) < 0)
            {
                value = array[i];
                idx = i;
            }
        }
        return (value, idx);
    }

    public static (T value, int idx) Max<T>(T[] array) where T : IComparable<T>
    {
        T value = array[0];
        int idx = 0;
        for(int i = 1; i < array.Length; i++)
        {
            if(array[i].CompareTo(value) > 0)
            {
                value = array[i];
                idx = i;
            }
        }
        return (value, idx);
    }

    public static (bool contains, int idx) Contains<T>(T[] array, T target)
    {
        if(array.Length == 0) throw new ArgumentException("Array is empty");
        int len = array.Length;
        for(int i = 0; i < len; i++)
        {
            if(array[i].Equals(target))
            {
                return (true, i);
            }
        }
        return (false, -1);
    }

    public static bool ContainsAll<T>(T[] array, T[] targets)
    {
        if(array.Length == 0) throw new ArgumentException("Array is empty");
        if(targets.Length == 0) throw new ArgumentException("targets array is empty");
        int targetLen = targets.Length;
        for(int i = 0; i < targetLen; i++)
        {
            if(!Contains(array, targets[i]).contains)
            {
                return false;
            }
        }
        return true;
    }

    public static bool ContainsAny<T>(T[] array, T[] targets)
    {
        if(array.Length == 0) throw new ArgumentException("Array is empty");
        if(targets.Length == 0) throw new ArgumentException("targets array is empty");
        int targetLen = targets.Length;
        for(int i = 0; i < targetLen; i++)
        {
            if(Contains(array, targets[i]).contains)
            {
                return true;
            }
        }
        return false;
    }

    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        Array.Sort(array);
    }

    public static void Sort<T,U>(T[] array, Func<T, U> func) where U : IComparable<U>
    {
        Array.Sort(array, (a, b) => func(a).CompareTo(func(b)));
    }

    public static Q[] Select<T,Q>(T[] array, Func<T, Q> func)
    {
        int len = array.Length;
        Q[] result = new Q[len];
        for(int i = 0; i < len; i++)
        {
            result[i] = func(array[i]);
        }
        return result;
    }
    
    public static T[] Where<T>(T[] array, Func<T, bool> func)
    {
        int len = array.Length;
        int count = 0;
        Span<int> indexes = stackalloc int[len];
        for(int i = 0; i < len; i++)
        {
            if(func(array[i]))
            {
                indexes[count] = i;
                count++;
            }
        }

        T[] result = new T[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = array[indexes[i]];
        }
        return result;
    }

    public static T[] Union<T>(params T[][] arrays) where T : IEquatable<T>
    {
        HashSet<T> set = new HashSet<T>();
        int arrayCount = arrays.Length;
        for(int i = 0; i < arrayCount; i++)
        {
            int len = arrays[i].Length;
            for(int j = 0; j < len; j++)
            {
                set.Add(arrays[i][j]);
            }
        }
        return set.ToArray();
    }

    public static T[] Intersect<T>(params T[][] arrays) where T : IEquatable<T>
    {
        List<T> result = new List<T>();
        int arrayCount = arrays.Length;
        int minArrayLen = arrays[0].Length;
        int minArrayIdx = 0;

        for(int i = 1; i < arrayCount; i++)
        {
            if(arrays[i].Length < minArrayLen)
            {
                minArrayLen = arrays[i].Length;
                minArrayIdx = i;
            }
        }

        T[] minArray = arrays[minArrayIdx];
        for(int i = 0; i < minArrayLen; i++)
        {
            bool contains = true;
            for(int j = 0; j < arrayCount; j++)
            {
                if(j == minArrayIdx) continue;
                if(!Contains(arrays[j], minArray[i]).contains)
                {
                    contains = false;
                    break;
                }
            }
            if(contains)result.Add(minArray[i]);
        }

        return result.ToArray();
    }
    
    public static T[] Distinct<T>(params T[][] arrays) where T : IEquatable<T>
    {
        HashSet<T> set = new HashSet<T>();
        int len = arrays.Length;
        for(int i = 0; i < len; i++)
        {
            int innerLen = arrays[i].Length;
            for(int j = 0; j < innerLen; j++)
            {
                set.Add(arrays[i][j]);
            }
        }
        return set.ToArray();
    }

    public static T[] Concat<T>(params T[][] arrays)
    {
        int len = arrays.Length;
        int count = 0;
        for(int i = 0; i < len; i++)
        {
            count += arrays[i].Length;
        }

        T[] result = new T[count];
        int idx = 0;
        for(int i = 0; i < len; i++)
        {
            int innerLen = arrays[i].Length;
            for(int j = 0; j < innerLen; j++)
            {
                result[idx] = arrays[i][j];
                idx++;
            }
        }
        return result;
    }

    public static T[] Except<T>(T[] array, T[] targets) where T : IEquatable<T>
    {
        int len = array.Length;
        int targetLen = targets.Length;
        int count = 0;
        Span<int> indexes = stackalloc int[len];
        for(int i = 0; i < len; i++)
        {
            bool contains = false;
            for(int j = 0; j < targetLen; j++)
            {
                if(array[i].Equals(targets[j]))
                {
                    contains = true;
                    break;
                }
            }
            if(!contains)
            {
                indexes[count] = i;
                count++;
            }
        }

        T[] result = new T[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = array[indexes[i]];
        }
        return result;
    }

    public static float[] RangeFromStartToEnd(float start, float end, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float interval = (end - start) / (count - 1);
        float[] result = new float[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }

    public static float2[] RangeFromStartToEnd(float2 start, float2 end, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float2 interval = (end - start) / (count - 1);
        float2[] result = new float2[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }

    public static float3[] RangeFromStartToEnd(float3 start, float3 end, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float3 interval = (end - start) / (count - 1);
        float3[] result = new float3[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }

    public static float[] RangeWithCenterAndInterval(float center, float interval, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float start = center - (count - 1) * interval / 2;
        float[] result = new float[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }   

    public static float2[] RangeWithCenterAndInterval(float2 center, float2 interval, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float2 start = center - (count - 1) * interval / 2;
        float2[] result = new float2[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }

    public static float3[] RangeWithCenterAndInterval(float3 center, float3 interval, int count)
    {
        if(count <= 1) throw new ArgumentException("Count must be larger than 1");
        
        float3 start = center - (count - 1) * interval / 2;
        float3[] result = new float3[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = start + i * interval;
        }

        return result;
    }
}