using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public static class RaycastUtil
{
    private static RaycastHit2D[] hits = new RaycastHit2D[20];


    /// 레이어 넘버가 5 (UI 레이어) 이상의 홀수인 모든 UI를 대상으로 레이캐스트합니다. 
   public static void RaycastUI (in Vector2 pixelPos, List<GameObject> result)
   {
        var m_PointerEventData = new PointerEventData(EventSystem.current);
        m_PointerEventData.position = pixelPos;
        List<RaycastResult> temp = ListPool<RaycastResult>.Get();
        temp.Clear();
        result.Clear();

        EventSystem.current.RaycastAll(m_PointerEventData, temp);
        
        int count = temp.Count;
        for (int i = 0; i < count; i++)
        {
            if(!temp[i].gameObject.activeSelf) continue;
            int layer = temp[i].gameObject.layer;
            if ( layer == 5 || layer == 7 || layer == 9)
            {
                result.Add(temp[i].gameObject);
            }
        }

        temp.Clear();
        ListPool<RaycastResult>.Release(temp);
   }



    /// 레이어 넘버가 5 (UI 레이어) 이상의 홀수인 모든 Sprite를 대상으로 레이캐스트합니다. 
   public static void RaycastSprite (in Vector2 worldPos, List<GameObject> result)
   {
        result.Clear();
        int count = Physics2D.RaycastNonAlloc(worldPos, Vector2.zero, hits);
        for (int i = 0; i < count; i++)
        {
            if(!hits[i].collider.gameObject.activeSelf) continue;
            int layer = hits[i].collider.gameObject.layer;
            if ( layer == 5 || layer == 7 || layer == 9)
            {
                result.Add(hits[i].collider.gameObject);
            }
        }
   }

}