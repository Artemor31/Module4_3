using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Window
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _expFill;
    [SerializeField] private InventoryWindow _inventory;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private TextMeshProUGUI _goldText;

    public override void Construct(Player player)
    {
        base.Construct(player);
        _player.GetComponent<Health>().OnHealthChanged += OnHealthChanged;
        _player.Experience.OnExpChanged += OnExpChanged;
        _player.Gold.OnGoldChanged += OnGoldChanged;
        OnGoldChanged(_player.Gold.CurrentGold);

        _inventoryButton.onClick.AddListener(OnInventoryClicked);
    }

    private void OnGoldChanged(int value) => _goldText.text = value.ToString();
    private void OnExpChanged(int current, int max) => _expFill.fillAmount = current / (float)max;
    private void OnInventoryClicked() => _inventory.gameObject.SetActive(true);
    private void OnHealthChanged(Health health) => _healthFill.fillAmount = health.CurrentHealth / health.MaxHealth;

    private void OnDestroy()
    {
        if (_player == null) return;

        _player.GetComponent<Health>().OnHealthChanged -= OnHealthChanged; 
        _player.Experience.OnExpChanged -= OnExpChanged;
        _player.Gold.OnGoldChanged -= OnGoldChanged;
        _inventoryButton.onClick.RemoveListener(OnInventoryClicked);
    }
}
