using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

    public InputManager manager;
    public int hp;
    public int dmg;
    public AIPath pFScript;
    public string enemyTag = "Enemy";

    bool added=false;


	// Use this for initialization
	protected void Awake() {
        pFScript = GetComponent<AIPath>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
	}

    public virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 objScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            objScreenPos.y = Screen.height - objScreenPos.y;

            if (InputManager.dragSelection.Contains(objScreenPos,true))
            {
                 RemoveFromList();
                 AddToList();
                 Debug.LogError("Hello!");
            }
            else
            {
            }
        }
    }


    public virtual void AddToList()
    {
        if (!added)
        {
            Debug.Log("Adding");
            transform.GetChild(0).renderer.material.SetColor("_Color", manager.GetComponent<PlayerStats>().playerColor);
            manager.unitList.Add(gameObject);
        }
        added = true;
    }

    public virtual void RemoveFromList()
    {
        if (added)
        {
            transform.GetChild(0).renderer.material.SetColor("_Color", Color.white);
            manager.unitList.Remove(gameObject);
        }
        added = false;

    }
}
