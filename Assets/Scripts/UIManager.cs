using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private DiContainer container;
    private ScreenDatabase screenDatabase;
    private Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();
    private Canvas canvas;

    // Inject dependencies through Zenject
    [Inject]
    public void Construct(DiContainer container)
    {
        this.container = container ?? throw new System.ArgumentNullException(nameof(container), "DiContainer cannot be null");
    }

    private void Awake()
    {
        InitializeCanvas();
        StartCoroutine(LoadScreens());
    }

    private void InitializeCanvas()
    {
        canvas = FindFirstObjectByType<Canvas>();
    }

    // Load screen database asynchronously
    private IEnumerator LoadScreens()
    {
        var handle = Addressables.LoadAssetAsync<ScreenDatabase>("ScreensAddress");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            screenDatabase = handle.Result;
            InitializeScreens();
            ToggleScreen("IGPostScreen", true); // Mở màn hình đầu tiên sau khi tải
        }
    }

    private void InitializeScreens()
    {
        foreach (var screen in screenDatabase.screens)
        {
            CreateScreen(screen.nameScreen);
        }
    }

    private void CreateScreen(string screenName)
    {
        if (screens.ContainsKey(screenName)) return;

        // Tìm prefab của màn hình
        GameObject prefab = GetScreenPrefab(screenName);
        if (prefab == null)
        {
            Debug.LogError($"Prefab for screen '{screenName}' not found.");
            return;
        }

        // Instantiate prefab và tiêm phụ thuộc
        GameObject screenInstance = container.InstantiatePrefab(prefab);
        if (screenInstance == null)
        {
            Debug.LogError($"Failed to instantiate prefab for screen '{screenName}'.");
            return;
        }

        // Thiết lập parent và ẩn màn hình ban đầu
        screenInstance.transform.SetParent(canvas.transform, false);
        screenInstance.SetActive(false);

        // Lưu màn hình vào danh sách
        screens[screenName] = screenInstance;
    }

    private GameObject GetScreenPrefab(string screenName)
    {
        var screen = screenDatabase.screens.Find(s => s.nameScreen == screenName);
        return screen?.prefab;
    }

    public void ToggleScreen(string screenName, bool state)
    {
        DeActiveAllScreen();

        if (screens.ContainsKey(screenName))
        {
            screens[screenName].SetActive(state);
        }
    }

    private void DeActiveAllScreen()
    {
        foreach (var screen in screens)
        {
            screen.Value.SetActive(false);
        }
    }
}
