using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Color hover;
    public Renderer rend;
    private Color original;
    private GameObject turret;
    public Vector3 towerOffset;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        original = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter() 
    {
        rend.material.color = hover;
    }

    void OnMouseExit()
    {
        rend.material.color = original;
    }

    void OnMouseDown()
    {
        if (turret == null)
        {
            GameObject buildTurret = User.instance.getTurret();
            if (buildTurret == null)
            {
                return;
            }
            turret = (GameObject) Instantiate(buildTurret, transform.position + towerOffset, transform.rotation);
            

        } else 
        {
            Turret t = turret.GetComponent<Turret>();
            t.upgrade();
        }
    }
}
