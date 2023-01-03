using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("HuyTools/PathCreator")]
public class PathCreator : MonoBehaviour
{
    [SerializeField]
    bool isLoop = false;
    [SerializeField]
    public List<Vector3> List_Points;

    protected Vector3 _originalTransformPosition;
    public Vector3 originalTransformPosition => _originalTransformPosition;

    protected bool _originalTransformPositionStatus = false;
    public bool originalTransformPositionStatus => _originalTransformPositionStatus;

    private void OnEnable() 
    {
        transform.parent = null;
    }

    protected virtual void Start()
    {
        Initialization();
    }

    public Vector3 GetPos(int indexPos)
    {
        Vector3 vt = originalTransformPosition + List_Points[indexPos];
        return vt;
    }

    public Vector3 GetOriginalPos(int indexPos)
    {
        Vector3 vt = transform.position + List_Points[indexPos];
        return vt;
    }

    protected virtual void Initialization()
    {

        if (List_Points == null || List_Points.Count < 1)
        {
            return;
        }

        if (!_originalTransformPositionStatus)
        {
            _originalTransformPositionStatus = true;
            _originalTransformPosition = transform.position;
        }
        transform.position = _originalTransformPosition;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (List_Points == null)
        {
            return;
        }

        if (List_Points.Count == 0)
        {
            return;
        }

        if (_originalTransformPositionStatus == false)
        {
            _originalTransformPosition = transform.position;
            _originalTransformPositionStatus = true;
        }
        if (transform.hasChanged)
        {
            _originalTransformPosition = transform.position;
        }

        for (int i = 0; i < List_Points.Count; i++)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_originalTransformPosition + List_Points[i], 0.1f);

            if ((i + 1) < List_Points.Count)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(_originalTransformPosition + List_Points[i], _originalTransformPosition + List_Points[i + 1]);
            }
        }

        if (isLoop)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(_originalTransformPosition + List_Points[List_Points.Count - 1], _originalTransformPosition + List_Points[0]);
        }
    }
#endif

}
