using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CuentaMonedas : MonoBehaviour {

    public TMP_Text NumCuarzosP1;
    public TMP_Text NumCuarzosP2;
    public TMP_Text TotCuarzos;
    
    // Use this for initialization
    void Start () {
        //NumCuarzos = GetComponentInChildren<TMP_Text>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Monedas>()) {
            Monedas mone = other.GetComponent<Monedas>();
            NumCuarzosP1.text = mone.MonedasP1Text;
            NumCuarzosP2.text = mone.MonedasP2Text;
            TotCuarzos.text = Monedas.NumMonedas.ToString();
            /*
            if (this.gameObject.CompareTag("jugador1"))
            {
                NumCuarzos.text = mone.MonedasP1Text;
            }

            if (this.gameObject.CompareTag("jugador2"))
            {
                NumCuarzos.text = mone.MonedasP2Text;
            }*/
        }
    }
}
