using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : CinemachineExtension
{
    // BU KAMERAYI Z EKSENİNDE -10 A SABİTLİYOR SİLME
    [Tooltip("Lock the camera's Z position to this value")]
    public float m_ZPosition = 10;

    [SerializeField] private GameObject player;
    [SerializeField] private int distance;
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.z = m_ZPosition;
            state.RawPosition = pos;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > distance)
        {
            Die.Instance.FinishGame();
        }
    }
}
