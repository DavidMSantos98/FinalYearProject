using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] InstructionsPages;

    private int numberOfPages;
    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;
        numberOfPages = InstructionsPages.Length;
        InstructionsPages[0].SetActive(true);
    }


    public void NextPage()
    {

        if (currentPage== numberOfPages-1)
        {
            NextScene();
        }
        else
        {
            InstructionsPages[currentPage].SetActive(false);
            currentPage++;
            InstructionsPages[currentPage].SetActive(true);
        }

    }

    public void PreviousPage()
    {
        if (currentPage - 1 >= 0)
        {
            InstructionsPages[currentPage].SetActive(false);
            currentPage--;
            InstructionsPages[currentPage].SetActive(true);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
