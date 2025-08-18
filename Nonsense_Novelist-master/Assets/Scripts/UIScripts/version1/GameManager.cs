using UnityEngine.SceneManagement;
using UnityEngine;

///<summary>
///
///</summary>
class GameManager : MonoBehaviour
{
    public GameObject exitPanel;
    public Canvas canvas;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject a = Instantiate(exitPanel,canvas.transform);
            Time.timeScale = 0f;
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void BackToGame()
    {
        Destroy(this.gameObject);
        Time.timeScale = GameMgr.instance.timeSpeed;
    }
}
