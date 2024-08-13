using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    private bool isActivated = false;

    public void Activate () {
      Debug.Log("Is Acivated!");
      isActivated = true;
    }
    
    public void FinishLevel() {
       if(isActivated) {
         gameObject.SetActive(false);
       }
    }
}
