using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : Photon.MonoBehaviour {

    public bool devTesting = false;
    public PhotonView photonView;
    public float VelMov;
    private float translation, straffe, jumpforce;
    private Vector3 RecibePosicion;
    private Quaternion RecibeRotacion;
    private GameObject sceneCam;
    public GameObject plCam;

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
