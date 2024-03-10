using UnityEngine;

public class CharacterVision : MonoBehaviour
{
    [SerializeField] private Camera cameraObject;

    private bool IsObjectObstructed(GameObject obj) =>
        !(Physics.Linecast(cameraObject.transform.position, obj.transform.position, out RaycastHit hit) 
          && hit.collider.gameObject == obj);

    public bool IsObjectVisible(GameObject obj)
    {
        if (IsObjectObstructed(obj)) return false;
        
        var bounds = obj.GetComponent<Collider>().bounds;
        var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cameraObject);

        return GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);

    }
}
