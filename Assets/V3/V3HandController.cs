using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class V3HandController : MonoBehaviour
{
    //input



    //interactors

    [SerializeField] private V3HandInteractor soulInteractor;
    [SerializeField] private int maxInteractors = 1;

    private List<V3HandInteractor> interactors = new List<V3HandInteractor>();
    private V3HandInteractor currentInteractor;
    private int currentIndex = 0;

    private void Start()
    {
        Clear();
    }

    public void Swap()
    {
        currentIndex++;
        Swap(currentIndex);
    }

    public void Swap(int _index) 
    {
        currentIndex %= interactors.Count;
        Swap(interactors[_index]);
    }

    public void Swap(V3HandInteractor _interactor) 
    {
        currentInteractor.gameObject.SetActive(false);
        currentInteractor = _interactor;
        currentInteractor.gameObject.SetActive(true);
    }

    public void Clear() 
    {
        currentIndex = 0;
        Swap(soulInteractor);
        interactors.Clear();
        interactors.Add(soulInteractor);
    }

    public void Add(V3HandInteractor _interactor) 
    {
        if (interactors.Count > (maxInteractors + 1) || interactors.Contains(_interactor)) return;
        
        interactors.Add(_interactor);
    }

    public void Remove(V3HandInteractor _interactor) 
    {
        if (!interactors.Contains(_interactor) || _interactor == soulInteractor) return;

        interactors.Remove(_interactor);
    }
}

public class V3Interactable : MonoBehaviour 
{

}
