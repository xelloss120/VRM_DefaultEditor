using UnityEngine;

public class CtrlView : MonoBehaviour
{
    [SerializeField] Camera TargetCam;
    [SerializeField] Transform ViewPoint;

    [SerializeField] float Angle_Speed = 1.0f;
    [SerializeField] float PosXY_Speed = 0.005f;
    [SerializeField] float Size_Speed = 0.5f;

    [SerializeField] RectTransform Left;
    [SerializeField] RectTransform Center;

    Vector3 MousePos;

    bool OutRange = false;

    /// <summary>
    /// プレビューのマウス操作
    /// </summary>
    void Update()
    {
        // マウスの移動量を取得
        var diff = MousePos - Input.mousePosition;
        MousePos = Input.mousePosition;

        if (MousePos.y < 0 ||
            MousePos.y > Center.rect.height ||
            MousePos.x < Left.rect.width ||
            MousePos.x > Left.rect.width + Center.rect.width)
        {
            // 範囲外なら中断
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                // 範囲外クリック
                OutRange = true;
            }
            return;
        }

        // スクロール（拡縮というか前後）
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (TargetCam.orthographicSize < 0.01 && scroll > 0)
        {
            scroll = 0;
        }
        TargetCam.orthographicSize -= scroll * Size_Speed;

        // 範囲内外判定
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // 範囲内クリック
            OutRange = false;
        }
        if (OutRange)
        {
            // 範囲外クリックなら中断
            return;
        }

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
            diff.x = +tmp.x;
            diff.y = +tmp.y;
            ViewPoint.position += ViewPoint.up * diff.y * PosXY_Speed * TargetCam.orthographicSize;
            ViewPoint.position += ViewPoint.right * diff.x * PosXY_Speed * TargetCam.orthographicSize;
        }
    }

    public void OnClick()
    {
        // Reset
        ViewPoint.position = new Vector3(0, 0.75f, 0);
        ViewPoint.eulerAngles = new Vector3(0, -180, 0);
        TargetCam.orthographicSize = 1;
    }
}