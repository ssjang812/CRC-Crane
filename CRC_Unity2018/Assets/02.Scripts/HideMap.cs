using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMap : MonoBehaviour //Mapbox API 함수 사용시 처음크기의 지도가필요한경우가있어 원본크기의 지도를 하나 그대로 남겨놓고 화면에 보이지않게하기위한 스크립트
{
    private AbstractMap abstractMap;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        abstractMap = GetComponent<AbstractMap>();
        abstractMap.OnTileFinished += turnOffMeshRenderer; //Mapbox API 지도불러오기가 끝나면 turnOffMeshRenderer가 호출됨
    }

    public void turnOffMeshRenderer(UnityTile tile) //meshRenderers에 등록된 모든 오브젝트의 랜더러를 끔
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }
}
