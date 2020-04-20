using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour, IFlammable
{
    public void Burn()
    {
        Destroy(this.gameObject);
    }

    public int FlameIncreaseNumber()
    {
        return 1;
    }

    public int SmokeNumber()
    {
        return 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
