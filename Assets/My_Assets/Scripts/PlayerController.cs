using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public GameObject prefabJoueur;
	public float speed = 5;
	public float puissanceCharge = 100;
	public float puissanceDash = 10;

	public float[] positionsAnimationPrematch = { -25, 25 };

	public UIDocument uIDocumentScore;
	private VisualElement rootScore;
	private int scoreJoueur = 0;

	public GameObject uiFin;

	private Animator animator;

	private Vector2 move;
    private float lookAt;
    private float chargeTime = 0;
    private bool hasCharged = false;
	private float chargingTime = 0;

	private bool isGameEnded = false;
	private bool isSpawningFirstTime = true;
	private float timerAnimationEntree = 0;
	private ColliderBarriere scriptBarriere;

	private int playerIndex;
	private float timerRetourMenu = 0;

	public AudioSource audioClip1;
	public AudioSource audioClip2;

	void Start()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		animator = transform.GetChild(0).GetComponent<Animator>();
		timerRetourMenu = Time.time;
		timerAnimationEntree = Time.time;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update()
    {		
        transform.Translate(new Vector3(move.x, 0, move.y) * speed * Time.deltaTime, Space.World);
		transform.rotation = Quaternion.Euler(new Vector3(0, lookAt, 0));

		if (isSpawningFirstTime && SceneManager.GetActiveScene().name == "Jeu")
		{
			AnimationSpawn();
		}

		if (hasCharged && Time.time-chargingTime > 0.3)
		{
			hasCharged = false;
		}

		if (isGameEnded && (Time.time-timerRetourMenu>10f) && scoreJoueur >= 5) //On retourne au menu après 10 secondes si le joueur a gagné.
		{
			GameObject[] allObjects = FindObjectsOfType<GameObject>();

			foreach (GameObject obj in allObjects)
			{
				Destroy(obj);
			}

			SceneManager.LoadScene("Menu");
		}

		if (transform.position.y > 2)
		{
			Rigidbody rb = transform.GetComponent<Rigidbody>();
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			Vector3 force = -transform.up * 10;
			rb.AddForce(force, ForceMode.Impulse);
		}
	}

	private void AnimationSpawn()
	{
		float distance = 5f;
		float duree = 5f;
		float vitesse;
		float tailleDeplacement = 0;

		if (Time.time-timerAnimationEntree < 5f)
		{
			//déplacement de 25 à 20
			vitesse = distance/duree;
			tailleDeplacement = vitesse * Time.deltaTime;
		} else if (Time.time-timerAnimationEntree < 10f)
		{
			//déplacement de 20 à 12
			distance = 8f;
			vitesse = distance / duree;
			tailleDeplacement = vitesse * Time.deltaTime;
		}
		else
		{
			isSpawningFirstTime = false;
			scriptBarriere.CloseFence();
			scriptBarriere.ReactivateBarriers();
		}
		Debug.Log(Time.time - timerAnimationEntree);

		// 0 : gauche; 1 : droite; 2 : haut; 3 : bas
		switch (playerIndex)
		{
			case 0:
				Vector3 vGauche = transform.position;
				vGauche.x += tailleDeplacement;
				transform.position = vGauche;
				break;
			case 1:
				Vector3 vDroite = transform.position;
				vDroite.x -= tailleDeplacement;
				transform.position = vDroite;
				break;
			case 2:
				Vector3 vHaut = transform.position;
				vHaut.z -= tailleDeplacement;
				transform.position = vHaut;
				break;
			case 3:
				Vector3 vBas = transform.position;
				vBas.z += tailleDeplacement;
				transform.position = vBas;
				break;
			default:
				break;
		}
	}

    public void OnMove(InputAction.CallbackContext context)
    {
		if (!isGameEnded && !isSpawningFirstTime)
		{
			move = context.ReadValue<Vector2>();
			if (-0.3 <= move.x && move.x < 0.3 && move.y < 0.3 && move.y >= -0.3)
			{
				move.x = 0;
				move.y = 0;
			}
			animator.SetBool("isMoving", move.x != 0 || move.y != 0);
		}
	}

    public void OnLookAt(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();

		//On ne fait rien si le joystick n'est pas assez incliné.
		if (v.x < 0.1 && v.x >= -0.1 && v.y < 0.1 && v.y >= -0.1)
			return;

		lookAt = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg; //On convertit les valeurs du joystick en angle.
    }

    public void OnCharge(InputAction.CallbackContext context)
	{
		if (!isSpawningFirstTime)
		{
			if (context.performed)
			{
				chargeTime = Time.time;
				speed = 1.5f;
				hasCharged = false;
			}
			else if (context.canceled)
			{
				if (!hasCharged)
				{
					float puissance = Mathf.Clamp((Time.time - chargeTime), 0, 0.5f) / 0.5f;
					speed = 10f;

					Rigidbody rb = transform.GetComponent<Rigidbody>();
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;

					Vector3 force = transform.forward * puissanceCharge * puissance;
					force.y = 0;
					rb.AddForce(force, ForceMode.Impulse);

					hasCharged = true;
					chargingTime = Time.time;
				}
			}
		}
	}

	public void OnDash(InputAction.CallbackContext context)
	{
		if (context.performed && !isSpawningFirstTime)
		{
			Rigidbody rb = transform.GetComponent<Rigidbody>();
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.GetComponent<Rigidbody>().AddForce(transform.forward * puissanceDash, ForceMode.Impulse);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player" && hasCharged)
		{
			Vector3 directionObjet1 = transform.forward;
			Vector3 directionObjet2 = collision.gameObject.transform.forward;

			float angle = Vector3.Angle(directionObjet1, directionObjet2);

			//Si les deux joueurs se font face, ils se repoussent
			if (angle > 180-45 && angle < 180+45)
			{
				collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * puissanceCharge, ForceMode.Impulse);
				GetComponent<Rigidbody>().AddForce(-transform.forward * puissanceCharge, ForceMode.Impulse);
			}
			else
			{
				//On crée un nouveau prefab pour la mort du joueur, et on téléporte le prefab à une nouvelle position via la fonction InstanciateRagdollBody.

				audioClip2.Play();

				InstanciateRagdollBody(collision.gameObject);

				//On ajoute un point au joueur qui a tué l'autre.
				rootScore.Q<Label>($"score-{playerIndex + 1}").text = (int.Parse(rootScore.Q<Label>($"score-{playerIndex + 1}").text) + 1).ToString();
				scoreJoueur = int.Parse(rootScore.Q<Label>($"score-{playerIndex + 1}").text);

				//On vérifie si le joueur a gagné.
				if (scoreJoueur >= 5 && !isGameEnded)
				{
					uiFin.SetActive(true);
					uiFin.GetComponent<UIDocument>().rootVisualElement.Q<Label>("numero-joueur").text = (playerIndex+1).ToString();

					GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

					foreach (GameObject player in players)
					{
						player.GetComponent<PlayerController>().isGameEnded = true;
					}

					timerRetourMenu = Time.time;
				}
			}

			audioClip1.Play();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Zone" && !isSpawningFirstTime)
		{
			InstanciateRagdollBody(this.gameObject);
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Jeu")
		{
			uiFin = GameObject.Find("/UI Documents").transform.Find("Fin").gameObject;

			scriptBarriere = GameObject.Find("Mur").GetComponent<ColliderBarriere>();

			playerIndex = GetComponent<PlayerInput>().playerIndex;
			uIDocumentScore = GameObject.Find("Score").GetComponent<UIDocument>();
			rootScore = uIDocumentScore.rootVisualElement;
			rootScore.Q<VisualElement>($"score-joueur-{playerIndex + 1}").style.display = DisplayStyle.Flex;

			//Changer pour que cela se fasse avec une entrée un peu des barres
			isSpawningFirstTime = true;
			timerAnimationEntree = Time.time;

			//gérer la position de départ du player une fois la scène Jeu chargée
			// 0 : gauche; 1 : droite; 2 : haut; 3 : bas
			switch (playerIndex)
			{
				case 0:
					transform.position = new Vector3(positionsAnimationPrematch[0], 1, 0);
					transform.rotation = Quaternion.Euler(0, 90, 0);
					break;
				case 1:
					transform.position = new Vector3(positionsAnimationPrematch[1], 1, 0);
					transform.rotation = Quaternion.Euler(0, -90, 0);
					break;
				case 2:
					transform.position = new Vector3(0, 1, positionsAnimationPrematch[1]);
					transform.rotation = Quaternion.Euler(0, 180, 0);
					break;
				case 3:
					transform.position = new Vector3(0, 1, positionsAnimationPrematch[0]);
					break;
				default:
					break;
			}
		}
	}

	private void InstanciateRagdollBody(GameObject go)
	{
		GameObject bouees = go.transform.GetChild(1).gameObject;
		int nbEnfantBouees = bouees.transform.childCount;
		GameObject boueesRagdoll = prefabJoueur.transform.GetChild(1).gameObject;

		for (int i = 0; i < nbEnfantBouees; i++)
		{
			if (bouees.transform.GetChild(i).gameObject.activeSelf)
			{
				boueesRagdoll.transform.GetChild(i).gameObject.SetActive(true);
			}
			else
			{
				boueesRagdoll.transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		GameObject tempPlayer = Instantiate(prefabJoueur, go.transform.position, go.transform.rotation);
		tempPlayer.transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = go.transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;

		float posX = Random.Range(-10, 10);
		float posZ = Random.Range(-10, 10);

		go.gameObject.transform.position = new Vector3(posX, 2, posZ);

		//On pousse le joueur mort à la direction opposée

		go.GetComponent<Rigidbody>().AddForce(transform.forward * puissanceCharge, ForceMode.Impulse);
	}
}
