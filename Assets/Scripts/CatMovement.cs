using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour
{
    public float velocityX = 2f;

    public float minTimePatrolling = 2f;
    public float maxTimePatrolling = 5f;
    public float minTimeResting = 1f;
    public float maxTimeResting = 3f;

    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            float direction = GetRandomDirection();
            float patrolTime = GetRandomTime(minTimePatrolling, maxTimePatrolling);
            float timer = 0f;

            while (timer < patrolTime)
            {
                // Detecta si ha llegado al borde y lanza patrulla reactiva
                if (IsAtScreenEdge())
                {
                    _rb.velocity = Vector2.zero;
                    yield return new WaitForSeconds(1f); // Pausa

                    // Inicia patrulla reactiva de 2 segundos en dirección contraria
                    yield return StartCoroutine(PatrolOppositeDirection(-direction, 2f));
                    break; // Rompe el bucle actual
                }

                _rb.velocity = new Vector2(direction * velocityX, _rb.velocity.y);

                if (_spriteRenderer != null)
                    _spriteRenderer.flipX = direction > 0;

                timer += Time.deltaTime;
                yield return null;
            }

            // Parar entre patrullas
            _rb.velocity = Vector2.zero;
            float restTime = GetRandomTime(minTimeResting, maxTimeResting);
            yield return new WaitForSeconds(restTime);
        }
    }

    private IEnumerator PatrolOppositeDirection(float direction, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            _rb.velocity = new Vector2(direction * velocityX, _rb.velocity.y);

            if (_spriteRenderer != null)
                _spriteRenderer.flipX = direction > 0;

            timer += Time.deltaTime;
            yield return null;
        }

        _rb.velocity = Vector2.zero;
    }

    private int GetRandomDirection()
    {
        int dir;
        do
        {
            dir = Random.Range(-1, 2); // -1, 0, 1
        } while (dir == 0);
        return dir;
    }

    private float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    private bool IsAtScreenEdge()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPos.x <= 0.05f || viewportPos.x >= 0.95f;
    }
}
