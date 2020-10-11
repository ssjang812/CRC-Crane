using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpMapComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject mapComponent;
    public GameObject MapComponent { get => mapComponent; set => mapComponent = value; }
}
