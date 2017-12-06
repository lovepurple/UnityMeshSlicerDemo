using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MathUtils
{

    /// <summary>
    /// 将区间上的数映射到另一区间
    /// </summary>
    /// <param name="mapValue"></param>
    /// <param name="iMax"></param>
    /// <param name="iMin"></param>
    /// <param name="oMax"></param>
    /// <param name="oMin"></param>
    /// <returns></returns>
    public static float Remap(float mapValue, float iMax, float iMin, float oMax, float oMin)
    {
        return oMin + (mapValue - iMin) * (oMax - oMin) / (iMax - iMin);
    }


}
