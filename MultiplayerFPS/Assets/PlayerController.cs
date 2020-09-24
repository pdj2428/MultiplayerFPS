using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();

    }

    private void Update()
    {
        //Calculate movement velocity as a 3d vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMove; 
        Vector3 _movVertical = transform.forward * _zMove;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3d vector (turning arround)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3d vector (turning arround)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotation);


    }
}
