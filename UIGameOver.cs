using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
        ASM_MN = FindObjectOfType<ASM_MN>();
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();

        

        ASM_MN.Instance.YC1(ScoreKeeper.Instance);
        ASM_MN.Instance.YC2();
        ASM_MN.Instance.YC3(ScoreKeeper.Instance.GetScore());
        ASM_MN.Instance.YC4(ScoreKeeper.Instance.GetID());
        ASM_MN.Instance.YC5();
        ASM_MN.Instance.YC6();
        ASM_MN.Instance.YC7();
    }




}
