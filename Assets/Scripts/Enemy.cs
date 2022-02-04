using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float speed;
    public float damage;
    public float defense;
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    private readonly List<Element> inflictedElements = new List<Element>( );

    private void Start ( ) {

    }

    private void Update ( ) {

    }

    public void ApplyFire ( ) {
        if (inflictedElements.FindAll(x => x == Element.fire).Count > 1) {

        }
    }
}