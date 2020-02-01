using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject girlPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SetDefault();
        SpawnGirls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDefault()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawners");
    }

    public void SpawnGirls()
    {
        var rnd = new System.Random();
        System.Array roleList = System.Enum.GetValues(typeof(GirlStats.Roles));
        System.Array factionList = System.Enum.GetValues(typeof(GirlStats.Factions));

        foreach (var spawner in spawners)
        {
            var newGirl = Instantiate(girlPrefab, spawner.transform.position, Quaternion.identity);
            var newGirlStats = newGirl.GetComponent<GirlStats>();
            newGirlStats.maxHappiness = 1;
            newGirlStats.hunting = Random.Range(0, 3);
            newGirlStats.gathering = Random.Range(0, 3);
            newGirlStats.scouting = Random.Range(0, 3);
            newGirlStats.mobility = Random.Range(0, 3);
            newGirlStats.hunger = Random.Range(0, 2);
            newGirlStats.thristiness = Random.Range(0, 2);
            newGirlStats.role = (GirlStats.Roles)roleList.GetValue(rnd.Next(roleList.Length));
            newGirlStats.faction = (GirlStats.Factions)factionList.GetValue(rnd.Next(factionList.Length));

            newGirl.GetComponent<GirlController>().SetDefaults();
        }
    }
}
