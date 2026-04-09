using UnityEngine;

public class BOSS_TigerMoveState : MonoBehaviour
{
    //列挙型
    private enum State
    {
        Patrol,     //巡回状態(ターゲットを見つけてない、探している状態)
        Chase       //追跡状態(ターゲットを既に見つけており、追いかけてる状態)
    }

    //State型の変数に「Patrol」を代入 最初はパトロール状態からスタートする
    private State _currentState = State.Patrol;

    //ターゲット
    private Transform _target;

    //検知する距離
    [SerializeField] private float _detectDistance = 3f;

    //巡回速度
    [SerializeField] private float _patrolSpeed = 1.0f;

    //追跡速度
    [SerializeField] private float _chaseSpeed = 3.0f;

    [Header("左右反転させるTransform")]
    [SerializeField] private Transform _enemyTransform;

    //デフォルトの(_enemyTransformの)Scale
    private Vector3 _defaultScale;

    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        //ターゲットを探す
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //デフォルトのScaleをセット
        _defaultScale = _enemyTransform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    private void FixedUpdate()
    {
        UpdateState();
    }

    private void CheckDistance()
    {
        //ターゲットとの距離を計算
        float distance = Vector2.Distance(transform.position, _target.position);

        //ターゲットが検知距離内にいるかどうかをチェック
        if (distance <= _detectDistance)
        {
            //ターゲットが検知距離内にいる場合、追跡状態に切り替える
            _currentState = State.Chase;
        }
        else
        {
            //ターゲットが検知距離外にいる場合、巡回状態に切り替える
            _currentState = State.Patrol;
        }
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    private void Patrol()
    {
        if (_enemyTransform.localScale.x > 0)
        {
            //左方向に移動
            _rb.linearVelocityX = -_patrolSpeed;
        }
        else if (_enemyTransform.localScale.x < 0)
        {
            //右方向に移動
            _rb.linearVelocityX = _patrolSpeed;
        }
    }

    private void Chase()
    {
        //Sign:プラスかマイナスを返す
        float direction = Mathf.Sign(_target.position.x - transform.position.x);

        //directionが1ならxのScaleにマイナスをつけて左右反転
        _enemyTransform.localScale = new Vector3(-direction * _defaultScale.x, _defaultScale.y, _defaultScale.z);

        //ターゲットの方向に移動
        _rb.linearVelocityX = direction * _chaseSpeed;
    }
}
