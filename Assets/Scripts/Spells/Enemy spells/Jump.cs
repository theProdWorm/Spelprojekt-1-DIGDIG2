using System.Collections;
using UnityEngine;

public class Jump : Spell {
    private void Awake ( ) {
        //Destroy(gameObject);
        StartCoroutine(DestroyThis( ));
    }

    private IEnumerator DestroyThis ( ) {
        yield return new WaitForSeconds(.05f);
        Destroy(gameObject);
    }
}