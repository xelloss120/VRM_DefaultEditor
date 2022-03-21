using UnityEngine;

public class DisplaySwitching : MonoBehaviour
{
    [SerializeField] GameObject go1;
    [SerializeField] GameObject go2;

    public void OnClick()
    {
        go1.SetActive(true);
        go2.SetActive(false);
    }
}
