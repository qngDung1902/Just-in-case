using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.SetActive(false);
            StartCoroutine(Respawn(collider.GetComponent<Player>()));
        }
    }

    IEnumerator Respawn(Player player)
    {
        yield return new WaitForSeconds(2f);

        if (PlayerDataManager.Health == 0)
        {
            GameUIManager.Instance.ShowPopupLose(true);
        }
        else
        {
            PlayerDataManager.Health -= 1;
            GameUIManager.Instance.UpdateHealth();
        }

        player.gameObject.SetActive(true);
        player.Spawn();
    }
}
