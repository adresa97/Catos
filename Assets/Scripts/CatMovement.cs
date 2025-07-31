using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour
{
    public float minTimePatrolling = 1f;
    public float maxTimePatrolling = 2f;
    [Range(0, 1)] public float idleProbability = 0.3f; // Probabilidad de quedarse quieto

    private Rigidbody2D _rb;
    private Animator _animator;
    private Coroutine _patrolCoroutine;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartPatrol();
    }

    private void OnDisable()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }
    }

    public void StartPatrol()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
        }
        _patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            int direction = GetRandomDirection();
            Debug.Log("Direction: " + direction);
            
            float patrolTime = GetRandomTime(minTimePatrolling, maxTimePatrolling);
            float timer = 0f;

            while (timer < patrolTime)
            {
                _rb.velocity = new Vector2(direction, _rb.velocity.y);
                _animator.SetFloat("Movement", direction); // Usar valor absoluto para la animación
                
                timer += Time.deltaTime;
                yield return null;
            }
            
            // Pequeña pausa entre movimientos
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Devuelve -1, 0 o 1 con probabilidad configurable
    private int GetRandomDirection()
    {
        float randomValue = Random.value;
        
        if (randomValue < idleProbability)
        {
            return 0; // Quedarse quieto
        }
        else
        {
            // Mover izquierda o derecha con igual probabilidad
            return Random.value < 0.5f ? -1 : 1;
        }
    }

    private float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}