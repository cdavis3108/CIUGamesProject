using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool open;
    public float maxRot = 135f;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject leftDoor;
    
    public float degreesPerSecond = 15.0f;
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            // Spin object around Y-Axis
            rightDoor.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
            leftDoor.transform.Rotate(new Vector3(0f, -Time.deltaTime * degreesPerSecond, 0f), Space.World);
            
            if (Mathf.Abs(rightDoor.transform.localEulerAngles.y) > maxRot && Mathf.Abs(leftDoor.transform.localEulerAngles.y) > maxRot)
                open = false;
        }
	}
}
