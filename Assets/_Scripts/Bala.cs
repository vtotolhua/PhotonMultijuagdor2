using UnityEngine;

public class Bala : Photon.MonoBehaviour {
    
    public float FuerzaBala = 80;
    private Vector3 selfPos;
    private Quaternion selfRot;
    public PhotonView photonViewBala;
    public float PuntosDanio = 10.0f;
    private ParticleSystem EfectoBala = null;

    private void Awake()
    {
        Destroy(gameObject, 3.0f);
        EfectoBala = GetComponent<ParticleSystem>();
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
        Salud sal = other.gameObject.GetComponent<Salud>();
        if (sal == null) return;
        sal.PuntosSalud -= PuntosDanio;
        Debug.Log(sal.PuntosSalud);
        Destroy(gameObject);

        //otra forma de contabilizar el daño //
        /*    Target target = other.transform.GetComponent<Target>();
            Rigidbody tempRigid = other.GetComponent<Rigidbody>();

            if (target != null)
            {
                target.impacto(dano);
            }

            if (tempRigid != null)
            {
                tempRigid.AddForce(-Vector3.forward * FuerzaBala, ForceMode.Acceleration);
            }
        */
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
            stream.SendNext(transform.rotation);
        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
            selfRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
