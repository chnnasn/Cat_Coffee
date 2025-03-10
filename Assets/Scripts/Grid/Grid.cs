using System;
using UnityEngine;
using System.Collections.Generic;

public class Grid<TGrip>
{
    int height;
    int width;
    float size;
    TGrip[,] gridArray;
    Vector3 startPO; 
    MeshRenderer[,] meshRenderer;

    public Grid(int width, int height, float size, Vector3 startPO, Transform parent, bool generateUp)
    {
        this.height = height;
        this.width = width;
        this.size = size;
        this.startPO = startPO;
        gridArray = new TGrip[width, height];
        meshRenderer = new MeshRenderer[width, height];

        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                Vector3 p1 = getPosition(x, y, generateUp);
                Vector3 p2 = getPosition(x, y + 1, generateUp);
                Vector3 p3 = getPosition(x + 1, y + 1, generateUp);
                Vector3 p4 = getPosition(x + 1, y, generateUp);
                Debug.DrawLine(p1, p2, Color.white, 100f);
                Debug.DrawLine(p2, p3, Color.white, 100f);
                Debug.DrawLine(p3, p4, Color.white, 100f);
                Debug.DrawLine(p4, p1, Color.white, 100f);

                CreateSquareMesh(p1, p2, p3, p4, parent, x, y,generateUp);
            }
        }

        Debug.DrawLine(getPosition(0, this.height, generateUp), getPosition(this.width, this.height, generateUp), Color.white, 100f);
        Debug.DrawLine(getPosition(this.width, 0, generateUp), getPosition(this.width, this.height, generateUp), Color.white, 100f);
    }

    // 更新 getPosition 函数，添加 generateUp 参数
    Vector3 getPosition(int x, int y, bool generateUp)
    {
        float isoX = (x - y) * 0.5f * size;
        float isoY = (x + y) * 0.25f * size;

        if (!generateUp) 
        {
            isoY = -isoY; // 如果向下生成，改变 Y 值的方向
        }

        Vector3 position = new Vector3(isoX, isoY, 0) + startPO;
        return position;
    }

    void getXY(string index, out int x, out int y)
    {
        x = int.Parse(index.Split("_")[0]);
        y = int.Parse(index.Split("_")[1]);
    }

    public void SetValue(string index, TGrip value)
    {
        int x, y;
        getXY(index, out x, out y); 
        SetValue(x, y, value);          
    }

    public TGrip GetValue(string index)
    {
        int x, y;
        getXY(index, out x, out y);  
        return GetValue(x, y);          
    }

    public void SetValue(int x, int y, TGrip value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;

            // 判断值是否为1，改变颜色
            if (EqualityComparer<TGrip>.Default.Equals(value, (TGrip)(object)1))
            {
                meshRenderer[x, y].material.color = Color.red;
            }else if (EqualityComparer<TGrip>.Default.Equals(value, (TGrip)(object)3))
            {
                meshRenderer[x, y].material.color = Color.yellow;
            }
            else
            {
                meshRenderer[x, y].material.color = Color.green;
            }
        }
    }

    public TGrip GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGrip);
        }
    }

    void CreateSquareMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Transform parent, int x, int y,bool Line)
    {
        GameObject gridCell = new GameObject(x + "_" + y);
        gridCell.transform.position = (p1 + p2 + p3 + p4) / 4; // 网格的中心点作为位置
        gridCell.transform.SetParent(parent);

        // 创建 MeshFilter 和 MeshRenderer
        MeshFilter meshFilter = gridCell.AddComponent<MeshFilter>();
        meshRenderer[x, y] = gridCell.AddComponent<MeshRenderer>();

        // 转换为局部坐标
        p1 = gridCell.transform.InverseTransformPoint(p1);
        p2 = gridCell.transform.InverseTransformPoint(p2);
        p3 = gridCell.transform.InverseTransformPoint(p3);
        p4 = gridCell.transform.InverseTransformPoint(p4);

        if (Line)
        {
            // 创建 PolygonCollider2D
            PolygonCollider2D polygonCollider = gridCell.AddComponent<PolygonCollider2D>();
            
            // 设置 PolygonCollider2D 的顶点
            Vector2[] colliderPoints = new Vector2[4]
            {
                new Vector2(p1.x, p1.y),
                new Vector2(p2.x, p2.y),
                new Vector2(p3.x, p3.y),
                new Vector2(p4.x, p4.y)
            };
            polygonCollider.SetPath(0, colliderPoints); // 设置碰撞体的路径
        }

        // 创建方形网格
        meshFilter.mesh = CreateSquareMesh(p1, p2, p3, p4);

        // 给网格添加材质
        meshRenderer[x, y].material = new Material(Shader.Find("Unlit/Color")) { color = Color.green };

        if (Line)
        {
            // 创建 LineRenderer 来显示四个顶点
            LineRenderer lineRenderer = gridCell.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 5; // 4个顶点，最后一个点与第一个点相同，闭合形状

            // 将局部坐标转为世界坐标
            lineRenderer.SetPosition(0, gridCell.transform.TransformPoint(p1));
            lineRenderer.SetPosition(1, gridCell.transform.TransformPoint(p2));
            lineRenderer.SetPosition(2, gridCell.transform.TransformPoint(p3));
            lineRenderer.SetPosition(3, gridCell.transform.TransformPoint(p4));
            lineRenderer.SetPosition(4, gridCell.transform.TransformPoint(p1)); // 闭合四边形

            // 设置 LineRenderer 的样式
            lineRenderer.startWidth = 0.02f;  // 线条宽度
            lineRenderer.endWidth = 0.02f;    // 线条宽度
            lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.white }; // 线条颜色
        }
    }

    Mesh CreateSquareMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        Mesh mesh = new Mesh();
    
        // 设置顶点
        Vector3[] vertices = new Vector3[4] { p1, p2, p3, p4 };
    
        // 设置三角形的顶点顺序
        int[] triangles = new int[6] { 0, 1, 2, 0, 2, 3 };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    
        // 计算法线
        mesh.RecalculateNormals();

        return mesh;
    }
}

