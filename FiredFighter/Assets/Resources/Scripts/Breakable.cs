using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float health = 1f;
    public int size;  // how much wood it produces
    public GameObject woodPrefab;
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
        Vector3 currentPosition = this.transform.position;
        for(int i = 0; i < size; i++)
        { 
            Instantiate(woodPrefab, currentPosition + (i == 0 ? Vector3.zero : new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0)), 
                        Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void TakeHit(float damage, float hitstun)
    {
        Debug.Log("take damange");
        health -= damage;
        //this.hitstun = hitstun;
    }
}
