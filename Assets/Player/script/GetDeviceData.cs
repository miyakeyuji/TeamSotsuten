using UnityEngine;
using System.Collections;

public class GetDeviceData : MonoBehaviour {
    [SerializeField]
    GameObject device;
	// Use this for initialization
	void Start ()
    {
        gameObject.transform.position = device.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = device.transform.position;
    }
}
