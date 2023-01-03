using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoving : MonoBehaviour
{
    [Header("-------Reference-------")]
    [SerializeField] private PathCreator path;
    [Header("--------Config---------")]
    [Range(-1f, 1f)] public int offset;
    public Ease easeType;
    public float time;
    public float delayTime;
    public bool isUsingInteract;

    // public AudioClip openDoorSound;
    // public AudioClip closeDoorSound;

    private int localIndex = 0;
    private int endIndex = 0;
    private float distance;
    [HideInInspector] public List<Interact> InteractLst = new List<Interact>();
    Tween tween;
    void Start()
    {
        OnStart();
    }

    private void OnStart()
    {
        switch (offset)
        {
            case -1:
                endIndex = 1;
                localIndex = 0;
                break;
            case 1:
                endIndex = 0;
                localIndex = 1;
                break;
            case 0:
                gameObject.transform.position = path.GetOriginalPos(localIndex);
                return;
        }
        distance = Vector3.Distance(path.GetOriginalPos(localIndex), path.GetOriginalPos(endIndex));
        gameObject.transform.position = path.GetOriginalPos(localIndex);
        if (isUsingInteract) return;
        Move();
    }

    private void Move()
    {
        // SoundManager.Instance.PlaySfxRewind(endIndex == 0? openDoorSound : closeDoorSound);
        transform.DOMove(path.GetOriginalPos(endIndex), time).SetDelay(delayTime).SetEase(easeType).OnComplete(() =>
        {
            SwapEndIndex(endIndex);
            Move();
        });
    }

    private void SwapEndIndex(int _endIndex)
    {
        switch (_endIndex)
        {
            case 1:
                endIndex = 0;
                break;

            case 0:
                endIndex = 1;
                break;
        }
    }

    public void Active()
    {

        float a = Vector3.Distance(transform.position, path.GetOriginalPos(endIndex));
        // if (a != 0)
        // {
        //     SoundManager.Instance.PlaySfxRewind(openDoorSound);
        // }
        tween.Kill();
        tween = transform.DOMove(path.GetOriginalPos(endIndex), time * a / distance).SetEase(easeType);
    }

    public void DeActive()
    {

        float a = Vector3.Distance(transform.position, path.GetOriginalPos(localIndex));
        // if (a != 0)
        // {
        //     SoundManager.Instance.PlaySfxRewind(closeDoorSound);
        // }
        tween.Kill();
        tween = transform.DOMove(path.GetOriginalPos(localIndex), time * a / distance).SetEase(easeType);
    }
}
