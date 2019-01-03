using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salud : MonoBehaviour {
    public GameObject ParticulasMuerte = null;
    private Transform ThisTransform = null;
    public bool QuieresDestruiralMorir = false;

	// Use this for initialization
	void Start () {
        ThisTransform = GetComponent<Transform>();
	}

    //
	public float PuntosSalud
    {
        get
        {
            return _PuntosSalud;
        }
        set
        {
            _PuntosSalud = value;

            if (_PuntosSalud <= 0.0f)
            {
                SendMessage("Morir", SendMessageOptions.DontRequireReceiver);

                if (ParticulasMuerte != null) Instantiate(ParticulasMuerte, ThisTransform.position, ThisTransform.rotation);

                if (QuieresDestruiralMorir) Destroy(gameObject);
            }
        }      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PuntosSalud = 0;
    }

    [SerializeField]
    private float _PuntosSalud = 100f;
}
