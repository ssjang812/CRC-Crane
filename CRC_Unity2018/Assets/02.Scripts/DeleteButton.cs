using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    private int index;

    public int Index { get => index; set => index = value; }

    public void Delete()
    {
        GameObject buttonsParent = gameObject.transform.parent.gameObject;
        CpMapComponent cpMapComopnent = buttonsParent.GetComponent<CpMapComponent>();
        
        buttonsParent.transform.parent.GetComponent<CpMapComponentManager>().OnDeleteComponenet(index);
        Destroy(cpMapComopnent.PlotGroupObj);
        Destroy(buttonsParent);
    }
}
