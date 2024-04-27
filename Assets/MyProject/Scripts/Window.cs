using UnityEngine;

public class Window : MonoBehaviour 
{
    protected Player _player;
    public virtual void Construct(Player player)
    {
        _player = player;
    }
}
