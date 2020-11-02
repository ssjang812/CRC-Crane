using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{
    public AbstractMap abstractMap;
    public int floatDistance;
    public TextAsset[] RCCdata;
    public TextAsset[] WNCdata;
    private PlotData[] RCCplotData;
    private PlotData[] WNCplotData;
    public int axisXcolumn;
    public int axisYcolumn;
    public int axisZcolumn;
    public GameObject RCCprefab;
    public GameObject WNCprefab;
    public Transform parentDirectoryForPlot;

    public event Action OnPlotGenEnd = delegate { };


    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        RCCplotData = new PlotData[RCCdata.Length];
        WNCplotData = new PlotData[WNCdata.Length];

        for (int i = 0; i < RCCdata.Length; i++)
        {
            RCCplotData[i] = new PlotData();
            RCCplotData[i].PointList = CSVReader.Read(RCCdata[i]);
        }

        for (int i = 0; i < WNCdata.Length; i++)
        {
            WNCplotData[i] = new PlotData();
            WNCplotData[i].PointList = CSVReader.Read(WNCdata[i]);
        }

        PlotData.AxisXcolumn = axisXcolumn;
        PlotData.AxisYcolumn = axisYcolumn;
        PlotData.AxisZcolumn = axisZcolumn;

        List<string> columnList = new List<string>(RCCplotData[0].PointList[0].Keys);
        PlotData.XName = columnList[axisXcolumn];
        PlotData.YName = columnList[axisYcolumn];
        PlotData.ZName = columnList[axisZcolumn];
    }

    public GameObject GeneratePlot() // Plot은 데이터 로드할때 한번에 가져와서 배열에 가지고있는게 편하지만, 각각 맵 세트로 만들어서 내보낼때는 궂이 배열로 가지고 있을필요가 없을것 같다.
    {
        GameObject PlotGroup = new GameObject(); // 객체에 달려서 플롯 각각에 대한 접근 정보를 가지고있는 스크립트도 붙여서 돌려줘야함
        PlotGroup plotGroup;
        PlotGroup.AddComponent<PlotGroup>();
        plotGroup = PlotGroup.GetComponent<PlotGroup>();

        PlotGroup.transform.parent = parentDirectoryForPlot;
        plotGroup.instantiateArray(RCCdata.Length, WNCdata.Length);

        for (int i = 0; i < RCCplotData.Length; i++)
        {
            plotGroup.RCCobj[i] = new GameObject();
            plotGroup.RCCobj[i].name = $"RCC Plot {i}";
            plotGroup.RCCobj[i].transform.parent = PlotGroup.transform;

            for (var j = 0; j < RCCplotData[i].PointList.Count; j++)
            {
                Vector2d worldXZ = Conversions.GeoToWorldPosition((float)RCCplotData[i].PointList[j][PlotData.XName],
                    (float)RCCplotData[i].PointList[j][PlotData.YName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
                Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

                GameObject RCCpoint = Instantiate(RCCprefab, worldPosition, Quaternion.identity);
                RCCpoint.transform.parent = plotGroup.RCCobj[i].transform;
                string RCCpointName = RCCplotData[i].PointList[j][PlotData.XName] + " " + RCCplotData[i].PointList[j][PlotData.YName] + " " + RCCplotData[i].PointList[j][PlotData.ZName];
                RCCpoint.transform.name = RCCpointName;
            }
            plotGroup.RCCobj[i].SetActive(false);
        }

        for (int i = 0; i < WNCplotData.Length; i++)
        {
            plotGroup.WNCobj[i] = new GameObject();
            plotGroup.WNCobj[i].name = $"WNC Plot {i}";
            plotGroup.WNCobj[i].transform.parent = PlotGroup.transform;

            for (var j = 0; j < WNCplotData[i].PointList.Count; j++)
            {
                Vector2d worldXZ = Conversions.GeoToWorldPosition((float)WNCplotData[i].PointList[j][PlotData.XName],
                    (float)WNCplotData[i].PointList[j][PlotData.YName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
                Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

                GameObject WNCpoint = Instantiate(WNCprefab, worldPosition, Quaternion.identity);
                WNCpoint.transform.parent = plotGroup.WNCobj[i].transform;
                string WNCpointName = WNCplotData[i].PointList[j][PlotData.XName] + " " + WNCplotData[i].PointList[j][PlotData.YName] + " " + WNCplotData[i].PointList[j][PlotData.ZName];
                WNCpoint.transform.name = WNCpointName;
            }
            plotGroup.WNCobj[i].SetActive(false);
        }
        return PlotGroup;
    }
}