using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 저택 외부씬으로 전환
 * 카메라가 유우지에게 포커스 -> 카메라 매니저
 * 문 여는 소리 재생 후 김신에게 카메라 포커스 -> 카메라 매니저 + 김신
 * 괴물 사운드 재생 -> 카메라 매니저
 * 김신 뒷걸음질 -> 김신
 * 괴물 등장 -> 괴물
 * 김신 대사 출력 -> 김신 + dialogueManager
 * 카메라 유우지에게 포커스 -> 카메라 매니저
 * 저택 내부로 진입 -> 유우지
 */
public class YujiKidController : MonoBehaviour
{
    public Animator _animator;
    public void SetTurnBack()
    {
        _animator.SetFloat("DirX",0.0f);
        _animator.SetFloat("DirY", 1.0f);
    }


}
