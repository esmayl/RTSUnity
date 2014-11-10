using UnityEngine;
using System.Collections;

public class Enemy : Units {


    float counter = 0;
    float cooldown=5;

	// Use this for initialization
	void Start () {
        transform.tag = "Enemy";
        hp = 2;
        dmg = 1;
        renderer.material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (counter > cooldown)
        {
            DoDmg();

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
    }

    public override void RemoveFromList()
    {
        base.RemoveFromList();
    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }
    public void DoDmg()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in hits)
        {
            if (c.tag == "Player")
            {
                c.GetComponent<MeleeUnit>().TakeDamge(dmg);
            }
        }
    }

}
