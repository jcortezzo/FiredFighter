using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public int fillAmt = 1;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.onWaterObjects.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
