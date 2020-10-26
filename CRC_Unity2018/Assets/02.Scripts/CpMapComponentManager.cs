using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpMapComponentManager : MonoBehaviour
{
    public GameObject cpMapComponent;
    public GridLayoutGroup gridLayoutGroup;
    public Plotter plotter;
    private Vector2 cellSize;
    private List<GameObject> mapComponents;

    // Start is called before the first frame update
    void Start()
    {
        mapComponents = new List<GameObject>();
        cellSize = new Vector2(0, 180);
    }

    public void GenerateNewComponent()
    {
        gridLayoutGroup.cellSize += cellSize;
        GameObject componentObj = Instantiate(cpMapComponent);
        componentObj.transform.parent = transform;
        componentObj.transform.localScale = Vector3.one;
        componentObj.transform.localPosition = new Vector3(componentObj.transform.localPosition.x, componentObj.transform.localPosition.y, -0.1f);
        componentObj.transform.localRotation = Quaternion.identity;
        mapComponents.Add(componentObj);
        CpMapComponent componentScript = componentObj.GetComponent<CpMapComponent>();
        GameObject gameObject = plotter.GeneratePlot();
        MapScaler.Scale(gameObject);
        componentScript.MapComponent = gameObject;

        foreach (Transform child in componentObj.transform)
        {
            if (child.tag == "DeleteButton")
            {
                Button Button = child.GetComponent<Button>();
                DeleteButton deleteFunc = child.GetComponent<DeleteButton>();
                deleteFunc.Index = mapComponents.Count-1;
                Button.onClick.AddListener(() => deleteFunc.Delete());
            }
        }

        if (mapComponents.Count > 1)
        {
            GameObject gObject;
            CpMapComponent cpScript;
            for (int i = 0; i < mapComponents.Count-1; i++)
            {
                gObject = mapComponents[i];
                cpScript = gObject.GetComponent<CpMapComponent>();
                MapScaler.MapUp(cpScript.MapComponent);

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

    public void OnDeleteComponenet(int index)
    {
        gridLayoutGroup.cellSize -= cellSize;

        CpMapComponent componentScript = mapComponents[index].GetComponent<CpMapComponent>();
        Destroy(componentScript.MapComponent);

        Debug.Log("index " + index);
        Debug.Log("mapComponents.Count " + mapComponents.Count);
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
                        Debug.Log(deleteFunc.Index);
                    }
                }
            }
            // 그래픽 초기화는 기존애들이 밀려올라가고 새로생긴애들이 아래에 생기는 구조라서 이전에 생긴애들을 아래로 당겨줘야한다.
            for (int i = 0; i < index; i++)
            {
                gObject = mapComponents[i];
                cpScript = gObject.GetComponent<CpMapComponent>();
                MapScaler.MapDown(cpScript.MapComponent);
            }
        }
        mapComponents.RemoveAt(index);
    }
}
