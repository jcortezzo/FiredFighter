using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float health = 1f;
    public int size;  // how much wood it produces

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        // make wood
    }

    public void TakeHit(float damage, float hitstun)
    {
        health -= damage;
        //this.hitstun = hitstun;
    }
}
