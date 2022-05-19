
using RosMessageTypes.UnityRobotics;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

using System;

public class ROSUnityCamera : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "pos_camera";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 1.0f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    private RenderTexture renderTexture;

    public Camera sensorCamera;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<CameraMsg>(topicName);

        renderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24);
        renderTexture.Create();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {

            byte[] rawImageData = CaptureScreenshot();

            string data = Convert.ToBase64String(rawImageData);
            CameraMsg cameraMsg = new CameraMsg(
                data
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, cameraMsg);

            timeElapsed = 0;
        }
    }

    private byte[] CaptureScreenshot()
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

        Camera.main.targetTexture = null;
        return imageBytes;
    }

}