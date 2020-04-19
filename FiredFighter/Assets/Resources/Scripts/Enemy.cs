using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : LivingEntity
{
    //private Rigidbody2D rb;
    //public float speed;
    //public float health;
    //public Weapon primary;
    //public Weapon secondary;
    //public Vector3 direction;
    //public Transform weaponHolder;
    //SpriteRenderer sr;
    //SpriteRenderer weaponSR;
    //bool flip = false;
    //public LevelManager levelManager;

    [SerializeField] public FieldOfView fov;

    Path path;
    Seeker seeker;
    int currentWayPoint = 0;
    bool reachEndOfPath;
    float nextWaypointDistance = 3f;

    public Player player;
    public GameObject target;
    public float sight;
    public float attackingDistance;
    public float attackSpeed = 0.2f;
    public float attackTimer = 0;
    public int TargetSize { get { return targets.Count;  }}
    public ISet<GameObject> targets;
    public bool detectPlayer;
    //public ISet<LivingEntity> friendlyTargets;


    //public List<EffectProbabilityGroup> effectProbability;
    //private EffectRangeSearch effectRangeSearch;

    //private const string WEAPON_GIVER_LOC = "Prefabs/Weapons/WeaponGiver";
    //private const string STAT_GIVER_LOC = "Prefabs/Stats/StatGiver";
    //public float damage;
    //public float hitstun = 0;
    //private float lastHitTime;
    //Animator anim;
    // Start is called before the first frame update
    private float initalDamage;
    private bool projectileWeapon;

    private float WeaponScale(float initalDamage, float x)
    {
        float xStart = 0;
        float yStart = 0.75f; // starting damage
        float xEnd = 6; // at level 6, enemy will be back at normal damage
        float yEnd = initalDamage;

        float m = (yEnd - yStart) / (xEnd - xStart);
        float b = yStart;

        return m * x + b;
    }
    protected override void Start()
    {
        base.Start();

        // Scaling damage for version 5


        //player = LevelManager.Instance.player;
        //alignment = Alignment.FOE;
        targets = new HashSet<GameObject>();
        //friendlyTargets = new HashSet<LivingEntity>();
        seeker = this.gameObject.AddComponent<Seeker>();
        //seeker.drawGizmos = true;

        //GameObject child = new GameObject();
        //child.transform.parent = this.transform;
        //child.transform.localPosition = new Vector3(0, 0, 0);
        //child.AddComponent<EnemySight>();
        //CircleCollider2D collider = child.AddComponent<CircleCollider2D>();
        //collider.radius = sight;
        //collider.isTrigger = true;
        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            if (target != null)
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    public override void Update()
    {
        if (health <= 0)
        {
            EndLife();
            //levelManager.UpdateUI();
        }
        float angle = GetAngleFromVectorFloat(direction);
        //fov.SetOrigin(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)));//transform.position);
        //fov.SetAimDirection(direction);
        float offset = 0.75f;
        float x = transform.position.x + (offset * Mathf.Cos(angle));
        float y = transform.position.y + (offset * Mathf.Sin(angle));
        fov.SetOrigin(new Vector3(x, y, transform.position.z));
        //fov.SetOrigin(transform.position);
        fov.SetAimDirection(direction);
        if (fov.Spot(player.gameObject))
        {
            //Debug.Log("player detected!");
        }
        base.Update();
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public virtual void EndLife()
    {
        //DropStat();
        //GlobalValues.Instance.money += speed * 50;
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        //if (hitstun > 0) return;
        //Debug.Log("move pleess");
        if (target == null || path == null) return;
        //Debug.Log("move pleess 2");
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }else
        {
            reachEndOfPath = false;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.y - this.transform.position.y, 2) +
                                    Mathf.Pow(target.transform.position.x - this.transform.position.x, 2));
        if (IsFriendTargetingPlayer() && distance < 2)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (distance <= sight)
        {
            Vector2 aStarDirection = (path.vectorPath[currentWayPoint] - this.transform.position).normalized;
            rb.velocity = aStarDirection * speed;// * Time.deltaTime;
        } else
        {
            rb.velocity = Vector2.zero;//new Vector2();
        }

        float dis = Vector2.Distance(path.vectorPath[currentWayPoint], this.transform.position);
        if(dis < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }


    public override void GetDirectionalInput()
    {
        //if (weaponHolder.primary != null && weaponHolder.primary.attacking || target == null) return;
        //direction = (target.transform.position - this.transform.position).normalized;
        //if (weaponHolder.primary != null && !weaponHolder.primary.attacking)
        //{
        //    weaponHolder.primary.direction = direction;
        //}
    }

    public override bool IsAttacking()
    {
        //if (attackTimer > 0) return false;
        //attackTimer = attackSpeed;
        if (target == null) return false;
        float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.y - this.transform.position.y, 2) +
                                    Mathf.Pow(target.transform.position.x - this.transform.position.x, 2));
        return distance < attackingDistance && !IsFriendTargetingPlayer();
    }

    public void SetTarget(GameObject go)
    {
        target = go;
    }

    public bool IsFriendTargetingPlayer()
    {
        return false;
        //return alignment == Alignment.FRIEND && target == player;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
