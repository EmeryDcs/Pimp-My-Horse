//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class SelectPlayer : MonoBehaviour
//{
//	public GameObject playerManager;
//	private List<GameObject> listPlayer;

//	private UIDocument uiDocument;
//    private VisualElement root;
//	private RadioButtonGroup radioGroupPlayer1;
//	private RadioButtonGroup radioGroupPlayer2;
//	private RadioButtonGroup radioGroupPlayer3;
//	private RadioButtonGroup radioGroupPlayer4;

//	void Update()
//	{
//		switch (listPlayer.Count)
//		{
//			case 0:
//				break;
//			case 1:
//				root.Q<VisualElement>("Joueur1").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur2").style.display = DisplayStyle.None;
//				root.Q<VisualElement>("Joueur3").style.display = DisplayStyle.None;
//				root.Q<VisualElement>("Joueur4").style.display = DisplayStyle.None;
//				break;
//			case 2:
//				root.Q<VisualElement>("Joueur1").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur2").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur3").style.display = DisplayStyle.None;
//				root.Q<VisualElement>("Joueur4").style.display = DisplayStyle.None;
//				break;
//			case 3:
//				root.Q<VisualElement>("Joueur1").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur2").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur3").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur4").style.display = DisplayStyle.None;
//				break;
//			case 4:
//				root.Q<VisualElement>("Joueur1").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur2").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur3").style.display = DisplayStyle.Flex;
//				root.Q<VisualElement>("Joueur4").style.display = DisplayStyle.Flex;
//				break;
//			default:
//				break;
//		}
//	}

//	// Start is called before the first frame update
//	void OnEnable()
//    {
//		listPlayer = playerManager.GetComponent<PlayerManager>().listPlayer;

//		uiDocument = GetComponent<UIDocument>();
//		root = uiDocument.rootVisualElement;

//		radioGroupPlayer1 = root.Q<VisualElement>("Joueur1").Q<RadioButtonGroup>();
//		radioGroupPlayer2 = root.Q<VisualElement>("Joueur2").Q<RadioButtonGroup>();
//		radioGroupPlayer3 = root.Q<VisualElement>("Joueur3").Q<RadioButtonGroup>();
//		radioGroupPlayer4 = root.Q<VisualElement>("Joueur4").Q<RadioButtonGroup>();

//		if (radioGroupPlayer1 != null)
//			radioGroupPlayer1.RegisterCallback<ChangeEvent<int>>(OnRadioGroupPlayer1Change);

//		if (radioGroupPlayer2 != null)
//			radioGroupPlayer2.RegisterCallback<ChangeEvent<int>>(OnRadioGroupPlayer2Change);

//		if (radioGroupPlayer3 != null)
//			radioGroupPlayer3.RegisterCallback<ChangeEvent<int>>(OnRadioGroupPlayer3Change);

//		if (radioGroupPlayer4 != null)
//			radioGroupPlayer4.RegisterCallback<ChangeEvent<int>>(OnRadioGroupPlayer4Change);
//	}

//	void OnRadioGroupPlayer1Change(ChangeEvent<int> evt)
//	{
//		Debug.Log("Player 1 selected: " + evt.newValue);
//	}
	
//	void OnRadioGroupPlayer2Change(ChangeEvent<int> evt)
//	{
//		Debug.Log("Player 2 selected: " + evt.newValue);
//	}

//	void OnRadioGroupPlayer3Change(ChangeEvent<int> evt)
//	{
//		Debug.Log("Player 3 selected: " + evt.newValue);
//	}

//	void OnRadioGroupPlayer4Change(ChangeEvent<int> evt)
//	{
//		Debug.Log("Player 4 selected: " + evt.newValue);
//	}
//}
