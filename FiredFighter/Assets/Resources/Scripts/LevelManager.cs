using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Player player;
    public float houseHealth = 100;
    public List<GameObject> onFireObjects;
    public List<GameObject> onWaterObjects;
    bool aflame = false;
    [SerializeField] private Transform winFirepf;
    public float timeElapse;
    public float timeMax = 10f;
    public bool fireStarted = false;
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
        Jukebox.Instance.PlaySound(0);
        //InvokeRepeating("GrowFire", 10, 10);
    }

    void BurnHouse()
    {
        if (!fireStarted) return;
        houseHealth -= Fire.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireStarted && onFireObjects.Count == 0)
        {
            Fire.totalHealth = 0;
            //Lose();
        }
        if (houseHealth <= 0 && !aflame)
        {
            for (int i = 0; i < 50; i++)
            {
                float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
                float r = 20 * Mathf.Sqrt(Random.Range(0f, 1f));
                float x = r * Mathf.Cos(a);
                float y = r * Mathf.Sin(a);
                WinFire winFire = Instantiate(winFirepf, new Vector3(x, y, transform.position.z), Quaternion.identity).GetComponent<WinFire>();
                //smoke.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * 0.5f;
            }
            Jukebox.Instance.PlaySound(1);
            aflame = true;
        }
        
        if (fireStarted && Fire.totalHealth <= 0)
        {
            for (int i = 0; i < onFireObjects.Count; i++)
            {
                Destroy(onFireObjects[i]);
            }
        }

        if(houseHealth <= 0)
        {
            timeElapse += Time.deltaTime;
        }
        if(timeElapse >= timeMax)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        Fire.damage = 0;
        Fire.totalHealth = 20;
        Fire.numFires = 0;
        GameSceneManager.Instance.LoadNextLevel();
    }

    public void Lose()
    {
        Fire.damage = 0;
        Fire.numFires = 0;
        Fire.totalHealth = 20;
        SceneManager.LoadScene("Lose");
    }
}
