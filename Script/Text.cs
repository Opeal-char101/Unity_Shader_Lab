
using System;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    
    public Transform prefab;            //被实例化的预制体
    public int gridResolution = 10;     //10个为一条边的单位 10*10*10（长 高 宽）
    private Transform[] grid;           //用来存放被实例化的物体
    private List<Transformation> transformations;
    private Matrix4x4 transformation;
    
    
    
    private void Awake()
    {
        //测试
        Debug.Log(this.GetType());
        
        transformations = new List<Transformation>();
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        //三重嵌套循环（x、y、z）来遍历三维网格
        for (int i = 0, z = 0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                for (int x = 0; x < gridResolution; x++, i++)
                {
                    //方法被调用来创建每个网格点，并将返回的Transform对象存储在grid数组中的适当索引处
                    grid[i] = CreateGridPoint(x, y, z); 
                }
            }
        }
    }

    private void Update()
    {
        UpdateTransformation();
        GetComponents<Transformation>(transformations);
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    private void UpdateTransformation()
    {
        GetComponents<Transformation>(transformations);
        if (transformations.Count > 0)
        {
            transformation = transformations[0].Matrix;
            for (int i = 1; i < transformations.Count; i++)
            {
                transformation = transformations[i].Matrix * transformation;
            }
        }
    }

    Vector3 TransformPoint (int x, int y, int z) {
        // Vector3 coordinates = GetCoordinates(x, y, z);
        // for (int i = 0; i < transformations.Count; i++) {
        //     coordinates = transformations[i].Apply(coordinates);
        // }
        // return coordinates;
        Vector3 coordinates = GetCoordinates(x, y, z);
        return transformation.MultiplyPoint(coordinates);
    }
    
    Transform CreateGridPoint(int x, int y, int z)
    {
        //实例化 挂上的预制体
        Transform point = Instantiate<Transform>(prefab);
        //本地位置设置为通过 GetCoordinates 方法计算的坐标。
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );
        return point;
    }

    Vector3 GetCoordinates(int x, int y, int z)
    {
        //GetCoordinates 方法用于计算网格点在三维空间中的准确位置。
        //根据传入的x、y、z坐标，计算出网格点在网格中心为原点的位置。
        
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }
    
}
