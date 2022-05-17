# ROS2_Docker

Das Projekt verwendet die Funktionalitäten von [Unity Robotics Hub](https://github.com/Unity-Technologies/Unity-Robotics-Hub)

## DockerImage
Für das Bauen des Images folgenden Befehl ausführen:
`docker build -t ros_docker -f Dockerfile .`

Das Starten des Containers über den Befehl 

* Volume Verzeichnis auf den `./ros2_docker/ros2_packages/` des Hostsystems setzen

```
docker run -d -it --rm --name ros_docker -p 10000:10000 -v [~Volume]:/home/dev_ws/src/ ros_docker
```

## im Docker Container

Mit Container Verbinden:
```
docker exec -it ros_docker bash
```

Für das Starten des ROS2 Servers folgede Befehle ausführen:

```
source install/setup.bash
colcon build
source install/setup.bash
ros2 run ros_tcp_endpoint default_server_endpoint --ros-args -p ROS_IP:=0.0.0.0
```
Der Server sollte in der Konsole ausgeführt werden.

Wichtig! Console nicht schließen, ansonsten wird der Server beendet.

In einer weiteren Console wieder mit den Container verbinden. 
Es können nun verschiedenen Scripte ausgeführt werden, um ROS2 zu verwenden.

Beispiel:
```
source install/setup.bash
colcon build
source install/setup.bash
ros2 topic echo pos_camera
```
## Anpassen ROS2 Scripte
Die ROS 2 Scripte können im `./ros2_docker/ros2_packages/` angepasst werden. 

Sie werden direkt in den Container gemappt. Nach bauen der ROS2 Umgebung können Änderungen verwendet werden.
```
source install/setup.bash
colcon build
source install/setup.bash
```