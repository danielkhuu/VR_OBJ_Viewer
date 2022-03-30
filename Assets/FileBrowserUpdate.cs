
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;


public class FileBrowserUpdate : MonoBehaviour
{
    GameObject loadedObject;
    [SerializeField] Canvas launcher = null;
    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "OBJ files (*.obj) | *.obj;";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load stl from local path with UWR
            StartCoroutine(LoadSTL(path));
        });
    }

    IEnumerator LoadSTL(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                //var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                //rawImage.texture = uwrTexture;
                if(loadedObject != null)            
                    Destroy(loadedObject);
                loadedObject = new OBJLoader().Load(path);
                loadedObject.AddComponent<Rigidbody>();
                loadedObject.AddComponent<BoxCollider>();

                launcher.gameObject.SetActive(false);
            }
        }
    }
}
