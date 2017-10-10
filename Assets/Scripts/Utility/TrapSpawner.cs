using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public List<TrapInfo> trapLocations;

	void Start ()
    {
		
	}
}

[System.Serializable]
public class TrapInfo
{
    public TrapType type;
    public Vector2 location;
    public List<Directions> possibleDirections;

    public TrapInfo (List<Directions> trapDirection, TrapType trapType, Vector2 trapLocation)
    {
        type = trapType;
        location = trapLocation;
        possibleDirections = trapDirection;
    }

    public enum Directions
    {
        North,
        East,
        South,
        West
    }

    public enum TrapType
    {
        Fire,
        Spike
    }
}