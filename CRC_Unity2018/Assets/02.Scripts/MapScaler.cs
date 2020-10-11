using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScaler : MonoBehaviour
{
    private AbstractMap abstractMap;
    private void Awake()
    {
        abstractMap = GetComponent<AbstractMap>();
        abstractMap.OnInitialized += MapScale;
    }

    private void MapScale()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.position = new Vector3(0f, -0.5f, 1.0f);
    }

    public static void Scale(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        gameObject.transform.position = new Vector3(0f, -0.5f, 1.0f);
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
