using Unity.Mathematics;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{
    public static Gridmanager instance;

    public int height;
    public int width;
    public float Size;

    private Grid<int> grid;
    private Camera mainCamera;

    bool ShowMesh = false;

    public Transform Build;
    

    private void Awake()
    {
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<int>(width, height, Size, transform.position, transform.GetChild(0), true);

        transform.GetChild(0).gameObject.SetActive(ShowMesh);
    }

    private void Update()
    {
        if (!transform.GetChild(0).gameObject.activeSelf && ShowMesh)
        {
            transform.GetChild(0).gameObject.SetActive(ShowMesh);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(ShowMesh);
        }

        if (ShowMesh)
        {
            MouseBuild();
            if (ClikMesh())
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Buildpresets buildpresets = Build.GetComponentInChildren<Buildpresets>();
                    buildpresets.CreateBuild(Physics2D.Raycast(Build.GetChild(0).GetChild(0).position,
                        Vector2.zero).collider.transform.position);

                }
            }

            
            
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = RaycastAtMouse();

            if (hit.collider != null)
            {
                grid.SetValue(hit.collider.transform.name, 3);
            }
        }*/
    }

    // 将所有网格的值重置为0
    void ResetGridValues()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid.GetValue(x, y) != 3)
                {
                    grid.SetValue(x, y, 0);
                }
            }
        }
    }

    Vector3 ScreenToWorldPosition(Vector3 screenPosition)
    {
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }

    RaycastHit2D RaycastAtMouse()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(mouseWorldPos, Vector2.zero);
    }

    public void ChangeTF()
    {
        ShowMesh = !ShowMesh;
    }

    void MouseBuild()
    {
        Vector3 po = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        po.z = 0;
        Build.position = po;
    }

    // 当 Build 移动时调用此函数
    bool ClikMesh()
    {

        ResetGridValues();
        
        Buildpresets buildpresets = Build.GetComponentInChildren<Buildpresets>();
        Transform[] mesh = buildpresets.ShootRaysFromChildren(); // 获取命中的所有网格

        set(mesh);
        
        // 判断是否有命中物体
        if (mesh != null && mesh.Length > 0)
        {
            // 遍历所有命中的网格
            foreach (var h in mesh)
            {
                // 如果网格的值是1，返回false
                if (grid.GetValue(h.name) == 3)
                {
                    return false;
                }
            }

            // 如果没有命中值为1的网格，返回true
            return true;
        }
        else
        {
            // 如果没有命中任何网格，返回false
            return false;
        }
    }

    public void SetValue(Transform[] Trs)
    {
        foreach (var h in Trs)
        {
            grid. SetValue(h.name,3);
        }
    }

    void set(Transform[] pos)
    {
        if (pos != null)
        {
            foreach (var h in pos)
            {
                if (grid.GetValue(h.name) == 0)
                {
                    grid.SetValue(h.name,1);
                }
                
            }
        }

    }

}
