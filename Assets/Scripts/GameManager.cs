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
    public GirlsSpawner gSpawn;
    private Vector3 startPoint;
    private Vector3 endPoint;

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
        this.gameObject.GetComponent<MapGenerator>().GenerateMap(out startPoint, out endPoint);
        FindObjectOfType<MovementControllerMc>().gameObject.transform.position = new Vector3(startPoint.x + 0.05f, startPoint.y + 0.35f, 0f);
        gSpawn = GetComponent<GirlsSpawner>();
        gSpawn.SpawnGirls();
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
