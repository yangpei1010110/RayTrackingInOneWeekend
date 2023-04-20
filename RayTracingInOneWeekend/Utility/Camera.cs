using System.Numerics;

namespace RayTracingInOneWeekend.Utility;

public struct Camera
{
    public Vector3 Origin;
    public Vector3 LowerLeftCorner;
    public Vector3 Horizontal;
    public Vector3 Vertical;

    public Camera()
    {
        float aspectRatio = 16.0f / 9.0f;
        float viewportHeight = 2.0f;
        float viewportWidth = aspectRatio * viewportHeight;
        float focalLength = 1.0f;

        Origin = Vector3.Zero;
        Horizontal = new Vector3(viewportWidth, 0, 0);
        Vertical = new Vector3(0, viewportHeight, 0);
        LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - new Vector3(0, 0, focalLength);
    }

    public Ray GetRay(float u, float v) => new(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
}