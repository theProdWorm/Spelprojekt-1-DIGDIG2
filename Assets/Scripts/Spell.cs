using UnityEngine;
using Aarthificial.Reanimation;

public abstract class Spell : MonoBehaviour {
    public float damage;

    [Tooltip("(%)")]
    public float slowAmount;

    [Tooltip("(seconds)")]
    public float slowDuration;

    [Tooltip("(seconds)")]
    public float stunDuration;

    public Element element;

    protected Reanimator reanimator;

    protected void Start ( ) {
        reanimator = GetComponent<Reanimator>( );
    }
}