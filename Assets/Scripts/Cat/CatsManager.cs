using System;
using Unity.VisualScripting;
using UnityEngine;

public class CatsManager : MonoBehaviour
{
    [SerializeField]
    private GameEvents catEvents;

    [SerializeField]
    private CatWardrobe[] cats;

    [SerializeField]
    private Sprite[] headAccesories;

    [SerializeField]
    private ShirtSprites[] shirts;

    [SerializeField]
    private ShoesSprites[] shoes;

    public void OnEnable()
    {
        catEvents.AddListener(CatEventsCallback);
    }

    public void OnDisable()
    {
        catEvents.RemoveListener(CatEventsCallback);
    }

    private void CatEventsCallback(object data)
    {
        if(data is SendCatEvent)
        {
            ChangeCat((data as SendCatEvent).catIndex, (data as SendCatEvent).hatIndex, (data as SendCatEvent).shirtIndex, (data as SendCatEvent).shoeIndex);
        }
    }

    private bool ChangeCat(int catIndex, int hatIndex, int shirtIndex, int shoeIndex)
    {
        if (catIndex >= cats.Length)
        {
            Debug.LogWarning("⚠️ Índice de gato fuera de rango");
            return false;
        }

        if (cats[catIndex] == null)
        {
            Debug.LogWarning("⚠️ Gato no asignado");
            return false;
        }

        cats[catIndex].ChangeClothes(headAccesories[hatIndex], shirts[shirtIndex], shoes[shoeIndex]);
        return true;
    }
}

[Serializable]
public struct ShirtSprites
{
    public Sprite main;
    public Sprite dress;
    public Sprite leftArm;
    public Sprite rightArm;
    public Sprite back;
}

[Serializable]
public struct ShoesSprites
{
    public Sprite leftShoe;
    public Sprite rightShoe;
}