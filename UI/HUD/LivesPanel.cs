using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanel : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<Image> LivesImages;

    public void Start()
    {
        if (playerController)
            playerController.PlayerModel.ChangeLivesEvent.AddListener(SetLives);
    }

    public void SetLives(int count)
    {
        if ((count >= 0) && (count <= 7))
        {
            foreach (var live in LivesImages)
            {
                live.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }

            for (int i = 0; i < count; i++)
            {
                LivesImages[i].color = new Color(1, 1, 1, 1);
            }
        }

        //
    }
}
