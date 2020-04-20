using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Player player;
    public float houseHealth = 100;
    public List<GameObject> onFireObjects;
    public List<GameObject> onWaterObjects;
    bool aflame = false;
    [SerializeField] private Transform winFirepf;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        //onFireObjects = new List<GameObject>();
        InvokeRepeating("BurnHouse", 1, 1);
        //InvokeRepeating("GrowFire", 10, 10);
    }

    void BurnHouse()
    {
        houseHealth -= Fire.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (houseHealth <= 0 && !aflame)
        {
            for (int i = 0; i < 50; i++)
            {
                float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
                float r = 50 * Mathf.Sqrt(Random.Range(0f, 1f));
                float x = r * Mathf.Cos(a);
                float y = r * Mathf.Sin(a);
                WinFire winFire = Instantiate(winFirepf, new Vector3(x, y, transform.position.z), Quaternion.identity).GetComponent<WinFire>();
                //smoke.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * 0.5f;
            }
            aflame = true;
        }
    }
}
