using System.Collections.Generic;
using UnityEngine;

public class TrapsDatabase : MonoBehaviour
{
    public static List<GameObject> staticTraps;
    [SerializeField] private List<GameObject> traps = new List<GameObject>();

	void Awake ()
    {
        staticTraps = traps;
	}
}

[System.Serializable]
public class TrapInfo
{
    public TrapType type;
    public Vector2 location;
    public List<Directions> possibleDirections;

    public TrapInfo(List<Directions> trapDirection, TrapType trapType, Vector2 trapLocation)
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
        Fire = 0,
        Spike = 1
    }
}