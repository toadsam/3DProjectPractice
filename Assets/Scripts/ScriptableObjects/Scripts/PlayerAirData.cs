using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAirData  //위에 있을 때 처리할 데이터
{
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 4f;

}