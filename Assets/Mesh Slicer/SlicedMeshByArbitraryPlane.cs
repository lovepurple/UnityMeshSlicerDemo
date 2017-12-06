using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 使用任意平面切割模型
/// </summary>
public class SlicedMeshByArbitraryPlane : MonoBehaviour
{
    public Vector3 slicerNormal = Vector3.up;
    public float slicerDistance = 0;

    public GameObject TargetGameobject;

    public bool IncludeOppsiteSide = false;
    public bool IncludeIntersection = false;
    public bool RemainOriginGameObject = true;

    public Material SlicedUpperMaterial = null;
    public Material SlicedUpperIntersectionMaterial = null;
    public Material SlicedUnderMaterial = null;
    public Material SlicedUnderIntersectionMaterial = null;

    private MeshSlicer m_meshSlicer = null;


    private void OnGUI()
    {
        if (GUILayout.Button("slice mesh", GUILayout.Width(100), GUILayout.Height(60)))
        {
            Plane slicerPlane = new Plane(slicerNormal, slicerDistance);

            Mesh targetMesh = null;
            Material originalMaterial = null;

            MeshFilter meshFilter = TargetGameobject.GetComponentInChildren<MeshFilter>();
            if (meshFilter != null)
                targetMesh = meshFilter.mesh;
            else
            {
                SkinnedMeshRenderer skinMeshRenderer = TargetGameobject.GetComponentInChildren<SkinnedMeshRenderer>();
                if (skinMeshRenderer != null)
                {
                    targetMesh = skinMeshRenderer.sharedMesh;
                    originalMaterial = skinMeshRenderer.material;
                }
            }

            if (!targetMesh)
            {
                Debug.LogError("target mesh is null");
                return;
            }

            if (!originalMaterial)
            {
                MeshRenderer meshRenderer = TargetGameobject.GetComponentInChildren<MeshRenderer>();
                if (meshRenderer)
                    originalMaterial = meshRenderer.material;
            }
            m_meshSlicer = new MeshSlicer(targetMesh, slicerPlane);
            m_meshSlicer.Slice(IncludeIntersection, IncludeOppsiteSide);

            if (!SlicedUpperMaterial)
                SlicedUpperMaterial = originalMaterial;

            m_meshSlicer.RenderSlicedGameObject(SlicedUpperMaterial, SlicedUnderMaterial, SlicedUpperIntersectionMaterial, SlicedUnderIntersectionMaterial, TargetGameobject.name);

            if (RemainOriginGameObject)
                TargetGameobject.SetActive(false);
            else
                GameObject.Destroy(TargetGameobject);

        }


    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector3(0, slicerDistance, 0), new Vector3(5, 0, 5));
    }

}
