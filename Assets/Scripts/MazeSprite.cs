
using UnityEngine;


// created 25-01-2020
// modified 20-03-2020


public class MazeSprite : MonoBehaviour
{
    // reference to game object's sprite renderer component
    SpriteRenderer spriteRenderer;



    void Awake()
    {
        // set reference to the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // sets the required sprite and sprite's sorting order to the game object
    public void SetSprite(Sprite sprite, int sortingOrder)
    {
        // set the sprite
        spriteRenderer.sprite = sprite;

        // set the sorting order for the sprite
        spriteRenderer.sortingOrder = sortingOrder;
    }


    public void SetSpriteColour(float r, float g, float b)
    {
        // set the sprite colour
        r /= 255;
        g /= 255;
        b /= 255;

        spriteRenderer.color = new Color(r, g, b);
    }


} // end of class
