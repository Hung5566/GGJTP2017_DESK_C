using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCtrl : MonoBehaviour
{

    /// <summary>
    /// 參考點
    /// </summary>
    private Vector2 MouseReferencePos;
    public static PlayerCtrl Instance;

    void Awake()
    {
        Instance = this;


    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseReferencePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    /// <summary>
    /// 得到現在誤差值
    /// </summary>
    /// <returns></returns>
    public float GetNowDifference()
    {
        float _Diff = 0;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            _Diff=Physics.Raycast(ray, out hit) ? hit.point.y: 0;
        }
        Debug.Log(_Diff);
        return _Diff;
    }


}
