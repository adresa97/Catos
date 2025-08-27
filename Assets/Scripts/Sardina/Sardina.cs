using UnityEngine;

public class Sardina : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gatos"))
        {
            CatBehaviour gato = other.GetComponent<CatBehaviour>();
            if (gato != null)
            {
                gato.OnFoodStart();
                Destroy(gameObject);
            }
        }
    }
}