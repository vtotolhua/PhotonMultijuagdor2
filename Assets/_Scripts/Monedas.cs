using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : Photon.MonoBehaviour {

    public static int NumMonedas;
    public static int MonedasP1 = 0;
    public static int MonedasP2 = 0;
    private PhotonView PV;
    private Vector3 Recibeposicion;
    private Quaternion RecibeRotacion;

    // Use this for initialization
    void Start () {
        ++Monedas.NumMonedas;
        Debug.Log("numero de monedas " + NumMonedas);
        PV = gameObject.GetPhotonView();
	}

    private void Update()
    {
        if (PV.isMine) {
            transform.Rotate(Vector3.forward * 30 * Time.deltaTime);
        }
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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //poseemos el objeto mandamos la informacion a los demas juagdores.
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
        }
        //si no poseemos el objeto, enviamos la posicion del objeto a los demas jugadores
        else {
            Recibeposicion = (Vector3)stream.ReceiveNext();
            RecibeRotacion = (Quaternion)stream.ReceiveNext();

        }


    }


}
