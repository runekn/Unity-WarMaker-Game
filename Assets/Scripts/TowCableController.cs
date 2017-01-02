using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowCableController : MonoBehaviour {

    public GameObject CableSection;
    public float CableSectionLength;

    public void GenerateCable(GameObject from, GameObject to)
    {
        to.GetComponent<HingeJoint2D>().enabled = true;
        from.GetComponent<HingeJoint2D>().enabled = true;
        var fromRigid = from.GetComponent<Rigidbody2D>();
        var toRigid = to.GetComponent<Rigidbody2D>();
        var distance = Vector2.Distance(from.transform.position, to.transform.position);

        int numberOfSections = (int)(distance / CableSectionLength);
        var position = from.transform.position;
        var previousSection = fromRigid;

        for(var x = 0; x < numberOfSections; x++)
        {
            var section = Instantiate(CableSection, position, Quaternion.LookRotation(to.transform.position), transform);
            section.GetComponent<HingeJoint2D>().connectedBody = previousSection;
            previousSection = section.GetComponent<Rigidbody2D>();
        }

        to.GetComponent<HingeJoint2D>().connectedBody = previousSection;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
