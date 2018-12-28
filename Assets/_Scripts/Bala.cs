using UnityEngine;

public class Bala : Photon.MonoBehaviour {
    
    public float FuerzaBala = 20;
    private Vector3 selfPos;
    public PhotonView photonViewBala;
    public float dano = 10.0f;

    private void Awake()
    {
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        if (photonViewBala.isMine)
        {
            transform.Translate(Vector3.forward * FuerzaBala * Time.deltaTime);
            //Otras líneas para hacer que se mueva la bala, pero no funcionaron bien. 
            //CuerpoBala.velocity = transform.forward * FuerzaBala * Time.deltaTime;
            //gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * FuerzaBala * Time.deltaTime;    
        }
        else smoothNetMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.transform.GetComponent<Target>();
        Rigidbody tempRigid = other.GetComponent<Rigidbody>();

        if (target != null)
        {
            target.impacto(dano);
        }

        if (tempRigid != null)
        {
            tempRigid.AddForce(-Vector3.forward* FuerzaBala, ForceMode.Acceleration);
        }
    }

    private void smoothNetMovement()
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

    }
}
