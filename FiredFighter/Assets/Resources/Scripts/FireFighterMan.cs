using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFighterMan : Enemy
{
    public override void Update()
    {
        base.Update();
        if(fov.GetPlayerTarget() != null)
        {
            SetTarget(fov.GetPlayerTarget());
        } else
        {

            GameObject closestFire = GetClosestFire();
            SetTarget(closestFire);
            //Debug.Log("Find closest fire: " + closestFire);
        }

    }

    private GameObject GetClosestFire()
    {
        List<GameObject> list = GlobalValues.Instance.onFireObjects;
        GameObject closest = null;
        float minDis = float.MaxValue;
        foreach(GameObject go in list)
        {
            if (closest == null) closest = go;
            else
            {
                if (go == null) continue;
                float dis = Vector3.Distance(this.transform.position, go.transform.position);
                if(dis < minDis)
                {
                    minDis = dis;
                    closest = go;
                }
            }
        }
        return closest;
    }

}
