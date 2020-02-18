using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardMoveCounterPresenter : Presenter<BoardMoveCounterMediator>
{
	public TextMeshProUGUI TextField;

	public void OnMoveCountChanged(int moveCount)
	{
		TextField.SetText(moveCount.ToString());
	}
}

