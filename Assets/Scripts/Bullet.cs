using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 10;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceTravel = speed * Time.deltaTime;

        if (dir.magnitude <= distanceTravel)
        {
            Hit();
            return;
        }

        transform.Translate(dir.normalized * distanceTravel, Space.World);
    }

    public void SetTarget (Transform t)
    {
        target = t;
    }

    void Hit()
    {
        Debug.Log("Hit!");
        GameObject efx = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(efx, 2f);
        Enemy e = target.GetComponent<Enemy>();
        e.GetHit();
        //Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
