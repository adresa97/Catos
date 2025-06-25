using UnityEngine;

public class Gato : MonoBehaviour
{
    public Vestidor vestidor;
    //public string name;
    public GameObject cap;
    public GameObject tShirt;

    public void ActualizarRopa(int indexCap, int indexTshirt)
    {
        changeCap(indexCap);
        changeTshirt(indexTshirt);
    }

    public void changeCap(int index)
    {
        cap.GetComponent<SpriteRenderer>().sprite = vestidor.cap[index];
    }

    public void changeTshirt(int index)
    {
        tShirt.GetComponent<SpriteRenderer>().sprite = vestidor.tshirt[index];
    }
}
