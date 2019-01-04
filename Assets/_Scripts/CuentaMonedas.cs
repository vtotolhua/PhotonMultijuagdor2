using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CuentaMonedas : MonoBehaviour {

    public static string NumMonedas;
    public Text NumMonedasUITex;
    public TMP_Text Num;

    
    // Use this for initialization
	void Start () {
        NumMonedasUITex = GetComponentInChildren<Text>();
        Num = GetComponentInChildren<TMP_Text>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Monedas>()) {
            Monedas mone = other.GetComponent<Monedas>();

            if (this.gameObject.CompareTag("jugador1")) {

                NumMonedas = mone.MonedasP1Text;
                NumMonedasUITex.text = NumMonedas;
                Num.text = NumMonedas;

                Debug.Log("GUIText " + NumMonedasUITex.text);
                Debug.Log("TextMesh" + Num);
            }

            if (this.gameObject.CompareTag("jugador2"))
            {
                NumMonedas = mone.MonedasP2Text;
                NumMonedasUITex.text = NumMonedas;
                Num.text = NumMonedas;

                Debug.Log("GUIText" + NumMonedasUITex.text);
                Debug.Log("TextMesh" + Num);
            }
        }
    }
}
