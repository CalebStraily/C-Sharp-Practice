using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamScaleLerp : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LerpSlamScale(60.0f));
    }

    IEnumerator LerpSlamScale(float lerpTime)
    {
        var originalScale = transform.localScale;
        var targetScale = originalScale + new Vector3(20f, 20f, 20f);
        var originalTime = lerpTime;

        while (lerpTime > 0.0f)
        {
            lerpTime -= Time.deltaTime;
            
            transform.localScale = Vector3.Lerp(targetScale, originalScale, lerpTime / originalTime);
            yield break;
        }
    }
}


