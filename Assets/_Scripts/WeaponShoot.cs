using UnityEngine;


public class WeaponShoot : Photon.MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject Bala;
   
    public void Update()
    {
               if (OVRInput.GetDown(OVRInput.Button.One))
            {
                PhotonNetwork.Instantiate(Bala.name, SpawnPoint.transform.position, SpawnPoint.transform.rotation, 0);
            }
    }
}