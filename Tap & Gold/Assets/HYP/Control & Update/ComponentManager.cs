using System;
using System.Collections.Generic;
using UnityEngine;


/// ComponentManager는 병렬 계산에 사용되는 모든 컴포넌트 데이터를 보관합니다.
public class ComponentManager
{
    private static Dictionary<string, Dictionary<Type, Component>> componentDict;

    public static void Init()
    {
        componentDict = new Dictionary<string, Dictionary<Type, Component>>();
    }

    public static void AddComponent<T>(string hierarchyName, T component) where T : Component
    {
        if(!componentDict.ContainsKey(hierarchyName))
        {
            componentDict.Add(hierarchyName, new Dictionary<Type, Component>());
        }
        componentDict[hierarchyName].Add(typeof(T), component);
    }


    public static T GetComponent<T>(string hierarchyName) where T : Component
    {
        if(!componentDict.ContainsKey(hierarchyName)) return null;
        if(!componentDict[hierarchyName].ContainsKey(typeof(T))) return null;
        return (T)componentDict[hierarchyName][typeof(T)];
    }

    
    /// 슬래시 기호로 표현된 자식 객체의 이름을 사용해서 부모 객체의 이름을 찾습니다.
    /// 예를 들어서 자식 객체의 이름이 ParentName/ChildName 일때, ParentName 을 리턴합니다.
    /// 어디까지나 문자열만을 확인하므로 실제 오브젝트가 존재하는지 확인하지는 않습니다.
    public static string GetParentName(string name)
    {
        string parentName = "";

        int len = name.Length;

        int i = len - 1;
        while (i >= 0 && name[i] != '/')
        {
            i--;
        }

        parentName = name.Substring(0, i);

        return parentName;
    }


    /// 슬래시 기호로 표현된 부모 객체의 이름을 사용해서 모든 자식 객체의 이름을 찾습니다.
    /// 예를 들어서 자식 객체의 이름이 ParentName 일때, 모든 ParentName/ChildName 을 리턴합니다.
    /// 이 메소드는 내부 딕셔너리에 접근하여 실제 자식 오브젝트가 존재하는지 확인합니다. 
    public static string[] GetContainedNames(string name)
    {
        int len = name.Length;
        var list = new List<string>();

        foreach (var key in componentDict.Keys)
        {
            if(key.Length <= len) continue;
            for (int i = 0; i < len; i++)
            {
                if(key[i] != name[i])
                {
                    break;
                }

                if(i == len - 1 && key.Length > len)
                {
                    list.Add(key);
                }
            }
        }

        return list.ToArray();
    }



    public static T[] GetComponents<T> () where T : Component
    {
        var list = new List<T>();

        foreach (var key in componentDict.Keys)
        {
            if(componentDict[key].ContainsKey(typeof(T)))
            {
                list.Add((T)componentDict[key][typeof(T)]);
            }
        }

        return list.ToArray();
    }


    public static bool HasComponent<T>(string hierarchyName) where T : Component
    {
        return componentDict.ContainsKey(hierarchyName) && componentDict[hierarchyName].ContainsKey(typeof(T));
    }
}
