﻿using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour
{
    public TextAsset data;
    public List<Dictionary<string, object>> pointList;
    public int axisXcolumn;
    public int axisYcolumn;
    public int axisZcolumn;
    public string xName;
    public string yName;
    public string zName;
    public GameObject pointPrefab;
    public GameObject pointHolder;
    public GameObject map;
    AbstractMap abstractMap;
    public int floatDistance = 10;

    void Start()
    {
        Debug.Log(data);
        pointList = CSVReader.Read(data);
        List<string> columnList = new List<string>(pointList[0].Keys);
        abstractMap = map.GetComponent<AbstractMap>();
        //   foreach (string key in columnList)
        //       Debug.Log("Column name is " + key);

        xName = columnList[axisXcolumn];
        yName = columnList[axisYcolumn];
        zName = columnList[axisZcolumn];

        abstractMap.OnInitialized += _map_OnInitialized;
        _map_OnInitialized();
    }


    void Update()
    {
        
    }

    void _map_OnInitialized()
    {
        Debug.Log("_map_OnInitialized");
        for (var i = 0; i < pointList.Count; i++)
        {
            //Vector2d latLong = new Vector2d((float)pointList[i][xName], (float)pointList[i][yName]);
            //Debug.Log(Conversions.GeoToWorldPosition(latLong, abstractMap.CenterMercator, abstractMap.WorldRelativeScale).ToVector3xz());

            Debug.Log((float)pointList[i][xName] + " " + (float)pointList[i][yName]);
            Vector2d worldXZ = Conversions.GeoToWorldPosition((float)pointList[i][xName], (float)pointList[i][yName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
            Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

            GameObject dataPoint = Instantiate(pointPrefab, worldPosition, Quaternion.identity);
            dataPoint.transform.parent = pointHolder.transform;
            string dataPointName = pointList[i][xName] + " " + pointList[i][yName] + " " + pointList[i][zName];
            dataPoint.transform.name = dataPointName;
        }
    }
}
