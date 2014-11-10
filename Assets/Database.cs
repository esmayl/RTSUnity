using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database : MonoBehaviour {
    
    public static List<Units> units = new List<Units>();
    public Units testUnit;
	// Use this for initialization
    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            SetUnits(testUnit);
        }
    }


	// Update is called once per frame
	void Update () {
	
	}

    public static List<Units> GetUnits()
    {
        return units;
    }

    public static void SetUnits(Units t)
    {
        units.Add(t);
    }
}
