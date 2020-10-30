using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotData : MonoBehaviour
{
    private List<Dictionary<string, object>> pointList;
    private static int axisXcolumn;
    private static int axisYcolumn;
    private static int axisZcolumn;
    private static string xName;
    private static string yName;
    private static string zName;

    public List<Dictionary<string, object>> PointList { get => pointList; set => pointList = value; }
    public static int AxisXcolumn { get => axisXcolumn; set => axisXcolumn = value; }
    public static int AxisYcolumn { get => axisYcolumn; set => axisYcolumn = value; }
    public static int AxisZcolumn { get => axisZcolumn; set => axisZcolumn = value; }
    public static string XName { get => xName; set => xName = value; }
    public static string YName { get => yName; set => yName = value; }
    public static string ZName { get => zName; set => zName = value; }
}
