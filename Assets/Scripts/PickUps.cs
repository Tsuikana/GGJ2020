using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int hungerUp;
    public int thirstUp;
    public int partsUp;
    
    private GameManager gameMan;

    // Start is called before the first frame update
    public void Start()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        gameMan = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void Use()
    {
        Debug.Log("Picking up " + name);
        gameMan.partyMan.pickUp(thirstUp, hungerUp, partsUp);
        Destroy(gameObject);
    }
}
