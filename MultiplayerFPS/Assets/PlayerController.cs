using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

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
       // motor.Move(_velocity);
    }
}
