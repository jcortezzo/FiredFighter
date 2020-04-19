using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Item, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        player.holder.Pickup(this.gameObject);
    }
}
