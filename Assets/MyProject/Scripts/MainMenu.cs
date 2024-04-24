using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private List<RoleButton> _roleButtons;

    private void OnEnable()
    {
        _startGame.onClick.AddListener(StartClicked);
        foreach (var button in _roleButtons)
        {
            button.Init();
            button.OnClicked += OnClicked;
        }
    }

    private void OnClicked(Role role)
    {
        StaticData.PlayerRole = role;

        foreach (var item in _roleButtons)
        {
            if (item.Role == role)
            {
                item.ResetView();
            }
            else
            {
                item.GrayView();
            }
        }

    }

    private void StartClicked()
    {
        if (StaticData.PlayerRole == null) return;
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        _startGame.onClick.RemoveListener(StartClicked);
        foreach (var button in _roleButtons)
        {
            button.UnInit();
            button.OnClicked -= OnClicked;
        }
    }
}

[Serializable]
public class RoleButton
{
    public event Action<Role> OnClicked;
    public Role Role;
    public Button Button;

    public void Init() => Button.onClick.AddListener(OnButtonClicked);
    private void OnButtonClicked() => OnClicked?.Invoke(Role);
    public void UnInit() => Button.onClick.RemoveListener(OnButtonClicked);
    public void ResetView() => Button.image.color = Color.white;
    public void GrayView() => Button.image.color = Color.gray;
}
