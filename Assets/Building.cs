using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Building : Selectable {

    public string name;
    public string description;

    //resource variables
    public int gold;
    public int goldWorth = 100;

    //end resource variables

    float counter = 0;
    float cooldown = 3;

    public Canvas c;
    public Button b;


	void OnEnable () {
        hp = 100;
        gold = 20;
        goldWorth = 100;
        c.enabled = false;
        b.GetComponentInChildren<Text>().text = "Sell";
	}
	
	public override void Update () 
    {
        if (counter >= cooldown)
        {
            PlayerStats.AddToStat(gold);
            c.GetComponentInChildren<Text>().text = gold + "gold Added!";

            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
	}

    public override void AddToList()
    {
        base.AddToList();
        ShowInfoScreen();
    }

    public override void RemoveFromList()
    {
        base.RemoveFromList();
        RemoveInfoScreen();
    }

    public void ShowInfoScreen()
    {
        c.enabled = true;
        c.transform.parent = transform;
        c.transform.localPosition = new Vector3(0, 6, -Camera.main.transform.forward.z*3);
    }

    public void RemoveInfoScreen()
    {
        c.transform.parent = null;
        c.transform.position = Vector3.zero;
    }

    public void SellBuilding()
    {
        PlayerStats.AddToStat(goldWorth);
        Destroy(transform.gameObject);
        AstarPath.active.Scan();
    }
}
