using System.Numerics;
using RayTracingInOneWeekend.Utility.Extensions;

namespace RayTracingInOneWeekend.Utility;

public struct Camera
{
    public Vector3 Origin;
    public Vector3 LowerLeftCorner;
    public Vector3 Horizontal;
    public Vector3 Vertical;
    public Vector3 u, v, w;
    float          lensRadius;

    public Camera(
        Vector3 lookFrom,
        Vector3 lookAt,
        Vector3 vUp,
        float   vFov,
        float   aspectRatio,
        float   aperture,
        float   focusDist)
    {
        float theta = vFov * MathF.PI / 180.0f;
        float h = MathF.Tan(theta / 2);
        float viewportHeight = 2.0f * h;
        float viewportWidth = aspectRatio * viewportHeight;

        w = Vector3.Normalize(lookFrom - lookAt);
        u = Vector3.Normalize(Vector3.Cross(vUp, w));
        v = Vector3.Cross(w, u);

        Origin = lookFrom;
        Horizontal = focusDist * viewportWidth * u;
        Vertical = focusDist * viewportHeight * v;
        LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - focusDist * w;
        lensRadius = aperture / 2;
    }

    public Ray GetRay(float s, float t)
    {
        Vector3 rd = lensRadius * Vector3Extension.RandomInUnitDisk();
        Vector3 offset = u * rd.X + v * rd.Y;
        return new Ray(Origin + offset, LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
    }
}