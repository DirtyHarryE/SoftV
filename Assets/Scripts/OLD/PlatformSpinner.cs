using UnityEngine;
using System.Collections;

// Script to rotate a platform with speed and direction input
public class PlatformSpinner : MonoBehaviour {

    public int speed;
    public int direction;
	
	void Update () {
        transform.Rotate(new Vector3(0,0,direction) * speed * Time.deltaTime);
	}

}
