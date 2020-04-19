using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Breakable, IInteractable, IFlammable
{
    public void Burn()
    {
        Destroy(this.gameObject);
    }

    public int SmokeNumber()
    {
        return 6;
    }

    public int FlameIncreaseNumber()
    {
        return 2;
    }


    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }

}
