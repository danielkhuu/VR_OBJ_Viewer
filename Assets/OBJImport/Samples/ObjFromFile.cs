using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using Dummiesman;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ObjFromFile : MonoBehaviour
{
    //string objPath = "models/test.obj";
    string objPath = string.Empty;
    string error = string.Empty;
    GameObject loadedObject;

    private void OnGUI() {
        //objPath = GUI.TextField(new Rect(0, 0, 256, 32), objPath);
        GUI.Label(new Rect(0, 0, 256, 32), "Obj Path:");
        if(GUI.Button(new Rect(256, 32, 64, 32), "Load File"))
        {
            //file path
            if (!File.Exists(objPath))
            {
                error = "File doesn't exist.";
            }else{
                if(loadedObject != null)            
                    Destroy(loadedObject);
                loadedObject = new OBJLoader().Load(objPath);
                error = string.Empty;
            }
        }

        if(!string.IsNullOrWhiteSpace(error))
        {
            GUI.color = Color.red;
            GUI.Box(new Rect(0, 64, 256 + 64, 32), error);
            GUI.color = Color.white;
        }
    }
}
