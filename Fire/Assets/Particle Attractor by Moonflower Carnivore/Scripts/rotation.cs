using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {
	public float xRotation = 0F;
	public float yRotation = 0F;
	public float zRotation = 0F;
	void OnEnable(){
		InvokeRepeating("rotate", 0f, 0.03f);
    }
	void OnDisable(){
		CancelInvoke();
	}
	public void clickOn(){
		InvokeRepeating("rotate", 0f, 0.03f);
	}

	public void clickOff(){
		CancelInvoke();
	}

	void rotate(){
		this.transform.localEulerAngles += new Vector3(xRotation,yRotation,zRotation);
	}


}
