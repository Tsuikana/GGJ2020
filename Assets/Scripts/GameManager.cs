using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager charMan;
    public CursorManager cursMan;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        charMan = GetComponent<CharacterManager>();
        cursMan = FindObjectOfType<CursorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
}
