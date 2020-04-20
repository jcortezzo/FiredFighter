using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelHealth : MonoBehaviour
{
    public CommentPair[] commentPairs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class CommentPair
{
    public string text;
    public int percent;
}
