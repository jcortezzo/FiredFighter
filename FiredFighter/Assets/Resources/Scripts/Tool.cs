using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public float actionCoolDown;
    public float maxCoolDown = 2;
    public bool action;
    protected Vector3 direction;

    protected Holder holder;

    // Start is called before the first frame update
    void Start()
    {
        holder = GetComponentInParent<Holder>();
        holder.tool = this;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (actionCoolDown > 0)
        {
            actionCoolDown -= Time.deltaTime;
        }
    }

    protected virtual void FixedUpdate()
    {
        direction = holder.direction;
    }

    public abstract void Action();

    public bool IsCD()
    {
        return actionCoolDown > 0;
    }
}
