using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GirlsSpawner : MonoBehaviour
{
    public GameObject girlPrefab;
    public Tilemap tileMapCollection;
    public List<Vector3> tileWorldLocations;

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
        tileWorldLocations = new List<Vector3>();
    }

    public void SpawnGirls()
    {
        print("Spawning Girls.");
        Tilemap baseTilemap = GameObject.Find("Base Tilemap").GetComponent<Tilemap>();
        tileMapCollection = baseTilemap.GetComponent<Tilemap>();
        foreach (var pos in tileMapCollection.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tileMapCollection.HasTile(localPlace))
            {
                var isometricLocation = tileMapCollection.CellToWorld(localPlace);
                tileWorldLocations.Add(new Vector3(isometricLocation.x + 0.05f, isometricLocation.y + 0.35f, 0f));
            }
        }
        var rnd = new System.Random();
        System.Array roleList = System.Enum.GetValues(typeof(GirlStats.Roles));
        System.Array factionList = System.Enum.GetValues(typeof(GirlStats.Factions));

        int numOfGirls = tileWorldLocations.Count / 50;
        print(string.Format("Spawning {0} girls in this world of {1} tiles.", numOfGirls, tileWorldLocations.Count));

        for (int i = 0; i < numOfGirls; i++)
        {
            var location = tileWorldLocations[rnd.Next(tileWorldLocations.Count)];
            var newGirl = Instantiate(girlPrefab, location, Quaternion.identity);
            
            var newGirlStats = newGirl.GetComponent<GirlStats>();
            newGirlStats.maxHappiness = 1;
            newGirlStats.hunting = Random.Range(0, 3);
            newGirlStats.gathering = Random.Range(0, 3);
            newGirlStats.scouting = Random.Range(0, 3);
            newGirlStats.mobility = Random.Range(0, 3);
            newGirlStats.hungerConsumed = Random.Range(0, 1);
            newGirlStats.thristinessConsumed = Random.Range(0, 1);
            
            var newRole = (GirlStats.Roles)roleList.GetValue(rnd.Next(roleList.Length));
            newGirlStats.role = newRole;
            var newFaction = (GirlStats.Factions)factionList.GetValue(rnd.Next(factionList.Length));
            newGirlStats.faction = newFaction;
            newGirlStats.SetDefaults();
            var sprite = Resources.Load(string.Format("SpriteFolder/{0}_{1}_Sprite", newFaction.ToString(), newRole.ToString())) as Sprite;
            if (sprite)
            {
                newGirl.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            else
            {
                print(string.Format("{0}_{1}_Sprite not found.", newFaction.ToString(), newRole.ToString()));
            }
           
            var newAnimController = Resources.Load(string.Format("Controllers/{0}_{1}", newFaction.ToString(), newRole.ToString())) as RuntimeAnimatorController;
            if (newAnimController)
            {
                newGirl.GetComponent<Animator>().runtimeAnimatorController = Instantiate(newAnimController);
            }
            else
            {
                print(string.Format("{0}_{1} animation controller not found", newFaction.ToString(), newRole.ToString()));
            }

            newGirl.GetComponent<GirlController>().SetDefaults();
        }
    }
}
