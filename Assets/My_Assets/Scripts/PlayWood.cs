using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWood : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GetComponent<AudioSource>().Play();	
		}
	}
}
