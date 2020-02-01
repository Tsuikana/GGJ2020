using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int hungerUp;
    public int thirstUp;
    public int partsUp;

    public enum PickUpType { Chicken, Cabbage, Bucket_Water, Cactus, Hamburger, Soda, TV_Dinner, Gatorade, Parts };
    public PickUpType environmentType;
    
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
        gameMan.partyMan.PickUp(thirstUp, hungerUp, partsUp, environmentType);
        Destroy(gameObject);
    }
}
