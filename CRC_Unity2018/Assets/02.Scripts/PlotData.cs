using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotData
{
    private List<Dictionary<string, object>> pointList; // csv파일의 모든 데이터를 파싱해서 일단 가지고있는 데이터
    private static int axisXcolumn; // axisX~Z는 데이터중 사용할 항목들을 빼두기위해 만든 변수들 - 위도, 경도, 종 데이터를 각각이 의미 (다른 항목을 꺼내쓰려면 변수추가필요)
    private static int axisYcolumn; // 이 변수들이 유니티씬의 Plotter와 Plotter스크립트에서 어떤식을 쓰이는지 보면 다른 변수도 같은방식으로 활용 가능
    private static int axisZcolumn; //ß 위의 설명대로 코드를보기전에 CSVReader 에서 어떤형태로 파싱해서 '헤더값'에 따라 'Dictionary'들을 만들고 또 이를 모아 'List'로 만들었는지 이해필요
    private static int speciesCntColumn; // 두루미 개체수 
    private static string xName;
    private static string yName;
    private static string zName;
    private static string cntName;

    public List<Dictionary<string, object>> PointList { get => pointList; set => pointList = value; }
    public static int AxisXcolumn { get => axisXcolumn; set => axisXcolumn = value; }
    public static int AxisYcolumn { get => axisYcolumn; set => axisYcolumn = value; }
    public static int AxisZcolumn { get => axisZcolumn; set => axisZcolumn = value; }
    public static int SpeciesCntColumn { get => speciesCntColumn; set => speciesCntColumn = value; }
    public static string XName { get => xName; set => xName = value; }
    public static string YName { get => yName; set => yName = value; }
    public static string ZName { get => zName; set => zName = value; }
    public static string CntName { get => cntName; set => cntName = value; }
}
