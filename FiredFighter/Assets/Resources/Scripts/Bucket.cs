using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Tool
{
    //public int water = 5;
    public Fire fire;

    public override void Action()
    {
        if (!IsCD())
        {
            actionCoolDown = maxCoolDown;
            if(fire != null)// && water > 0)
            {
                fire.Extinguish(1);
                //water--;
            }
            return;
        }
        else
        {
            return;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fire f = collision.gameObject.GetComponent<Fire>();
        if (f != null) fire = f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        Fire f = collision.gameObject.GetComponent<Fire>();
        if (f != null) fire = f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (fire == collision.gameObject.GetComponent<Fire>())
        {
            fire = null;
        }
    }
}
