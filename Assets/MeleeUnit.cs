using UnityEngine;
using System.Collections;
using Pathfinding;

public class MeleeUnit : Units {

    private float counter = 0;
    private float cooldown = 1;

	void Start () 
    {
        goldWorth = 10;
        hp = 10;
        dmg = 3;
        range = 2;//5 = ranged
        renderer.material.color = Color.red;
	
	}
	
	public override void Update ()
    {
        base.Update();

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (counter >= cooldown)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range);
            foreach (Collider c in hits)
            {
                if (c.tag == enemyTag)
                {

                    if (c.GetComponent<Enemy>())
                    {
                        Debug.LogWarning("" + c.name);
                        c.GetComponent<Enemy>().TakeDmg(dmg);
                    }
                }
            }
            counter = 0;

        }
        else
        {
            counter += Time.deltaTime;
        }
	}
}
