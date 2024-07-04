using UnityEngine;

public class RotationTransformation : Transformation
{
    public Vector3 rotation;

    /*public override Vector3 Apply(Vector3 point)
    {
        //目前是只支持绕z轴旋转  z轴的矩阵
        /*
         * cosa  -sina   0  0
         * sina   cosa   0  0
         *   0     0     1  0
         *   0     0     0  1
         #1#
        
        //目前是绕X轴旋转  x轴的矩阵
        /*
         *   1     0       0    0
         *   0    cosa   -sina  0
         *   0    sina    cosa  0
         *   0     0       0    1
         #1#
       
        //目前是绕X轴旋转  x轴的矩阵
        /*
         *  cosa   0    -sina   0
         *   0     1      0     0
         *  sina   0     cosa   0
         *   0     0       0    1
         #1#

        float ranX = rotation.x * Mathf.Deg2Rad;    //将度数（degrees）转换为弧度（radians） 弧度 = 度数 * (π / 180)
        float ranY = rotation.y * Mathf.Deg2Rad;  
        float ranZ = rotation.z * Mathf.Deg2Rad;
        Vector3 cos = new Vector3(Mathf.Cos(ranX), Mathf.Cos(ranY), Mathf.Cos(ranZ));
        Vector3 sin = new Vector3(Mathf.Sin(ranX), Mathf.Sin(ranY), Mathf.Sin(ranZ));
        Vector3 xAxis = new Vector3(
            cos.y * cos.z,
            cos.x * sin.z + sin.x * sin.y * cos.z,
            sin.x * sin.z - cos.x * sin.y * cos.z
        );
        Vector3 yAxis = new Vector3(
            -cos.y * sin.z,
            cos.x * cos.z - sin.x * sin.y * sin.z,
            sin.x * cos.z + cos.x * sin.y * sin.z
        );
        Vector3 zAxis = new Vector3(
            sin.y,
            -sin.x * cos.y,
             cos.x * cos.y
        );
        return xAxis * point.x + yAxis * point.y + zAxis * point.z;
    }*/

    public override Matrix4x4 Matrix
    {
        get
        {
            float ranX = rotation.x * Mathf.Deg2Rad;    //将度数（degrees）转换为弧度（radians） 弧度 = 度数 * (π / 180)
            float ranY = rotation.y * Mathf.Deg2Rad;  
            float ranZ = rotation.z * Mathf.Deg2Rad;
            Vector3 cos = new Vector3(Mathf.Cos(ranX), Mathf.Cos(ranY), Mathf.Cos(ranZ));
            Vector3 sin = new Vector3(Mathf.Sin(ranX), Mathf.Sin(ranY), Mathf.Sin(ranZ));
            
            Matrix4x4 matrix = new Matrix4x4();
            
            matrix.SetColumn(0, new Vector4(
                cos.y * cos.z,
                cos.x * sin.z + sin.x * sin.y * cos.z,
                sin.x * sin.z - cos.x * sin.y * cos.z,
                0f
            ));
            
            matrix.SetColumn(1, new Vector4(
                -cos.y * sin.z,
                cos.x * cos.z - sin.x * sin.y * sin.z,
                sin.x * cos.z + cos.x * sin.y * sin.z,
                0f
            ));
            
            matrix.SetColumn(2, new Vector4(
                sin.y,
                -sin.x * cos.y,
                cos.x * cos.y,
                0f
            ));
            matrix.SetColumn(3, new Vector4(0f,0f,0f,1f));
            return matrix;
        }
    }
}
