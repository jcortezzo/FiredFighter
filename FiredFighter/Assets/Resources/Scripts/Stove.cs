using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{

    public Gas gasPref;

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
        
    }

    IEnumerator Smoke(IFlammable flammable)
    {
        int cycles = 5;
        for (int i = 0; i < cycles; i++)
        {
            //CreateSmoke(flammable);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
