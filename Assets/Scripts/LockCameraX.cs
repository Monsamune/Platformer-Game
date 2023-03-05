using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockCameraX : CinemachineExtension {
   protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime){
        if(stage == CinemachineCore.Stage.Body){
            Vector3 position = state.RawPosition;
            position.x = 0;
            state.RawPosition = position;

        }

    }
}
