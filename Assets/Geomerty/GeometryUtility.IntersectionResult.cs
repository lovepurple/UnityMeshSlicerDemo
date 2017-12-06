using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 各种相交检测结果载体
/// </summary>
public partial class GeometryUtility
{
    /// <summary>
    /// 模型的三角面
    /// </summary>
    public class Triangle
    {
        //顶点数据
        private Vector3 m_vertexPos0;
        private Vector3 m_vertexPos1;
        private Vector3 m_vertexPos2;

        //uv数据
        private Vector2 m_uv0;
        private Vector2 m_uv1;
        private Vector2 m_uv2;

        //三角面的绕向
        private TriangleWindingOrder m_windingOrder;

        public Triangle(Vector3 vertexPosition0, Vector3 vertexPosition1, Vector3 vertexPosition2)
            : this(vertexPosition0, vertexPosition1, vertexPosition2, Vector2.zero, Vector2.zero, Vector2.zero)
        {

        }

        public Triangle(Vector3 vertexPosition0, Vector3 vertexPosition1, Vector3 vertexPosition2, Vector2 uv0, Vector2 uv1, Vector2 uv2)
        {
            this.m_vertexPos0 = vertexPosition0;
            this.m_vertexPos1 = vertexPosition1;
            this.m_vertexPos2 = vertexPosition2;

            this.m_uv0 = uv0;
            this.m_uv1 = uv1;
            this.m_uv2 = uv2;

            this.m_windingOrder = GeometryUtility.GetTriangleWindingOrder(vertexPosition0, vertexPosition1, vertexPosition2);
        }

        public TriangleWindingOrder WindingOrder
        {
            get
            {
                return this.m_windingOrder;
            }
            set
            {
                if (this.m_windingOrder != value)
                {
                    Vector3 tempPos = this.m_vertexPos0;
                    Vector2 tempUV = this.m_uv0;

                    this.m_vertexPos0 = this.m_vertexPos2;
                    this.m_vertexPos2 = tempPos;

                    this.m_uv0 = this.m_uv2;
                    this.m_uv2 = tempUV;
                }
            }
        }

        public Vector3 VertexPosition0
        {
            get { return m_vertexPos0; }
        }
        public Vector3 VertexPosition1
        {
            get { return m_vertexPos1; }
        }
        public Vector3 VertexPosition2
        {
            get { return m_vertexPos2; }
        }

        public Vector2 UV0
        {
            get { return m_uv0; }
        }
        public Vector2 UV1
        {
            get { return m_uv1; }
        }
        public Vector2 UV2
        {
            get { return m_uv2; }
        }
    }

    /// <summary>
    /// 平面与三角形相交结果
    /// </summary>
    public class PlaneTriangleIntersectionResult
    {
        //平面正方向的顶点
        private List<Triangle> m_planeUpperTriangleList = new List<Triangle>();

        //平面反方向的顶点
        private List<Triangle> m_planeUnderTriangleList = new List<Triangle>();

        //相交的顶点
        private List<Triangle> m_planeIntersectionTriangleList = new List<Triangle>();

        private List<Vector3> m_planeIntersectionPointList = new List<Vector3>();

        public bool IsIntersection
        {
            get
            {
                return m_planeUpperTriangleList.Count > 0;
            }
        }

        public List<Vector3> IntersectionPointList
        {
            get { return m_planeIntersectionPointList; }
        }

        public List<Triangle> UpperTriangleList
        {
            get { return m_planeUpperTriangleList; }
        }

        public List<Triangle> UnderTriangleList
        {
            get { return m_planeUnderTriangleList; }
        }

    }
}
