//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class CameraMsg : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/Camera";
        public override string RosMessageName => k_RosMessageName;

        public byte[] rawimagedata;

        public CameraMsg()
        {
            this.rawimagedata = new byte[0];
        }

        public CameraMsg(byte[] rawimagedata)
        {
            this.rawimagedata = rawimagedata;
        }

        public static CameraMsg Deserialize(MessageDeserializer deserializer) => new CameraMsg(deserializer);

        private CameraMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.rawimagedata, sizeof(byte), deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.rawimagedata);
            serializer.Write(this.rawimagedata);
        }

        public override string ToString()
        {
            return "CameraMsg: " +
            "\nrawimagedata: " + System.String.Join(", ", rawimagedata.ToList());
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
