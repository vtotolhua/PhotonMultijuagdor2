using UnityEngine;
using System.Collections;

public class HeliController : MonoBehaviour
{
    private GameObject _heliPivot;

    public bool EnableControls;
    public bool MouseControl;

    #region Helicopter parameters
    public float MaxHeight; //Maximum height by Y in world space
    public float MaxSpeed; //Maximim speed to any direction

    public float MaxAngle; //Maximum rotation angle 
    private float _minAngle;

    public float MaxEngineForce; //Maximum engine force. This value affect only to vertical moving
    [System.NonSerialized]
    public float CurEngineForce;
    
    public float Acceleration; //Horizontal acceleration
    private float _curAcceleration;

    public float RotationSpeed; //Rotation speed. This value affect only to horizontal rotation 
    public float TiltSpeed; //Speed of tilting in any direction

    public float StabilizationForce; //Speed of rotation stabilization
    #endregion

    private float[] _input = new float[4]; //_input[0] = right; _input[1] = left; _input[2] = forward; _input[3] = back;
    private bool _anyInput; //If any tilt keys is pressed or mouse move?

    [System.NonSerialized]
    public bool HeliBladesHitting; //Is there objects collides with helicopter blades? 

    private Vector3 _lastPos; //Last world position of helicopter
    private Rigidbody _rigidbody;

    void Awake()
    {
        _heliPivot = transform.Find("HeliPivot").gameObject;
        _curAcceleration = Acceleration;
        _minAngle = 360 - MaxAngle;

        _rigidbody = GetComponent<Rigidbody>();
    }
	
	void Update()
	{
        #region Input
        if (EnableControls)
        {
            if (MouseControl)
            {
                if (Input.GetAxis("Mouse X") > 0 && Input.GetAxis("Mouse X") < 2)
                    _input[0] = Input.GetAxis("Mouse X");
                else if (Input.GetAxis("Mouse X") < 0 && Input.GetAxis("Mouse X") > -2)
                    _input[1] = Mathf.Abs(Input.GetAxis("Mouse X"));
                if (Input.GetAxis("Mouse X") == 0)
                {
                    _input[0] = 0;
                    _input[1] = 0;
                }

                if (Input.GetAxis("Mouse Y") > 0 && Input.GetAxis("Mouse Y") < 2)
                    _input[2] = Input.GetAxis("Mouse Y");
                else if (Input.GetAxis("Mouse Y") < 0 && Input.GetAxis("Mouse Y") > -2)
                    _input[3] = Mathf.Abs(Input.GetAxis("Mouse Y"));
                if (Input.GetAxis("Mouse Y") == 0)
                {
                    _input[2] = 0;
                    _input[3] = 0;
                }
                
            }
            else
            {
                if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.L))
                    _input[0] = 1;
                else _input[0] = 0;

                if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.J))
                    _input[1] = 1;
                else _input[1] = 0;

                if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.I))
                    _input[2] = 1;
                else _input[2] = 0;

                if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.K))
                    _input[3] = 1;
                else _input[3] = 0;
            }

            if (_input[0] > 0 || _input[1] > 0 || _input[2] > 0 || _input[3] > 0)
                _anyInput = true;
            else _anyInput = false;
        }
	    
        #endregion
    }

    private float _angVelocityBoost;
    private float _forceToTakeOff;
    private bool _takeOff;
    void FixedUpdate ()
    {
        _forceToTakeOff = (MaxEngineForce*0.3f);
        _takeOff = CurEngineForce > _forceToTakeOff;

        CurEngineForce -= Time.deltaTime * 0.6f;
        if (EnableControls)
        {
            #region Vertical movement
            if (Input.GetAxis("Vertical") == 0)
            {
                if (_takeOff)
                {
                    if (CurEngineForce <= (MaxEngineForce * 0.9f))
                        CurEngineForce += Time.deltaTime * (MaxEngineForce * 0.9f);
                    else CurEngineForce -= Time.deltaTime * (MaxEngineForce * 0.9f);
                }
            }

            if (Input.GetAxis("Vertical") == 1)
            {
                CurEngineForce += Time.deltaTime * (MaxEngineForce * 0.8f);

                if (_takeOff)
                {
                    float engineAcceleration = 0;
                    if (transform.position.y < _lastPos.y)
                        engineAcceleration = (_lastPos.y - transform.position.y) * 5000;
                    else engineAcceleration = 0;
                    _rigidbody.AddForce(Vector3.up * ((CurEngineForce * 0.8f) + engineAcceleration));
                }
            }
            else if (Input.GetAxis("Vertical") == -1)
            {
                CurEngineForce -= Time.deltaTime * (MaxEngineForce * 0.15f);

                if (_takeOff)
                {
                    _rigidbody.AddForce(Vector3.down * CurEngineForce * 0.2f);
                }
            }
            else
            {
                if (_takeOff)
                    _rigidbody.AddForce(Vector3.up * (_rigidbody.mass * 9.75f));
            }
            #endregion
        }
        else CurEngineForce -= Time.deltaTime*_forceToTakeOff;

        if (CurEngineForce <= 0)
            CurEngineForce = 0;
        if (CurEngineForce >= MaxEngineForce)
            CurEngineForce = MaxEngineForce;

	    _heliPivot.transform.position = transform.position;
        _heliPivot.transform.rotation = new Quaternion(0, transform.rotation.y, 0, _heliPivot.transform.rotation.w);

        if (_takeOff)
        {
            #region Horizontal speed stabilization
            if (EnableControls)
            {
                if (Input.GetAxis("Horizontal") == 1)
                {
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, new Vector3(_rigidbody.angularVelocity.x, RotationSpeed, _rigidbody.angularVelocity.z), Time.deltaTime);
                }
                if (Input.GetAxis("Horizontal") == -1)
                {
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, new Vector3(_rigidbody.angularVelocity.x, -RotationSpeed, _rigidbody.angularVelocity.z), Time.deltaTime);
                }
                if (Input.GetAxis("Horizontal") == 0)
                {
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, new Vector3(_rigidbody.angularVelocity.x, 0, _rigidbody.angularVelocity.z), Time.deltaTime * StabilizationForce);
                }
            }
            #endregion

            #region Tilt limits
            if (transform.localRotation.z < MaxAngle || transform.localEulerAngles.z > _minAngle)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, (-transform.forward * TiltSpeed) * _input[0], Time.deltaTime);
            if (transform.localEulerAngles.z > _minAngle || transform.localEulerAngles.z < MaxAngle)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, (transform.forward * TiltSpeed) * _input[1], Time.deltaTime);

            if (transform.localEulerAngles.x < MaxAngle || transform.localEulerAngles.x > _minAngle)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, (transform.right * TiltSpeed) * _input[2], Time.deltaTime);
            if (transform.localEulerAngles.x > _minAngle || transform.localEulerAngles.x < MaxAngle)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, (-transform.right * TiltSpeed) * _input[3], Time.deltaTime);
            #endregion

            if (_rigidbody.velocity.magnitude > MaxSpeed) //Speed limitation
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * MaxSpeed;
            }

            #region Horizontal movement
            if (transform.localEulerAngles.x <= 180)
                _rigidbody.AddRelativeForce(new Vector3(0, 0, transform.localEulerAngles.x * _curAcceleration));
            else _rigidbody.AddRelativeForce(new Vector3(0, 0, -(360 - transform.localEulerAngles.x) * _curAcceleration));

            if (transform.localEulerAngles.z <= 180)
                _rigidbody.AddRelativeForce(new Vector3(-transform.localEulerAngles.z * _curAcceleration, 0, 0));
            else _rigidbody.AddRelativeForce(new Vector3((360 - transform.localEulerAngles.z) * _curAcceleration, 0, 0));

            if (transform.localEulerAngles.z < 100 && transform.localEulerAngles.z > 80 && _curAcceleration > 0)
                _curAcceleration -= Time.deltaTime * 10;
            if (transform.localEulerAngles.z < 280 && transform.localEulerAngles.z > 260 && _curAcceleration > 0)
                _curAcceleration -= Time.deltaTime * 10;

            if (_curAcceleration < Acceleration)
                _curAcceleration += Time.deltaTime * 2;
            #endregion

            #region Stabilization
            float stabilizationForce = (transform.localEulerAngles.x*(StabilizationForce*0.01f) * TiltSpeed);

            if (transform.localEulerAngles.x > MaxAngle && transform.localEulerAngles.x < 180f)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, -transform.right * stabilizationForce, Time.deltaTime);
            else if (transform.localEulerAngles.x < _minAngle && transform.localEulerAngles.x > 180f)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, transform.right * stabilizationForce, Time.deltaTime);

            stabilizationForce = (transform.localEulerAngles.z * (StabilizationForce * 0.01f) * TiltSpeed);

            if (transform.localEulerAngles.z > MaxAngle && transform.localEulerAngles.z < 180f)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, -transform.forward * stabilizationForce, Time.deltaTime);
            else if (transform.localEulerAngles.z < _minAngle && transform.localEulerAngles.z > 180f)
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, transform.forward * stabilizationForce, Time.deltaTime);

            if (_input[0] == 0 && _input[1] == 0)
            {
                if (transform.localEulerAngles.z < 360 && transform.localEulerAngles.z > 180)
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, transform.forward * ((360 - transform.localEulerAngles.z) * 0.1f), Time.deltaTime);
                if (transform.localEulerAngles.z > 0 && transform.localEulerAngles.z < 180)
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, -transform.forward * (transform.localEulerAngles.z * 0.1f), Time.deltaTime);
            }

            if (_input[2] == 0 && _input[3] == 0)
            {
                if (transform.localEulerAngles.x < 360 && transform.localEulerAngles.x > 180)
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, transform.right * ((360 - transform.localEulerAngles.x) * 0.1f), Time.deltaTime);
                if (transform.localEulerAngles.x > 0 && transform.localEulerAngles.x < 180f)
                    _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity, -transform.right * (transform.localEulerAngles.x * 0.1f), Time.deltaTime);
            }

            if (!_anyInput)
            {
                _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, Time.deltaTime * 0.2f);
            }
            #endregion
        }

        if (transform.position.y > MaxHeight)
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.down * (transform.position.y - MaxHeight), Time.deltaTime * 1f);

        _lastPos = transform.position;


	
	}

    public void RotateAround(float force)
    {
        if (CurEngineForce > 0)
            _rigidbody.angularVelocity = 
                Vector3.Lerp(_rigidbody.angularVelocity, new Vector3
                    (_rigidbody.angularVelocity.x, -force * (CurEngineForce / MaxEngineForce), _rigidbody.angularVelocity.z), Time.deltaTime);
        
    }

    public void EngineSlowDown(float value)
    {
        if (CurEngineForce > MaxEngineForce * 0.75f)
            CurEngineForce -= Time.deltaTime * (MaxEngineForce * value);
    }

    public float GetEngineForceValue()
    {
        return CurEngineForce/MaxEngineForce;
    }


}
