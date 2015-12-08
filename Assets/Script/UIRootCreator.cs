using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRootCreator : MonoBehaviour {

    [SerializeField]
    Camera rightCamera = null;

    [SerializeField]
    Camera leftCamera = null;

    Canvas canvas = null;

	// Use this for initialization
	void Start () 
    {
        canvas = GetComponent<Canvas>();

        if (SequenceManager.Instance.IsBuildWatch)
        {
            canvas.worldCamera = Camera.main;
        }
        else
        {
            var clone = (GameObject)Instantiate(gameObject);
            clone.transform.SetParent(transform.parent);
            var cloneCanvas = clone.GetComponent<Canvas>();

            canvas.worldCamera = rightCamera;
            cloneCanvas.worldCamera = leftCamera;

            Destroy(clone.GetComponent<UIRootCreator>());
        }

        Destroy(this);

	}
	

}
