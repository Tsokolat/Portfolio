using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

public class BoardMoveCounterMediator : Mediator<BoardMoveCounterPresenter>
{
	private Board board;
	public override void Mediate()
	{
		board = ServiceLocator.Resolve<Board>();

		Presenter.OnMoveCountChanged(0);
		board.OnMoveCountChanged += Presenter.OnMoveCountChanged;
	}
}
