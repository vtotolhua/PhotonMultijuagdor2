using UnityEngine;
using System.Collections;

public class DemoScene : MonoBehaviour {


    private Rect _windowRect = new Rect(20, 20, 120, 50);

    private bool _showWindow;

    private GameObject _heli;

    private bool[] boolbalues = new bool[2];
    private float[] values = new float[9];

    void Awake()
    {
        _heli = GameObject.Find("Helicopter");

        values[0] = _heli.GetComponent<Rigidbody>().mass;
        values[1] = _heli.GetComponent<HeliController>().MaxHeight;
        values[2] = _heli.GetComponent<HeliController>().MaxSpeed;
        values[3] = _heli.GetComponent<HeliController>().MaxAngle;
        values[4] = _heli.GetComponent<HeliController>().MaxEngineForce;
        values[5] = _heli.GetComponent<HeliController>().Acceleration;
        values[6] = _heli.GetComponent<HeliController>().RotationSpeed;
        values[7] = _heli.GetComponent<HeliController>().TiltSpeed;
        values[8] = _heli.GetComponent<HeliController>().StabilizationForce;
        boolbalues[0] = _heli.GetComponent<HeliController>().EnableControls;
        boolbalues[1] = _heli.GetComponent<HeliController>().MouseControl;

    }

    void OnGUI()
    {
        _windowRect = GUI.Window(0, _windowRect, DoMyWindow, "Parameters");
    }

    void DoMyWindow(int windowID)
    {
        if (!_showWindow)
        {
            if (GUILayout.Button("Show"))
            {
                _windowRect = new Rect(20, 20, 300, 570);
                _showWindow = true;
            }
        }
        else
        {
            if (GUILayout.Button("Hide"))
            {
                _windowRect = new Rect(20, 20, 120, 50);
                _showWindow = false;
            }

            _heli.GetComponent<HeliController>().EnableControls = GUILayout.Toggle(_heli.GetComponent<HeliController>().EnableControls, " Enable controls");
            _heli.GetComponent<HeliController>().MouseControl = GUILayout.Toggle(_heli.GetComponent<HeliController>().MouseControl, " Mouse control");
            GUILayout.BeginHorizontal();
            _heli.GetComponent<Rigidbody>().mass = GUILayout.HorizontalSlider(_heli.GetComponent<Rigidbody>().mass, 30, 200);
            GUILayout.Label(" Mass");
            GUILayout.Label(_heli.GetComponent<Rigidbody>().mass.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().MaxHeight = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().MaxHeight, 10, 200);
            GUILayout.Label(" Maximum height");
            GUILayout.Label(_heli.GetComponent<HeliController>().MaxHeight.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().MaxSpeed = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().MaxSpeed, 1, 200);
            GUILayout.Label(" Maximum speed");
            GUILayout.Label(_heli.GetComponent<HeliController>().MaxSpeed.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().MaxAngle = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().MaxAngle, 1, 80);
            GUILayout.Label(" Maximum angle");
            GUILayout.Label(_heli.GetComponent<HeliController>().MaxAngle.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().MaxEngineForce = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().MaxEngineForce, 1, 5000);
            GUILayout.Label(" Maximum engine force");
            GUILayout.Label(_heli.GetComponent<HeliController>().MaxEngineForce.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().Acceleration = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().Acceleration, 1, 200);
            GUILayout.Label(" Horizontal acceleration");
            GUILayout.Label(_heli.GetComponent<HeliController>().Acceleration.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().RotationSpeed = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().RotationSpeed, 0.1f, 50);
            GUILayout.Label(" Rotation speed");
            GUILayout.Label(_heli.GetComponent<HeliController>().RotationSpeed.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().TiltSpeed = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().TiltSpeed, 0.1f, 10);
            GUILayout.Label(" Tilt speed");
            GUILayout.Label(_heli.GetComponent<HeliController>().TiltSpeed.ToString("00.00"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _heli.GetComponent<HeliController>().StabilizationForce = GUILayout.HorizontalSlider(_heli.GetComponent<HeliController>().StabilizationForce, 0f, 5);
            GUILayout.Label(" Stabilization force");
            GUILayout.Label(_heli.GetComponent<HeliController>().StabilizationForce.ToString("00.00"));
            GUILayout.Label("");
            GUILayout.EndHorizontal();
            if (GUILayout.Button("By default"))
            {
                RestoreParam();
            }
            GUILayout.Label("");
            GUILayout.Label("[ W / S ] - Up / Down");
            GUILayout.Label("[ A / D ] - Rotate left / Rotate right");
            GUILayout.Label("[ Num8 / Num2 ] - Tilt forward / back");
            GUILayout.Label("[ Num4 / Num6 ] - Tilt left / right");
            GUILayout.Label("[ LMB or Ctrl / RMB or Atl ] - Fire1 / Fire2");
            GUILayout.Label("");
            GUILayout.Label("[ T ] - Lock / unlock cursor");
            GUILayout.Label("[ R ] - Reload scene");
        }
    }

    public void RestoreParam()
    {
        _heli.GetComponent<Rigidbody>().mass = values[0];
        _heli.GetComponent<HeliController>().MaxHeight = values[1];
        _heli.GetComponent<HeliController>().MaxSpeed = values[2];
        _heli.GetComponent<HeliController>().MaxAngle = values[3];
        _heli.GetComponent<HeliController>().MaxEngineForce = values[4];
        _heli.GetComponent<HeliController>().Acceleration = values[5];
        _heli.GetComponent<HeliController>().RotationSpeed = values[6];
        _heli.GetComponent<HeliController>().TiltSpeed = values[7];
        _heli.GetComponent<HeliController>().StabilizationForce = values[8];
        _heli.GetComponent<HeliController>().EnableControls = boolbalues[0];
        _heli.GetComponent<HeliController>().MouseControl = boolbalues[1];
    }

    // Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);
        if (Input.GetKeyDown(KeyCode.T))
            Cursor.lockState = CursorLockMode.Locked;
	
	}
}
