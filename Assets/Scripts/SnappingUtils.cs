using UnityEngine;

public static class SnappingUtils
{
    public static bool PlaceOnGround(Vector3 transformPosition, LayerMask groundLayerMask, out Vector3 position, out Vector3 normal)
    {
        Ray ray = new Ray(transformPosition, Vector3.down);
        RaycastHit hit;
        position = Vector3.zero;
        normal = Vector3.up;
        bool hitGround = false;
        if (Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity,
            groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            position = ray.GetPoint(hit.distance);
            normal = hit.normal;
            hitGround = true;
        }

        return hitGround;
    }
}