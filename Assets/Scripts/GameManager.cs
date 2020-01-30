using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CursorManager cursMan;
    public CharacterManager charMan;
    
    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        cursMan = GetComponent<CursorManager>();
        charMan = GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
