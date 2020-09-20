using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScaler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] plot;

    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.position = new Vector3(0f, -0.5f, 1.0f);

        foreach(GameObject p in plot)
        {
            p.SetActive(false);
        }
        plot[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
