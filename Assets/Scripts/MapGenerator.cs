﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap[] tiles;
    public int mapIndex = 0;

    //make random number range
    public int mapWidth = 100;
    public int mapLength = 100;
    public int regionMinSize = 5;
    public int regionMaxSize = 20;

    public GameObject[] forestTiles;

    public int difficulty = 0;

    public Transform sprite;

    public Tilemap currentMap;
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
        print(mapHelper);

        //load tile prefabs
        //Important note: place your prefabs folder(or levels or whatever) 
        //in a folder called "Resources" like this "Assets/Resources/Prefabs"
        forestTiles = Resources.LoadAll<GameObject>("MapTiles/Forest");

        GenerateMap();
    }

    public void CopyTiles(Tilemap to, Tilemap from, int startPosX, int startPosY)
    {
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
    }

    public void GenerateMap()
    {
        //currentMap = GetComponent<Tilemap>();
        //currentMap.ClearAllTiles();

        //create starting region
        int startX = mapWidth + (Random.Range(20, 100) * Random.Range(0, 2) * 2 - 1);
        if (startX > mapWidth)
        {
            startX = startX - mapWidth;
        }

        int startY = mapLength + (Random.Range(20, 100) * Random.Range(0, 2) * 2 - 1);
        if (startY > mapLength)
        {
            startY = startY - mapLength;
        }

        Vector2 startpos = new Vector2(startX, startY);

        //todo - always start with start spawn piece

        MapHelper.Region currentRegion = MapHelper.Region.Forest;//(MapHelper.Region)Random.Range(0, 3);
        difficulty = difficulty++;
        int regionSize = Random.Range(regionMinSize, regionMaxSize);
        int resourceNum = Mathf.CeilToInt((regionSize * 0.2f) * (difficulty * 0.3f) + (float)Random.Range(0, 3));
        int currentRegionSize = 0;

        print(currentRegion);
        print(regionSize);
        print(resourceNum);

        GameObject[] currentTileSet = null;

        if(currentRegion == MapHelper.Region.Forest)
        {
            currentTileSet = forestTiles;
        }

        GameObject currentTile = forestTiles[Random.Range(0, forestTiles.Length)];


        Tilemap prefabTilemap = currentTile.GetComponent<Tilemap>();
        //TileBase prefabTile = prefabTilemap.GetTile(new Vector3Int(-8, 2, 0));
        //currentMap.SetTile(new Vector3Int(0, -16, 0), prefabTile);

        currentMap.ClearAllTiles();
        CopyTiles(currentMap, prefabTilemap, 0, 0);//startX, startY);
        CopyTiles(currentMap, prefabTilemap, 5, 0);

        //check if tile has resource
        currentRegionSize++;

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
