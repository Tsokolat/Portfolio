using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

public abstract class Presenter : MonoBehaviour
{
	private ServiceLocator ServiceLocatorField;
	private ServiceLocator ServiceLocator => ServiceLocatorField;

	private void Awake()
	{
		ServiceLocatorField = FindObjectOfType<Game>().ServiceLocator;
		OnAwake();
	}

	public abstract void OnAwake();
}

public class Presenter<T> : Presenter where T : Mediator
{

	[SerializeField] protected T Mediator;

	public override void OnAwake()
	{
		Mediator = Mediator ?? (Mediator = GetComponent<T>());
	}

}
