using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{


    private Transform target;

    [Header("Tower Attributes")]

    private float range = 15f;
    public string enemyTag = "Enemy";

    public Transform pivot;
    public float rotationSpeed = 5f;

    [Header("Fire Attributes")]

    public float fireRate = 1f;
    public float fireCD = 0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(pivot.rotation,lookRotation,Time.deltaTime * rotationSpeed).eulerAngles;
        pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCD <= 0f)
        {
            Fire();
            fireCD = 1f/fireRate;
        }

        fireRate -= Time.deltaTime;
    }

    void Fire()
    {

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= range)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        } else
        {
            target = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
