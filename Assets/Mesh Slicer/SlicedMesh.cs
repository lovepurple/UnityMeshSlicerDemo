using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切割后的模型
/// </summary>
public class SlicedMesh
{
    private List<GeometryUtility.Triangle> m_upperMeshTriangleList = new List<GeometryUtility.Triangle>();
    private List<GeometryUtility.Triangle> m_underMeshTriangleList = new List<GeometryUtility.Triangle>();
    private List<Vector3> m_crossVertexList = new List<Vector3>();

    private GeometryUtility.TriangleWindingOrder m_windingOrder = GeometryUtility.TriangleWindingOrder.CounterClockWise;

    public SlicedMesh(GeometryUtility.TriangleWindingOrder meshWindingOrder)
    {
        this.m_windingOrder = meshWindingOrder;
    }

    public List<GeometryUtility.Triangle> UpperMeshTriangleList
    {
        get { return this.m_upperMeshTriangleList; }
    }
    public List<GeometryUtility.Triangle> UnderMeshTriangleList
    {
        get { return this.m_underMeshTriangleList; }
    }

    public List<Vector3> CrossMeshVertexList
    {
        get { return this.m_crossVertexList; }
    }

    public Mesh UpperMesh
    {
        get { return GeometryUtility.CreateUnityMeshByTriangleList(m_upperMeshTriangleList, this.m_windingOrder); }
    }
    public Mesh UnderMesh
    {
        get { return GeometryUtility.CreateUnityMeshByTriangleList(m_underMeshTriangleList, this.m_windingOrder); }
    }

    public Mesh GetUpperCrossMesh(Vector3 crossPlaneNormal)
    {
        return GeometryUtility.CreateConvexhullMeshByMonotoneChain(m_crossVertexList, -crossPlaneNormal, GeometryUtility.TriangleWindingOrder.ClockWise);
    }

    public Mesh GetUnderCrossMesh(Vector3 crossPlaneNormal)
    {
        return GeometryUtility.CreateConvexhullMeshByMonotoneChain(m_crossVertexList, crossPlaneNormal, GeometryUtility.TriangleWindingOrder.ClockWise);
    }

}
