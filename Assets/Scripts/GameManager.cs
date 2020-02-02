using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager charMan;
    public PartyManager partyMan;
    public CursorManager cursMan;
    public UIManager uiMan;
    public Interaction interaction;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    void SetDefaults()
    {
        charMan = GetComponent<CharacterManager>();
        interaction = GetComponent<Interaction>();
        cursMan = FindObjectOfType<CursorManager>();
        partyMan = FindObjectOfType<PartyManager>();
        uiMan = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    void EndGame(string deathReason)
    {
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 50), deathReason);
    }
}
