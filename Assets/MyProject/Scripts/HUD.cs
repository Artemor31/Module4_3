using UnityEngine;
using UnityEngine.UI;

public class HUD : Window
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private InventoryWindow _inventory;
    [SerializeField] private Button _inventoryButton;

    public override void Construct(Player player)
    {
        base.Construct(player);
        _player.GetComponent<Health>().OnHealthChanged += OnHealthChanged;
        _inventoryButton.onClick.AddListener(OnInventoryClicked);
    }

    private void OnInventoryClicked() => _inventory.gameObject.SetActive(true);
    private void OnHealthChanged(Health health) => _healthFill.fillAmount = health.CurrentHealth / health.MaxHealth;

    private void OnDestroy()
    {
        if (_player == null) return;

        _player.GetComponent<Health>().OnHealthChanged -= OnHealthChanged;
        _inventoryButton.onClick.RemoveListener(OnInventoryClicked);
    }
}
