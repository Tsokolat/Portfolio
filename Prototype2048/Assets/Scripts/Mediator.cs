using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{

}

public abstract class Mediator<TPresenter> : Mediator
	where TPresenter : Presenter
{
	private TPresenter PresenterField;
	protected TPresenter Presenter => PresenterField;

	private ServiceLocator ServiceLocatorField;
	protected ServiceLocator ServiceLocator => ServiceLocatorField;

	private void Awake()
	{
		PresenterField = GetComponent<TPresenter>();
		ServiceLocatorField = Game.Instance.ServiceLocator;
		Mediate();
	}

	public abstract void Mediate();
}
