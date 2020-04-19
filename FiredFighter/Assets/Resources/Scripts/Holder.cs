using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public GameObject item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup(GameObject go)
    {
        if (item != null) DropItem();
        item = go;
        go.transform.parent = this.transform;
        go.transform.localPosition = new Vector3(1f, 0, transform.position.z);
    }

    public void DropItem()
    {
        if(item != null)
        {
            item.transform.parent = null;
            item.transform.rotation = Quaternion.identity;
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, -1);
        }
    }
}
