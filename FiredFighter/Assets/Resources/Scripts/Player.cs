using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{
    //public ParticleSystem dustParticle;

    [SerializeField] private FieldOfView fov;

    private bool hidden;

    private float hiddenTime = 0.3f;
    private float hiddenTimer = 0f;

    private IInteractable interactable;
    public RectTransform panel;
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
        if (hiddenTimer > 0)
        {
            hiddenTimer -= Time.deltaTime;
        } else
        {
            Unhide();
        }
        if (Input.GetKeyDown(KeyCode.F) && holder.item != null) holder.DropItem();

        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            interactable.Interact(this);
            //interactable = null;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //fov.SetOrigin(rb.position);//transform.position);
        //fov.SetAimDirection(direction);
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
        anim.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
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
        IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        if (i != null)
        {
            panel.transform.position = Camera.main.WorldToScreenPoint(collision.gameObject.transform.position);
            interactable = i;
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        if (i != null)
        {
            panel.transform.position = Camera.main.WorldToScreenPoint(collision.gameObject.transform.position);
            interactable = i;
        }
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        if (i == interactable)
        {
            panel.transform.position = new Vector3(1000, 1000, 1000);
            interactable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        if (i != null)
        {
            //panel.transform.position = Camera.main.WorldToScreenPoint(collision.gameObject.transform.position);
            interactable = i;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        if (i == interactable)
        {
            //panel.transform.position = new Vector3(1000, 1000, 1000);
            interactable = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Smoke>() != null)
        {
            Hide();
        }
        //IInteractable i = collision.gameObject.GetComponent<IInteractable>();
        //if (i == interactable)
        //{
        //    panel.transform.position = Camera.main.WorldToScreenPoint(collision.gameObject.transform.position);
        //    interactable = null;
        //}
    }

    private void Hide()
    {
        //Debug.Log("hidden");
        hiddenTimer = hiddenTime;
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