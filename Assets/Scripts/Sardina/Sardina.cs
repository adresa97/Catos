using UnityEngine;

public class Sardina : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto que entra en el trigger tiene el tag "Gato"
        if (other.CompareTag("Gatos"))
        {   
            Debug.Log("COLISIÓN");
            // Intenta obtener el componente CatsManager del objeto
            CatBehaviour gato = other.GetComponent<CatBehaviour>();
            if (gato != null)
            {
                // Aquí llamas al método que quieras del CatsManager
                // Por ejemplo:
                gato.ComerSardina();

                // Opcional: destruir la sardina después de usarla
                Destroy(gameObject);
            }
        }
    }
}