using UnityEngine;
using System.Collections;

public class PyloneNode : MonoBehaviour {

    /// <summary>
    /// The Pylone's type.
    /// </summary>
    public GameBoard.PyloneType Type;

    /// <summary>
    /// The particle system attached to the pylone
    /// </summary>
    public ParticleSystem ParticleSystemH;
    public ParticleSystem ParticleSystemV;

    /// <summary>
    /// Is actived by the player ?
    /// </summary>
    public bool IsActive;
    public bool HActivated;

    void Start() {
        IsActive = false;
    }

    public void Activated() {
        
    }

    public void ShineV() {
        ParticleSystemV.transform.gameObject.SetActive(true);
        IsActive = true;
    }

    public void ShineH() {
        ParticleSystemH.transform.gameObject.SetActive(true);
        HActivated = true;
    }
}
