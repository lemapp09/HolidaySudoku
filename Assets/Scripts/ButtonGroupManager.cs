using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGroupManager : MonoBehaviour
{
    private Button[] buttons;
    private GameObject lastSelectedButton;

    void Awake()
    {
        // Find all buttons within this GameObject (make sure your buttons are children of this GameObject)
        buttons = GetComponentsInChildren<Button>();
        
        // Subscribe to each button's onClick event
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => ButtonClicked(button.gameObject));
        }
    }

    private void ButtonClicked(GameObject clickedButton)
    {
        if (lastSelectedButton != null)
        {
            // Optionally reset the last selected button to its normal state here
        }

        // Set the clicked button as the last selected one
        lastSelectedButton = clickedButton;

        // Optionally, set the button as 'selected' in the UI here
        // This will depend on how you want to visually represent the 'selected' state.
    }

    void Update()
    {
        // Check if the current selected GameObject is null or not a child of this button group
        if (EventSystem.current.currentSelectedGameObject == null ||
            EventSystem.current.currentSelectedGameObject.transform.parent != transform)
        {
            // If the last selected button is not null, set it as the selected GameObject in the EventSystem
            if (lastSelectedButton != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }
        }
        else
        {
            // Update last selected button based on the current selection
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }
    }
}