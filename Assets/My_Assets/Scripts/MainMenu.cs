using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject choixJoueur;

    private UIDocument uiDocument;
    private VisualElement root;
    private Button playButton;
    private Button optionButton;
    private Button quitButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
		optionButton = root.Q<Button>("OptionButton");
		quitButton = root.Q<Button>("QuitButton");

		if (playButton != null)
			playButton.clicked += OnPlayButtonClicked;

		if (optionButton != null)
			optionButton.clicked += OnOptionButtonClicked;

		if (quitButton != null)
			quitButton.clicked += OnQuitButtonClicked;
	}

	void OnQuitButtonClicked()
	{
		Debug.Log("Quit button clicked!");
		//à enlever une fois le jeu buildé
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	void OnOptionButtonClicked()
	{
		Debug.Log("Option button clicked!");
	}

    void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked!");
        transform.gameObject.SetActive(false);
        choixJoueur.GetComponent<UIDocument>().rootVisualElement.style.visibility = Visibility.Visible;
	}
}
