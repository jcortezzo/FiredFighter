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
        
    }

    public int SmokeNumber()
    {
        return 3;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
