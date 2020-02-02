using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStats : MonoBehaviour
{
    public MapHelper.Region region;
    public bool hasHunt;
    public bool hasGather;
    public bool hasGirl;
    public bool hasWarmth;
    public bool hasCollect;
    public List<Locator> locators = new List<Locator>();
}

[System.Serializable]
public struct Locator
{
    public Vector2 location;
    public MapHelper.ConnectionReqs req;
    public MapHelper.LocatorDirection dir;
    public int difficulty;

}
