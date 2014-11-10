using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Units : Selectable {
    public float range;
    public string unitName;
    public Texture2D thumbnailImage;
    public int goldWorth;
    private Transform t;

    void Awake()
    {
        transform.tag = "Selectable";
    }


    public void SetImage(string imageName)
    {
        unitName = gameObject.name;
        thumbnailImage = Resources.Load("Resources/Thumnnails/" + imageName) as Texture2D;
    }

    public override void AddToList()
    {
        base.AddToList();
        Debug.Log("Loaded base class!");
    }

    public void Move(Vector3 pos)
    {
        if (t == null)
        {
            t = (GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity) as GameObject).transform;
        }
        Renderer.Destroy(t.renderer);
        t.position = pos;
        pFScript.target = t;
        pFScript.SearchPath();
    }

    public void Attack(Transform target)
    {
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, 1, transform.position - target.position, out hitInfo))
        {
            if (hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("Visible!");
            }
        }
    }
    public void TakeDamge(int dmg)
    {
        hp -= dmg;
    }
}
