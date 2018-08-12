using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public float damage;
    bool damageDealt;

    // Use this for initialization
    void Start () {

        StartCoroutine(DestroySelf());

	}
	
	// Update is called once per frame
	void Update () {
		
        

	}


    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy" && damageDealt == false)
        {
            col.gameObject.GetComponent<EnemyBehavior>().health -= damage;
            damageDealt = true;
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
