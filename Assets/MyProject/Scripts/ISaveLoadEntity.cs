public interface ISaveLoadEntity<T> : ISaveLoadEntity where T : SaveData
{
    public T Restore();
}

public interface ISaveLoadEntity
{
    public void Save();
}