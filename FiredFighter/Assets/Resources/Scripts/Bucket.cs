using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Tool
{
    public int water = 1;
    public const int WATER_MAX = 3;
    public Fire fire;
    public WaterSource waterSource;
    

    public override void Action()
    {
        if (!IsCD())
        {
            actionCoolDown = maxCoolDown;
            if(fire != null && !IsEmpty())// && water > 0)
            {
                fire.Extinguish(1);
                water--;
            }
            else if (waterSource != null && IsEmpty())
            {
                water = Mathf.Min(water + 1, WATER_MAX);
            }
            return;
        }
        else
        {
            return;
        }
    }
    
    public bool IsEmpty()
    {
        return water <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fire f = collision.gameObject.GetComponent<Fire>();
        if (f != null) fire = f;

        WaterSource ws = collision.gameObject.GetComponent<WaterSource>();
        if (ws != null) waterSource = ws;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        Fire f = collision.gameObject.GetComponent<Fire>();
        if (f != null) fire = f;

        WaterSource ws = collision.gameObject.GetComponent<WaterSource>();
        if (ws != null) waterSource = ws;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (fire == collision.gameObject.GetComponent<Fire>())
        {
            fire = null;
        }

        if (waterSource == collision.gameObject.GetComponent<WaterSource>())
        {
            waterSource = null;
        }
    }
}
