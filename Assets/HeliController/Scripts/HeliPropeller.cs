using UnityEngine;
using System.Collections;

public class HeliPropeller : MonoBehaviour
{
    public enum Type
    {
        MainRotor, TailRotor
    }
    public Type RotorType;

    public GameObject HeliBlades;
    public GameObject HitParticles;
    public GameObject BladesBlur;

    private float _hitForce;
    private float _heliEngineForceValue;

    private bool _hit;

    private GameObject _heli;

    void Awake()
    {
        _heli = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger && _heliEngineForceValue > 0.3f)
        {
            _heli.GetComponent<HeliController>().HeliBladesHitting = true;
        }
    }

    void OnTriggerStay(Collider col)
	{
        if (!col.isTrigger)
        {
            if (_heliEngineForceValue > 0.3f)
            {
                _hit = true;
                HitParticles.GetComponent<ParticleSystem>().Play();
                if (!col.GetComponent<Rigidbody>())
                    _hitForce = 10;
                else
                {
                    if (col.GetComponent<Rigidbody>().mass < 10)
                        _hitForce = col.GetComponent<Rigidbody>().mass;
                }

                if (col.GetComponent<Rigidbody>() != null)
                {
                    col.GetComponent<Rigidbody>().AddForce((-transform.right * _heliEngineForceValue) * 100);
                    col.GetComponent<Rigidbody>().AddForce((Vector3.up * _heliEngineForceValue) * 100);
                }
            }
            if (_heliEngineForceValue < 0.5f)
                HitParticles.GetComponent<ParticleSystem>().Stop();
        }
	}

    void OnTriggerExit(Collider col)
    {
        if (!col.isTrigger)
        {
            _heli.GetComponent<HeliController>().HeliBladesHitting = false;
            _hit = false;
            HitParticles.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnCollisionStay(Collision col)
    {
        HitParticles.transform.position = col.contacts[0].point;
    }

    void FixedUpdate()
    {
        _heliEngineForceValue = _heli.GetComponent<HeliController>().GetEngineForceValue();

        if (_hitForce > 0)
        {
            _heli.GetComponent<HeliController>().RotateAround(_hitForce);
            _hitForce -= Time.deltaTime*6;
        }

        if (_hit)
            _heli.GetComponent<HeliController>().EngineSlowDown(0.6f);
        
        if (RotorType == Type.MainRotor)
            HeliBlades.transform.Rotate(0, (_heliEngineForceValue * 600) * Time.deltaTime, 0);
        else HeliBlades.transform.Rotate(0, 0, (_heliEngineForceValue * 600) * Time.deltaTime);
        

        if (BladesBlur != null)
            BladesBlur.GetComponent<Renderer>().material.color = new Color(1, 1, 1, _heliEngineForceValue - 0.2f);
    }
}
