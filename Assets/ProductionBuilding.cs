using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProductionBuilding : Building {

    public GameObject unitList;
    List<Button> buttons = new List<Button>();
    GameObject selectedUnit;

	// Use this for initialization
	void Awake () 
    {
        goldWorth = 100;
        gold = 0;
        hp = 20;
        dmg =0;
        if ((unitList.GetComponentsInChildren<Button>().Length >1))
        {
            List<Units> testList = Database.GetUnits();
            Debug.Log(""+testList);

            int counter = 0;
            foreach (Button t in unitList.GetComponentsInChildren<Button>())
            {
                buttons.Add(t);
                Debug.Log(""+Database.GetUnits()[counter].thumbnailImage.name);
                Sprite sprite = Sprite.Create(Database.GetUnits()[counter].thumbnailImage, new Rect(0, 0, 200, 50), new Vector2(0, 0));
                                   
                t.GetComponent<Image>().sprite = sprite;
                t.GetComponentInChildren<Text>().text = Database.GetUnits()[counter].unitName;
                counter++;
            }
        }

        
        //get database
        //get all units
        //put thumbnail on buttons
	
	}
	
    public void CreateUnit(GameObject go)
    {
        string spriteName = go.GetComponentInChildren<Text>().text;

        foreach (Units u in Database.GetUnits())
        {
            if (u.unitName == spriteName)
            {
                if(CheckCash(u))
                {
                    GameObject newUnit = Instantiate(u.gameObject)as GameObject;
                    newUnit.transform.parent = transform;
                    newUnit.transform.localPosition = Vector3.zero;
                    newUnit.transform.localPosition -= transform.forward*5;
                    newUnit.transform.localPosition += transform.up;
                    newUnit.transform.parent = null;
                    return;
                }

                
                //cooldown start
                //counter start

            }
        }

    }

    public bool CheckCash(Units u)
    {
        int currentCash = PlayerStats.GetStat();

        if (currentCash > u.goldWorth)
        {
            PlayerStats.AddToStat(-u.goldWorth);
            return true;
        }
        else
        {
            return false;
        }
    }

}
