using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector3 Motion => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    public bool Attacking => Input.GetMouseButtonDown(0);
}
