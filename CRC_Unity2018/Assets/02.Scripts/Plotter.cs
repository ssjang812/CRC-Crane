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
    private Plot[] RCCplot;
    private Plot[] WNCplot;
    private GameObject[] RCCobj;
    private GameObject[] WNCobj;
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
        RCCplot = new Plot[RCCdata.Length];
        WNCplot = new Plot[WNCdata.Length];
        RCCobj = new GameObject[RCCdata.Length];
        WNCobj = new GameObject[WNCdata.Length];

        for (int i = 0; i < RCCdata.Length; i++)
        {
            RCCplot[i] = new Plot();
            RCCplot[i].PointList = CSVReader.Read(RCCdata[i]);
        }

        for (int i = 0; i < WNCdata.Length; i++)
        {
            WNCplot[i] = new Plot();
            WNCplot[i].PointList = CSVReader.Read(WNCdata[i]);
        }

        Plot.AxisXcolumn = axisXcolumn;
        Plot.AxisYcolumn = axisYcolumn;
        Plot.AxisZcolumn = axisZcolumn;

        List<string> columnList = new List<string>(RCCplot[0].PointList[0].Keys);
        Plot.XName = columnList[axisXcolumn];
        Plot.YName = columnList[axisYcolumn];
        Plot.ZName = columnList[axisZcolumn];
    }

    public GameObject GeneratePlot()
    {
        GameObject PlotGroup = new GameObject();
        PlotGroup.transform.parent = parentDirectoryForPlot;

        for (int i = 0; i < RCCplot.Length; i++)
        {
            RCCobj[i] = new GameObject();
            RCCobj[i].name = $"RCC Plot {i}";
            RCCobj[i].transform.parent = PlotGroup.transform;

            for (var j = 0; j < RCCplot[i].PointList.Count; j++)
            {
                Vector2d worldXZ = Conversions.GeoToWorldPosition((float)RCCplot[i].PointList[j][Plot.XName],
                    (float)RCCplot[i].PointList[j][Plot.YName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
                Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

                GameObject RCCpoint = Instantiate(RCCprefab, worldPosition, Quaternion.identity);
                RCCpoint.transform.parent = RCCobj[i].transform;
                string RCCpointName = RCCplot[i].PointList[j][Plot.XName] + " " + RCCplot[i].PointList[j][Plot.YName] + " " + RCCplot[i].PointList[j][Plot.ZName];
                RCCpoint.transform.name = RCCpointName;
            }
            //RCCobj[i].SetActive(false);
        }

        for (int i = 0; i < WNCplot.Length; i++)
        {
            WNCobj[i] = new GameObject();
            WNCobj[i].name = $"WNC Plot {i}";
            WNCobj[i].transform.parent = PlotGroup.transform;

            for (var j = 0; j < WNCplot[i].PointList.Count; j++)
            {
                Vector2d worldXZ = Conversions.GeoToWorldPosition((float)WNCplot[i].PointList[j][Plot.XName],
                    (float)WNCplot[i].PointList[j][Plot.YName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
                Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

                GameObject WNCpoint = Instantiate(WNCprefab, worldPosition, Quaternion.identity);
                WNCpoint.transform.parent = WNCobj[i].transform;
                string WNCpointName = WNCplot[i].PointList[j][Plot.XName] + " " + WNCplot[i].PointList[j][Plot.YName] + " " + WNCplot[i].PointList[j][Plot.ZName];
                WNCpoint.transform.name = WNCpointName;
            }
            WNCobj[i].SetActive(false);
        }
        return PlotGroup;
    }
}