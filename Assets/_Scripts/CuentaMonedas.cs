using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CuentaMonedas : MonoBehaviour {

    public TMP_Text NumCuarzosP1;
    public TMP_Text NumCuarzosP2;
    public TMP_Text TotCuarzos;
    private Monedas Mone;
    private PhotonView PhotonGUI;
    private Vector3 PosGui;
    private Quaternion RotGui;


    // Use this for initialization
    private void Awake()
    {
        PhotonGUI = GetComponent<PhotonView>();
        TotCuarzos.text = Monedas.NumMonedas.ToString();
        NumCuarzosP1.text = Monedas.MonedasP1.ToString();
        NumCuarzosP2.text = Monedas.MonedasP2.ToString();
    }

    private void Update()
    {
        if (PhotonGUI.isMine)
        {
            NumCuarzosP1.text = Monedas.MonedasP1.ToString();
            NumCuarzosP2.text = Monedas.MonedasP2.ToString();
            TotCuarzos.text = Monedas.NumMonedas.ToString();
        }
        else SmoothMovement();

    }

    void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, PosGui, Time.deltaTime * 8);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            PosGui = (Vector3)stream.ReceiveNext();
            RotGui = (Quaternion)stream.ReceiveNext(); 
        }
    }
}
