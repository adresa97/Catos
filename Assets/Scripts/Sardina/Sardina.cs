using UnityEngine;

public class Sardina : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprueba si el objeto con el que choca tiene el tag "Gatos"
        if (collision.gameObject.CompareTag("Gatos"))
        {
            // Intenta obtener el script CatBehaviour del gato
            CatBehaviour gato = collision.gameObject.GetComponent<CatBehaviour>();
            
            if (gato != null)
            {
                // Llama a la función del gato cuando recibe comida
                gato.OnFoodStart();

                // Destruye la sardina tras ser comida
                Destroy(gameObject);
            }
        }
    }
}
