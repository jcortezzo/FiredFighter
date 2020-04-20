using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IInteractable
{
    public int level = 1;

    public static float damage = 0;
    public static int numFires = 0;
    public static float totalHealth = 20f;

    public static Dictionary<Vector3, Fire> firePositions = new Dictionary<Vector3, Fire>();
    
    private const float SPREAD_DISTANCE = 1f;
    private const int MAX_LEVEL = 7;

    public float splitTime = 10f;
    private float splitTimer;// = splitTime;

    private Animator animator;

    [SerializeField] private Transform smokepf;
    //[SerializeField] private Smoke smoke;

    
    // Start is called before the first frame update
    void Start()
    {
        if (firePositions.ContainsKey(transform.position))
        {
            Destroy(gameObject);
            return;
        }
        firePositions.Add(transform.position, this);
        //transform.localScale *= level;
        numFires++;
        splitTimer = splitTime;
        LevelManager.Instance.onFireObjects.Add(this.gameObject);

        animator = this.GetComponent<Animator>();
        InvokeRepeating("GrowFire", 10, 10);
    }

    public void SetDamage(float f)
    {
        damage = f;
    }

    private void wtf(string hngg)
    {
        //Debug.Log(hngg);
        
        foreach (KeyValuePair<Vector3, Fire> kvp in firePositions)
        {
            hngg += "{" + kvp.Key + ":, " + kvp.Value + "},";
        }
        Debug.Log(hngg);
    }

    void GrowFire()
    {
        wtf("before: ");
        int n = Split();
        wtf("after: ");
        level = Mathf.Min(level + 1, MAX_LEVEL);
        if (n > 0) damage += 1.0f / n;
    }

    // Update is called once per frame
    void Update()
    {
        if (level <= 0)
        {
            firePositions.Remove(transform.position);
            Destroy(this.gameObject);
        }
        animator.SetInteger("level", level);
    }

    protected int Split()
    {
        // instantiates all different positions that
        // the new fires can spread to
        Vector3[] positions = new Vector3[4];
        positions[0] = new Vector3(1, 0);  // right
        positions[1] = new Vector3(0, 1);  // up
        positions[2] = new Vector3(-1, 0); // left
        positions[3] = new Vector3(0, -1); // down

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] *= SPREAD_DISTANCE;// * level;
        }

        // instantiates fires in all four directions
        Fire[] fires = new Fire[4];
        for (int i = 0; i < fires.Length; i++)
        {
            Vector3 position = transform.position + positions[i];
            if (firePositions.ContainsKey(position))
            {
                continue;
            }
            fires[i] = Instantiate(this, position, Quaternion.identity);
            fires[i].level = level;
            LevelManager.Instance.onFireObjects.Add(fires[i].gameObject);
        }

        // count the number of fires that were
        // started successfully
        int count = 0;
        foreach (Fire f in fires)
        {
            if (f != null) count++;
        }
        return count;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            firePositions.Remove(transform.position);
            Destroy(this.gameObject);
        } else if(collision.gameObject.GetComponent<IFlammable>() != null)
        {
            IFlammable flammable = collision.gameObject.GetComponent<IFlammable>();
            BurnWood(flammable);
        }
    }

    private void OnDestroy()
    {
        //firePositions.Remove(transform.position);
        LevelManager.Instance.onFireObjects.Remove(gameObject);
    }

    public void Interact(Player player)
    {
        IFlammable wood = player.holder.item.GetComponent<IFlammable>();
        if ( wood != null)
        {
            Debug.Log("Bigger fire!!");
            BurnWood(wood);
        }
    }

    public void Extinguish(int damage)
    {
        level -= damage;
        totalHealth -= damage;
    }
    private void BurnWood(IFlammable flammable)
    {
        level += flammable.FlameIncreaseNumber();
        flammable.Burn();
        StartCoroutine(Smoke(flammable));
    }

    IEnumerator Smoke(IFlammable flammable)
    {
        int cycles = 5;
        for (int i = 0; i < cycles; i++)
        {
            CreateSmoke(flammable);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void CreateSmoke(IFlammable flammable)
    {
        //Debug.Log("smoke!");
        for (int i = 0; i < flammable.SmokeNumber(); i++)
        {
            Smoke smoke = Instantiate(smokepf, transform.position, Quaternion.identity).GetComponent<Smoke>();
            smoke.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * 0.5f;
        }
    }

    protected enum Directions {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
