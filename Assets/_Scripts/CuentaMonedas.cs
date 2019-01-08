using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CuentaMonedas : MonoBehaviour {

    public TMP_Text NumCuarzosP1;
    public TMP_Text NumCuarzosP2;
    public TMP_Text TotCuarzos;
    private float CheckMonedas = 0.5f;
    private Monedas Mone;


    // Use this for initialization
    private void Awake()
    {
        TotCuarzos.text = Monedas.NumMonedas.ToString();
        NumCuarzosP1.text = Monedas.MonedasP1.ToString();
        NumCuarzosP2.text = Monedas.MonedasP2.ToString();
    }


    private void Update()
    {
        NumCuarzosP1.text = Monedas.MonedasP1.ToString();
        NumCuarzosP2.text = Monedas.MonedasP2.ToString();
        TotCuarzos.text = Monedas.NumMonedas.ToString();
    }
}
