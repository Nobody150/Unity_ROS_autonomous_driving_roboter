#!/usr/bin/env python

import random
import rclpy

from unity_robotics_demo_msgs.srv import CameraService

from rclpy.node import Node

class CameraServiceNode(Node):

    def __init__(self):
        super().__init__('camera_service_node')
        self.srv = self.create_service(CameraService, 'camera', self.new_camera_callback)

    def new_camera_callback(self, request, response):
        response.output.rawimagedata = request.rawimagedata
        self.get_logger().info('CameraData - %f' % (self.request.rawimagedata))
        return response


def main(args=None):
    rclpy.init(args=args)

    camera_service = CameraServiceNode()

    rclpy.spin(camera_service)

    rclpy.shutdown()


if __name__ == '__main__':
    main()
# 