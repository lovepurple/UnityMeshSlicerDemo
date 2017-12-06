using UnityEngine;

public class AABBBounds
{
    //不能用0，做为初始值
    private Vector3 m_min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    private Vector3 m_max = new Vector3(float.MinValue, float.MinValue, float.MinValue);


    public void UpdateX(float x)
    {
        this.m_min.x = Mathf.Min(this.m_min.x, x);
        this.m_max.x = Mathf.Max(this.m_max.x, x);
    }

    public void UpdateY(float y)
    {
        this.m_min.y = Mathf.Min(this.m_min.y, y);
        this.m_max.y = Mathf.Max(this.m_max.y, y);
    }

    public void UpdateZ(float z)
    {
        this.m_min.z = Mathf.Min(this.m_min.z, z);
        this.m_max.z = Mathf.Max(this.m_max.z, z);
    }

    public void UpdatePoint(Vector3 point)
    {
        UpdateX(point.x);
        UpdateY(point.y);
        UpdateZ(point.z);
    }


    public Vector3 Min
    {
        get { return m_min; }
    }

    public Vector3 Max
    {
        get { return m_max; }
    }

    public Vector3 Center
    {
        get
        {
            return (this.m_max + this.m_min) / 2;
        }
    }

    public Vector3 Size
    {
        get { return (this.Max - this.Min); }
    }

    public Vector3 Extends
    {
        get { return Size / 2; }
    }

}
