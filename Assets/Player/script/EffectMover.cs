using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float speed = 5.0f;

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("push space");
            gameObject.transform.position = player.transform.position;
            iTween.MoveTo(gameObject, iTween.Hash("position", enemy.transform.position, "speed", speed, "easetype", iTween.EaseType.linear));
        }
    }
}
