
using UnityEngine;

public class CarControler : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbrakeForce;
    private bool isBreaking;
    

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FrontLeftWheelCollider;
    [SerializeField] private WheelCollider FrontRightWheelCollider;
    [SerializeField] private WheelCollider RearLeftWheelCollider;
    [SerializeField] private WheelCollider RearRightWheelCollider;
    
    [SerializeField] private Transform FrontLeftWheelTransform;
    [SerializeField] private Transform FrontRightWheelTransform;
    [SerializeField] private Transform RearLeftWheelTransform;
    [SerializeField] private Transform RearRightWheelTransform;

    //Right
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    //Right
    private void HandleMotor()
    {
        FrontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        FrontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbrakeForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }
    //Right
    private void ApplyBreaking()
    {
        FrontLeftWheelCollider.brakeTorque = currentbrakeForce;
        FrontRightWheelCollider.brakeTorque = currentbrakeForce;
        RearLeftWheelCollider.brakeTorque = currentbrakeForce;
        RearRightWheelCollider.brakeTorque = currentbrakeForce;
    }

     //Right
     private void GetInput()
     {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);


     }
    //Right
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FrontLeftWheelCollider.steerAngle = currentSteerAngle;
        FrontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    //Right
    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
    }
    //Right
    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {

        Vector3 pos;
        Quaternion rot;
        WheelCollider.GetWorldPose(out pos, out rot);
        WheelTransform.rotation = rot;
        WheelTransform.position = pos;

    }


}
