using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject map;
    AbstractMap abstractMap;
    public int floatDistance = 10;

    void Start()
    {
        abstractMap = map.GetComponent<AbstractMap>();
        Vector2d worldXZ = Conversions.GeoToWorldPosition(38.273627, 127.313927, abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
        Debug.Log(worldXZ);
        Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);
        GameObject dataPoint = Instantiate(pointPrefab, worldPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
