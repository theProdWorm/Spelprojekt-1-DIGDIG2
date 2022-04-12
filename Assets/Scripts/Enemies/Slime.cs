using System.Linq;
using UnityEngine;

public enum Size {
    tiny = 0, medium = 1, large = 2
}

public class Slime : Entity {
    public Size size;

    public float scanRadius;
    [HideInInspector]
    public Slime[ ] partners;

    private float fuseCD;

    [HideInInspector]
    public Vector2 i_splitForceVector;
    [HideInInspector]
    public float splitForceTimer;

    private void Awake ( ) {
        partners = new Slime[2];
    }

    protected override void Update ( ) {
        base.Update( );

        if (splitForceTimer > 0) {
            splitForceTimer -= Time.deltaTime;
            splitForceTimer = Mathf.Clamp01(splitForceTimer);

            rb.velocity = i_splitForceVector * splitForceTimer;
            return;
        }

        if (fuseCD > 0)
            fuseCD -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return) && size > 0)
            Split( );

        bool success = false;
            if (fuseCD <= 0 && size < Size.large) success = TryFuse( );
            else if (size == Size.large) success = TryConsume( );
            
            if (!success) Nibble( );
    }

    private bool TryConsume ( ) {
        return false;
    }

    private void Nibble ( ) {

    }

    /// <summary>
    /// Upon finding partners, rendezvous and fuse.
    /// </summary>
    private bool TryFuse ( ) {
        // scans for slimes of the same size within scanRadius (excludes this)
        Slime[ ] singleSlimes = (from slime in FindObjectsOfType<Slime>( )
                                   where Vector2.Distance(slime.transform.position, transform.position) <= scanRadius
                                   && slime.size == size && slime != this // same size and not self
                                   && slime.partners.All(x => x is null)  // no partners
                                   && slime.fuseCD <= 0                   // can fuse
                                   select slime).OrderByDescending(slime => Vector2.Distance(slime.transform.position, transform.position)).Reverse( ).ToArray( );

        if (partners.All(x => x is null)) {
            if (singleSlimes.Length >= 2)
                BindPartners(singleSlimes);
            else return false;
        }

        Slime[ ] rel = new Slime[ ] { partners[0], partners[1], this };

        // rendezvous midway between slimes in relationship
        Vector3 rendezvous = Vector3.zero;
        for (int i = 0; i < rel.Length; i++) {
            rendezvous += rel[i].transform.position;
        }
        rendezvous /= rel.Length;

        animator.SetInteger("facing", rendezvous.x - transform.position.x > 0 ? 1 : 0);

        // move toward rendezvous
        transform.position = Vector2.MoveTowards(transform.position, rendezvous, c_speed * Time.deltaTime);
        if (transform.position != rendezvous) return true;

        if (partners.All(slime => slime.transform.position == rendezvous))
            Fuse( );

        return true;
    }

    /// <summary>
    /// Binds the first two slimes in 'slimes' as partners.
    /// </summary>
    private void BindPartners (Slime[ ] slimes) {
        partners[0] = slimes[0];
        partners[1] = slimes[1];

        slimes[0].partners[0] = this;
        slimes[0].partners[1] = slimes[1];

        slimes[1].partners[0] = this;
        slimes[1].partners[1] = slimes[0];
    }

    /// <summary>
    /// Instantiates a bigger slime and destroys all slimes involved in the fusion.
    /// </summary>
    private void Fuse ( ) {
        var fusion = Instantiate(gameObject).GetComponent<Slime>( );

        ++fusion.size;
        fusion.fuseCD = 5.0f;

        fusion.name = char.ToUpper($"{fusion.size}"[0]) + $"{fusion.size} slime".Substring(1);


        for (int i = 0; i < partners.Length; i++) {
            Destroy(partners[i].gameObject);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Instantiate smaller slimes, shooting out from the original one in random directions
    /// </summary>
    /// <returns></returns>
    private void Split ( ) {
        for (int i = 0; i < 3; i++) {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float force = 2.0f;

            Vector2 forceVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * force;

            var newSlime = Instantiate(gameObject).GetComponent<Slime>( );

            --newSlime.size;

            newSlime.i_splitForceVector = forceVector;
            newSlime.splitForceTimer = 1.0f;

            newSlime.fuseCD = 5.0f;

            newSlime.name = char.ToUpper($"{newSlime.size}"[0]) + $"{newSlime.size} slime".Substring(1);
        }

        Destroy(gameObject);
    }

    protected override void Die ( ) {
        if (size > 0) Split( );
        else base.Die( );
    }
}