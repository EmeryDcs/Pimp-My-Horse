using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSelection
{
    public static int nbSkin;
	public static int nbBouee;

	private DropdownField dropdown;
    private DropdownField dropdownSkin;
    private Button readyButton;
    private bool isReady = true;
	private bool hasValueDropDownChanged = false;
	private float timerDropdown = 0f;

	public void Initialize(DropdownField dropdown, DropdownField dropdownSkin, Button readyButton, PlayerInput playerInput)
    {
        this.dropdown = dropdown;
        this.readyButton = readyButton;
		this.dropdownSkin = dropdownSkin;

        playerInput.actions["Ready"].performed += ctx =>
        {
            isReady = !isReady;
            readyButton.text = isReady ? "Ready" : "Not Ready";
            if (isReady)
            {
                readyButton.style.backgroundColor = new Color(0.38823529411764707f, 0f, 0.3254901960784314f, 1.0f);

			}
            else
            {
                readyButton.style.backgroundColor = new Color(0.07450980392156863f, 0.38823529411764707f, 0.0f, 1.0f);
            }
        };

        playerInput.actions["Move"].performed += ctx =>
		{
			if (!isReady)
            {
                NavigateDropdown(ctx);
            }
        };

        playerInput.actions["InputSkinDown"].performed += ctx =>
        {
            if (!isReady)
            {
                ChangeSkin(ctx);
            }
        };
    }

    private void ChangeSkin(InputAction.CallbackContext context)
	{
		float direction = context.ReadValue<Vector2>().y;

		if (!hasValueDropDownChanged)
		{
			if (direction < -0.8)
			{
				if (dropdownSkin.index < nbSkin-1)
					dropdownSkin.index += 1;
				hasValueDropDownChanged = true;
				timerDropdown = Time.time;
			}
			else if (direction > 0.8)
			{
				if (dropdownSkin.index > 0)
					dropdownSkin.index -= 1;
				hasValueDropDownChanged = true;
				timerDropdown = Time.time;
			}
		}
		else if (hasValueDropDownChanged)
		{
			if (Time.time - timerDropdown > 0.5f)
				hasValueDropDownChanged = false;
		}
	}

    private void NavigateDropdown(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<Vector2>().y;

        if (!hasValueDropDownChanged) 
        {
            if (direction < -0.8)
			{
                if (dropdown.index < nbBouee-1)
                    dropdown.index += 1;
                hasValueDropDownChanged = true;
                timerDropdown = Time.time;
            }
            else if (direction > 0.8)
			{
				if (dropdown.index > 0)
                    dropdown.index -= 1;
                hasValueDropDownChanged = true;
                timerDropdown = Time.time;
            }
        }
        else if (hasValueDropDownChanged)
        {
            if (Time.time-timerDropdown > 0.5f)
                hasValueDropDownChanged = false;
        }
	}

    public bool IsReady()
	{
		return isReady;
	}
}
