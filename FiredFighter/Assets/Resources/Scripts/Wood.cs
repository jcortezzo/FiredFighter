using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IInteractable, IFlammable
{

    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }

    public void Burn()
    {
        Destroy(this.gameObject);
    }

    public int SmokeNumber()
    {
        return 3;
    }

    public int FlameIncreaseNumber()
    {
        return 1;
    }

    public bool ShowPrompt()
    {
        return true;
    }
}
