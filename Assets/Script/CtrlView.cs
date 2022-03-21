using UnityEngine;

public class CtrlView : MonoBehaviour
{
    [SerializeField] Camera TargetCam;
    [SerializeField] Transform ViewPoint;

    [SerializeField] float Angle_Speed = 1.0f;
    [SerializeField] float PosXY_Speed = 0.005f;
    [SerializeField] float Size_Speed = 0.5f;

    Vector3 MousePos;

    /// <summary>
    /// プレビューのマウス操作
    /// </summary>
    void Update()
    {
        // マウスの移動量を取得
        var diff = MousePos - Input.mousePosition;
        MousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            // 左クリック（回転）
            var tmp = diff;
            diff.x = +tmp.y;
            diff.y = -tmp.x;
            ViewPoint.eulerAngles += diff * Angle_Speed;
        }
        else if (Input.GetMouseButton(1))
        {
            // 右クリック（移動）
            var tmp = diff;
            diff.x = -tmp.x;
            diff.y = +tmp.y;
            ViewPoint.position += diff * PosXY_Speed;
        }

        // スクロール（拡縮というか前後）
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (TargetCam.orthographicSize < 0.01 && scroll > 0)
        {
            scroll = 0;
        }
        TargetCam.orthographicSize -= scroll * Size_Speed;
    }

    public void OnClick()
    {
        ViewPoint.position = new Vector3(0, 0.75f, 0);
        ViewPoint.eulerAngles = new Vector3(0, -180, 0);
        TargetCam.orthographicSize = 1;
    }
}