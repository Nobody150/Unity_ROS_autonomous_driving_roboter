
using RosMessageTypes.UnityRoboticsDemo;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

/// <summary>
///
/// </summary>
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
        ros.RegisterPublisher<PosRotMsg>(topicName);

        renderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24, UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB);
        renderTexture.Create();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {

            byte[] rawImageData = CaptureScreenshot();

            CameraMsg cameraMsg = new CameraMsg(
                rawImageData
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, cameraMsg);

            timeElapsed = 0;
        }
    }

    /// <summary>
    ///     Capture the main camera's render texture and convert to bytes.
    /// </summary>
    /// <returns>imageBytes</returns>
    private byte[] CaptureScreenshot()
    {
        sensorCamera.targetTexture = renderTexture;
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        sensorCamera.Render();
        Debug.Log(sensorCamera.targetTexture);
        Texture2D mainCameraTexture = new Texture2D(renderTexture.width, renderTexture.height);
        mainCameraTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        mainCameraTexture.Apply();
        RenderTexture.active = currentRT;
        // Get the raw byte info from the screenshot
        byte[] imageBytes = mainCameraTexture.GetRawTextureData();

        Camera.main.targetTexture = null;

        /**var oldRT = RenderTexture.active;
        RenderTexture.active = sensorCamera.targetTexture;
        sensorCamera.Render();
        Debug.Log(sensorCamera.activeTexture);
        // Copy the pixels from the GPU into a texture so we can work with them
        // For more efficiency you should reuse this texture, instead of creating a new one every time
        Texture2D camText = new Texture2D(sensorCamera.targetTexture.width, sensorCamera.targetTexture.height);
        camText.ReadPixels(new Rect(0, 0, sensorCamera.targetTexture.width, sensorCamera.targetTexture.height), 0, 0);
        camText.Apply();
        RenderTexture.active = oldRT;
        
        // Encode the texture as a PNG, and send to ROS
        byte[] imageBytes = camText.EncodeToPNG();**/


        Debug.Log(imageBytes[1]);
        return imageBytes;
    }

}