using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

    public List<GameObject> unitList;
    public List<GameObject> enemyList;
    public GameObject selectedBuilding;
    public Texture2D selectionObject;

    Event e;
    bool leftClicked = false;
    bool rightClicked = false;
    bool drag = false;
    public static Rect dragSelection = new Rect(0, 0, 0, 0);

    void OnGUI()
    {
        if (dragSelection.x != -1f)
        {
            GUI.DrawTexture(dragSelection, selectionObject);
        }
    }

    private static float Test1(float y)
    {
        return Screen.height - y;
    }

	void Update () 
    {
        Debug.Log(dragSelection);
        if (Input.GetMouseButtonDown(0) && !UserInterface.MenuOpen())
        {
            dragSelection.x = Input.mousePosition.x;
            dragSelection.y = Input.mousePosition.y;
            leftClicked = true;
        }

            dragSelection.width = 0;
            dragSelection.height = 0;

        if (Input.GetMouseButtonDown(1) && !UserInterface.MenuOpen())
        {
            rightClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftClicked = false;
            dragSelection.x = -1f;
            dragSelection.y = -1f;
        }

        if (Input.GetKey(KeyCode.LeftShift) )
        {
            if (leftClicked && unitList.Count >= 1)
            {
                Debug.Log("Shift+LeftClick!");
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                RaycastHit hit;
                if (Physics.SphereCast(ray, 0.5f, out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.tag == "Selectable")
                    {
                        hit.transform.SendMessage("AddToList");
                    }
                }
            }
            leftClicked = false;

        }

        if (leftClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.5f, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    ClearList();
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.SendMessage("RemoveInfoScreen");
                    }
                }
                if (hit.transform.tag == "Building")
                {
                    selectedBuilding = hit.transform.gameObject;
                    selectedBuilding.SendMessage("RemoveInfoScreen");
                    ClearList();
                    selectedBuilding.SendMessage("AddToList");
                }
                if (hit.transform.tag == "Selectable")
                {
                    hit.transform.SendMessage("AddToList");
                }
            }
            leftClicked = false;
        }

        if (Input.GetMouseButton(0) && !UserInterface.MenuOpen())
        {

            dragSelection = new Rect(dragSelection.x, Test1(dragSelection.y), Input.mousePosition.x - dragSelection.x, Test1(Input.mousePosition.y) - (Screen.height - dragSelection.y));
            if (dragSelection.height < 0)
            {
                dragSelection.y += dragSelection.height;
                dragSelection.height = -dragSelection.height;
            }            
        }

        if (rightClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,200f))
            {
                if (hit.transform.tag != "Enemy")
                {
                    foreach (GameObject go in unitList)
                    {
                        go.SendMessage("Move", hit.point);
                        Debug.Log("Moving To " + hit.point);

                    }
                }
                if (unitList.Count > 0)
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        AddEnemy(hit.transform.gameObject);
                        foreach (GameObject go in unitList)
                        {
                            go.SendMessage("Move", hit.transform.position );
                        }

                    }
                }
                rightClicked = false;
            }
        }
	}

    public void AddEnemy(GameObject enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemy.renderer.material.SetColor("_MainColor", Color.red);
            enemyList.Add(enemy);

        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void ClearList()
    {
        foreach (GameObject go in unitList)
        {
            if (go != null)
            {
                go.SendMessage("RemoveFromList");
            }
        }
        unitList.Clear();

    }

}
