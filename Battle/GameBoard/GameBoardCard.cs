using PSW.Core.Enums;
using PSW.Core.Extensions;
using UnityEngine;
using DG.Tweening;

public class GameBoardCard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private SpriteRenderer frameImage;
    [SerializeField] private SpriteRenderer cover;
    [SerializeField] private Transform particleCase;
    
    private GameBoard board;
    private ItemBlueprintCard data;

    public CardType CardType { get; private set; }
    public CardDetailType CardDetailType { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsSpawned => this.gameObject.activeSelf;

    public string GetCardName()
    {
        return this.data.CardName;
    }

    public void Set(GameBoard board, ItemBlueprintCard data)
    {
        this.board = board;
        SetData(data);
    }

    /// <summary>
    /// 카드 데이터 가져오기
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ItemBlueprintCard data)
    {
        this.data = data;
        this.cardImage.sprite = data.ItemImage;
        this.cardImage.color = data.CardColor;
        this.frameImage.sprite = data.CardFrame;

        this.CardType = data.CardType;
        this.CardDetailType = data.CardDetailType;
    }

    public void Selected()
    {
        this.cover.DOFade(0.5f, 0.1f);
    }

    public void Deselected()
    {
        this.cover.DOFade(0.0f, 0.1f);
    }

    /// <summary>
    /// 카드 오브젝트 이동 
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="time"></param>
    public void Move(Vector3 targetPos, float time)
    {
        this.IsMoving = true;
        this.transform.MoveCoroutine(targetPos, time, () => { this.IsMoving = false; });
    }

    /// <summary>
    /// 카드 오브젝트 활성화 및 spawn 애니메이션
    /// </summary>
    public void Spawn()
    {
        // select된 카드 cover sprite 투명도 초기화
        Color coverAlpha = this.cover.color;
        coverAlpha.a = 0.0f;
        this.cover.color = coverAlpha;

        this.gameObject.SetActive(true);

        Vector2 startScale = Vector2.one * 0.1f;
        Vector2 endScale = Vector2.one * 1.0f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f);
    }

    /// <summary>
    /// 카드 despawn 애니메이션 및 오브젝트 비활성화
    /// </summary>
    public void Despawn()
    {
        Vector2 startScale = Vector2.one * 1.0f;
        Vector2 endScale = Vector2.one * 0.1f;

        this.transform.ScaleCoroutine(startScale, endScale, 0.2f, () => { Init(); });
    }

    private void Init()
    {
        this.gameObject.SetActive(false);

        if (this.CardType == CardType.Obstacle) return;
        if (this.CardDetailType == CardDetailType.Normal) return;

        this.board.SkillCardReset(this.data);
    }
}
