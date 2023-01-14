using UnityEngine;

public class SpringStep : MonoBehaviour
{
    [Header("--------Config---------")]
    [SerializeField] private float springForce;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal == Vector2.down && collision.gameObject.CompareTag("Player"))
        {
            // SoundManager.Instance.PlaySfxRewind(boungh);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * springForce, ForceMode2D.Impulse);

        }
    }
}
