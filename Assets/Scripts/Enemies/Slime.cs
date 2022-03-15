using System.Linq;
using UnityEngine;

public enum Size {
    small = 0, medium = 1, large = 2
}

public class Slime : Entity {
    public Size size;

    public float partnerSearchRadius;

    private float lastSplit; // time since last split
    
    protected override void Start ( ) {
        base.Start( );
    }

    protected override void Update ( ) {
        base.Update( );

        bool success = false;
        if (lastSplit > 0 && size < Size.large) success = TryFuse( );
        else if (size == Size.large) TryConsume( );
        else if (!success) Nibble( );
    }

    private void TryConsume ( ) {
        

    }

    private void Nibble ( ) {

    }

    /// <summary>
    /// Scans for slimes of the same size to fuse with, picking the center between them all as their meeting point
    /// </summary>
    private bool TryFuse ( ) {
        Slime[ ] slimesInRadius = (from slime in FindObjectsOfType<Slime>( )
                                   where Vector2.Distance(slime.transform.position, transform.position) <= partnerSearchRadius
                                   && slime.size == size
                                   select slime).OrderByDescending(x => Vector2.Distance(x.transform.position, transform.position)).ToArray( );

        if (slimesInRadius.Length < 3) return false;

        Vector2 meetingPoint = Vector2.zero;

        for (int i = 0; i < slimesInRadius.Length; i++) {
            meetingPoint += (Vector2) slimesInRadius[i].transform.position;
        }

        meetingPoint /= slimesInRadius.Length;

        transform.position = Vector2.MoveTowards(transform.position, meetingPoint, c_speed);

        return true;
    }

    /// <summary>
    /// Instantiate smaller slimes, shooting out from the original one in random directions
    /// </summary>
    /// <returns></returns>
    private void Split ( ) {
        for (int i = 0; i < 3; i++) {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float force = Random.Range(1.0f, 2.0f);

            Vector2 forceVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * force;

            var newSlime = Instantiate(gameObject).GetComponent<Slime>( );

            newSlime.size = size - 1;
            newSlime.rb.AddForce(forceVector);
        }
    }

    protected override void Die ( ) {
        if (size > 0) Split( );
        else base.Die( );
    }
}