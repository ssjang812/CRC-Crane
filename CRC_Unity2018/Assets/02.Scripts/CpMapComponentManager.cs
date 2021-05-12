using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CpMapComponentManager : MonoBehaviour // Controll Panel의 'Create', 'Delete' 버튼입력에따라 동적으로 컨트롤패널 UI를 추가삭제하고, plotter에서 생성한 새 layer와의 기능 연결을 하는 스크립트
{
    public GameObject cpMapComponent; // 'Create'를 눌렀을시 생성될 UI 컴포넌트 셋트(드롭다운, 버튼, 슬라이더로 구성된 셋트)
    public GridLayoutGroup gridLayoutGroup; // 컴포넌트가 생성될 위치 
    public Plotter plotter; // 컨트롤 패널의 컴포넌트와 연동시킬 plot 을 생성할 plotter
    private Vector2 cellSize; // 하나의 컴포넌트의 세로 크기 (컴포넌트 추가 삭제시 전체크기 수정이 필요하기때문)
    private List<GameObject> mapComponents; 
    public MapScaler mapScaler;

    // Start is called before the first frame update
    void Start()
    {
        mapComponents = new List<GameObject>();
        cellSize = new Vector2(0, 180);
    }

    public void GenerateNewComponent() //'Create'를 눌렀을시 새 UI를 생성함
    {
        gridLayoutGroup.cellSize += cellSize; //패널에 UI추가를 추가하기위해 크기를 늘려줌
        GameObject componentObj = Instantiate(cpMapComponent);
        componentObj.transform.parent = transform;
        componentObj.transform.localScale = Vector3.one;
        componentObj.transform.localPosition = new Vector3(componentObj.transform.localPosition.x, componentObj.transform.localPosition.y, -0.1f);
        componentObj.transform.localRotation = Quaternion.identity;
        mapComponents.Add(componentObj);
        CpMapComponent componentScript = componentObj.GetComponent<CpMapComponent>(); //패널 UI를 새로 생성 (동작 연결X, 존재만 연결)
        componentScript.Instantiate(); 
        GameObject newPlot = plotter.GeneratePlot(); //새로운 레이어 생성(Plot 생성)
        mapScaler.Scale(newPlot); //생성한 레이어의 Scale을 조절해줌
        componentScript.PlotGroupObj = newPlot;
        componentScript.PlotGroupScript = newPlot.GetComponent<PlotGroup>();

        foreach (Transform child in componentObj.transform) //패널 UI의 삭제버튼에 삭제기능 부여 (Plot자체와 컨트롤 UI 삭제)
        {
            if (child.tag == "DeleteButton")
            {
                Button Button = child.GetComponent<Button>();
                DeleteButton deleteFunc = child.GetComponent<DeleteButton>();
                deleteFunc.Index = mapComponents.Count-1;
                Button.onClick.AddListener(() => deleteFunc.Delete());
            }

            if (child.tag == "Slider") //패널 UI의 슬라이더에 조절기능 부여
            {
                Slider slider = child.GetComponent<Slider>();
                SliderControl sliderControl = child.GetComponent<SliderControl>();
                slider.onValueChanged.AddListener(sliderControl.SliderValueSwitch);
                sliderControl.instantiate();
                sliderControl.SliderValueSwitch(slider.value);
            }

            if (child.tag == "Dropdown") //패널 UI의 드롭다운에 조절기능 부여
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

        if (mapComponents.Count > 1) //'Create'시 기존에 생성되었던 Layer들이 있으면 각각을 위로 밀어올림
        {
            GameObject gObject;
            CpMapComponent cpScript;
            for (int i = 0; i < mapComponents.Count-1; i++)
            {
                gObject = mapComponents[i];
                cpScript = gObject.GetComponent<CpMapComponent>();
                MapScaler.MapUp(cpScript.PlotGroupObj);
            }
        }
    }

    public void OnDeleteComponenet(int index) // Called when Delete() function called (in DeleteButton)
    {
        gridLayoutGroup.cellSize -= cellSize;

        CpMapComponent componentScript = mapComponents[index].GetComponent<CpMapComponent>();

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
            //자료구조 처리와 달리 보이는 그래픽은 반대로 기존애들이 밀려올라가고 새로생긴애들이 아래에 생기는 구조라서 이전에 생긴애들을 아래로 당겨줘야한다.
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
