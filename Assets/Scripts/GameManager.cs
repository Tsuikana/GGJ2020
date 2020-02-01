using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager charMan;
    public PartyManager partyMan;
    public CursorManager cursMan;
    public UIManager uiMan;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        charMan = GetComponent<CharacterManager>();
        cursMan = FindObjectOfType<CursorManager>();
        partyMan = FindObjectOfType<PartyManager>();
        uiMan = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
}
