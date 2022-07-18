using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 15f;

    private Transform target;

    private int waveIdx = 0;

    public int level;
    public int hp;

    public Color level2;
    public Color level3;
    public int moneyMultiplier; // how much user earns per enemy

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

    public void SetLevel(int l)
    {
        level = l;
        hp = l;
        Renderer rend = GetComponent<Renderer>();
        if (level == 2)
        {
            rend.material.color = level2;
        }
        if (level == 3)
        {
            rend.material.color = level3;
        }
    }

    public void GetHit()
    {
        hp -= 1;
        if (hp == 0)
        {
            User.instance.GainMoney(level * moneyMultiplier);
            Destroy(gameObject);
            return;
        }
    }
}
