using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private BoxCollider2D bc2d;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        open = true;
        bc2d.isTrigger = true;
    }

    public void Close()
    {
        open = false;
        bc2d.isTrigger = false;
    }

    public bool IsOpen()
    {
        return open;
    }

    public void Interact(Player player)
    {
        Debug.Log("Door state changed");
        if (!open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
