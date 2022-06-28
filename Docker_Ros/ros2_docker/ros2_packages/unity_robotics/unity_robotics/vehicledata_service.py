#!/usr/bin/env python

import random
import rclpy

from unity_robotics_msgs.srv import VehicleData

from rclpy.node import Node

class VehicleDataServiceNode(Node):

    def __init__(self):
        super().__init__('vehicledata_service')
        self.subscription  = self.create_subscription(VehicleData, 'vehicledata', self.listener_callback, 10)
        self.subscription 

    def listener_callback(self, msg):
        self.get_logger().info('I heard: "%s"' % msg.data)


def main(args=None):
    rclpy.init(args=args)

    vehicledata_service = VehicleDataServiceNode()
    
    rclpy.spin(vehicledata_service)

    rclpy.shutdown()


if __name__ == '__main__':
    main()