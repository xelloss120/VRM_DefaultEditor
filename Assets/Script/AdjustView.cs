using UnityEngine;

public class AdjustView : MonoBehaviour
{
    [SerializeField] RectTransform Panel;
    [SerializeField] RectTransform Image;

    void Start()
    {
        Resize();
    }

    /// <summary>
    /// 3Dビューを正方形に維持
    /// </summary>
    void Update()
    {
        Resize();
    }

    /// <summary>
    /// 3Dビューを正方形に維持
    /// </summary>
    void Resize()
    {
        var size = Mathf.Min(Panel.rect.width, Panel.rect.height);
        Image.localScale = Vector3.one * size;
    }
}
