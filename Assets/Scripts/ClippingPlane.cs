using UnityEngine;

[ExecuteAlways]
public class ClippingPlane : MonoBehaviour
{
    public Material mat;

    void Update()
    {
        Plane plane = new Plane(transform.up, transform.position);
        //transfer values from plane to vector4
        Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        //pass vector to shader
        mat.SetVector("_Plane", planeRepresentation);
    }
}