using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    

    [SerializeField] private UnityEvent OnMenuOn;
    [SerializeField] private UnityEvent OnMenuOff;
    [SerializeField] private UnityEvent OnMenuToggle;

    public bool toggleValue { get; private set; }
    public void QuitGame() 
    {
        Application.Quit();
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMenu() 
    {
        ToggleMenu(!toggleValue);
    }

    public void ToggleMenu(bool value) 
    {
        toggleValue = value;
        menu.SetActive(toggleValue);
        if (toggleValue) OnMenuOn?.Invoke();
        else OnMenuOff?.Invoke();
        OnMenuToggle?.Invoke();
    }

}
