using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	public Transform checkPoint;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
            col.attachedRigidbody.GetComponent<PlayerController>().checkPointPosition = checkPoint.position;
		}
	}
}
