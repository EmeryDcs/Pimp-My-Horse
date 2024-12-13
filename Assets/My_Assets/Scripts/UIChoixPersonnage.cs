using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIChoixPersonnage : MonoBehaviour
{
	public GameObject[] joueurs = new GameObject[4];
	public List<RenderTexture> renderTextureBouees = new List<RenderTexture>();
	public List<RenderTexture> renderTextureBonhommes = new List<RenderTexture>();
	public List<GameObject> skinPersoMenus = new List<GameObject>();

	public List<Material> materials = new List<Material>();
	public int nombreBouees;
	public int nombreSkins;

	private VisualElement root;
	private DropdownField[] dropdownJoueurs = new DropdownField[4];
	private DropdownField[] dropdownSkinJoueurs = new DropdownField[4];
	private int playerCount;

	private void Start()
	{
		root = GetComponent<UIDocument>().rootVisualElement;
		root.style.visibility = Visibility.Hidden;

		nombreBouees = renderTextureBouees.Count;
		nombreSkins = materials.Count;

		for (int i = 0; i < 4; i++)
		{
			dropdownJoueurs[i] = root.Q<DropdownField>($"bouee-{i+1}");
			dropdownSkinJoueurs[i] = root.Q<DropdownField>($"character-{i + 1}");
		}
	}

	private void Update()
	{
		//A changer pour être effectif face à 2 joueurs dont l'un est joueur 1 et l'autre joueur 3
		for (int i = 0; i < playerCount; i++)
		{
			GameObject bouee = joueurs[i].transform.GetChild(1).gameObject;
			GameObject skin = joueurs[i].transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

			skinPersoMenus[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = materials[dropdownSkinJoueurs[i].index];
			skin.GetComponent<SkinnedMeshRenderer>().material = materials[dropdownSkinJoueurs[i].index];
			root.Q<VisualElement>($"texture-{i + 1}").style.backgroundImage = Background.FromRenderTexture(renderTextureBouees[dropdownJoueurs[i].index]);

			for (int j = 0; j < nombreBouees; j++)
			{
				if (j == dropdownJoueurs[i].index)
					bouee.transform.GetChild(j).gameObject.SetActive(true);
				else
					bouee.transform.GetChild(j).gameObject.SetActive(false);
			}
		}
	}

	public void SetPlayerCount(int count)
	{
		playerCount = count;
	}

	public int GetNbBouees()
	{
		return nombreBouees;
	}

	public int GetNbSkins()
	{
		return nombreSkins;
	}
}
