using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dùng để load các Resource ( chỉ 1 lần duy nhất )
/// </summary>

public class ResourceCTL
{
    private static ResourceCTL _instance = null;
    public static ResourceCTL Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceCTL();
            }
            return _instance;
        }
    }

    #region Thuộc tính

    private Material _blackCellMaterial = null;
    public Material BlackCellMaterial
    {
        get
        {
            if (_blackCellMaterial == null)
            {
                //Load Material
                _blackCellMaterial = Resources.Load<Material>("Materials/BlackCell");
            }
            return _blackCellMaterial;
        }
    }

    private Material _whiteCellMaterial = null;
    public Material WhiteCellMaterial
    {
        get
        {
            if (_whiteCellMaterial == null)
            {
                //Load Material
                _whiteCellMaterial = Resources.Load<Material>("Materials/WhiteCell");
            }
            return _whiteCellMaterial;
        }
    }

    #endregion

    private ResourceCTL()
    {

    }

    private Dictionary<string, GameObject> _dict = new Dictionary<string, GameObject>();

    /// <summary>
    /// Lấy GameObject từ trong resource ra
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject(string path)
    {
        if (_dict.ContainsKey(path) == false)
        {
            _dict.Add(path, Resources.Load<GameObject>(path));
        }
        return _dict[path];

    }
}
