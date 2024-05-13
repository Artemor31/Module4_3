using System.Collections.Generic;

public class SaveService
{
    private List<ISaveLoadEntity> _entities = new();

    public void SaveAll()
    {
        foreach (var entity in _entities)
        {
            entity.Save();
        }
    }

    public void AddEntity(ISaveLoadEntity entity) => _entities.Add(entity);

    public void RemoveEntity(ISaveLoadEntity entity) => _entities.Remove(entity);
}