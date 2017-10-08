using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowParticle : MonoBehaviour {

    public GameObject fireEffect;

    //private GameObject sphere;
	// Use this for initialization
	void Start () {
        //sphere = GameObject.Find("Sphere");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //instantiate fireEffect prefab
            Vector3 gemPos = this.transform.position;
            Instantiate(fireEffect, gemPos, Quaternion.identity);
            //Destroy(fireEffect, 5);
        }

    }

    /*
    void OnTriggerExit(Collider other)
    {
        
    }
    */
}
