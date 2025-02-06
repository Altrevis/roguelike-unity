using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassSelection : MonoBehaviour
{
    public void SelectMage()
    {
        PlayerPrefs.SetString("SelectedClass", "Mage");
        SceneManager.LoadScene("GameScene");
    }

    public void SelectWarrior()
    {
        PlayerPrefs.SetString("SelectedClass", "Warrior");
        SceneManager.LoadScene("GameScene");
    }
}
