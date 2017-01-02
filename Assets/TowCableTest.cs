using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowCableTest : MonoBehaviour {

    public GameObject Tow;
    public GameObject To;
    public GameObject From;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            var tow = Instantiate(Tow);
            tow.GetComponent<TowCableController>().GenerateCable(To, From);
        }
	}
}
