using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    //public ParticleSystem dustParticle;

    [SerializeField] private FieldOfView fov;

    private bool hidden;
    
    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        GetDirectionalInput();
        //fov = GetComponent<FieldOfView>();
        //health = GlobalValues.Instance.playerHealth;
        //alignment = Alignment.FRIEND;
        //dustParticle = this.GetComponentInChildren<ParticleSystem>();
    }

    public override void Update()
    {
        GetDirectionalInput();
        base.Update();
        //fov.SetOrigin(transform.position);
        //fov.SetAimDirection(direction);
        //if (Input.GetKeyDown(KeyCode.R) && (weaponHolder.primary == null || !weaponHolder.primary.attacking)) weaponHolder.SwitchWeapon();
        if (Input.GetKeyDown(KeyCode.F) && holder.item != null) holder.DropItem();


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        fov.SetOrigin(rb.position);//transform.position);
        fov.SetAimDirection(direction);
        //Debug.Log(direction);
    }

    public override void GetDirectionalInput()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = this.transform.position.z;
        direction = (mousePos - this.transform.position).normalized;
        //Debug.Log(direction);
    }

    public override void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 newVelocity = new Vector2(horizontal, vertical);// * speed;
        newVelocity.Normalize();

        rb.velocity = newVelocity * speed;// * Time.deltaTime;
        
        float e = Mathf.Pow(10, -2);
        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) > e)
        {
            //dustParticle.Play();
        }
        else
        {
            //dustParticle.Stop();
        }
        //anim.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    public override bool IsAttacking()
    {
        //return false;
        return (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space));
        //&&
        //        weaponHolder.primary != null && !weaponHolder.primary.attacking;
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        if(collision.gameObject.tag == "Item")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                IInteractable interact = collision.gameObject.GetComponent<IInteractable>();
                interact.Interact(this);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Smoke>() != null)
        {
            Hide();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Smoke>() != null)
        {
            Unhide();
        }
    }

    private void Hide()
    {
        Debug.Log("hidden");
        sr.color = Color.gray;
        hidden = true;
    }

    private void Unhide()
    {
        sr.color = Color.white;
        hidden = false;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    private void OnDestroy()
    {
        Debug.Log("player instance destroyed");
    }
}