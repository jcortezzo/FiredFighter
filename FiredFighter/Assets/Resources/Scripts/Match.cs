using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : Tool
{
    private int charges = 1;
    private const float SHOOT_DISTANCE = 1f;
    [SerializeField] private Transform firepf;

    protected override void Update()
    {
        base.Update();
        if (charges <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Action()
    {
        if (!IsCD())
        {
            Debug.Log("its lit");
            actionCoolDown = maxCoolDown;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, SHOOT_DISTANCE);
            Vector3 firePos = hit.collider != null ? hit.collider.transform.position : transform.position + direction * SHOOT_DISTANCE;
            Instantiate(firepf, firePos, Quaternion.identity);
            charges--;
            return;
        }
        else
        {
            return;
        }
    }
}
