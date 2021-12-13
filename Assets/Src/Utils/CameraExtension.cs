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

    public static bool IsObjectVisible(this Camera camera, Vector3 point)
    {
        Vector3 viewPoint = camera.WorldToViewportPoint(point);
        float x = viewPoint.x;
        float y = viewPoint.y;
        float z = viewPoint.z;
        bool isInCameraBox = (x > 0 && x < 1 && y > 0 && y < 1 && z > 0);

        Vector3 direction = point - camera.gameObject.transform.position;
        float distance = direction.magnitude;
        bool isNotBlocked = !Physics.Raycast(camera.gameObject.transform.position, direction, distance, LayerMask.GetMask("Map"));
        return isInCameraBox && isNotBlocked;
    }
}