using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Breakable, IInteractable
{
 
    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }
}
