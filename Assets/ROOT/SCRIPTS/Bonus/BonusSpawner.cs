using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusPool;
    
    private Slider _slider;
    public float currentValue;
    [SerializeField] private int scoreForBonus;
    private Coroutine _coroutine;
    [SerializeField] private float radius = 10;
    private Transform _planet;
    

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _planet = FindObjectOfType<Planet>().transform;
    }

    private void UpdateBonus()
    {
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Utils.ChangeFloat(_slider.value, currentValue, 0.2f, f => _slider.value = f, () => 
            { 
                if (currentValue >= _slider.maxValue)
                {
                    SpawnBonus();
                }
            }));
    }

    public void AddBonus()
    {
        float oneShot = 1f / scoreForBonus;
        currentValue += oneShot;
        UpdateBonus();
    }

    private void SpawnBonus()
    {
        DiscardBonus();
        currentValue = 0;
        var bonus = PoolManager.GetObject(bonusPool[Random.Range(0, bonusPool.Count)].gameObject.name, transform.position, Quaternion.identity);
        Vector3 random = new Vector3(Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad), Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad), 0);
        Vector3 randomPosition = _planet.position + random;
        var dir = (_planet.position - randomPosition).normalized;
        Vector3 pos = _planet.position + (dir * radius);
        bonus.transform.position = _planet.position;
        StartCoroutine(Utils.MoveToPos(bonus.transform, pos, 1f));
    }

    public void DiscardBonus()
    {
        currentValue = 0;
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Utils.ChangeFloat(_slider.value, 0, 0.2f, f => _slider.value = f, () => { }));
    }
    
    // public IEnumerator ChangeFloat(float a, float b, float duration, System.Action<float> CallBack, Action action)
    // {
    //     float t = 0;
    //     float value = a;
    //
    //     while (t < 1)
    //     {
    //         value = Mathf.Lerp(a, b, t);
    //         t += Time.deltaTime / duration;
    //         CallBack(value);
    //         yield return null;
    //     }
    //     CallBack(b);
    //     action.Invoke();
    //     a = b;
    // }
    // public IEnumerator MoveToPos(Transform obj, Vector3 pos, float animationDuration)
    // {
    //     Vector3 startPosition = obj.position;
    //     float t = 0;
    //
    //     while (t < 1)
    //     {
    //         obj.position = Vector3.Lerp(startPosition, pos, t * t);
    //         t += Time.deltaTime / animationDuration;
    //         yield return null;
    //     }
    //     obj.position = pos;
    // }
}
