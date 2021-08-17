using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class V3BodyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform headSetController = null;

    [Header("Collider Settings")]
    [SerializeField] private bool autoCollider = true;
    private CapsuleCollider bodyCollider;

    [Header("Bounds Settings")]
    [SerializeField] private bool autoBounds = true;
    [SerializeField] private bool inBoundsDistanceCheck = true;
    [SerializeField] private bool inBoundsCollisionCheck = true;
    [SerializeField] private float boundsTether = 2f;
    [SerializeField] private LayerMask boundsMask;
    private bool isOutOfBounds = false;
    private bool previousBounds = false;

    [Header("Lean Settings")]
    [SerializeField] private bool autoLean = true;
    [SerializeField] private bool leanOnNotGroundedCheck = true;
    [SerializeField] private bool leanOnCollisionCheck = true;
    [SerializeField] private bool unLeanOnMaxDistanceCheck = true;
    [SerializeField] private float leanTether = 1f;
    [SerializeField] private LayerMask leanMask;
    private bool isLeaning = false;
    private bool previousLeaning = false;

    private void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider>();
        UpdateCollider();
    }
    
    private void FixedUpdate()
    {
        if (headSetController == null) return;

        if (autoCollider) UpdateCollider();
        if (autoBounds) UpdateBoundsPosition();
        if (autoLean) UpdateLeanPosition();
        UpdateBodyPosition();
    }

    #region Update Collider
    public void UpdateCollider()
    {
        if (bodyCollider == null) return;

        float _height = headSetController.localPosition.y;
        _height = Mathf.Clamp(_height, 0.2f, 2f);

        Vector3 _center = bodyCollider.center;
        float _radius = bodyCollider.radius;

        if (_height > _radius) _center.y = _height / 2;
        else _center.y = _radius;

        bodyCollider.height = _height;
        bodyCollider.center = _center;
    }
    
    #endregion

    #region Update Bounds
    public void UpdateBoundsPosition()
    {
        isOutOfBounds = CheckOutOfBounds();

        if (isOutOfBounds == previousBounds) return;
        previousBounds = isOutOfBounds;

        if (isOutOfBounds)
        {
            //disable certain controls etc...
            //enables visual tether
            //call OnOutOfBoundsEvent
        }
        else
        {
            //enable certain controls
            //disables visual tether 
            //call OnInBoundsEvent
        }
    }
    public bool CheckOutOfBounds()
    {
        bool _outOfBounds = isOutOfBounds;

        //if our head is colliding with something, we are out of bounds 100% ... 
        //if our body is not colliding with something, we are in bounds
        //if our body is not past the bounds tether, we are in bounds


        if (inBoundsCollisionCheck)
        {
            //if something is inbetween the body and the head ... we are out of bounds
            if (!Physics.CapsuleCast(bodyCollider.center - (Vector3.up * bodyCollider.center.y), bodyCollider.center + (Vector3.up * bodyCollider.center.y), bodyCollider.radius,
                new Vector3(headSetController.position.x, transform.position.y, headSetController.position.z), Mathf.Infinity, boundsMask)) return false;
        }

        if (inBoundsDistanceCheck)
        {
            //if our distance is greater then the tether, we are out of bounds
            if (GetDistance() <= boundsTether) return false;
        }

        return _outOfBounds;
    }
    public void SetBounds(bool bounds) 
    {
        isOutOfBounds = !bounds;
    }

    #endregion

    #region Update Lean
    public void UpdateLeanPosition()
    {
        isLeaning = CheckToLean();
        
        if (isLeaning == previousLeaning) return;
        previousLeaning = isLeaning;

        if (isLeaning)
        {
            //call OnLean event
        }
        else
        {
            //call OnUnLean event
        }
    }
    private bool CheckToLean()
    {
        bool _leaning = false;

        //then check if we are over the edge
        if (leanOnNotGroundedCheck)
        {
            //if we are not grounded, then leaning = true
        }

        //if the distance between the head and the tether are too great, then stop leaning
        if (unLeanOnMaxDistanceCheck)
        {
            if (GetDistance() > leanTether) _leaning = false;
        }

        //if we are colliding with an object, we are considered leaning but simply going back into the tether distance wont fix it
        if (leanOnCollisionCheck)
        {
            //if we are colliding with an object, we should try to lean
            if (Physics.CapsuleCast(bodyCollider.center - (Vector3.up * bodyCollider.center.y), bodyCollider.center + (Vector3.up * bodyCollider.center.y), bodyCollider.radius,
                new Vector3(headSetController.position.x, transform.position.y, headSetController.position.z), Mathf.Infinity, boundsMask)) _leaning = true;
        }

        return _leaning;
    }

    #endregion

    #region Update Body
    private void UpdateBodyPosition() 
    {
        //if we arent out of bounds and not leaning then just move to the correct position
        if (!isOutOfBounds && !isLeaning)
        {
            Vector3 _position = headSetController.position;
            _position.y = transform.position.y;
            transform.position = _position;
            Quaternion _rotation = transform.rotation;
            Vector3 _euler = _rotation.eulerAngles;
            _euler.y = headSetController.rotation.eulerAngles.y;
            _rotation.eulerAngles = _euler;
            transform.rotation = _rotation;
            return;
        }
    }
    public float GetDistance()
    {
        if (headSetController == null) return -1;
        return Vector3.Distance(transform.position, new Vector3(headSetController.position.x, transform.position.y, headSetController.position.z));
    }

    #endregion

}

public enum LeanType {Static, Continuous, Dynamic }
