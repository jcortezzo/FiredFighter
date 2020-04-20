﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelHealth : MonoBehaviour
{
    public CommentPair[] commentPairs;
    public LevelManager level;
    public TextMeshProUGUI text;
    public Image healthImage;

    private int healthMax;
    private float percentage;
    // Start is called before the first frame update
    void Start()
    {
        level = LevelManager.Instance;
        healthMax = (int)level.houseHealth;
        percentage = (healthMax - level.houseHealth) / healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        percentage = (healthMax - level.houseHealth) / healthMax;
        healthImage.fillAmount = percentage;
        DisplayText();
    }

    void DisplayText()
    {
        for(int i = 0; i < commentPairs.Length; i++)
        {
            if(percentage >= commentPairs[i].percent / 100f)
            {
                text.text = commentPairs[i].text;
                
            } else
            {
                break;
            }
        }
    }
}

[System.Serializable]
public class CommentPair
{
    public string text;
    public int percent;
}