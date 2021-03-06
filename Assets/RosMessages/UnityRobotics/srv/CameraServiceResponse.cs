//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRobotics
{
    [Serializable]
    public class CameraServiceResponse : Message
    {
        public const string k_RosMessageName = "unity_robotics_msgs/CameraService";
        public override string RosMessageName => k_RosMessageName;

        public CameraMsg output;

        public CameraServiceResponse()
        {
            this.output = new CameraMsg();
        }

        public CameraServiceResponse(CameraMsg output)
        {
            this.output = output;
        }

        public static CameraServiceResponse Deserialize(MessageDeserializer deserializer) => new CameraServiceResponse(deserializer);

        private CameraServiceResponse(MessageDeserializer deserializer)
        {
            this.output = CameraMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.output);
        }

        public override string ToString()
        {
            return "CameraServiceResponse: " +
            "\noutput: " + output.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}
