//親の親オブジェクトにAnswerManagement.csを入れておくこと
using UnityEngine;

// Sprite表示用
[RequireComponent(typeof(UnityEngine.RectTransform))]
[RequireComponent(typeof(UnityEngine.SpriteRenderer))]
// 当たり範囲設定用
[RequireComponent(typeof(UnityEngine.PolygonCollider2D))]


public class LifelineDropButton : MonoBehaviour
{
    private int _spriteNumber;
    /*
     0 = default
     1 = selected
     2 = confirmed
     */
    private AnswerManagement _answerManagement;
    private SpriteRenderer _spriteR;
    private Sprite _defaultSprite;
    [SerializeField] Sprite m_highlightedSprite;
    [SerializeField] Sprite m_pressedSprite;
    [SerializeField] Sprite m_selectedSprite;
    [SerializeField] Sprite m_confirmPressedSprite;
    [SerializeField] Sprite m_confirmedSprite;
    void Start()
    {
        _spriteR = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteR.sprite;
        _answerManagement = GetComponentInParent<AnswerManagement>();
        _spriteNumber = 0;
    }
    void OnMouseEnter()
    {
        if (_answerManagement.FinalAnswer != 2 && _spriteNumber == 0)
        {
            Debug.Log("[LifelineDropButton] " + name + "の上に来たぞ！");
            _spriteR.sprite = m_highlightedSprite;
        }
    }
    void OnMouseExit()
    {
        if (_spriteR.sprite == m_highlightedSprite)
        {
            Debug.Log("[LifelineDropButton] " + name + "から離れた！");
            _spriteR.sprite = _defaultSprite;
        }
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && _answerManagement.FinalAnswer != 2 && _answerManagement.Check(_spriteNumber, name))   // true時のみ実行
        {
            if (_spriteNumber == 1)
            {
                Debug.Log("[LifelineDropButton] " + name + "でファイナルアンサー？");
                _spriteR.sprite = m_confirmPressedSprite;
            }
            else
            {
                Debug.Log("[LifelineDropButton] " + name + "をクリック?");
                _spriteR.sprite = m_pressedSprite;
            }
        }
    }
    void OnMouseUpAsButton()
    {
        if (_answerManagement.FinalAnswer != 2 && _answerManagement.Check(_spriteNumber, name))
        {
            if (_spriteNumber == 1)
            {
                Debug.Log("[LifelineDropButton] " + name + "でファイナルアンサー！");
                _spriteR.sprite = m_confirmedSprite;
                _answerManagement.FinalAnswer = 2;
                _spriteNumber = 2;
            }
            else
            {
                Debug.Log("[LifelineDropButton] " + name + "をクリック！");
                _spriteR.sprite = m_selectedSprite;
                _answerManagement.FinalAnswer = 1;
                _spriteNumber = 1;
            }
        }

    }
    public void ResetSprite()
    {
        _spriteR.sprite = _defaultSprite;
        _spriteNumber = 0;
    }
}
