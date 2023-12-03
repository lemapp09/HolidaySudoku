using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputField))]
public class InputFieldController : MonoBehaviour, IPointerClickHandler
{
    private InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int number = GameController.Instance.CurrentSelectedNumber;

        if (number != 0) // If the number is not zero, then update the input field value
        {
            inputField.text = number.ToString();
        }
    }
}