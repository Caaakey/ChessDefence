using Unity.Mathematics;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private const int MAX_BUFFER = 30;

    [SerializeField] private float3 _TargetPosition;
    [SerializeField] private float _Angle;
    [SerializeField] private float _Radius;
    [SerializeField] private MeshRenderer _SectorMaterial;

    private int _DetectLayer;
    private int _DetectCount = 0;
    private Collider[] _OverlapCollisiton;

    public float Angle
    {
        get => _Angle;
        set
        {
            Transform t = _SectorMaterial.transform;

            t.localRotation = Quaternion.Euler(0, -_Angle / 2f, 0);
            _SectorMaterial.sharedMaterial.SetFloat("_Angle", value);

            _Angle = value;
        }
    }

    private void Awake()
    {
        _DetectLayer = 1 << LayerMask.NameToLayer("Enemy");
        _OverlapCollisiton = new Collider[MAX_BUFFER];

        Angle = _Angle;
    }

    private void Update()
    {
        //  벡터 크기를 비교하는 방법은 아래 함수로 대체
        _DetectCount = 0;

        int count = Physics.OverlapSphereNonAlloc(transform.position, _Radius, _OverlapCollisiton, _DetectLayer);
        if (count > 0)
        {
            float3 forward = math.normalize(_TargetPosition);
            forward.y = 0;

            for (int i = 0; i < count; ++i)
            {
                float3 p = _OverlapCollisiton[i].transform.position;
                p.y = 0;

                float inner = math.dot(math.normalize(p), forward);
                float theta = math.acos(inner);
                float degree = math.degrees(theta);

                Debug.Log($"{inner} : {theta} : {degree}");
                if (degree <= _Angle / 2f)
                    _DetectCount++;
            }
        }
    }

    private void OnGUI()
    {
        int2 screen = new(360, 640);
        using OnGUIScaler scaler = new(screen.x, screen.y);

        GUI.Label(new Rect(screen.x - 132, screen.y - 25, 128, 21), new GUIContent($"Detect Count : {_DetectCount}"));
    }

    //private void OnDrawGizmos()
    //{
    //    if (_Angle <= 0f || _Radius <= 0f) return;

    //    Vector3 forward = (Vector3)_TargetPosition - transform.position;
    //    forward.y = 0;

    //    float halfAngle = _Angle / 2f;

    //    //  position, normalVector, target vecvtor, radian , radius
    //    Handles.DrawSolidArc(transform.position, Vector3.up, forward, halfAngle, _Radius);
    //    Handles.DrawSolidArc(transform.position, Vector3.up, forward, -halfAngle, _Radius);
    //}
}
