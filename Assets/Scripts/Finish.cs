using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    private bool _isActivated = false;

    public void Activate () {
      Debug.Log("Is Acivated!");
      _isActivated = true;
    }
    
    public void FinishLevel() {
       if(_isActivated) {
         gameObject.SetActive(false);
       }
    }
}
