using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Vector2 hidePos;
    [SerializeField] private Vector2 showPos;
    [SerializeField] private Button restartButton;
    [SerializeField] private Volume  ppProfile;
    [SerializeField] private DepthOfField _depthOfField;
    private Coroutine _coroutine;

    private void Awake()
    {
        if (ppProfile.profile.TryGet<DepthOfField>(out var deapth))
        {
            _depthOfField = deapth;
            //deapth.focusDistance = new MinFloatParameter(Single.MinValue, 0);
        }
    }

    private void Start()
    {
        _depthOfField.focusDistance = new MinFloatParameter(10, 0, true);
    }

    public void ShowHideMenu(bool showhide)
    {
        if (_coroutine != null)
        {
            _coroutine = null;
        }
        switch (showhide)
        {
            case true:
                Vector2 pos1 = gameObject.transform.position;
                pos1.y = showPos.y;
                _coroutine = StartCoroutine(ChangePos(gameObject, pos1, 1));
                break;
            case false:
                Vector2 pos2 = gameObject.transform.position;
                pos2.y = hidePos.y;
                _coroutine = StartCoroutine(ChangePos(gameObject, pos2, 1));
                break;
        }
    }

    public IEnumerator ChangePos(GameObject obj, Vector2 end, float speed)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / speed;
            obj.transform.position = Vector2.Lerp(obj.transform.position, end, t);
            yield return 0;
        }

        obj.transform.position = end;
    }
}
