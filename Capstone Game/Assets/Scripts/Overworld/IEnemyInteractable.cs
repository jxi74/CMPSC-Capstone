using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyInteractable
{
   public string InteractionPrompt { get; }

   public bool Interact(EnemyInteractor interactor);
}
