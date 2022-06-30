using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveCameraImages : MonoBehaviour
{

    // Publish the cube's position and rotation every N seconds
    public float saveImageFrequency = 1.0f;

    private CarController carController;
    public bool SaveSimulationData;
    public int resultionWidth = 848;
    public int resultionHeight = 480;
    
    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    private string time;
    private float counter = 1;

    public Camera sensorCamera;
     private RenderTexture renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        carController = gameObject.GetComponent<CarController>();
        if(SaveSimulationData){
            renderTexture = new RenderTexture(resultionWidth, resultionHeight, 24);
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
        if(SaveSimulationData){
            timeElapsed += Time.deltaTime;
            if (timeElapsed > saveImageFrequency) {
                CaptureScreenshot();
                SaveInputToFile();
                counter++;
                timeElapsed = 0;
            }
        }
    }

    
    void SaveInputToFile(){
        StreamWriter file = new StreamWriter("./SimulationData/Data"+ time +".csv", append: true);
        file.Write("camera_"+ counter +".png"+";"+carController.currentSteerAngle+";"+ carController.currentmotorForce + ";" + carController.currentbreakForce + ";\n");
        file.Close();
    }

    private void CaptureScreenshot()
    {
        sensorCamera.targetTexture = renderTexture;
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        sensorCamera.Render();
        Texture2D mainCameraTexture = new Texture2D(renderTexture.width, renderTexture.height);
        mainCameraTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        mainCameraTexture.Apply();
        RenderTexture.active = currentRT;
        // Get the raw byte info from the screenshot
        byte[] imageBytes = mainCameraTexture.GetRawTextureData();
        byte[] bytes;
        bytes = mainCameraTexture.EncodeToPNG();
        
        string path =  "./SimulationData/"+ time + "/" + "camera_"+ counter +".png";

        SaveImage(path, bytes);

        //File.WriteAllBytes(path, bytes);
        //AssetDatabase.ImportAsset(path);
        //Debug.Log("Saved to " + path);
    }

    private async void SaveImage(string path, byte[] bytes){
        using (FileStream SourceStream = File.Open(path, FileMode.OpenOrCreate))
        {
            SourceStream.Seek(0, SeekOrigin.End);
            await SourceStream.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
