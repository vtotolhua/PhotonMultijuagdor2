using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Salud : Photon.MonoBehaviour {
    public GameObject ParticulasMuerte = null;
    private Transform ThisTransform = null;
    public bool QuieresDestruiralMorir = false;
    public TMP_Text SaludText = null;
    //private string SaludString = null;
    
    // Use this for initialization
	void Start () {
        ThisTransform = GetComponent<Transform>();
	}

    /*private void Update()
    {
        if (this.gameObject.tag == "jugador1" || this.gameObject.tag == "jugador2")
        {
         //   Debug.Log(this.gameObject.tag + " " + "puntos salud " + PuntosSalud);
            //SaludString = PuntosSalud.ToString();
            //salud.text = SaludString;
            SaludText.text = PuntosSalud.ToString();
        }
    }*/

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
                SendMessage("Destruye", SendMessageOptions.DontRequireReceiver);

                if (ParticulasMuerte != null) Instantiate(ParticulasMuerte, ThisTransform.position, ThisTransform.rotation);

                if (QuieresDestruiralMorir) {
                    if (this.gameObject.tag == "jugador1" || this.gameObject.tag == "jugador2") {
                        //this.gameObject.GetComponent<PlayerMov>().VelMov = 0;
                    }
                    else Destroy(gameObject);
                } 
            }
        }      
    }
    [SerializeField]
    private float _PuntosSalud = 100f;
}
