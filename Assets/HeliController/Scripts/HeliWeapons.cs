using UnityEngine;
using System.Collections;

public class HeliWeapons : MonoBehaviour
{
    public GameObject[] WeaponsPivot; 

    public int Rockets; //Current ammo
    public int Bullets; //Current ammo

    public float RocketReloadTime;
    private float _curRocketReload;
    public float BulletReloadTime;
    private float _curBulletReload;

    private bool _rightRocketSide;

    private GameObject _heli;

    void Awake()
    {
        _heli = transform.parent.gameObject;
    }

	void Update () 
    {
        if (_curBulletReload > 0)
            _curBulletReload -= Time.deltaTime;
        if (_curRocketReload > 0)
            _curRocketReload -= Time.deltaTime;

        if (_heli.GetComponent<HeliController>().EnableControls)
        {
            if (Input.GetAxis("Fire1") == 1 && Bullets > 0)
            {
                if (_curBulletReload <= 0)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(WeaponsPivot[0].transform.position, WeaponsPivot[0].transform.forward, out hit))
                    {
                        StartCoroutine(BulletFlight(hit.point, hit.collider.gameObject,
                                                    (Vector3.Distance(WeaponsPivot[0].transform.position, hit.point))*
                                                    0.012f));
                    }

                    WeaponsPivot[0].GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                    WeaponsPivot[0].GetComponent<AudioSource>().Play();

                    WeaponsPivot[0].GetComponent<ParticleSystem>().Emit(1);
                    Bullets -= 1;
                    _curBulletReload = BulletReloadTime;
                }
            }

            if (Input.GetAxis("Fire2") == 1 && Rockets > 0)
            {
                if (_curRocketReload <= 0)
                {
                    Transform pivot;

                    if (_rightRocketSide)
                    {
                        pivot = WeaponsPivot[1].transform;

                        WeaponsPivot[1].GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
                        WeaponsPivot[1].GetComponent<AudioSource>().Play();

                        WeaponsPivot[1].GetComponent<ParticleSystem>().Emit(1);
                        _rightRocketSide = !_rightRocketSide;

                    }
                    else
                    {
                        pivot = WeaponsPivot[2].transform;

                        WeaponsPivot[2].GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
                        WeaponsPivot[2].GetComponent<AudioSource>().Play();

                        WeaponsPivot[2].GetComponent<ParticleSystem>().Emit(1);
                        _rightRocketSide = !_rightRocketSide;
                    }

                    RaycastHit hit;
                    if (Physics.Raycast(pivot.position, pivot.forward, out hit))
                    {
                        StartCoroutine(RocketFlight(hit.point, hit.collider.gameObject,
                                                    (Vector3.Distance(pivot.position, hit.point))*0.018f));
                    }

                    Rockets -= 1;
                    _curRocketReload = RocketReloadTime;

                }
            }
        }

    }

    IEnumerator BulletFlight(Vector3 point, GameObject target, float time)
    {
        yield return new WaitForSeconds(time);

        //target.GetComponent<Enemy>().Damage(100); //Enter your code here!

        if (target.GetComponent<Rigidbody>() != null)
        {
            target.GetComponent<Rigidbody>().AddForce(_heli.transform.forward * 300);
        }
    }

    IEnumerator RocketFlight(Vector3 point, GameObject target, float time)
    {
        yield return new WaitForSeconds(time);

        //target.GetComponent<Enemy>().Damage(500); //Enter your code here!

        if (target.GetComponent<Rigidbody>() != null)
        {
            target.GetComponent<Rigidbody>().AddForce(_heli.transform.forward * 600);
        }

        Collider[] colliders = Physics.OverlapSphere(point, 10);
        foreach (Collider hit in colliders)
        {
            if (hit.GetComponent<Rigidbody>())
                hit.GetComponent<Rigidbody>().AddExplosionForce(300, point, 10, 3.0F);

        }
    }
}
