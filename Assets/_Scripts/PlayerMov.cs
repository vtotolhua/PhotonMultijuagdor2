using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMov : Photon.MonoBehaviour {

    public bool devTesting = false;
    public PhotonView photonView;
    public float VelMov;
    private float translation, straffe, jumpforce;
    private Vector3 RecibePosicion;
    private Quaternion RecibeRotacion;
    private GameObject sceneCam;
    public GameObject plCam;
    private Monedas mone;
    private Text TotMonedas;

    private void Awake () {
        if (!devTesting && photonView.isMine) {
            sceneCam = GameObject.Find("Main Camera");
            sceneCam.SetActive(false);
            plCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            if (GameObject.FindGameObjectWithTag("jugador1") != null) {
                gameObject.tag = "jugador2";
            }
        }
        TotMonedas = GetComponentInChildren<Text>();
    }
	
	void Update () {

        if (!devTesting)
        {
            if (photonView.isMine)
            {
                checkInput();
            }
            else smoothNetMovement();
        }
        else {
            checkInput();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Monedas>()) {
            //Debug.Log("Choco con monedas");
            //Debug.Log("Soy el jugador " + this.gameObject.tag);
            
    /////////*por revisar para mostrar el score en el GUI////////////////////////////////
            mone = other.GetComponent<Monedas>();
            TotMonedas.text = mone.MonedasP1Text;
            Debug.Log("total de monedas " + TotMonedas);
        }

        if (other.CompareTag("monedas"))
        {
            Debug.Log("Choco con monedas");
            Debug.Log("Soy el jugador " + this.gameObject.tag);
            mone = other.GetComponent<Monedas>();
            TotMonedas.text = mone.MonedasP1Text;
            Debug.Log("total de monedas " + TotMonedas.text);
            


            if (this.gameObject.tag == "jugador1")
            {
                TotMonedas.text = mone.MonedasP1Text;
                Debug.Log("moenas " + TotMonedas.text);
            }

            if (gameObject.tag == "jugador2")
            {
                TotMonedas.text = mone.MonedasP2Text;
                Debug.Log("Moenas " + TotMonedas.text);
            }
        }
    }*/

    private void checkInput() {
        translation = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal") * VelMov;
        straffe = Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical") * VelMov;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
 
        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void smoothNetMovement() {
        transform.position = Vector3.Lerp(transform.position, RecibePosicion, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
        }
        else {
            RecibePosicion = (Vector3)stream.ReceiveNext();
            RecibeRotacion = (Quaternion)stream.ReceiveNext();
        }
    }
}
