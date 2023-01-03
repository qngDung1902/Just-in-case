using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject spriteDisplay;
    [SerializeField] private List<PathMoving> ObjectInteract;
    private List<GameObject> ObjectList = new List<GameObject>();
    [Header("--------Config---------")]
    // private int Offset = -1;
    // [Tooltip("Loại sử dụng INTERACT(button, lever,...)")]
    [SerializeField] private InteractType interactType;

    // public bool cameraTracking;
    [HideInInspector] public bool isActive;
    // [SerializeField] private AudioClip touch;
    // [SerializeField] private AudioClip openDoorSound;
    // [SerializeField] private AudioClip closeDoorSound;

    GameObject h;
    private Tween tween;

    private void Start()
    {
        // switch (interactType)
        // {
        //     case InteractType.LEVER:
        //     if (Offset == 1)
        //     {
        //         spriteDisplay.transform.rotation = Quaternion.Euler(0f, 0f, -50f);
        //     }
        //     break;
        // }
        if (transform.localScale.x < 0)
        {
            Active();
        }
    }


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        switch (interactType)
        {
            case InteractType.BUTTON:
                if (_collision.gameObject.CompareTag("Player"))
                {
                    // SoundManager.Instance.PlaySfxRewind(touch);
                    isActive = true;
                    foreach (var path in ObjectInteract)
                    {
                        path.InteractLst.Add(this);
                    }
                    if (!ObjectList.Contains(_collision.gameObject))
                    {
                        ObjectList.Add(_collision.gameObject);
                        Active();
                        foreach (var path in ObjectInteract)
                        {
                            path.Active();
                        }

                    }
                }
                break;

            case InteractType.LEVER:
                // Debug.Log(Mathf.Sign((_collision.transform.position - transform.position).x));

                float dir = Mathf.Sign((_collision.transform.position - transform.position).x);
                if (_collision.gameObject.CompareTag("Player") && dir == -transform.localScale.x)
                {
                    if (transform.localScale.x > 0)
                    {
                        Active();
                    }
                    else
                    {
                        DeActive();
                    }
                    foreach (var path in ObjectInteract)
                    {
                        path.Active();
                    }
                }

                else if (_collision.gameObject.CompareTag("Player") && dir == transform.localScale.x)
                {
                    if (transform.localScale.x < 0)
                    {
                        Active();
                    }
                    else
                    {
                        DeActive();
                    }
                    foreach (var path in ObjectInteract)
                    {
                        path.DeActive();
                    }
                }

                break;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        switch (interactType)
        {
            case InteractType.BUTTON:
                if (_collision.gameObject.CompareTag("Player") || _collision.gameObject.CompareTag("Object"))
                {
                    foreach (var path in ObjectInteract)
                    {
                        path.InteractLst.Remove(this);
                    }
                    if (ObjectList.Contains(_collision.gameObject))
                    {
                        ObjectList.Remove(_collision.gameObject);
                    }

                    if (ObjectList.Count == 0)
                    {
                        DeActive();
                    }

                    foreach (var path in ObjectInteract)
                    {
                        if (path.InteractLst.Count == 0)
                        {
                            isActive = false;
                        }
                    }

                    if (ObjectList.Count == 0 && !isActive)
                    {
                        DeActive();
                        foreach (var path in ObjectInteract)
                        {
                            path.DeActive();
                        }
                    }

                    Destroy(h);
                }
                break;

            case InteractType.LEVER:
                return;
        }


    }
    private void Active()
    {
        switch (interactType)
        {
            case InteractType.BUTTON:
                tween.Kill();
                tween = spriteDisplay.transform.DOLocalMoveY(-0.037f, 0.6f).SetEase(Ease.OutExpo);
                break;

            case InteractType.LEVER:
                float beforeRot = spriteDisplay.transform.localEulerAngles.z;
                // if (beforeRot != -50f && beforeRot != 310f)
                // {
                //     SoundManager.Instance.PlaySfxRewind(touch);
                // }
                tween.Kill();
                tween = spriteDisplay.transform.DORotate(new Vector3(0, 0, -50f), 0.6f, RotateMode.Fast);
                break;
        }
    }
    private void DeActive()
    {
        switch (interactType)
        {
            case InteractType.BUTTON:
                tween.Kill();
                tween = spriteDisplay.transform.DOLocalMoveY(0.1f, 0.6f).SetEase(Ease.OutExpo);
                break;

            case InteractType.LEVER:
                float beforeRot = spriteDisplay.transform.localEulerAngles.z;
                // if (beforeRot != 50f)
                // {
                //     SoundManager.Instance.PlaySfxRewind(touch);

                // }
                tween.Kill();
                tween = spriteDisplay.transform.DORotate(new Vector3(0, 0, 50f), 0.6f, RotateMode.Fast);
                break;
        }
    }



    enum InteractType
    {
        BUTTON,
        LEVER,
    }
}
