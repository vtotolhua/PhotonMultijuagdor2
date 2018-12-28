using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour {

    public static int NumMonedas;
    public static int MonedasP1 = 0;
    public static int MonedasP2 = 0;
    private PhotonView PV;
    

    // Use this for initialization
    void Start () {
        ++Monedas.NumMonedas;
        Debug.Log("numero de monedas " + NumMonedas);
        PV = gameObject.GetPhotonView();
	}

    private void Update()
    {
        transform.Rotate(Vector3.forward * 30 * Time.deltaTime );
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision con " + other.tag);

        if (other.CompareTag("jugador1")) {
            MonedasP1++;
            Destroy(gameObject);
            Debug.Log("Monedas jugador 1 " + MonedasP1);
            Debug.Log("numero de monedas" + NumMonedas);
        }

        if (other.CompareTag("jugador2"))
        {
            MonedasP2++;
            Destroy(gameObject);
            Debug.Log("Monedas jugador 2 " + MonedasP2);
            Debug.Log("numero de monedas" + NumMonedas);
        }
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
        Debug.Log("Monedas jugador 1 " + MonedasP1);
        Debug.Log("Monedas jugador 2 " + MonedasP2);
        if (MonedasP1 > MonedasP2) {
            Debug.Log("Ganador es Jugador 1 ");
        }

        if (MonedasP1 < MonedasP2)
        {
            Debug.Log("Ganador es Jugador 2 ");
        }
    }

}
