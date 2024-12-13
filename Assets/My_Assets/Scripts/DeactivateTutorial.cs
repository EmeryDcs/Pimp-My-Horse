using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTutorial : MonoBehaviour
{
    private float timer = 0f;

	private void Start()
	{
		timer = Time.time;
	}

	// Update is called once per frame
	void Update()
    {
        if (Time.time - timer > 5f)
            gameObject.SetActive(false);
	}
}
