using UnityEngine;


public class WeaponShoot : Photon.MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject Bala;
   // public PhotonView photonViewweapon;
    private Vector3 selfPos;
    private Transform Deltaposs; 


    public void Update()
    {
     //   if (photonViewweapon.isMine) {

//            transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                PhotonNetwork.Instantiate(Bala.name, SpawnPoint.transform.position, SpawnPoint.transform.rotation, 0);
            }
       // }
         //else smoothNetMovement();
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