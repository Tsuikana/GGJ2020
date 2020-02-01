using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStats : MonoBehaviour
{
    public MapHelper.Region region;

    public Locator[] locators;
}

[System.Serializable]
public struct Locator
{
    public Vector2 location;
    public MapHelper.ConnectionReqs[] reqs;
    public MapHelper.LocatorDirection dir;

}
