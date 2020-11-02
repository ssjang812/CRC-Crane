using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CpMapComponentManager : MonoBehaviour // 패널, 맵의 각 레이어 단위로 묶어서 관리하는 매니져. -> 각 레이어의 내부 맵들을 따로 관리하는 스크립트 만들어줘야함
{
    public GameObject cpMapComponent;
    public GridLayoutGroup gridLayoutGroup;
    public Plotter plotter;
    private Vector2 cellSize;
    private List<GameObject> mapComponents;
    public MapScaler mapScaler;

    // Start is called before the first frame update
    void Start()
    {
        mapComponents = new List<GameObject>();
        cellSize = new Vector2(0, 180);
    }

    public void GenerateNewComponent()
    {
        gridLayoutGroup.cellSize += cellSize; //패널에 UI추가
        GameObject componentObj = Instantiate(cpMapComponent);
        componentObj.transform.parent = transform;
        componentObj.transform.localScale = Vector3.one;
        componentObj.transform.localPosition = new Vector3(componentObj.transform.localPosition.x, componentObj.transform.localPosition.y, -0.1f);
        componentObj.transform.localRotation = Quaternion.identity;
        mapComponents.Add(componentObj);
        CpMapComponent componentScript = componentObj.GetComponent<CpMapComponent>(); //패널 UI에 플롯을 생성해서 연결시킴 (동작 연결X, 존재만 연결)
        componentScript.Instantiate(); 
        GameObject newPlot = plotter.GeneratePlot();
        mapScaler.Scale(newPlot);
        componentScript.PlotGroupObj = newPlot;
        componentScript.PlotGroupScript = newPlot.GetComponent<PlotGroup>();

        foreach (Transform child in componentObj.transform) //패널 UI의 삭제버튼에 삭제기능 부여 (Plot, UI 삭제)
        {
            if (child.tag == "DeleteButton")
            {
                Button Button = child.GetComponent<Button>();
                DeleteButton deleteFunc = child.GetComponent<DeleteButton>();
                deleteFunc.Index = mapComponents.Count-1;
                Button.onClick.AddListener(() => deleteFunc.Delete());
            }

            if (child.tag == "Slider")
            {
                Slider slider = child.GetComponent<Slider>();
                SliderControl sliderControl = child.GetComponent<SliderControl>();
                slider.onValueChanged.AddListener(sliderControl.SliderValueSwitch);
                sliderControl.instantiate();
                sliderControl.SliderValueSwitch(slider.value);
            }

            if (child.tag == "Dropdown")
            {
                TMP_Dropdown dropDown = child.GetComponent<TMP_Dropdown>();
                DropdownControl dropDownControl = child.GetComponent<DropdownControl>();
                dropDownControl.PlotGroupObj = newPlot;
                dropDownControl.PlotGroupScript = newPlot.GetComponent<PlotGroup>();
                dropDown.onValueChanged.AddListener(dropDownControl.DropdownValueSwitch);
                dropDownControl.Instantiate();
                dropDownControl.DropdownValueSwitch(dropDown.value);
            }
        }

        if (mapComponents.Count > 1) //기존 맵이있으면 기존맵들을 위로 밀어올림
        {
            GameObject gObject;
            CpMapComponent cpScript;
            for (int i = 0; i < mapComponents.Count-1; i++)
            {
                gObject = mapComponents[i];
                cpScript = gObject.GetComponent<CpMapComponent>();
                MapScaler.MapUp(cpScript.PlotGroupObj);

                ////debug code
                //foreach (Transform child in gObject.transform)
                //{
                //    if (child.tag == "DeleteButton")
                //    {
                //        Button Button = child.GetComponent<Button>();
                //        DeleteButton deleteFunc = child.GetComponent<DeleteButton>();
                //        Debug.Log(deleteFunc.Index);
                //    }
                //}
            }
        }
    }

    public void OnDeleteComponenet(int index) // Called when Delete() function called (in DeleteButton)
    {
        gridLayoutGroup.cellSize -= cellSize; // 삭제된 UI 만큼 레이아수틀 줄여주기

        CpMapComponent componentScript = mapComponents[index].GetComponent<CpMapComponent>();
        //Destroy(componentScript.MapComponent); //<= Delete() 함수에서 지워주기때문에 여기선 안써야할듯

        if (index < mapComponents.Count-1)
        {
            GameObject gObject;
            CpMapComponent cpScript;
            // 인덱스 초기화는 삭제된 인덱스보다 나중에 생긴애들을 아래로 당겨줘야하고
            for (int i = index+1; i < mapComponents.Count; i++)
            {
                gObject = mapComponents[i];

                foreach (Transform child in gObject.transform)
                {
                    if (child.tag == "DeleteButton")
                    {
                        Button Button = child.GetComponent<Button>();
                        DeleteButton deleteFunc = child.GetComponent<DeleteButton>();
                        deleteFunc.Index--;
                    }
                }
            }
            // 그래픽 초기화는 기존애들이 밀려올라가고 새로생긴애들이 아래에 생기는 구조라서 이전에 생긴애들을 아래로 당겨줘야한다.
            for (int i = 0; i < index; i++)
            {
                gObject = mapComponents[i];
                cpScript = gObject.GetComponent<CpMapComponent>();
                MapScaler.MapDown(cpScript.PlotGroupObj);
            }
        }
        mapComponents.RemoveAt(index); // ui, plot 삭제하고 마지막에 리스트에서 삭제
    }
}
