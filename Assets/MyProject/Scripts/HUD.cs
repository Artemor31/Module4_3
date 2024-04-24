using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private InventoryWindow _inventory;
    [SerializeField] private Button _inventoryButton;
    private Health _player;

    void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Health>();
        _player.OnHealthChanged += OnHealthChanged;
        _inventoryButton.onClick.AddListener(OnInventoryClicked);
    }

    private void OnInventoryClicked() => _inventory.gameObject.SetActive(true);

    private void OnHealthChanged(Health health) => _healthFill.fillAmount = health.CurrentHealth / health.MaxHealth;

    private void OnDestroy()
    {
        _player.OnHealthChanged -= OnHealthChanged;
        _inventoryButton.onClick.RemoveListener(OnInventoryClicked);
    }
}
