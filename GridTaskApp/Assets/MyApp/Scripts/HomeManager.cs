using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HomePage
{
    None = 0,
    List = 1,   //一覧
    Add = 2,    //登録
    Option = 3, //
}
public class HomeManager : MonoBehaviour
{
    [SerializeField,Header("各ページのプレハブを生成する親オブジェクト")]
    GameObject m_pagesParent;

    [SerializeField,Header("各ページプレハブ")]
    GameObject[] m_pagePrefubs = null;

    //現在のページ名
    HomePage m_currentPage = HomePage.List;

    //TODO 後で消す　テスト用
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        //全てのページを非アクティブにしてからオンにする
        m_currentPage = HomePage.List;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(int nextPage)
    {
        //同じページだったらreturn
        if (nextPage == (int)m_currentPage)
            return;


    }
}
