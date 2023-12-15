using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _hP;
    [SerializeField]
    TextMeshProUGUI _gameOverText;
    [SerializeField]
    Button _restartButton;

    private void Start()
    {
        EventsMediator.AddHealthChangedListener(ChangedPlayerHPEventHandler);
        EventsMediator.AddPlayerDiedListener(GameOverEventHandler);
    }

    void ChangedPlayerHPEventHandler(int playerHP)
    {
        _hP.text = "HP: " + playerHP.ToString();
    }

    void GameOverEventHandler()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _hP.gameObject.SetActive(false);
    }


}
