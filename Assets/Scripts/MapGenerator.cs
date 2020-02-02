using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap[] tiles;
    public int mapIndex = 0;
    public Vector2 startPoint;

    //make random number range
    public int mapWidth = 100;
    public int mapLength = 100;
    public int regionMinSize = 5;
    public int regionMaxSize = 20;
    public int totalTileMin = 100;
    public int totalTileMax = 150;

    public float girlChance;
    public float forestHuntChance = 0.1f;
    public float forestGatherChance = 0.1f;
    public float forestWarmthChance = 0.1f;
    public float forestResourceChance = 0.2f;
    public float forestResourceMin = 0.02f;

    public float desertHuntChance = 0.05f;
    public float desertGatherChance = 0.15f;
    public float desertWarmthChance = 0f;
    public float desertResourceChance = 0.1f;
    public float desertResourceMin = 0.01f;

    public float cityHuntChance = 0.08f;
    public float cityGatherChance = 0.12f;
    public float cityWarmthChance = 0.07f;
    public float cityResourceChance = 0.15f;
    public float cityResourceMin = 0.02f;

    public float neonHuntChance = 0.07f;
    public float neonGatherChance = 0.07f;
    public float neonWarmthChance = 0.2f;
    public float neonResourceChance = 0.1f;
    public float neonResourceMin = 0.01f;

    public float difficultyResourceModifier = 0.01f;


    public GameObject[] forestTiles;
    public GameObject[] desertTiles;
    public GameObject[] cityTiles;
    public GameObject[] neonTiles;

    //public int difficulty = 0;
    public int tileNum = 0;

    public Transform sprite;

    public Tilemap currentMap;
    public GameObject resourcesContainer;
    //public Tilemap gatherTilemap;
    //public Tilemap warmthTilemap;
    private MapHelper.Region currentRegion;
    private float currentResourceChance;
    public TileBase testTile;
    MapHelper mapHelper;

    //make regions based on tile palette? base on tag?
    //region tile types in tile array?
        //could be object for all i care
    //public TerrainType[] regions;

    // Start is called before the first frame update
    void Start()
    {
        mapHelper = GetComponent<MapHelper>();
        //print(mapHelper);

        //load tile prefabs
        //Important note: place your prefabs folder(or levels or whatever) 
        //in a folder called "Resources" like this "Assets/Resources/Prefabs"
        forestTiles = Resources.LoadAll<GameObject>("MapTiles/Forest");
        desertTiles = Resources.LoadAll<GameObject>("MapTiles/Desert");
        cityTiles = Resources.LoadAll<GameObject>("MapTiles/City");
        neonTiles = Resources.LoadAll<GameObject>("MapTiles/Neon");

        //GenerateMap(out Vector3 startPoint, out Vector3 endPoint);
    }

    public void CopyTiles(Tilemap to, Tilemap from, int startPosX, int startPosY)
    {
        //print("add new tile");
        foreach (var pos in from.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = from.CellToWorld(localPlace);
            if (from.HasTile(localPlace))
            {
                TileBase tile = from.GetTile(localPlace);
                to.SetTile(new Vector3Int(localPlace.x + startPosX, localPlace.y + startPosY, 0), tile);
            }
        }
        tileNum++;
    }

    public void CopyTiles(Tilemap to, Tilemap from, Locator start, out List<Locator> newLocs)
    {
        //print("add new tile");
        //print(start.location);
        Locator matchingLoc = MatchingLoc(from.GetComponent<TileStats>(), start.dir);
        //print(matchingLoc.location);
        foreach (var pos in from.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (from.HasTile(localPlace))
            {
                TileBase tile = from.GetTile(localPlace);
                to.SetTile(new Vector3Int(localPlace.x + ((int)start.location.x - (int)matchingLoc.location.x),
                    localPlace.y + ((int)start.location.y - (int)matchingLoc.location.y), 0), tile);
            }
        }

        List<Locator> locs = from.GetComponent<TileStats>().locators;

        newLocs = new List<Locator>();

        foreach(Locator l in locs)
        {
            if(l.location == matchingLoc.location)
            {
                continue;
            }
            Locator newLoc = l;
            //print("old loc: " + l.location);
            newLoc.location.x = l.location.x + ((int)start.location.x - (int)matchingLoc.location.x);
            newLoc.location.y = l.location.y + ((int)start.location.y - (int)matchingLoc.location.y);
            //print("new locs " + newLoc.location);
            newLocs.Add(newLoc);
        }
        CopyProps(from, start, matchingLoc);

        tileNum++;

    }

    public void CopyProps(Tilemap from, Locator dest, Locator start)
    {
        //Tilemap to;
        foreach (Transform child in from.transform)
        {
            Vector3 position = child.position + (currentMap.CellToWorld(new Vector3Int((int)dest.location.x, (int)dest.location.y, 0))
                - currentMap.CellToWorld(new Vector3Int((int)start.location.x, (int)start.location.y, 0)));
            var newResource = Instantiate(child.gameObject, position, Quaternion.identity);
            newResource.transform.parent = resourcesContainer.transform;
        }
    }

    public GameObject[] FindTile(GameObject[] tileset, Locator nextLoc, MapHelper.ResourceType resourceType)
    {
        List<GameObject> availableTiles = new List<GameObject>();

        foreach (GameObject tile in tileset)
        {
            TileStats tileStats = tile.GetComponent<TileStats>();
            foreach (Locator loc in tileStats.locators)
            {
                if (!loc.req.Equals(nextLoc.req))
                {
                    continue;
                }
                switch (nextLoc.dir)
                {
                    case MapHelper.LocatorDirection.North:

                        if (loc.dir == MapHelper.LocatorDirection.South)
                        {
                            //print("add tile North");
                            availableTiles.Add(tile);
                        }
                        break;
                    case MapHelper.LocatorDirection.East:

                        if (loc.dir == MapHelper.LocatorDirection.West)
                        {
                            //print("add tile East");
                            availableTiles.Add(tile);
                        }
                        break;
                    case MapHelper.LocatorDirection.South:

                        if (loc.dir == MapHelper.LocatorDirection.North)
                        {
                            //print("add tile South");
                            availableTiles.Add(tile);
                        }
                        break;
                    case MapHelper.LocatorDirection.West:

                        if (loc.dir == MapHelper.LocatorDirection.East)
                        {
                            //print("add tile West");
                            availableTiles.Add(tile);
                        }
                        break;
                }
            }
        }
        List<GameObject> filteredTiles = CheckTileResource(availableTiles, resourceType);
        
        return filteredTiles.ToArray();
    }

    public List<GameObject> CheckTileResource(List<GameObject> tiles, MapHelper.ResourceType resource)
    {
        List<GameObject> resourceList = new List<GameObject>();
        List<GameObject> noResourceList = new List<GameObject>();

        foreach (GameObject obj in tiles)
        {
            switch (resource)
            {
                case MapHelper.ResourceType.Hunt:
                    if (obj.GetComponent<TileStats>().hasHunt)
                    {
                        resourceList.Add(obj);
                    }
                    break;
                case MapHelper.ResourceType.Gather:
                    if (obj.GetComponent<TileStats>().hasGather)
                    {
                        resourceList.Add(obj);
                    }
                    break;
                case MapHelper.ResourceType.Warmth:
                    if (obj.GetComponent<TileStats>().hasWarmth)
                    {
                        resourceList.Add(obj);
                    }
                    break;
                case MapHelper.ResourceType.Collect:
                    if (obj.GetComponent<TileStats>().hasCollect)
                    {
                        resourceList.Add(obj);
                    }
                    break;
                default:
                    if(!obj.GetComponent<TileStats>().hasCollect && !obj.GetComponent<TileStats>().hasWarmth
                        && !obj.GetComponent<TileStats>().hasGather && !obj.GetComponent<TileStats>().hasHunt)
                    {
                        noResourceList.Add(obj);
                    }
                    break;
            }
        }

        List<GameObject> tileList = noResourceList;

        if (resource != MapHelper.ResourceType.None)
        {
            if (resourceList.Count > 0)
            {
                tileList = resourceList;
            }
        }

        return tileList;
    }

    public Locator MatchingLoc(TileStats tilestat, MapHelper.LocatorDirection dir)
    {
        List<Locator> match = new List<Locator>();

        foreach (Locator loc in tilestat.locators)
        {
            switch (dir)
            {
                case MapHelper.LocatorDirection.North:

                    if (loc.dir == MapHelper.LocatorDirection.South)
                    {
                        match.Add(loc);
                        break;
                    }
                    break;
                case MapHelper.LocatorDirection.East:

                    if (loc.dir == MapHelper.LocatorDirection.West)
                    {
                        match.Add(loc);
                        break;
                    }
                    break;
                case MapHelper.LocatorDirection.South:

                    if (loc.dir == MapHelper.LocatorDirection.North)
                    {
                        match.Add(loc);
                        break;
                    }
                    break;
                case MapHelper.LocatorDirection.West:

                    if (loc.dir == MapHelper.LocatorDirection.East)
                    {
                        match.Add(loc);
                        break;
                    }
                    break;
            }
        }

        Locator[] matchArray = match.ToArray();
        return matchArray[UnityEngine.Random.Range(0, matchArray.Length)];
    }

    public bool isLocatorOverlap(int xPos, int yPos, MapHelper.LocatorDirection dir)
    {
        //print("check locator: " + xPos + " " + yPos);
        //print("locator overlapping at: ");
        switch (dir)
        {
            case MapHelper.LocatorDirection.West:
                for (int i = -2; i < 5; i++)
                {
                    for (int j = 1; j < 10; j++)
                    {
                        if (currentMap.HasTile(new Vector3Int(xPos + i, yPos + j, 0)))
                        {
                            //print("x: " + i + ", y: " + j);
                            //print(new Vector3Int(xPos + i, yPos + j, 0));
                            return true;
                        }
                    }
                }
                break;
            case MapHelper.LocatorDirection.South:
                for (int i = -1; i > -10; i--)
                {
                    for (int j = -2; j < 5; j++)
                    {
                        if (currentMap.HasTile(new Vector3Int(xPos + i, yPos + j, 0)))
                        {
                            //print("x: " + i + ", y: " + j);
                            //print(new Vector3Int(xPos + i, yPos + j, 0));
                            return true;
                        }
                    }
                }
                break;
            case MapHelper.LocatorDirection.East:
                for (int i = -2; i < 5; i++)
                {
                    for (int j = -1; j > -10; j--)
                    {
                        if (currentMap.HasTile(new Vector3Int(xPos + i, yPos + j, 0)))
                        {
                            //print("x: " + i + ", y: " + j);
                            //print(new Vector3Int(xPos + i, yPos + j, 0));
                            return true;
                        }
                    }
                }
                break;
            case MapHelper.LocatorDirection.North:
                for (int i = 1; i < 10; i++)
                {
                    for (int j = -2; j < 5; j++)
                    {
                        if (currentMap.HasTile(new Vector3Int(xPos + i, yPos + j, 0)))
                        {
                            //print("x: " + i + ", y: " + j);
                            //print(new Vector3Int(xPos + i, yPos + j, 0));
                            return true;
                        }
                    }
                }
                break;
        }

        return false;
    }

    public void GenerateRegion(List<Locator> openLocators, out List<Locator> previousRegionLocators)
    {

        previousRegionLocators = openLocators;
        if (openLocators.Count <= 0)
        {
            Debug.LogWarning("no locators left!");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, openLocators.Count);
        Locator locator = openLocators[randomIndex];
        openLocators = new List<Locator>();
        openLocators.Add(locator);

        int difficulty = locator.difficulty + 1;
        GameObject[] currentTileSet = setRandomRegion(difficulty);
        int regionSize = UnityEngine.Random.Range(regionMinSize, regionMaxSize);
        //int resourceNum = Mathf.CeilToInt((regionSize * 0.2f) * (difficulty * 0.3f) + (float)UnityEngine.Random.Range(0, 3));

        print("Region: " + currentRegion);
        print("Region Size: " + regionSize);

        print("Region Difficulty: " + difficulty);

        //get random locator
        for (int x = 0; x < regionSize; x++)
        {
            bool locatorFound = false;
            int y = 0;

            MapHelper.ResourceType currentResource = MapHelper.ResourceType.None;
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < currentResourceChance)
            {
                //print(rand);
                //print(currentResourceChance);
                currentResource = (MapHelper.ResourceType)UnityEngine.Random.Range(0, 3);
                //print(currentResource);
            }

            while (!locatorFound)
            {
                y++;
                if(openLocators.Count <= 0)
                {
                    Debug.LogWarning("no locators left!");
                    return;
                }

                randomIndex = UnityEngine.Random.Range(0, openLocators.Count);
                //print(randomIndex);
                locator = openLocators[randomIndex];

                GameObject[] possibleTiles = FindTile(currentTileSet, locator, currentResource);

                if (possibleTiles.Length > 0)
                {
                    //print("found locator");
                    locatorFound = true;
                    openLocators.Remove(locator);
                    GameObject currentTile = possibleTiles[UnityEngine.Random.Range(0, possibleTiles.Length)];
                    Tilemap prefabTilemap = currentTile.GetComponent<Tilemap>();
                    List<Locator> newLocs;
                    //print(prefabTilemap);
                    CopyTiles(currentMap, prefabTilemap, locator, out newLocs);
                    openLocators.AddRange(newLocs);
                    y = 0;
                }
                else
                {
                    Debug.LogWarning("possible tile not found");
                    //print(locator.dir);
                }
                if (y == 1000)
                {
                    Debug.LogWarning("infinite loop????");
                    locatorFound = true;
                }
            }

            List<Locator> noOverlapLocators = new List<Locator>();

            foreach (Locator l in openLocators)
            {
                if (!isLocatorOverlap((int)l.location.x, (int)l.location.y, l.dir))
                {
                    noOverlapLocators.Add(l);
                }
            }

            openLocators = noOverlapLocators;
        }

        for (int i = 0; i < openLocators.Count; i++)
        {
            Locator temp = openLocators[i];
            temp.difficulty = difficulty;
            openLocators[i] = temp;
        }

        previousRegionLocators.AddRange(openLocators);
    }

    public GameObject[] setRandomRegion(int difficulty)
    {
        currentRegion = (MapHelper.Region)UnityEngine.Random.Range(0, 3);
        float difficultyMultiplier = difficultyResourceModifier * difficulty;

        GameObject[] currentTileSet = null;

        if (currentRegion == MapHelper.Region.Forest)
        {
            currentTileSet = forestTiles;
            currentResourceChance = forestResourceChance - difficultyMultiplier;
        }
        else if (currentRegion == MapHelper.Region.Desert)
        {
            currentTileSet = desertTiles;
            currentResourceChance = desertResourceChance - difficultyMultiplier;
        }
        else if (currentRegion == MapHelper.Region.City)
        {
            currentTileSet = cityTiles;
            currentResourceChance = cityResourceChance - difficultyMultiplier;
        }
        else if (currentRegion == MapHelper.Region.Neon)
        {
            currentTileSet = desertTiles;// neonTiles;
            currentResourceChance = neonResourceChance - difficultyMultiplier;
        }

        return currentTileSet;
    }

    public void GenerateMap(out Vector3 startPoint, out Vector3 endPoint)
    {
        //currentMap = GetComponent<Tilemap>();
        currentMap.ClearAllTiles();

        int totalTileNum = UnityEngine.Random.Range(totalTileMin, totalTileMax);

        /*
        //create starting region
        int startX = mapWidth + (UnityEngine.Random.Range(0, 10) * UnityEngine.Random.Range(0, 2) * 2 - 1);
        if (startX > mapWidth)
        {
            startX = startX - mapWidth;
        }

        int startY = mapLength + (UnityEngine.Random.Range(0, 10) * UnityEngine.Random.Range(0, 2) * 2 - 1);
        if (startY > mapLength)
        {
            startY = startY - mapLength;
        }

        startX = 0;
        startY = 0;
        Vector2 startpos = new Vector2(startX, startY);
        */

        //todo - always start with start spawn piece

        currentRegion = MapHelper.Region.Forest;//(MapHelper.Region)Random.Range(0, 3);

        GameObject[] currentTileSet = setRandomRegion(0);

        GameObject currentTile = currentTileSet[UnityEngine.Random.Range(0, currentTileSet.Length)];
        Tilemap prefabTilemap = currentTile.GetComponent<Tilemap>();
        List<Locator> previousRegionLocators = new List<Locator>();

        print("add first tile");
        //print(prefabTilemap);
        CopyTiles(currentMap, prefabTilemap, 0, 0);
        previousRegionLocators.AddRange(prefabTilemap.GetComponent<TileStats>().locators);
        Locator startLocator = previousRegionLocators[UnityEngine.Random.Range(0, previousRegionLocators.Count)];
        startPoint = prefabTilemap.CellToWorld(new Vector3Int((int)startLocator.location.x, (int)startLocator.location.y, 0));

        for (int i = 0; i < previousRegionLocators.Count; i++)
        {
            Locator temp = previousRegionLocators[i];
            temp.difficulty = 0;
            previousRegionLocators[i] = temp;
        }

        print("add first region");
        GenerateRegion(previousRegionLocators, out previousRegionLocators);
        //print("locator list count: " + prefabTilemap.GetComponent<TileStats>().locators.Count);
        //print("locator count: " + openLocators.Count);


        while (tileNum < totalTileNum)
        {
            GenerateRegion(previousRegionLocators, out previousRegionLocators);
        }

        List<Locator> endPointLocators = new List<Locator>();
        endPoint = new Vector3(0, 0, 0);

        //mark end point location
        foreach(Locator l in previousRegionLocators)
        {
            if (l.difficulty >= 4)
            {
                endPointLocators.Add(l);
            }
        }

        if(endPointLocators.Count <= 0)
        {
            Debug.LogError("no endpoint found");
            return;
        }

        Locator endPointLocator = endPointLocators[UnityEngine.Random.Range(0, endPointLocators.Count)];
        endPoint = currentMap.CellToWorld(new Vector3Int((int)endPointLocator.location.x, (int)endPointLocator.location.y, 0));

        //CopyTiles(currentMap, prefabTilemap, 5, 0);

        //check if tile has resource
        //currentRegionSize++;

        //grid, instead of simplifying 2,1, use actual tile numbers - 21, 67, then i have exact offset of each til
            //can calculate fitting
            //e.g. tile at x:25-53 are filled with one prefab, next prefab can start at 54
                //bool filled, bool locator if open locator
        //on generate region - copy tile over immediately, fill out helper grid values, don't keep reference to prefab?
            //only really need filled/open locator values
            //on fill tile, check if surrounding tile is locator, if so, remove open locator
            //set of open locator, closed locator



        //for (int x = 0; x < mapWidth; x++)
        //{
            //for (int y = 0; y < mapLength; y++)
            //{
                


                //check region type of tile to connect with
                //starting region always minimum size (length + width?)

                //on creating new region
                //create random number of region size
                //assign difficulty number based on connecting square
                //if region changes, difficulty + 1
                //if region same, same difficulty
                //given random number, generate number of resources tiles
                //percent + noise

                //note adjust add tile frequency based on how initial maps turn out
                //what if each tile has approx size attached?
                //e.g. [2,1] or [5,2]
                //helper script object
                //holds region type
                //region tile number
                //resource number
                //array of tile size
                //holds prefabs of which tiles, and position start of tiles?
                //calculate based on locator connection, tag match, positional offset of locator
                //tile 0
                //pick random connector
                //add matching tile
                //assign tile location
                //add to list
                //set tile 0 locator as filled, + index of filling tile
                //set tile 1 locator as filled, + index of filling tile
                //back to tile 0
                //random locator not filled
                //roll if add tile
                //fill if roll passed
                //set as filled without prefab if roll failed
                //if all connectors filled, go to next index tile
                //repeat until region filled
                //if last index of tile filled without meeting region number requirements
                //rand index # of array range
                //pick a filled locator without a prefab
                //add tile
                //repeat until quota filled
                //roll for chance of resource tile
                //percent different based on difficulty
                //different pool of region tiles if resource tile
                //change region type
                //update difficulty
                //make random # of region tiles
                //pick random existing tile
                //pick random number of array
                //pick random direction
                //move in tile direction until i hit something that isn't populated
                //check to see there's at least a 3x3 grid of open space
                //start filling in from the first tile
                //for locator checks
                //when random locator chosen, check the corresponding grid tile
                //make sure no prefab already exists, if exist then assign to that locator

                //fill in large grid tracker as tiles are populated
                //for each region made, add region size to total size tracker
                //total size also random
                //when total size >= size set
                //make new tilemap, copy paste tiles based on grid location, and tile location inside prefab
                //overwrite tiles already existing
                //use helper function to ensure correct sprite

                //add props/exit


                //fake array grid with max side ranges, e.g. max 100 pieces, then 100x100
                //pick random grid to start from from an area near the edge
                //grid numbers e.g. 50,3, with reference to the tile that makes up the area

                //exit - find all tiles >= a given distance from entrance
                //pick random tile

                //helper script for replacing tile types
                //check all tiles around it, check all tiles about to be around it from prefab?
                //change sprite depending on which side(s) are free


                //create random array (2d?) of which tile type
                //e.g. [[1,2,3],[2,4],[,8,1]]
                //choose random tile   
                //characteristics of tile kept in public script?
                //e.g. [north, pond], [east]
                //along with which instance is attached to it
                //choose random direction of existing connectors
                //get direction connection requirements based on tags?
                //add random tile fulfilling requirements based on tag
                //add to total # of tiles in region
                //roll random number of if next connector gets a connection


                //the smaller the number of region type in group, the more likely the connecting tile is same region
                //starting region difficulty 1
                //
            //}
        //}
    }
}
