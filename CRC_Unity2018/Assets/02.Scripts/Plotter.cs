using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour // CSV파일을 파싱해서 유니티상에 Plot을 찍어주는 스크립트.
{
    public AbstractMap abstractMap;
    public int floatDistance;
    public TextAsset[] RCCdata;
    public TextAsset[] WNCdata;
    public TextAsset[] SpeciesCntdata;
    private PlotData[] RCCplotData;
    private PlotData[] WNCplotData;
    private PlotData[] SpeciesCntplotdata;
    public int axisXcolumn;
    public int axisYcolumn;
    public int axisZcolumn;
    public int speciesCntColumn;
    public GameObject RCCprefab;
    public GameObject RCCprefab2;
    public GameObject RCCprefab3;
    public GameObject WNCprefab;
    public GameObject WNCprefab2;
    public GameObject WNCprefab3;
    public Transform parentDirectoryForPlot;

    public event Action OnPlotGenEnd = delegate { };


    void Start()
    {
        LoadData();
    }

    private void LoadData() // Plotter에 등록한 CSV파일들을 CSVReader 스크립트를 사용해 파싱하여 본 프로젝트에서정의한 PlotData형태의 자료구조에 보관
    {
        RCCplotData = new PlotData[RCCdata.Length];
        WNCplotData = new PlotData[WNCdata.Length];
        SpeciesCntplotdata = new PlotData[SpeciesCntdata.Length];

        for (int i = 0; i < RCCdata.Length; i++) // CSV파일은 일단 파싱해서 가지고있는 단계
        {
            RCCplotData[i] = new PlotData();
            RCCplotData[i].PointList = CSVReader.Read(RCCdata[i]);
        }

        for (int i = 0; i < WNCdata.Length; i++)
        {
            WNCplotData[i] = new PlotData();
            WNCplotData[i].PointList = CSVReader.Read(WNCdata[i]);
        }

        for (int i = 0; i < SpeciesCntdata.Length; i++) // CSV파일은 일단 파싱해서 가지고있는 단계
        {
            SpeciesCntplotdata[i] = new PlotData();
            SpeciesCntplotdata[i].PointList = CSVReader.Read(SpeciesCntdata[i]);
        }

        PlotData.AxisXcolumn = axisXcolumn; // CSV파일에서 파싱한 데이터들중 사용할 column들을 지정하는 단계
        PlotData.AxisYcolumn = axisYcolumn;
        PlotData.AxisZcolumn = axisZcolumn;
        PlotData.SpeciesCntColumn = speciesCntColumn;


        List<string> columnList = new List<string>(RCCplotData[0].PointList[0].Keys);
        PlotData.XName = columnList[axisXcolumn];
        PlotData.YName = columnList[axisYcolumn];
        PlotData.ZName = columnList[axisZcolumn];
        PlotData.CntName = columnList[speciesCntColumn];
    }

    public GameObject GeneratePlot() // create 버튼을 누를시 새로운 레이어(plot) 생성
    {
        GameObject PlotGroup = new GameObject();
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

            for (var j = 0; j < RCCplotData[i].PointList.Count; j++) // 위에서 사용하기로 지정한 Column(위도, 경도) 데이터를 가져와 Mapbox API를 사용해 유니티상의 지도 좌표로 변환
            {
                Vector2d worldXZ = Conversions.GeoToWorldPosition((float)RCCplotData[i].PointList[j][PlotData.XName],
                    (float)RCCplotData[i].PointList[j][PlotData.YName], abstractMap.CenterMercator, abstractMap.WorldRelativeScale);
                Vector3 worldPosition = new Vector3((float)worldXZ.x, floatDistance, (float)worldXZ.y);

                if ((int)RCCplotData[i].PointList[j][PlotData.CntName] > 10)
                {
                    GameObject RCCpoint = Instantiate(RCCprefab3, worldPosition, Quaternion.identity);
                    RCCpoint.transform.parent = plotGroup.RCCobj[i].transform;
                    string RCCpointName = RCCplotData[i].PointList[j][PlotData.XName] + " " + RCCplotData[i].PointList[j][PlotData.YName] + " " + RCCplotData[i].PointList[j][PlotData.ZName];
                    RCCpoint.transform.name = RCCpointName;
                }

                else if ((int)RCCplotData[i].PointList[j][PlotData.CntName] > 5)
                {
                    GameObject RCCpoint = Instantiate(RCCprefab2, worldPosition, Quaternion.identity);
                    RCCpoint.transform.parent = plotGroup.RCCobj[i].transform;
                    string RCCpointName = RCCplotData[i].PointList[j][PlotData.XName] + " " + RCCplotData[i].PointList[j][PlotData.YName] + " " + RCCplotData[i].PointList[j][PlotData.ZName];
                    RCCpoint.transform.name = RCCpointName;
                }

                else
                {
                    GameObject RCCpoint = Instantiate(RCCprefab, worldPosition, Quaternion.identity);
                    RCCpoint.transform.parent = plotGroup.RCCobj[i].transform;
                    string RCCpointName = RCCplotData[i].PointList[j][PlotData.XName] + " " + RCCplotData[i].PointList[j][PlotData.YName] + " " + RCCplotData[i].PointList[j][PlotData.ZName];
                    RCCpoint.transform.name = RCCpointName;
                }

                
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

                if ((int)WNCplotData[i].PointList[j][PlotData.CntName] > 10)
                {
                    GameObject WNCpoint = Instantiate(WNCprefab3, worldPosition, Quaternion.identity);
                    WNCpoint.transform.parent = plotGroup.WNCobj[i].transform;
                    string WNCpointName = WNCplotData[i].PointList[j][PlotData.XName] + " " + WNCplotData[i].PointList[j][PlotData.YName] + " " + WNCplotData[i].PointList[j][PlotData.ZName];
                    WNCpoint.transform.name = WNCpointName;
                }

                else if ((int)WNCplotData[i].PointList[j][PlotData.CntName] > 5)
                {
                    GameObject WNCpoint = Instantiate(WNCprefab3, worldPosition, Quaternion.identity);
                    WNCpoint.transform.parent = plotGroup.WNCobj[i].transform;
                    string WNCpointName = WNCplotData[i].PointList[j][PlotData.XName] + " " + WNCplotData[i].PointList[j][PlotData.YName] + " " + WNCplotData[i].PointList[j][PlotData.ZName];
                    WNCpoint.transform.name = WNCpointName;
                }

                else
                {
                    GameObject WNCpoint = Instantiate(WNCprefab, worldPosition, Quaternion.identity);
                    WNCpoint.transform.parent = plotGroup.WNCobj[i].transform;
                    string WNCpointName = WNCplotData[i].PointList[j][PlotData.XName] + " " + WNCplotData[i].PointList[j][PlotData.YName] + " " + WNCplotData[i].PointList[j][PlotData.ZName];
                    WNCpoint.transform.name = WNCpointName;
                }
                
            }
            plotGroup.WNCobj[i].SetActive(false);

            Debug.Log(SpeciesCntdata);
        }
        return PlotGroup;
    }
}