//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRobotics
{
    [Serializable]
    public class VehicleDataRequest : Message
    {
        public const string k_RosMessageName = "unity_robotics_msgs/VehicleData";
        public override string RosMessageName => k_RosMessageName;

        public VehicleDataMsg input;

        public VehicleDataRequest()
        {
            this.input = new VehicleDataMsg();
        }

        public VehicleDataRequest(VehicleDataMsg input)
        {
            this.input = input;
        }

        public static VehicleDataRequest Deserialize(MessageDeserializer deserializer) => new VehicleDataRequest(deserializer);

        private VehicleDataRequest(MessageDeserializer deserializer)
        {
            this.input = VehicleDataMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.input);
        }

        public override string ToString()
        {
            return "VehicleDataRequest: " +
            "\ninput: " + input.ToString();
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
