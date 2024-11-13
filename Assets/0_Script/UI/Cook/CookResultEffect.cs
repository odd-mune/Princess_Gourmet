using UnityEngine;

public class CookResultEffect : MonoBehaviour
{
    public bool bIsSuccess;
    public Vector2 Position;
    private Animator mAnimator;

    public float AngleMinInRadian;
    public float AngleMaxInRadian;

    public RuntimeAnimatorController SuccessController;
    public RuntimeAnimatorController FailController;

    // Start is called before the first frame update
    void Start()
    {
        if (mAnimator == null)
        {
            mAnimator = GetComponent<Animator>();
            if (mAnimator == null)
            {
                Debug.LogError("Animator component is missing!!");
                Debug.Break();
            }
        }
    }

    private void OnEnable()
    {
        if (mAnimator == null)
        {
            mAnimator = GetComponent<Animator>();
            if (mAnimator == null)
            {
                Debug.LogError("Animator component is missing!!");
                Debug.Break();
            }
        }

        CookPanel2 cookPanel = GetComponentInParent<CookPanel2>();
        if (cookPanel == null)
        {
            Debug.LogError("CookPanel2 component가 부모로 있어야 합니다!!");
            Debug.Break();
        }

        if (cookPanel.hasDishFailed == true)
        {
            mAnimator.runtimeAnimatorController = FailController;
        }
        else
        {
            mAnimator.runtimeAnimatorController = SuccessController;
        }

        float randomValueForRadius = Random.value;
        float randomRadius = (cookPanel.EffectOutRadius - cookPanel.EffectInnerRadius) * randomValueForRadius + cookPanel.EffectInnerRadius;

        float randomValueForRadian = Random.value;
        float randomRadian = randomValueForRadian * (AngleMaxInRadian - AngleMinInRadian) + AngleMinInRadian;

        //transform.SetLocalPositionAndRotation(new Vector3(randomRadius * Mathf.Cos(randomRadian), randomRadius * Mathf.Sin(randomRadian), transform.localPosition.z), transform.rotation);
        transform.SetLocalPositionAndRotation(new Vector3(0.0f, 0.0f, transform.localPosition.z), transform.rotation);
    }

    private void OnDisable()
    {
        if (mAnimator == null)
        {
            mAnimator = GetComponent<Animator>();
            if (mAnimator == null)
            {
                Debug.LogError("Animator component is missing!!");
                Debug.Break();
            }
        }
    }
}
