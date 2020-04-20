using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableStuff : Breakable, IInteractable, IFlammable
{
    public int smokeNumber;
    public int flameIncrease;

    public void Burn()
    {
        Destroy(this.gameObject);
    }

    public int SmokeNumber()
    {
        return smokeNumber;
    }

    public int FlameIncreaseNumber()
    {
        return flameIncrease;
    }


    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }

    public bool ShowPrompt()
    {
        return true;
    }
}
