using UnityEngine;
using UnityEngine.UI;

public class FSM_Player : MonoBehaviour
{
    public float JumpForce;
    public string[] States = new string[] { "State1_Idle", "State2_Jump", "State3_Duck", "State4_Swap", "State5" };
    public Text StateText;

    public readonly State1 S1 = new State1();
    public readonly State2 S2 = new State2();
    public readonly State3 S3 = new State3();
    public readonly State4 S4 = new State4();

    private Rigidbody rb;

    private BaseState currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetState(States[0]);
    }

    private void Start()
    {
        TransitionToState(S1);
    }


    void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void SetState(string state)
    {
        StateText.text = state;
    }
}
