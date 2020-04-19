using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public float actionCoolDown;
    public float maxCoolDown = 2;
    public bool action;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCoolDown > 0)
        {
            actionCoolDown -= Time.deltaTime;
        }
    }

    public abstract void Action();

    public bool IsCD()
    {
        return actionCoolDown > 0;
    }
}
