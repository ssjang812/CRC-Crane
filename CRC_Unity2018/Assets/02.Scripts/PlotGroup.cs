using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotGroup : MonoBehaviour
{
    public GameObject[] RCCobj;
    public GameObject[] WNCobj;

    public void instantiateArray(int rccLength, int wncLength)
    {
        RCCobj = new GameObject[rccLength];
        WNCobj = new GameObject[wncLength];
    }
}
