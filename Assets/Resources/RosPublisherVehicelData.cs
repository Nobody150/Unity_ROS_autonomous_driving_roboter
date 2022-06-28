using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRobotics;

public class RosPublisherVehicelData : MonoBehaviour
{

    
    ROSConnection ros;
    public string topicName = "vehicledata";

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 1.0f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    private short breakforce;
    private short steeringangle;
    private short motorforce;


    // Start is called before the first frame update
    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<VehicleDataMsg>(topicName);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {

           steeringangle++;
           breakforce++;
           motorforce++;
            Debug.Log("Test" + steeringangle);
            VehicleDataMsg cameraMsg = new VehicleDataMsg(
                steeringangle,
                motorforce,
                motorforce,
                breakforce
            );

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, cameraMsg);

            timeElapsed = 0;
        }
    }
}