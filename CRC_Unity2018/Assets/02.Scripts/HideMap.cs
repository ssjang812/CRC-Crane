using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMap : MonoBehaviour
{
    private AbstractMap abstractMap;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        abstractMap = GetComponent<AbstractMap>();
        abstractMap.OnTileFinished += turnOffMeshRenderer;
    }

    public void turnOffMeshRenderer(UnityTile tile)
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }
}
