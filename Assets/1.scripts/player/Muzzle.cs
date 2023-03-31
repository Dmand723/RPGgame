using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public SpriteRenderer getMuzzleSprite()
    {
        return GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
