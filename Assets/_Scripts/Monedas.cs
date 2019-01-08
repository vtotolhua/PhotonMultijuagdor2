using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : Photon.MonoBehaviour {

    public static int NumMonedas;
    public static int MonedasP1 = 0;
    public static int MonedasP2 = 0;
    public string MonedasP1Text = "";
    public string MonedasP2Text = "";
    private PhotonView PV;
    private Vector3 Recibeposicion;
    private Quaternion RecibeRotacion;

    // Use this for initialization
    void Start () {
        ++Monedas.NumMonedas;
        PV = gameObject.GetComponent<PhotonView>();
	}

    private void Update()
    {
        if (PV.isMine)
        {
            transform.Rotate(Vector3.forward * 20 * Time.deltaTime);
        }
        //else smoothNetMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("jugador1")) {
            MonedasP1 = MonedasP1 + 1;
            MonedasP1Text = MonedasP1.ToString();
            Debug.Log("Monedas P1 " + MonedasP1Text);
            Destroy(gameObject);
        }

        if (other.CompareTag("jugador2"))
        {
            MonedasP2 = MonedasP2 + 1;
            MonedasP2Text = MonedasP2.ToString();
            Debug.Log("Monedas P2 " + MonedasP1Text);
            Destroy(gameObject);
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

        if (MonedasP1 > MonedasP2) {
            //Debug.Log("Ganador es Jugador 1 ");
        }

        if (MonedasP1 < MonedasP2)
        {
            //Debug.Log("Ganador es Jugador 2 ");
        }
    }

    /*private void smoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, Recibeposicion, Time.deltaTime * 8);
    }*/

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //poseemos el objeto mandamos la informacion a los demas juagdores.
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        //si no poseemos el objeto, enviamos la posicion del objeto a los demas jugadores
        else {
            Recibeposicion = (Vector3)stream.ReceiveNext();
            RecibeRotacion = (Quaternion)stream.ReceiveNext();
        }
    }
}
