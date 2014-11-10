using UnityEngine;
using Pathfinding;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public Color playerColor;

    //resources variables
    static int gold=0;



    public static void AddToStat(int stat)
    {
        gold += stat;
    }

    public static int GetStat()
    {
        return gold;
    }
}
