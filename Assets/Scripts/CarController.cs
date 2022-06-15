using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private float currentmotorForce;
    private bool isBreaking;

    InputMaster inputMaster;

    public Boolean SaveSimulationData;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Vector2 steer; 
    private Vector2 drive; 

    private float forward;
    private float backward;
    private float left;
    private float right;
    private string time;

    void Start()
    {
        //AssetDatabase.CreateFolder("Assets/SimulationData", System.DateTime.Now+"");
        time = System.DateTime.Now+"";
        time = time.Replace(":", "_");
    }

    void Awake(){
        inputMaster = new InputMaster();
        //Controler
        inputMaster.Car.Steer.performed += ctx => steer = ctx.ReadValue<Vector2>();
        inputMaster.Car.Break.started += ctx => isBreaking = true;
        inputMaster.Car.Break.canceled += ctx => isBreaking = false;
        inputMaster.Car.Drive.performed += ctx => drive = ctx.ReadValue<Vector2>();

        //Keyboard
        inputMaster.Car.Forward.performed += ctx => forward = 1;
        inputMaster.Car.Forward.canceled += ctx => forward = 0;

        inputMaster.Car.Backward.started += ctx => backward = 1;
        inputMaster.Car.Backward.canceled += ctx => backward = 0;

        inputMaster.Car.Left.started += ctx => left = 1;
        inputMaster.Car.Left.canceled += ctx => left = 0;

        inputMaster.Car.Right.started += ctx => right = 1;
        inputMaster.Car.Right.canceled += ctx => right = 0;

        inputMaster.Car.Break_Keyboard.started += ctx => isBreaking = true;
        inputMaster.Car.Break_Keyboard.canceled += ctx => isBreaking = false;
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        SaveInputToFile(SaveSimulationData);

    }

public int conuter;
    void SaveInputToFile(Boolean SaveSimulationData){
        if (SaveSimulationData){
            StreamWriter file = new StreamWriter("./SimulationData/Data"+ time +".csv", append: true);
            file.Write(currentSteerAngle+";"+ currentmotorForce + ";" + currentbreakForce + ";\n");
            file.Close();
            conuter++;
        }
    }

    void OnEnable(){
        inputMaster.Car.Enable();
    }

    void OnDisable(){
        inputMaster.Car.Disable();
    }

    private void HandleMotor()
    {
        float force = drive.y;
        if (forward == 1 || backward == 1){
            force = forward - backward;
        }

        rearLeftWheelCollider.motorTorque = force * motorForce;
        rearRightWheelCollider.motorTorque = force * motorForce;
        currentmotorForce = force * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        float direction = steer.x;
        if (left == 1 || right == 1){
            direction = right - left;
        }
    
        currentSteerAngle = maxSteerAngle * direction;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}