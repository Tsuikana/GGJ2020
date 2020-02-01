using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStats : MonoBehaviour
{
    public MapHelper.Region region;
    public bool hasResource;
    public List<Locator> locators = new List<Locator>();
}

[System.Serializable]
public struct Locator
{
    public Vector2 location;
    public MapHelper.ConnectionReqs req;
    public MapHelper.LocatorDirection dir;

}
