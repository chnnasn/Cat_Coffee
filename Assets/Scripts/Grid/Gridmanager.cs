using UnityEngine;
using UnityEngine.Events;

public class IndexEvent : MonoBehaviour
{
    public static UnityEvent<int> Index = new UnityEvent<int>();

    public static void GetIndex(int index)
    {
        Index.Invoke(index);
    }
}

public class Gridmanager : MonoBehaviour
{
    public static Gridmanager instance;

    public int height;
    public int width;
    public float Size;

    private Grid<int> grid;
    private Camera mainCamera;

    bool ShowMesh = false;

    Buildpresets Build;

    public Builds BuildValue;
    

    private void OnEnable()
    {
        IndexEvent.Index.AddListener(OnBuildingSelected);
    }
    
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

        CreatePresets();

        transform.GetChild(0).gameObject.SetActive(ShowMesh);
    }

    private void Update()
    {
        if (!transform.GetChild(0).gameObject.activeSelf && ShowMesh)
        {
            transform.GetChild(0).gameObject.SetActive(ShowMesh);
            transform.GetChild(1).gameObject.SetActive(ShowMesh);
        }
        else if (transform.GetChild(0).gameObject.activeSelf && !ShowMesh)
        {
            Build = null;
            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(ShowMesh);
            transform.GetChild(1).gameObject.SetActive(ShowMesh);
        }

        if (ShowMesh)
        {
            MouseBuild();
            if (ClikMesh())
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Build.transform.GetChild(0).position, Vector2.zero);
                    if (hit != null && grid.GetValue(hit.collider.name)!=3)
                    {
                        int x = int.Parse(hit.collider.name.Split("_")[0]);
                        int y = int.Parse(hit.collider.name.Split("_")[1]);
                        Build.CreateBuild(hit.collider.transform.position,(width - x) + (height - y));
                    }
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

    private void OnDisable()
    {
        IndexEvent.Index.RemoveListener(OnBuildingSelected);
    }
    
    
    void OnBuildingSelected(int Index)
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
        
        Build = transform.GetChild(1).GetChild(Index).GetComponentInChildren<Buildpresets>();
        Build.transform.parent.gameObject.SetActive(true);
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
        if (Build !=null)
        {
            Vector3 po = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            po.z = 0;
            Build.transform.parent.position = po;
        }
    }

    // 当 Build 移动时调用此函数
    bool ClikMesh()
    {

        if (Build !=null)
        {
            ResetGridValues();
            Transform[] mesh = Build.ShootRaysFromChildren(); // 获取命中的所有网格

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
        else
        {
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

    void CreatePresets()
    {
        for (int i = 0; i < BuildValue.Values.Count; i++)
        {
            GameObject pa = new GameObject();
            pa.transform.SetParent(transform.GetChild(1));
            pa.name =  BuildValue.Values[i].Sprite.name;
            GameObject Center = new GameObject();
            Center.name = "Center";
            Center.transform.SetParent(pa.transform);
            Center.transform.localPosition =
                new Vector3(BuildValue.Values[i].offset.x, BuildValue.Values[i].offset.y, 0);
            SpriteRenderer spriteRenderer = pa.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = BuildValue.Values[i].Sprite;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                0.5f);
            Buildpresets buildpresets = Center.AddComponent<Buildpresets>();
            buildpresets.Create( BuildValue.Values[i].Size.x, BuildValue.Values[i].Size.y);
        }
        transform.GetChild(1).gameObject.SetActive(false);

    }

}
