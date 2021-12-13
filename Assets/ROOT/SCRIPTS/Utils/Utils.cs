using System;
using System.Collections;
using UnityEngine;

public class Utils
{
    public static IEnumerator ChangeFloat(float a, float b, float duration, Action<float> CallBack, Action action)
    {
        float t = 0;
        float value = a;

        while (t < 1)
        {
            value = Mathf.Lerp(a, b, t);
            t += Time.deltaTime / duration;
            CallBack(value);
            yield return null;
        }
        CallBack(b);
        action.Invoke();
        a = b;
    }
    public static IEnumerator MoveToPos(Transform obj, Vector3 pos, float animationDuration)
    {
        Vector3 startPosition = obj.position;
        float t = 0;

        while (t < 1)
        {
            obj.position = Vector3.Lerp(startPosition, pos, t * t);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
        obj.position = pos;
    }
}
