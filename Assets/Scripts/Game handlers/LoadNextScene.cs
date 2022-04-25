using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour {
    private void Start ( ) {
        GetComponent<Button>( ).onClick.AddListener(( ) => SceneManager.LoadScene(SceneManager.GetActiveScene( ).buildIndex + 1));
    }
}