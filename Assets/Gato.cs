using UnityEngine;

public class Gato : MonoBehaviour
{
    public Vestidor vestidor;
    //public string name;
    public SpriteRenderer cap;
    public SpriteRenderer tShirt;

    public void ActualizarRopa(int indexCap, int indexTshirt)
    {
        changeCap(indexCap);
        changeTshirt(indexTshirt);
    }

    private void changeCap(int index)
    {
        cap.sprite = vestidor.GetCapByIndex(index);
    }

    private void changeTshirt(int index)
    {
        tShirt.sprite = vestidor.GetShirtByIndex(index);
    }
}
