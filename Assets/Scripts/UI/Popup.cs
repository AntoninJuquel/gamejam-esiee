using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI text;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100.0f))
            {
                Open(hit.transform.gameObject);
            }
        }
    }

    private void Open(GameObject go)
    {
        text.text = go.name;
    }

    private void Close()
    {
    }
}