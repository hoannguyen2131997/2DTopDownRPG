using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : Singleton<MapData>
{
    [SerializeField] private Tilemap ground;

    protected override void Awake()
    {
        base.Awake();
    }

    public Tilemap GetMap()
    {
        return ground;
    }
}
