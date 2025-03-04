using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Buildpresets : MonoBehaviour
{
    public int height;
    public int width;

    private Grid<int> grid;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount == 0)
        {
            grid = new Grid<int>(width, height, Gridmanager.instance.Size, transform.position, transform, false);
        }
    }

    private void Update()
    {
        
    }

    // 发射射线的方法
    public Transform[] ShootRaysFromChildren()
    {
        List<Transform> mesh = new List<Transform>();
        
        foreach (Transform child in transform) // 遍历所有子对象
        {
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.zero);
            
            // 判断是否击中物体
            if (hit.collider == null)
            {
                // 如果没有击中物体，跳过当前子物体
                break;
            }
            else
            {
                // 如果击中物体，添加到列表
                mesh.Add(hit.collider.transform);
            }
        }

        // 如果没有任何碰撞，返回空数组
        return mesh.Count > 0 ? mesh.ToArray() : null;
    }
    
    
    public void CreateBuild(Vector3 POS)
    {
        /*Debug.Log(POS);*/
    
        // 实例化对象
        Buildpresets buildpresets = Instantiate(transform.parent.gameObject, POS, quaternion.identity)
            .GetComponentInChildren<Buildpresets>();

        // 获取第一个子对象的第一个子对象
        Transform targetChild = buildpresets.transform.GetChild(buildpresets.transform.childCount - 1);

        if (targetChild != null)
        {
            // 计算偏移量
            Vector3 offset = targetChild.position - buildpresets.transform.position;

            offset.y = Gridmanager.instance.Size + offset.y;
        
            // 让目标子对象的坐标与POS一致
            buildpresets.transform.parent.position += offset;
        }

        // 更新Gridmanager
        Gridmanager.instance.SetValue(buildpresets.ShootRaysFromChildren());
    }
    
}