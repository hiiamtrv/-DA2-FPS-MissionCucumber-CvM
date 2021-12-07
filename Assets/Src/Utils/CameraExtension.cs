using UnityEngine;

public static class CameraExtension
{
    public static bool IsObjectVisible(this Camera camera, Renderer renderer)
    {
        bool isInCameraBox = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);

        Vector3 direction = renderer.gameObject.transform.position - camera.gameObject.transform.position;
        float distance = direction.magnitude;
        bool isNotBlocked = !Physics.Raycast(camera.gameObject.transform.position, direction, distance, LayerMask.GetMask("Map"));
        return isInCameraBox && isNotBlocked;
    }
}