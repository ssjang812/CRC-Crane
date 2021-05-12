using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGroup : MonoBehaviour // 생성한 Layer들을(plot 들을), 보관하는 객체
{
    public GameObject[] RCCobj;
    public GameObject[] WNCobj;

    public void instantiateArray(int rccLength, int wncLength)
    {
        RCCobj = new GameObject[rccLength];
        WNCobj = new GameObject[wncLength];
    }
}
