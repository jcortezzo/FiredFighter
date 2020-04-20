using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Player player;
    public float houseHealth = 100;
    public List<GameObject> onFireObjects;
    public List<GameObject> onWaterObjects;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        //onFireObjects = new List<GameObject>();
        InvokeRepeating("BurnHouse", 1, 1);
        //InvokeRepeating("GrowFire", 10, 10);
    }

    void BurnHouse()
    {
        houseHealth -= Fire.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(houseHealth <= 0)
        {
            Win();
        }
        
    }

    private void Win()
    {
        
    }

    public void Lose()
    {
        SceneManager.LoadScene("Lose");
    }


}
