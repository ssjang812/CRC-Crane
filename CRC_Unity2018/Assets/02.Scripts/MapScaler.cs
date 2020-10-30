using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScaler : MonoBehaviour
{
    public Transform sceneOrigin;
    private AbstractMap abstractMap;
    private Vector3 scale;

    private void Awake()
    {
        abstractMap = GetComponent<AbstractMap>();
        abstractMap.OnInitialized += MapScale;
        scale = Vector3.one * 0.01f;
    }

    private void MapScale()
    {
        transform.localScale = scale;
        transform.position = sceneOrigin.position;
    }

    public void Scale(GameObject gameObject)
    {
        gameObject.transform.localScale = scale;
        gameObject.transform.position = sceneOrigin.position;
    }

    public static void MapUp(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.up * 0.05f;
    }

    public static void MapDown(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.down * 0.05f;
    }
}
