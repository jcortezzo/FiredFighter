using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Breakable, IInteractable, IFlammable
{
    public void Burn()
    {
        
    }

    public int SmokeNumber()
    {
        return 6;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }

}
