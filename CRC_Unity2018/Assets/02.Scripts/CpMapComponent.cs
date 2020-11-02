using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class CpMapComponent : MonoBehaviour
{
    private GameObject plotGroupObj;
    private PlotGroup plotGroupScript;
    private float formalSliderValue;
    private int formalDropdownValue;
    private float sliderValue;
    private int dropdownValue;

    public float FormalSliderValue { get => formalSliderValue; set => formalSliderValue = value; }
    public int FormalDropdownValue { get => formalDropdownValue; set => formalDropdownValue = value; }
    public GameObject PlotGroupObj { get => plotGroupObj; set => plotGroupObj = value; }
    public PlotGroup PlotGroupScript { get => plotGroupScript; set => plotGroupScript = value; }
    public float SliderValue { get => sliderValue; set => sliderValue = value; }
    public int DropdownValue { get => dropdownValue; set => dropdownValue = value; }

    public void Instantiate()
    {
        formalSliderValue = 999;
        formalDropdownValue = 999;
        sliderValue = 999;
        dropdownValue = 999;
    }

    public void MapOn()
    {
        MapOff();
        if(dropdownValue == 0)
        {
            switch (sliderValue)
            {
                case 2009:
                    PlotGroupScript.RCCobj[0].SetActive(true);
                    break;
                case 2010:
                    PlotGroupScript.RCCobj[1].SetActive(true);
                    break;
                case 2011:
                    PlotGroupScript.RCCobj[2].SetActive(true);
                    break;
                case 2012:
                    PlotGroupScript.RCCobj[3].SetActive(true);
                    break;
                case 2013:
                    PlotGroupScript.RCCobj[4].SetActive(true);
                    break;
                case 2014:
                    PlotGroupScript.RCCobj[5].SetActive(true);
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (sliderValue)
            {
                case 2009:
                    PlotGroupScript.WNCobj[0].SetActive(true);
                    break;
                case 2010:
                    PlotGroupScript.WNCobj[1].SetActive(true);
                    break;
                case 2011:
                    PlotGroupScript.WNCobj[2].SetActive(true);
                    break;
                case 2012:
                    PlotGroupScript.WNCobj[3].SetActive(true);
                    break;
                case 2013:
                    PlotGroupScript.WNCobj[4].SetActive(true);
                    break;
                case 2014:
                    PlotGroupScript.WNCobj[5].SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    private void MapOff()
    {
        Debug.Log("Mapoff : " + formalDropdownValue + "  " + formalSliderValue);
        if(formalDropdownValue == 999 || formalSliderValue == 999)
        {
            return;
        }
        if (formalDropdownValue == 0)
        {
            switch (formalSliderValue)
            {
                case 2009:
                    PlotGroupScript.RCCobj[0].SetActive(false);
                    break;
                case 2010:
                    PlotGroupScript.RCCobj[1].SetActive(false);
                    break;
                case 2011:
                    PlotGroupScript.RCCobj[2].SetActive(false);
                    break;
                case 2012:
                    PlotGroupScript.RCCobj[3].SetActive(false);
                    break;
                case 2013:
                    PlotGroupScript.RCCobj[4].SetActive(false);
                    break;
                case 2014:
                    PlotGroupScript.RCCobj[5].SetActive(false);
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (sliderValue)
            {
                case 2009:
                    PlotGroupScript.WNCobj[0].SetActive(false);
                    break;
                case 2010:
                    PlotGroupScript.WNCobj[1].SetActive(false);
                    break;
                case 2011:
                    PlotGroupScript.WNCobj[2].SetActive(false);
                    break;
                case 2012:
                    PlotGroupScript.WNCobj[3].SetActive(false);
                    break;
                case 2013:
                    PlotGroupScript.WNCobj[4].SetActive(false);
                    break;
                case 2014:
                    PlotGroupScript.WNCobj[5].SetActive(false);
                    break;
                default:
                    break;
            }

        }
    }
}
