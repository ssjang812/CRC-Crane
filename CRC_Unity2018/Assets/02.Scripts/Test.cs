using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        MeshRenderer[] meshRenderers;
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        Debug.Log($"test code {renderer}");
        Debug.Log($"{meshRenderers}");
        Debug.Log($"{meshRenderers.Length}");
    }
}
