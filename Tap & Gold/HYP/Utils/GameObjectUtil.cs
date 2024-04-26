using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil
{
	public static GameObject GetRootGameObject(GameObject gameObject)
	{
		Transform root = gameObject.GetComponent<Transform>();
		while(root.parent != null) { root = root.parent; }
		return root.gameObject;
	}

	public static T GetRootComponent<T>(GameObject gameObject) where T : Component
	{
		return GetRootGameObject(gameObject).GetComponent<T>();
	}

	/// 자식 오브젝트중 타입 T 를 가지는 모든 컴포넌트를 반환합니다. (자신과 자식 오브젝트의 자식 오브젝트는 검색하지 않습니다.)
	public static T[] GetChildrenComponents<T>(GameObject gameObject) where T : Component
	{
		List<T> components = new List<T>();
		foreach(Transform child in gameObject.GetComponent<Transform>())
		{
			T component = child.GetComponent<T>();
			if(component != null) { components.Add(component); }
		}
		return components.ToArray();
	}


	public static string GetHierarchyName(GameObject gameObject)
	{
		var sb = new System.Text.StringBuilder();
		sb.Append(gameObject.name);
		Transform parent = gameObject.GetComponent<Transform>().parent;
		while(parent != null)
		{
			sb.Insert(0, "/");
			sb.Insert(0, parent.name);
			parent = parent.parent;
		}
		return sb.ToString();
	}


	/// 이 메소드는 유니티 API의 GetComponentInParent 와 동일합니다.
	/// 자신을 시작으로 위로 올라가며 부모 오브젝트를 탐색하며, 타입 T 를 가지는 컴포넌트를 반환합니다.
	/// 이 메소드는 자신의 컴포넌트 역시 검색합니다.
	public static T GetComponentInParent<T>(GameObject gameObject, bool includeInactive) where T : Component
	{
		return gameObject.GetComponentInParent<T>(includeInactive);
	}

	/// 이 메소드는 유니티 API의 GetComponentInChildren 와 동일합니다.
	/// 자신을 시작으로 아래로 내려가며 자식 오브젝트를 탐색하며, 타입 T 를 가지는 컴포넌트를 반환합니다.
	/// 이 메소드는 자신의 컴포넌트 역시 검색합니다.
	public static T GetComponentInChildren<T>(GameObject gameObject, bool includeInactive) where T : Component
	{
		return gameObject.GetComponentInChildren<T>(includeInactive);
	}
}
