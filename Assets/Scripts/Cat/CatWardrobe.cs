using System;
using UnityEngine;

public class CatWardrobe : MonoBehaviour
{
    [SerializeField]
    private CatBehaviour behaviour;

    [SerializeField]
    private SpriteRenderer headAccesory;

    [Serializable]
    struct ShirtRenderer
    {
        public SpriteRenderer main;
        public SpriteRenderer dress;
        public SpriteRenderer leftArm;
        public SpriteRenderer rightArm;
        public SpriteRenderer back;
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
        // Conditions
        Boolean hasNewHead = headAccesory.sprite != newHead;
        Boolean hasNewShoe = shoes.leftShoe.sprite != newShoes.leftShoe;
        Boolean hasNewBody = shirt.main.sprite != newShirt.main || shirt.dress.sprite != newShirt.dress || shirt.back.sprite != newShirt.back;

        // Animation Sequence
        int changeAnimation = 0;

        // Head Accessory
        if (hasNewHead)
        {
            if (newHead != null)
            {
                headAccesory.sprite = newHead;
                if (!headAccesory.enabled) headAccesory.enabled = true;
            }
            else
            {
                headAccesory.sprite = null;
                headAccesory.enabled = false;
            }

            changeAnimation += 2;
        }

        // Body Apparel
        if (hasNewBody)
        {
            if (newShirt.main != null || newShirt.dress != null || newShirt.back != null)
            {
                shirt.main.sprite = newShirt.main;
                shirt.dress.sprite = newShirt.dress;
                shirt.leftArm.sprite = newShirt.leftArm;
                shirt.rightArm.sprite = newShirt.rightArm;
                shirt.back.sprite = newShirt.back;
                if (!shirt.main.enabled)
                {
                    shirt.main.enabled = true;
                    shirt.dress.enabled = true;
                    shirt.leftArm.enabled = true;
                    shirt.rightArm.enabled = true;
                    shirt.back.enabled = true;
                }
            }
            else
            {
                shirt.main.sprite = null;
                shirt.dress.sprite = null;
                shirt.leftArm.sprite = null;
                shirt.rightArm.sprite = null;
                shirt.back.sprite = null;

                shirt.main.enabled = false;
                shirt.dress.enabled = false;
                shirt.leftArm.enabled = false;
                shirt.rightArm.enabled = false;
                shirt.back.enabled = false;
            }

            changeAnimation += 3;
        }

        // Shoes
        if (hasNewShoe)
        {
            if (newShoes.leftShoe != null)
            {
                shoes.leftShoe.sprite = newShoes.leftShoe;
                shoes.rightShoe.sprite = newShoes.rightShoe;
                if (!shoes.leftShoe.enabled)
                {
                    shoes.leftShoe.enabled = true;
                    shoes.rightShoe.enabled = true;
                }
            }
            else
            {
                shoes.leftShoe.sprite = null;
                shoes.rightShoe.sprite = null;

                shoes.leftShoe.enabled = false;
                shoes.rightShoe.enabled = false;
            }

            changeAnimation += 1;
        }

        // Result
        if (changeAnimation > 3) changeAnimation = 3;
        if (changeAnimation > 0)
        {
            behaviour.OnChangeClothesStart(changeAnimation);
        }
    }
}
