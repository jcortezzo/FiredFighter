using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int level = 1;
    public static float damage = 1;
    public static int numFires = 0;
    public static HashSet<Vector3> firePositions = new HashSet<Vector3>();
    
    private const float SPREAD_DISTANCE = 1f;

    public float splitTime = 10f;
    private float splitTimer;// = splitTime;

    // Start is called before the first frame update
    void Start()
    {
        if (firePositions.Contains(transform.position)) Destroy(gameObject);
        firePositions.Add(transform.position);
        numFires++;
        splitTimer = splitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (splitTimer > 0)
        {
            splitTimer -= Time.deltaTime;
        }
        else
        {
            int n = Split();
            level++;
            if (n > 0) damage++;
            splitTimer = splitTime;
        }
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
            positions[i] *= SPREAD_DISTANCE;
        }

        // instantiates fires in all four directions
        Fire[] fires = new Fire[4];
        for (int i = 0; i < fires.Length; i++)
        {
            Vector3 position = transform.position + positions[i];
            if (firePositions.Contains(position)) continue;
            fires[i] = Instantiate(this, position, Quaternion.identity);
            fires[i].level = level;
            GlobalValues.Instance.onFireObjects.Add(fires[i].gameObject);
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    protected enum Directions {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
