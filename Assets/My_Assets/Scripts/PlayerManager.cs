using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
	[SerializeField]
	public int nbSkin;
	public int nbBouee;
	[SerializeField]
	public UIDocument uiDocument;
	private int playerCount = 0;
	private PlayerSelection[] playerSelections = new PlayerSelection[4];
	private float timerReady;

	private void Start()
	{
		nbSkin = uiDocument.GetComponent<UIChoixPersonnage>().materials.Count;
		nbBouee = uiDocument.GetComponent<UIChoixPersonnage>().renderTextureBouees.Count;
		PlayerSelection.nbSkin = nbSkin;
		PlayerSelection.nbBouee = nbBouee;
		DontDestroyOnLoad(this.gameObject);

		for (int i = 0; i < 4; i++)
		{
			playerSelections[i] = new PlayerSelection();
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "Jeu")
		{
			GetComponent<PlayerInputManager>().DisableJoining();
		} else if (SceneManager.GetActiveScene().name == "Menu")
		{
			GetComponent<PlayerInputManager>().EnableJoining();
			if (playerSelections.All(p => p.IsReady()) && playerCount > 1)
			{
				if (Time.time - timerReady > 3f)
				{
					SceneManager.LoadScene("Jeu");
				}
			}
			else
			{
				timerReady = Time.time;
			}
		}
	}

	void OnPlayerJoined(PlayerInput p)
	{
		if (playerCount >= 4)
			return;

		playerCount++;

		InitializePlayerUI(p.playerIndex, p);

		uiDocument.gameObject.GetComponent<UIChoixPersonnage>().joueurs[p.playerIndex] = p.gameObject;
		uiDocument.gameObject.GetComponent<UIChoixPersonnage>().SetPlayerCount(playerCount);
		return;
	}

	private void InitializePlayerUI(int playerIndex, PlayerInput p)
	{
		VisualElement root = uiDocument.rootVisualElement;
		VisualElement playerUI = root.Q($"player-{playerIndex + 1}");
		if (playerUI == null)
			return;

		DropdownField dropdown = playerUI.Q<DropdownField>($"bouee-{playerIndex + 1}");
		DropdownField dropdownSkin = playerUI.Q<DropdownField>($"character-{playerIndex + 1}");
		Button readyButton = playerUI.Q<Button>($"ready-{playerIndex + 1}");

		playerUI.Q<VisualElement>($"texture-{playerIndex + 1}").style.visibility = Visibility.Visible;
		playerUI.Q<VisualElement>($"textureSkin-{playerIndex + 1}").style.visibility = Visibility.Visible;

		playerSelections[playerIndex].Initialize(dropdown, dropdownSkin, readyButton, p);
	}

	void OnPlayerLeft(PlayerInput p)
	{
		playerSelections[p.playerIndex] = new PlayerSelection();
		Destroy(p.gameObject);
		playerCount--;
	}

}
