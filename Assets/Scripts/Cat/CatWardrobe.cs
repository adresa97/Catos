using System;
using UnityEngine;

public class CatWardrobe : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer headAccesory;

    [Serializable]
    struct ShirtRenderer
    {
        public SpriteRenderer main;
        public SpriteRenderer leftArm;
        public SpriteRenderer rightArm;
    }

    [SerializeField]
    private ShirtRenderer shirt;

    [Serializable]
    struct ShoesRenderer
    {
        public SpriteRenderer leftShoe;
        public SpriteRenderer rightShoe;
    }

    [SerializeField]
    private ShoesRenderer shoes;

    public void ChangeClothes(Sprite newHead, ShirtSprites newShirt, ShoesSprites newShoes)
    {
        if (headAccesory.sprite != newHead)
        {
            headAccesory.sprite = newHead;
            if (newHead == null)
            {
                headAccesory.enabled = false;
            } else if (!headAccesory.enabled)
            {
                headAccesory.enabled = true;
            }
        }
        if (shirt.main.sprite != newShirt.main)
        {
            Debug.Log("Recibí una camiseta nueva");
            shirt.main.sprite = newShirt.main;
            shirt.leftArm.sprite = newShirt.leftArm;
            shirt.rightArm.sprite = newShirt.rightArm;
            if (newShirt.main == null)
            {
                Debug.Log("La camiseta es ir desnudo");
                shirt.main.enabled = false;
                shirt.leftArm.enabled = false;
                shirt.rightArm.enabled = false;
            }
            else if (!shirt.main.enabled)
            {
                Debug.Log("Ya basta de ir denudo por la vida");
                shirt.main.enabled = true;
                shirt.leftArm.enabled = true;
                shirt.rightArm.enabled = true;
            }
        }
        if (shoes.leftShoe.sprite != newShoes.leftShoe)
        {
            shoes.leftShoe.sprite = newShoes.leftShoe;
            shoes.rightShoe.sprite = newShoes.rightShoe;
            if (newShoes.leftShoe == null)
            {
                shoes.leftShoe.enabled = false;
                shoes.rightShoe.enabled = false;
            }
            else if (!shoes.leftShoe.enabled)
            {
                shoes.leftShoe.enabled = true;
                shoes.rightShoe.enabled = true;
            }
        }
    }
}
