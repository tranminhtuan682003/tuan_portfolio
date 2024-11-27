using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IGPostScreenManager : MonoBehaviour
{
    [Inject] private UIManager uIManager;

    private Button profileButton;

    private void Awake()
    {
        profileButton = FindButtonByName(transform, "ProfileButton");
        profileButton.onClick.AddListener(OnProfileButtonClick);
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

    private void OnProfileButtonClick()
    {
        uIManager.ToggleScreen("IGProfileScreen", true);
    }
}
