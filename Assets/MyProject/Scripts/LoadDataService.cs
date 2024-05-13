using UnityEngine;

public class LoadDataService
{
    private const string ProgressionPath = "Progression";

    public Progression LoadProgression() =>
        Resources.Load<Progression>(ProgressionPath);
}