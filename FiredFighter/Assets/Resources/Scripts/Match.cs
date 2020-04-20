using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : Tool, IInteractable
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
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, SHOOT_DISTANCE);
            //Vector3 firePos = hit.collider != null ? transform.TransformPoint(hit.collider.transform.position) : transform.position + direction * SHOOT_DISTANCE;
            Vector3 firePos = transform.position + direction * SHOOT_DISTANCE;
            Fire f = Instantiate(firepf, new Vector3(firePos.x, firePos.y, -1), Quaternion.identity).GetComponent<Fire>();
            f.SetDamage(Fire.damage + 1);
            charges--;
            return;
        }
        else
        {
            return;
        }
    }

    public void Interact(Player player)
    {
        player.holder.PickupTool(this);
    }

    public bool ShowPrompt()
    {
        return true;
    }
}
