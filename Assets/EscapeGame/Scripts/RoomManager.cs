using UnityEngine;

public class RoomManager : MonoBehaviour {

    /// <summary>
    /// Return true if the player is inside the room
    /// </summary>
    public bool IsPassedThrough;

    /// <summary>
    /// Starting points for draw raycasts at room door location
    /// </summary>
    public Transform TopLeft;
    public Transform BottomRight;
    public Transform TopRight;
    public Transform BottomLeft;

    /// <summary>
    /// Players Informations
    /// </summary>
    public LayerMask PlayerMask;

    /// <summary>
    /// Delegates for callback when player enter in the area
    /// </summary>
    public delegate void RoomInteractions(Collider collider);

    /// <summary>
    /// When player enter in the rooom
    /// When Player Leave the room
    /// When Player Validated the Sequences
    /// When Player Failed the sequences
    /// </summary>
    public RoomInteractions OnEnter;
    public RoomInteractions OnExit;
    public RoomInteractions OnSequenceValidated;
    public RoomInteractions OnFailed;

    private bool playerSet;

    private void Awake() {
        // Default OnEnterBehavior
        OnEnter = (Collider collider) => {
            Debug.Log(collider.name);
        };
    }

	private void Start() {
	}

	void Update() {
        if (!playerSet) {
            GameObject p = GameObject.Find("[VRTK][AUTOGEN][BodyColliderContainer]");
            GameObject foot = GameObject.Find("[VRTK][AUTOGEN][FootColliderContainer]");

            if (p != null) {
                foot.layer = 8;
                p.tag = "Player";
                p.layer = 8;
                playerSet = true;
            }
        }
        if (!IsPassedThrough && OnEnter != null)
            DetectPlayerIncoming();
    }

    private void DetectPlayerIncoming() {
        Ray leftToRightRay = new Ray(TopLeft.position, BottomRight.position - TopLeft.position);
        Ray rightToBottRay = new Ray(TopRight.position, BottomLeft.position - TopRight.position);

        RaycastHit leftCast;
        RaycastHit rightCast;

        Physics.Raycast(leftToRightRay, out leftCast, 9, PlayerMask);
        Physics.Raycast(rightToBottRay, out rightCast, 9, PlayerMask);

        if (leftCast.collider != null) {
            OnEnter(leftCast.collider);
            IsPassedThrough = true;
        }

        if (rightCast.collider != null) {
            OnEnter(rightCast.collider);
            IsPassedThrough = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        if (TopLeft != null && BottomRight != null
           && TopRight != null && BottomLeft != null) {
            Gizmos.DrawRay(TopLeft.position, BottomRight.position - TopLeft.position);
            Gizmos.DrawRay(TopRight.position, BottomLeft.position - TopRight.position);
        }
    }
}
