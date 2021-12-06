using UnityEngine;

public static class CameraExtension
{
    public static bool IsObjectVisible(this Camera camera, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
    }
}