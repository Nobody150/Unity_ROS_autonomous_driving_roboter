using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveCameraImages : MonoBehaviour
{

    // Publish the cube's position and rotation every N seconds
    public float saveImageFrequency = 1.0f;

    public bool SaveImagesToFile;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    private string time;
    private float counter = 0;

    public Camera sensorCamera;
     private RenderTexture renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        if(SaveImagesToFile){
            renderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24);
            renderTexture.Create();
            //AssetDatabase.CreateFolder("Assets/SimulationData", System.DateTime.Now+"");
            time = System.DateTime.Now+"";
            time = time.Replace(":", "_");
            string path = "./SimulationData/"+time;
            Directory.CreateDirectory(path);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SaveImagesToFile){
            timeElapsed += Time.deltaTime;
            if (timeElapsed > saveImageFrequency) {
                CaptureScreenshot();
                counter++;
                timeElapsed = 0;
            }
        }
    }

    private void CaptureScreenshot()
    {
        sensorCamera.targetTexture = renderTexture;
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        sensorCamera.Render();
        Texture2D mainCameraTexture = new Texture2D(renderTexture.width, renderTexture.height);
        mainCameraTexture.ReadPixels(new Rect(renderTexture.width, renderTexture.width, renderTexture.width, renderTexture.height), 0, 0);
        mainCameraTexture.Apply();
        RenderTexture.active = currentRT;
        // Get the raw byte info from the screenshot
        byte[] imageBytes = mainCameraTexture.GetRawTextureData();
        byte[] bytes;
        bytes = mainCameraTexture.EncodeToPNG();
        
        string path =  "./SimulationData/"+ time + "/" + "camera_"+ counter +".png";
        System.IO.File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
        Debug.Log("Saved to " + path);
    }
}