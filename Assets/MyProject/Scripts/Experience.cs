using System;

public class Experience
{
    public event Action<int, int> OnExpChanged;
    public event Action<int> OnLevelUp;

    public int CurrentLevel { get; private set; }
    public int CurrentExp {  get; private set; }
    public int MaxExp { get; private set; }

    private readonly Progression _progression;

    public Experience(Progression progression, PlayerSaveData data)
    {
        _progression = progression;
        LoadProgress(data);
    }

    public void AddExp(int exp)
    {
        CurrentExp += exp;

        if (CurrentExp > MaxExp)
        {
            CurrentExp -= MaxExp;
            CurrentLevel++;
            MaxExp = _progression.MaxExpFor(CurrentLevel);

            OnLevelUp?.Invoke(CurrentLevel);
        }

        OnExpChanged?.Invoke(CurrentExp, MaxExp);
    }

    private void LoadProgress(PlayerSaveData data)
    {
        CurrentLevel = data.Level;
        CurrentExp = data.Exp;
        MaxExp = _progression.MaxExpFor(CurrentLevel);
        OnLevelUp?.Invoke(CurrentLevel);
        OnExpChanged?.Invoke(CurrentExp, MaxExp);
    }
}