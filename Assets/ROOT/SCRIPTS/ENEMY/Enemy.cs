using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private PoolObject _poolObject;
    private Animator _animator;
    private bool alive;
    private Outline[] _outlines;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _poolObject = GetComponent<PoolObject>();
        _outlines = GetComponentsInChildren<Outline>();
    }

    private void Start()
    {
        _target = FindObjectOfType<Planet>().gameObject.transform;
        _speed = Random.Range(1, 6);
    }

    private void OnEnable()
    {
        AnimationState(false);
        alive = true;
        ChangeOutline(Color.white, 6f);
    }

    private void Update()
    {
        if (LevelProgress.pause)
        {
            BackToPool();
        }

        transform.LookAt(_target.transform);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void Die()
    {
        if (!alive) return;
        AnimationState(true);
        alive = false;
        ChangeOutline(Color.red, 0f);
    }

    private void ChangeOutline(Color color, float outlineWidth)
    {
        foreach (var item in _outlines)
        {
            item.OutlineColor = color;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            if(gameObject.activeInHierarchy)
                _coroutine = StartCoroutine(Utils.ChangeFloat(item.OutlineWidth, outlineWidth, 2, f => {item.OutlineWidth = f;}, () => { }));
        }
    }

    private void AnimationState(bool state)
    {
        _animator.SetBool("status", state);
    }

    private void BackToPool()
    {
        _poolObject.ReturnToPool();
    }

    private void GiveDamage(GameObject obj)
    {
        if (alive)
        {
            LevelProgress.gameScore--;
            obj.GetComponent<Planet>().getDamage.Invoke();
        }
        BackToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            GiveDamage(collision.gameObject);
        }
    }
}