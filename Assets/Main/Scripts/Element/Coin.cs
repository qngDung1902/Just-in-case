using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt($"coin_{transform.name}", 1) == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        transform.DOMoveY(transform.position.y - 0.1f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            PlayerDataManager.Score += 1;
        }

        PlayerPrefs.SetInt($"coin_{transform.name}", 0);
    }
}
