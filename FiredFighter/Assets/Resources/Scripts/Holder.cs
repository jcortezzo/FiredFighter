using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public Item item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropWeapon()
    {
        if(item != null)
        {
            item.transform.parent = null;
            item.transform.rotation = Quaternion.identity;
        }
    }
}
