using UnityEngine;

public class InputManager
{
    private Camera _camera;
    private RaycastHit[] _raycastHit = new RaycastHit[1];
    private LayerMask _layerMask;

    public Vector3 Motion => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    public bool Attacking => Input.GetMouseButtonDown(0);
    public bool Casting => Input.GetMouseButtonDown(1);
    public InputManager(Camera camera, LayerMask layerMask)
    {
        _camera = camera;
        _layerMask = layerMask;
    }

    public bool TryGetPointerWorldPosition(out Vector3 point)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.RaycastNonAlloc(ray, _raycastHit, 1000, _layerMask) > 0)
        {
            point = _raycastHit[0].point;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}
