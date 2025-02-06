using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject magePrefab;
    public GameObject warriorPrefab;

    private GameObject currentPlayer;

    void Start()
    {
        SelectClass();
    }

    void SelectClass()
    {
        if (PlayerPrefs.GetString("SelectedClass") == "Mage")
        {
            currentPlayer = Instantiate(magePrefab, transform.position, transform.rotation);
        }
        else
        {
            currentPlayer = Instantiate(warriorPrefab, transform.position, transform.rotation);
        }
    }
}
