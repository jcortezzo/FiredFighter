using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public static GlobalValues Instance;

    public List<GameObject> onFireObjects;
    public List<GameObject> onWaterObjects;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
