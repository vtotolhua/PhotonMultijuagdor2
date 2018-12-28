using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour {

    public static int NumMonedas;
    public int MonedasP1;
    public int MonedasP2;

    // Use this for initialization
    void Start () {
        ++Monedas.NumMonedas;
	}
	
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnDestroy()
    {
        --Monedas.NumMonedas;

        if (NumMonedas <= 0) {
            ComparPlayers();
        }
    }

    public void ComparPlayers() {
        Debug.Log("Ganador es ");
        
    }

}
