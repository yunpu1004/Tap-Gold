using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class Test_Execute : MonoBehaviour
{
    public RectTransform rectTransform;

    public void Test()
    {
        
    }

    private void Update() 
    {
        // var localAnchorMinMax = RectTransformUtil.GetLocalAnchorMinMax(rectTransform, rectTransform.parent.GetComponent<RectTransform>());
        // var worldAnchorMinMax = RectTransformUtil.GetWorldAnchorMinMax(rectTransform);

        // Debug.Log($"localAnchorMinMax: {localAnchorMinMax}");
        // Debug.Log($"worldAnchorMinMax: {worldAnchorMinMax}");
    }

    private void WaitForFiveSeconds()
    {
        StartCoroutine(WaitForFiveSecondsCoroutine());

        IEnumerator WaitForFiveSecondsCoroutine()
        {
            yield return new WaitForSeconds(5f);
        }
    }


}

