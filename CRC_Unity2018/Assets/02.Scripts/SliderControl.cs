using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    private GameObject parentObj;
    private CpMapComponent parentCpMapComponent;

    /*
    void Start()
    {
        parentObj = gameObject.transform.parent.gameObject;
        parentCpMapComponent = parentObj.GetComponent<CpMapComponent>();
    }
    */
    public void instantiate()
    {
        parentObj = gameObject.transform.parent.gameObject;
        parentCpMapComponent = parentObj.GetComponent<CpMapComponent>();
    }

    public void SliderValueSwitch(float value)
    {
        parentCpMapComponent.FormalSliderValue = parentCpMapComponent.SliderValue; // set formal info to turn off the formal map
        parentCpMapComponent.FormalDropdownValue = parentCpMapComponent.DropdownValue;

        switch (value)
        {
            case 2009:
                parentCpMapComponent.SliderValue = 2009;
                break;
            case 2010:
                parentCpMapComponent.SliderValue = 2010;
                break;
            case 2011:
                parentCpMapComponent.SliderValue = 2011;
                break;
            case 2012:
                parentCpMapComponent.SliderValue = 2012;
                break;
            case 2013:
                parentCpMapComponent.SliderValue = 2013;
                break;
            case 2014:
                parentCpMapComponent.SliderValue = 2014;
                break;
            default:
                break;
        }
        Debug.Log("Slider control : " + value);
        parentCpMapComponent.MapOn();
    }
}
