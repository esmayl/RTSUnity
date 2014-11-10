using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pathfinding;

public class UserInterface : MonoBehaviour {

    public Text gold;

    public GameObject buildingGo;
    GameObject buildingInstance;
    public GameObject gridGo;
    Transform gridInstance;
    public GameObject gridParent;

    float width = 3;
    float height = 5;
    static bool menuOpen = false;
    bool buildMenuOpen;

	void Start () {
        OpenGrid(transform.position);
	}
	
	void Update () {
        gold.text = "Gold : " + PlayerStats.GetStat();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            buildMenuOpen = !buildMenuOpen;
            Time.timeScale = buildMenuOpen ? 0 : 1;
        }
        if (buildMenuOpen)
        {
            OpenBuildMenu();
        }
        else
        {
            MoveGrid(Vector3.one);
        }
	}

    public static bool MenuOpen()
    {
        return menuOpen;   
    }
    public static void SetMenu(bool state)
    {
        Debug.Log("Old State: " + menuOpen);
        menuOpen = state;
        Debug.Log("New State: " + menuOpen);
    }

    /// <summary>
    /// Instantiates a grid of 5 by 3 in a diamond shape
    /// </summary>
    /// <param name="location"></param>
    public void OpenGrid(Vector3 location)
    {

        //diamond grid creation
        //1st side x+ and z - to +
        for (float i = 0; i < height; i++)
        {
            //instantiate from 0 to 3 in z
            for (float p = 0f; p < width; p++)
            {
                gridInstance = (Instantiate(gridGo, location, Quaternion.identity) as GameObject).transform;
                gridInstance.parent = gridParent.transform;
                gridInstance.transform.localPosition = new Vector3(i, 0, p);
            }

            //instantiate from -1 to -3 in z
            for (float p = -1f; p > -width; p--)
            {
                gridInstance = (Instantiate(gridGo, location, Quaternion.identity) as GameObject).transform;
                gridInstance.parent = gridParent.transform;
                gridInstance.transform.localPosition = new Vector3(i, 0, p);
            }
            width--;

        }

        width = 3;

        //2nd side x- and z - to +
        for (float i = -1f; i > -height; i--)
        {
            //instantiate from 0 to 3 in z
            for (float p = 0f; p < width; p++)
            {
                gridInstance = (Instantiate(gridGo, location - transform.up, Quaternion.identity) as GameObject).transform;
                gridInstance.parent = gridParent.transform;
                gridInstance.transform.localPosition = new Vector3(i, 0, p);
            }
            //instantiate from -1 to -3 in z
            for (float p = -1f; p > -width; p--)
            {
                gridInstance = (Instantiate(gridGo, location-transform.up, Quaternion.identity) as GameObject).transform;
                gridInstance.parent = gridParent.transform;
                gridInstance.transform.localPosition = new Vector3(i, 0, p);
            }
            width--;
        }
    }

    public void MoveGrid(Vector3 pos)
    {
        gridParent.transform.position = new Vector3(pos.x, 1, pos.z);

        foreach (Transform t in gridInstance.parent)
        {
            GraphNode n = (GraphNode)AstarPath.active.GetNearest(t.position);

            if (n.Walkable)
            {
                t.renderer.material.SetColor("_Color", Color.green);
            }
            else
            {
                t.renderer.material.SetColor("_Color", Color.red);
            }
        }
    }

    public bool CheckGrid()
    {
        int correctCounter = 0;

        foreach (Transform t in gridInstance.parent)
        {
            GraphNode n = (GraphNode)AstarPath.active.GetNearest(t.position);

            if (n.Walkable)
            {
                t.renderer.material.SetColor("_Color", Color.green);
                correctCounter++;
            }
            else
            {
                t.renderer.material.SetColor("_Color", Color.red);
            }
        }

        if (correctCounter == gridInstance.parent.childCount)
        {
            
            if (PlayerStats.GetStat() >= buildingGo.GetComponent<Building>().goldWorth)
            {
                PlaceBuilding(buildingGo);
                PlayerStats.AddToStat(-buildingGo.GetComponent<Building>().goldWorth);
                Debug.Log("instantiated!");

                return true;
            }
            else
            {
                Debug.Log("Not enough cash!");
                return false;
            }
        }
        else
        {
            Debug.Log("Not in the right spot, " + (gridInstance.parent.childCount - correctCounter) + " not in the right place.");
            return false;
        }
    }

    public void OpenBuildMenu()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.5f, out hit))
        {
            transform.SendMessage("MoveGrid", new Vector3(hit.transform.position.x, 1, hit.transform.position.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            transform.SendMessage("CheckGrid");
        }
    }

    public void PlaceBuilding(GameObject go)
    {
        GameObject tempGo = Instantiate(go)as GameObject;
        tempGo.transform.position = gridInstance.parent.position - transform.up;
        AstarPath.active.Scan();
    }

}
