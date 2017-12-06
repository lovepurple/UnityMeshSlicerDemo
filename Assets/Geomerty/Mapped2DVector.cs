using UnityEngine;

/// <summary>
/// 3维向量降维度到2维
/// 
/// 降维就是在另一个维度上的投影(任何空间都是)，3D 向量降维到2D 向一个平面上投影
/// 比如2维空间降维到1维，也就是把2维空间点在一条直线上做投射
/// </summary>
public class Mapped2DVector
{
    private Vector3 m_originalVector3;
    private Vector2 m_mappedValue;

    public Mapped2DVector(Vector3 originalVector, Vector3 uAxis, Vector3 vAxis)
    {
        this.m_originalVector3 = originalVector;
        this.m_mappedValue.x = Vector3.Dot(originalVector, uAxis);
        this.m_mappedValue.y = Vector3.Dot(originalVector, vAxis);
    }

    public Vector3 OriginalVector
    {
        get { return m_originalVector3; }
    }

    public Vector2 MappedVector2
    {
        get { return m_mappedValue; }
    }

    /// <summary>
    /// 获取Vector3的降维点
    /// </summary>
    /// <param name="originVector">原3维点</param>
    /// <param name="planeNormal">2维平面法线</param>
    /// <returns></returns>
    public static Mapped2DVector MapVector3ToVector2(Vector3 originVector, Vector3 planeNormal)
    {
        //构建维度x,y,z轴,x,y 计算出的方向是平面的x,z方向

        //辅助轴
        Vector3 rightAxis = Mathf.Abs(planeNormal.x) > Mathf.Abs(planeNormal.y) ? new Vector3(0, 1, 0) : new Vector3(1, 0, 0);

        Vector3 vAxis = Vector3.Cross(rightAxis, planeNormal).normalized;
        Vector3 uAxis = Vector3.Cross(planeNormal, vAxis);

        return new Mapped2DVector(originVector, uAxis, vAxis);
    }
}
