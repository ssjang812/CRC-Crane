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
        abstractMap.OnInitialized += MapScale; // Mapbox API 를통해 지도가 다 그려지면 MapScale함수를 호출하도록 등록
        scale = Vector3.one * 0.01f; // 기존지도크기의 0.01 scale로 축소할 것이라 명시
    }

    private void MapScale()
    {
        transform.localScale = scale; // Scale은 위에 등록한 0.01 Scale로 축소 (원하는 값으로 조절 가능)
        transform.position = sceneOrigin.position; // Position은 원래 위치 그대로
    }

    public void Scale(GameObject gameObject) // 특정 오브젝트를 임의로 parameter로 받아서 그 오브젝트의 크기를 조절하는 함수
    {
        gameObject.transform.localScale = scale;
        gameObject.transform.position = sceneOrigin.position;
    }

    public static void MapUp(GameObject gameObject) // 특정 오브젝트를 parameter로 받아서 유니티 좌표계에서 위쪽으로 이동시키기 (+y축 방향), plot을 위로 움직일때 사용됨
    {
        gameObject.transform.position += Vector3.up * 0.15f;
    }

    public static void MapDown(GameObject gameObject) // 특정 오브젝트를 parameter로 받아서 유니티 좌표계에서 아래쪽으로 이동시키기 (-y축 방향), plot을 아래로 움직일때 사용됨
    {
        gameObject.transform.position += Vector3.down * 0.05f;
    }
}
