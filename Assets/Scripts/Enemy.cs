using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 15f;

    private Transform target;

    private int waveIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        target =  WayPoints.points[0];
        return;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWAyPoint();
        }
    }

    void GetNextWAyPoint()
    {
        if (waveIdx >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            User.instance.LoseHP();
            return;
        }
        waveIdx++; 
        target = WayPoints.points[waveIdx];
    }
}
