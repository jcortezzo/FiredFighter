using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float lifespan = 20f;
    protected float timer;
    // Start is called before the first frame update
    void Start()
    {
        lifespan = Random.Range(lifespan - 5, lifespan + 10);
        timer = lifespan;
        LevelManager.Instance.smoke++;
        if(LevelManager.Instance.smoke >= LevelManager.SMOKE_CAP)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //}
}
