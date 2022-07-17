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
    public GameObject baseTurret;
    public GameObject headTurret;

    public Color level2;
    public Color level3;

    [Header("Fire Attributes")]

    public float fireRate = 0.5f;
    public float fireCD = 0f;

    public GameObject bulletPre;
    public Transform gun; 

    private int level = 1;

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

        fireCD -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject b = (GameObject) Instantiate(bulletPre, gun.position, gun.rotation);
        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
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

    public bool upgrade()
    {
        if (level < 3)
        {
            level += 1;
            Color newColor = (level == 2)? level2 : level3;
            var mats = baseTurret.GetComponent<Renderer>().materials;
            mats[0].color = newColor;
            mats = headTurret.GetComponent<Renderer>().materials;
            mats[1].color = newColor;
            return true;
        }
        return false;
    }
}
