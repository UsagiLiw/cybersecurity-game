using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    public void DeactivateMySelf()
    {
        this.gameObject.SetActive(false);
    }
}
