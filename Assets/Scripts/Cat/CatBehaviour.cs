using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CatBehaviour : MonoBehaviour
{
    public float minTimePatrolling = 5f;
    public float maxTimePatrolling = 10f;
    public float minTimeIdle = 1f;
    public float maxTimeIdle = 2f;
    [Range(0, 1)] public float idleProbability = 0.25f; // Probabilidad de quedarse quieto

    private Rigidbody2D _rb;
    private Animator _animator;
    private Coroutine _patrolCoroutine;

    private int direction;
    private int lastDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        direction = Random.value < 0.5f ? -1 : 1;
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
        SetRandomPatrol();
    }

    private void SetRandomPatrol()
    {
        if (_patrolCoroutine != null) StopCoroutine(_patrolCoroutine);

        lastDirection = direction;
        direction = GetRandomDirection();
        //Debug.Log("Direction: " + direction);

        _patrolCoroutine = StartCoroutine(PatrolRoutine(direction));
    }

    private void ChangeDirection()
    {
        if (_patrolCoroutine != null) StopCoroutine(_patrolCoroutine);

        lastDirection = direction;
        direction = -direction;
        //Debug.Log("Direction: " + direction);

        _patrolCoroutine = StartCoroutine(PatrolRoutine(direction));
    }

    private IEnumerator PatrolRoutine(int direction)
    {
        while (true)
        {
            float timeLimit = 0f;
            if (direction == -1 || direction == 1) timeLimit = GetRandomTime(minTimeIdle, maxTimeIdle);
            else timeLimit = GetRandomTime(minTimePatrolling, maxTimePatrolling);

            float timer = 0f;

            while (timer < timeLimit)
            {
                float moveDirection = 0.0f;
                if (direction > 1) moveDirection = 1.0f;
                else if (direction < -1) moveDirection = -1.0f;
                _rb.velocity = new Vector2(moveDirection, _rb.velocity.y);

                _animator.SetFloat("Movement", direction);
                
                timer += Time.deltaTime;
                yield return null;
            }

            _rb.velocity = new Vector2(0, _rb.velocity.y);

            float stopDirection = 1.0f;
            if (direction < 0) stopDirection = -1.0f;
            _animator.SetFloat("Movement", stopDirection);

            // Pequeña pausa entre movimientos
            yield return new WaitForSeconds(0.3f);
            SetRandomPatrol();
        }
    }

    // Devuelve -1, 0 o 1 con probabilidad configurable
    private int GetRandomDirection()
    {
        float randomValue = Random.value;
        
        if (randomValue < idleProbability)
        {
            if (lastDirection == 2) return 1; // Quieto derecha
            else if (lastDirection == -2) return -1; // Quieto izquierda
            else return lastDirection; // Misma dirección de quieto que antes
        }
        else
        {
            // Mover izquierda o derecha con igual probabilidad
            return Random.value < 0.5f ? -2 : 2;
        }
    }

    private float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void OnChangeClothesStart(int animation)
    {
        StopCoroutine(_patrolCoroutine);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _animator.SetInteger("IndexAnimacionRopa", animation);
    }

    public void OnChangeClothesEnd()
    {
        _animator.SetInteger("IndexAnimacionRopa", 0);
        SetRandomPatrol();
    }

    public void OnFoodStart()
    {
        if (_patrolCoroutine != null) StopCoroutine(_patrolCoroutine);
        _rb.velocity = Vector2.zero;
        _animator.SetTrigger("Sardina");
    }

    public void OnFoodEnd()
    {
        SetRandomPatrol();
        Debug.Log("Finish eating");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapBounds"))
        {
            ChangeDirection();
        }
    }
}