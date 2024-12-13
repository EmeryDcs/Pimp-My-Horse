using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRagdoll : MonoBehaviour
{
    public GameObject prefabRagdoll;

	public List<GameObject> prefabs = new List<GameObject>();

	public List<Material> materials = new List<Material>();
	private int lengthMaterials;

	private float timerSpawn;
	private float timerBump;

	private void Start()
	{
		timerSpawn = Time.time;
		timerBump = Time.time;
		lengthMaterials = materials.Count;
	}

	// Update is called once per frame
	void Update()
    {
        if (Time.time - timerSpawn > 1f)
		{
			timerSpawn = Time.time;
			float randomX = Random.Range(-5, 5);

			Vector3 position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
			Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

			int random = Random.Range(0, 5);

			for (int i = 0; i < 5; i++)
			{
				if (i == random)
				{
					prefabRagdoll.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
				} else
				{
					prefabRagdoll.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
				}
			}

			int randomMaterial = Random.Range(0, lengthMaterials);
			prefabRagdoll.transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = materials[randomMaterial];

			Instantiate(prefabRagdoll, position, rotation);

			prefabs.Add(prefabRagdoll);
		}

		if (Time.time - timerBump > 3f)
		{
			timerBump = Time.time;
			foreach (GameObject prefab in prefabs)
			{
				Rigidbody[] rbBoost = prefab.GetComponentsInChildren<Rigidbody>();
				foreach(Rigidbody rb in rbBoost)
				{
					rb.AddForce(Vector3.up * 100000000);
				}
			}
		}

	}
}
