using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapHelper: MonoBehaviour
{
    public enum Region
    {
        Forest,
        Desert,
        Snow,
        Swamp
    }

    public int[,] grid;

    public enum ConnectionReqs
    {
        none,
        river,
        bridge,

    }
    public enum LocatorDirection
    {
        North,
        East,
        South,
        West
    }

}

struct GridCell
{
    bool isLocator;
    MapHelper.LocatorDirection dir;
    MapHelper.ConnectionReqs[] req;
    bool isFilled;
    bool hasResource;
    MapHelper.Region region;
    int difficulty;
}
