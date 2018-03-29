using UnityEngine;
using VRTK;
using UnityEngine.Assertions;

public class ColoredBox : MonoBehaviour {

    public GameBoard gameBoard;
    public bool IsPositionned;

    public GameBoard.PyloneType Type;

    private VRTK_InteractableObject Interactions;
    private Vector3 BasePosition;

    void Start() {
        Interactions = GetComponent<VRTK_InteractableObject>();
        Assert.IsNotNull(Interactions);

        // Set Initial Position
        BasePosition = transform.position;

        // SET CALLBACKS
        Interactions.InteractableObjectGrabbed += OnGrabReciever;
        Interactions.InteractableObjectUngrabbed += OnUnGrabReciever;
        Interactions.InteractableObjectTouched += OnTouchedReceiver;
    }

    /// <summary>
    /// Callback initialized on grab events
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void OnGrabReciever(object sender, InteractableObjectEventArgs e) {
        Debug.Log("Grabedd: " + e.interactingObject.name);
    }

    /// <summary>
    /// Callback initialized on ungrabbed events
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void OnUnGrabReciever(object sender, InteractableObjectEventArgs e) {
        Debug.Log("UnGrabbed: " + e.interactingObject.name);
    }

    private void OnTouchedReceiver(object sender, InteractableObjectEventArgs e) {
        if (Interactions.IsGrabbed()) {
            if (gameBoard.SetBoxOnClosestPylone(this)) {
                IsPositionned = true;
            }
        }
    }

    /// <summary>
    /// Set the object Grabable or not
    /// </summary>
    /// <param name="value">If set to <c>true</c> value.</param>
    public void SetGrabbableState(bool value = false) {
        Interactions.isGrabbable = value;
    }

    public void UnGrab() {
        Interactions.ForceStopInteracting();
    }
}
