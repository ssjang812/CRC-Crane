using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAnchorSessionControl : MonoBehaviour
{
    private GameObject anchorModule;
    private AnchorModuleScript anchorModuleScript;
    // Start is called before the first frame update
    void Start()
    {
        anchorModule = GameObject.FindGameObjectWithTag("MapAnchor");
        anchorModuleScript = anchorModule.GetComponent<AnchorModuleScript>();
        anchorModuleScript.StartAzureSession();
        anchorModuleScript.GetAzureAnchorIdFromNetwork();
        anchorModuleScript.FindAzureAnchor();
        anchorModuleScript.RemoveLocalAnchor(anchorModule);
    }

    void OnApplicationQuit()
    {
        anchorModuleScript.DeleteAzureAnchor();
        anchorModuleScript.CreateAzureAnchor(anchorModule);
        anchorModuleScript.SaveAzureAnchorIdToDisk();
        anchorModuleScript.ShareAzureAnchorIdToNetwork();
        anchorModuleScript.StopAzureSession();
    }
}
