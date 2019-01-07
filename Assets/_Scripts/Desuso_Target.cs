using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Photon.MonoBehaviour {

    public float Salud = 50.0f;
    private Vector3 posicionVe;
    public PhotonView PviewTarget;
    private Vector3 selfPos;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bala")) {
                    
        }
        
    }*/

    /*public void Update()
    {
        if (PviewTarget.isMine)
        {
            posicionVe = gameObject.transform.position;
            transform.position = posicionVe;
        }
        else {
            smoothNetMovement();
        }

    }*/

    public void impacto(float dano) {
        Salud -= dano;
        Debug.Log(Salud);
        if (Salud <= 0) {
            destruye();
        }
    }

    private void destruye() {
        Destroy(gameObject);
    }

    /*private void smoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
        }

    }*/

}
