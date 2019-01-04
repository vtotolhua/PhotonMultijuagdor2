using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibeDanio : MonoBehaviour {

    //Daño por impacto
    public float PuntosDanio = 10f;


	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Salud sal = other.gameObject.GetComponent<Salud>();
        if (sal == null) return;
        sal.PuntosSalud -= PuntosDanio;
    }
}
