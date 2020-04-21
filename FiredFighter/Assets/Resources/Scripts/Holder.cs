using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public GameObject item;
    public Tool tool;

    public Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        direction = GetVectorFromAngle(transform.rotation.z).normalized;
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void Pickup(GameObject go)
    {
        if (item != null) DropItem();
        item = go;
        go.transform.parent = this.transform;
        go.transform.localPosition = new Vector3(1f, 0, transform.position.z);
        go.GetComponent<Collider2D>().enabled = false;
        AstarPath.active.Scan();
    }

    public void PickupTool(Tool gameObject)
    {
        if (item != null) DropTool();
        tool = gameObject;
        gameObject.transform.parent = this.transform;
        gameObject.transform.localPosition = new Vector3(1f, 0, transform.position.z);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void DropTool()
    {

    }

    public void DropItem()
    {
        if(item != null)
        {
            item.transform.parent = null;
            item.transform.rotation = Quaternion.identity;
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, -1);
            item.GetComponent<Collider2D>().enabled = true;
            AstarPath.active.Scan();
            item = null;
        }
    }
}
