using UnityEngine;
using System.Collections;

public class HeliSounds : MonoBehaviour
{
    private float _heliEngineForceValue;

    private GameObject _heli;

    void Awake()
    {
        _heli = transform.parent.gameObject;
    }

    void Start()
	{
        
	}
	
	void Update ()
	{
	    _heliEngineForceValue = _heli.GetComponent<HeliController>().GetEngineForceValue();

        if (_heliEngineForceValue < 0.4f)
        {
            gameObject.GetComponent<AudioSource>().volume = _heliEngineForceValue;
        }
        else gameObject.GetComponent<AudioSource>().volume = _heliEngineForceValue;

        gameObject.GetComponent<AudioSource>().pitch = 0.1f + _heliEngineForceValue;
	}
}
