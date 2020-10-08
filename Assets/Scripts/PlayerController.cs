using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;

    [SerializeField]
    private float thrusterforce = 1000f;

    [SerializeField]
    private float thrusterFuelBurnSpeed = 1f;
    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    [SerializeField]
    private LayerMask environmentMask;

    [Header("Spring settings:")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    // Component cashing
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        //Setting target position for spring
        //This makes the physics act right when it comes to
        //applying gravity when flying over objects
        RaycastHit _hit;
        if (Physics.Raycast (transform.position, Vector3.down, out _hit, 100f, environmentMask))
        {
            joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }

        //Calculate movement velocity as a 3d vector
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMove; 
        Vector3 _movVertical = transform.forward * _zMove;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Animate movement
        animator.SetFloat("ForwardVelocity",_zMove);

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3d vector (turning arround)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3d vector (turning arround)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        // Caculate the thrusterforce based on player input
        Vector3 _thrusterforce = Vector3.zero;
        if(Input.GetButton("Jump") && thrusterFuelAmount > 0f)
        {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

            if(thrusterFuelAmount >= 0.01f)
            {
                _thrusterforce = Vector3.up * thrusterforce;
                SetJointSettings(0f);

            }
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;

            SetJointSettings(jointSpring);
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        // Apply the thruster force
        motor.Applythruster(_thrusterforce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { 
            positionSpring = jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
