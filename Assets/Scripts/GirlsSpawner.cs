using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlsSpawner : MonoBehaviour
{
    public int numOfGirls;
    public GameObject girlPrefab;
    public GameObject baseTilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        SetDefault();
        //SpawnGirls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDefault()
    {
    }

    public void SpawnGirls()
    {
        var rnd = new System.Random();
        System.Array roleList = System.Enum.GetValues(typeof(GirlStats.Roles));
        System.Array factionList = System.Enum.GetValues(typeof(GirlStats.Factions));

        for (int i = 0; i < numOfGirls; i++)
        {
            Vector3 location = new Vector3(0, 0, 0);
            var newGirl = Instantiate(girlPrefab, location, Quaternion.identity);
            var newGirlStats = newGirl.GetComponent<GirlStats>();
            newGirlStats.maxHappiness = 1;
            newGirlStats.hunting = Random.Range(0, 3);
            newGirlStats.gathering = Random.Range(0, 3);
            newGirlStats.scouting = Random.Range(0, 3);
            newGirlStats.mobility = Random.Range(0, 3);
            newGirlStats.hungerConsumed = Random.Range(0, 1);
            newGirlStats.thristinessConsumed = Random.Range(0, 1);
            newGirlStats.role = (GirlStats.Roles)roleList.GetValue(rnd.Next(roleList.Length));
            newGirlStats.faction = (GirlStats.Factions)factionList.GetValue(rnd.Next(factionList.Length));

            newGirl.GetComponent<GirlController>().SetDefaults();
        }
    }
}
