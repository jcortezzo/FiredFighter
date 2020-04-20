using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    public int gasNumber = 3;
    public Gas gasPref;
    public bool isOn;
    public Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        isOn = !isOn;
        if (isOn) coroutine = StartCoroutine(Gas());
        else
        {
            if(coroutine != null) StopCoroutine(coroutine);
        }
        
    }

    private void CreateGas()
    {
        //Debug.Log("smoke!");
        for (int i = 0; i < gasNumber; i++)
        {
            Gas gas = Instantiate(gasPref, transform.position, Quaternion.identity).GetComponent<Gas>();
            gas.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * Random.Range(0.5f, 1f);
        }
    }

    IEnumerator Gas()
    {
        while(isOn)
        {
            CreateGas();
            yield return new WaitForSeconds(1.5f);
        }
       
    }
}
