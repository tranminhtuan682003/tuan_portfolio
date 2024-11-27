using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IGProfileScreenManager : MonoBehaviour
{
    [Inject] private UIManager uIManager;
    private Button homeButton;

    private void Awake()
    {
        homeButton = FindButtonByName(transform, "HomeButton");
        homeButton.onClick.AddListener(OnHomeButtonClick);

    }

    private Button FindButtonByName(Transform parentTransform, string buttonName)
    {
        Button[] buttons = parentTransform.GetComponentsInChildren<Button>(true);
        foreach (var button in buttons)
        {
            if (button.name == buttonName)
            {
                return button;
            }
        }
        return null;
    }

    private void OnHomeButtonClick()
    {
        Debug.Log("Da nhan");
        uIManager.ToggleScreen("IGPostScreen", true);
    }
}
