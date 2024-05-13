using System;

public class Gold
{
    public event Action<int> OnGoldChanged;
    public int CurrentGold { get; private set; }

    public Gold(PlayerSaveData data)
    {
        AddGold(data.Gold);
    }

    public void AddGold(int delta)
    {
        CurrentGold += delta;
        OnGoldChanged?.Invoke(CurrentGold);
    }
}