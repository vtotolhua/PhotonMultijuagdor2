using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruyeObjeto : MonoBehaviour {

    public float tiempoDestruccion = 2f;
	// Use this for initialization
	void Start () {
        Invoke("Destruye", tiempoDestruccion);
	}

    //Funcion de destruir
    void Destruye()
    {
        Destroy(gameObject);
    }
}
