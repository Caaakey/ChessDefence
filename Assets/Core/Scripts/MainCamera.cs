using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Camera _Camera;

    private void Awake()
    {
        _Camera = GetComponent<Camera>();
    }


}