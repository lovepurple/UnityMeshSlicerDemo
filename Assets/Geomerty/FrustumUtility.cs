using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各种锥体工具函数
/// </summary>
public static partial class GeometryUtility
{
    public static List<Vector3> GetFrustumConnerVertices(float fovDeg, float aspect, float zNear, float zFar)
    {
        List<Vector3> connerVertexList = new List<Vector3>(8);

        float tanHalfFov = Mathf.Tan(fovDeg / 2 * Mathf.Deg2Rad);
        float yNearValue = zNear * tanHalfFov;
        float xNearValue = yNearValue * aspect;

        float yFarValue = zFar * tanHalfFov;
        float xFarValue = yFarValue * aspect;

        GeometryUtility.AddVertexByExtensionList(xNearValue, yNearValue, zNear, ref connerVertexList);
        GeometryUtility.AddVertexByExtensionList(xFarValue, yFarValue, zFar, ref connerVertexList);

        return connerVertexList;
    }


    public static List<Vector3> GetCameraFrustumWorldConnerVerteies(Camera camera)
    {
        return GetCameraFrustumWorldConnerVerticesByZ(camera, camera.nearClipPlane, camera.farClipPlane);
    }

    /// <summary>
    /// 获取摄像机指定z范围的视锥顶点
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="targetNearZ"></param>
    /// <param name="targetFarZ"></param>
    /// <returns></returns>
    public static List<Vector3> GetCameraFrustumWorldConnerVerticesByZ(Camera camera, float targetNearZ, float targetFarZ)
    {
        List<Vector3> frustumVertexList = GetFrustumConnerVertices(camera.fieldOfView, camera.aspect, targetNearZ, targetFarZ);
        for (int i = 0; i < frustumVertexList.Count; ++i)
        {
            Vector3 vertexLocalPos = frustumVertexList[i];
            Vector3 vertexWorldPos = camera.transform.localToWorldMatrix.MultiplyPoint(vertexLocalPos);

            frustumVertexList[i] = vertexWorldPos;
        }

        return frustumVertexList;
    }

    /// <summary>
    /// 创建锥形Mesh
    /// </summary>
    /// <param name="frustumConnerVertexList"></param>
    /// <returns></returns>
    /// <remarks>Left Top Right Bottom Forward Back</remarks>
    public static Mesh GenerateFrustumMesh(List<Vector3> frustumConnerVertexList)
    {
        Mesh frustumMesh = new Mesh();
        frustumMesh.vertices = frustumConnerVertexList.ToArray();
        frustumMesh.triangles = new int[]
        {
            0,4,7,      //left
            0,7,3,
            0,4,5,      //top
            0,5,1,
            1,5,6,      //right
            1,6,2,
            3,7,6,      //bottom
            3,6,2,
            3,0,2,      //front
            0,1,2,
            7,4,6,      //back
            4,5,6
        };


        frustumMesh.RecalculateNormals();
        frustumMesh.RecalculateTangents();

        return frustumMesh;

    }

}

