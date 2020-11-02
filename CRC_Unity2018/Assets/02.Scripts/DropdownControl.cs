using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownControl : MonoBehaviour
{
    private GameObject plotGroupObj;
    private PlotGroup plotGroupScript;
    private GameObject parentObj;
    private CpMapComponent parentCpMapComponent;

    public GameObject PlotGroupObj { get => plotGroupObj; set => plotGroupObj = value; }
    public PlotGroup PlotGroupScript { get => plotGroupScript; set => plotGroupScript = value; }
    /*
    private void Start()
    {
        Debug.Log("DropdownControl Start");
        parentObj = gameObject.transform.parent.gameObject;
        parentCpMapComponent = parentObj.GetComponent<CpMapComponent>();
    }
    */
    public void Instantiate()
    {
        parentObj = gameObject.transform.parent.gameObject;
        parentCpMapComponent = parentObj.GetComponent<CpMapComponent>();
    }

    public void DropdownValueSwitch(int value)
    {
        parentCpMapComponent.FormalSliderValue = parentCpMapComponent.SliderValue; // set formal info to turn off the formal map
        parentCpMapComponent.FormalDropdownValue = parentCpMapComponent.DropdownValue;

        switch (value)
        {
            case 0:
                parentCpMapComponent.DropdownValue = 0;
                break;
            case 1:
                parentCpMapComponent.DropdownValue = 1;
                break;
            default:
                break;
        }
        parentCpMapComponent.MapOn();
        Debug.Log("Dropdown control : " + value);
    }
}
