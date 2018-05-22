using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    // Use this for initialization

    Vector3 localPosition = new Vector3();
	void Start () {
        Debug.Log("Start");
        localPosition = transform.position;

	}
    private void FixedUpdate()
    {
        //this.transform.Rotate (Vector3.forward*50*Time.deltaTime,Space.Self);
    }
    // Update is called once per frame
    void Awake () {
        Debug.Log("Awake");
    }
}
