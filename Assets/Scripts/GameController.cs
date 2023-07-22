using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    int width = 1800, height = 800;
    public GridLayoutGroup gridView;
    public GameObject congratulation;
    public GameObject card;
    GameObject firstCard=null,secondCard=null;
    bool isSelected = false;
    Result result;
    int numberOfSelectedCards;
    int shownCard = 0;
    public IEnumerator selectCard(GameObject _card)
    {
        shownCard++;
        if (shownCard == 1)
        {
            firstCard = _card;
  
        }else if (shownCard == 2)
        {
            secondCard = _card;
        }
        else
        {
            yield break;
        }
        AudioManager.Instance.play(true);

        
       
        if (secondCard != null&& firstCard.GetComponent<CardController>().block == secondCard.GetComponent<CardController>().block)
        {
            yield return new WaitForSeconds(0.5f);
            shownCard=0;
            isSelected = false;
            firstCard.GetComponent<CardController>().flipCard();
            firstCard = null;
            secondCard = null;
        }
        else
        {
            _card.GetComponent<CardController>().flipCard();
            yield return new WaitForSeconds(0.5f);


            if (!isSelected)
            {
                isSelected = true;
            }
            else
            {
                StartCoroutine(ApiManager.Instance.logGame(result.game_id, firstCard.GetComponent<CardController>().block, secondCard.GetComponent<CardController>().block));

                //here's local logic
                /*
                if (firstCard.GetComponent<CardController>().card.Equals(secondCard.GetComponent<CardController>().card))
                {
                    firstCard.GetComponent<CardController>().hide();
                    secondCard.GetComponent<CardController>().hide();
                    numberOfSelectedCards += 2;
                    if (numberOfSelectedCards == result.grid.Length)
                    {
                        endGame();
                    }
                }
                else
                {
                    selectedCard.GetComponent<CardController>().flipCard();
                    _card.GetComponent<CardController>().flipCard();
                }

                shownCard = 0;



                isSelected = false;

                */

            }
        }

    }
    public void updateGame(bool match)
    {
        if (match)
        {
            AudioManager.Instance.play(false);
            firstCard.GetComponent<CardController>().hide();
            secondCard.GetComponent<CardController>().hide();
            numberOfSelectedCards += 2;
            if (numberOfSelectedCards == result.grid.Length)
            {
                endGame();
            }
        }
        else
        {

            AudioManager.Instance.play(true);
            firstCard.GetComponent<CardController>().flipCard();
            secondCard.GetComponent<CardController>().flipCard();
        }
        firstCard = null;
        secondCard = null;

        shownCard = 0;



        isSelected = false;
    }


    void endGame()
    {
        gridView.gameObject.SetActive(false);
        congratulation.SetActive(true);

    }
    public void setGridProprietes(Result result)
    {
        gridView.gameObject.SetActive(true);
           numberOfSelectedCards = 0;
        this.result = result;
        int length = result.grid.Length;
        // Compute number of rows and columns, and cell size
        var ratio = width / height;
        var ncols_float = Math.Sqrt(length * ratio);
        var nrows_float = length / ncols_float;

        // Find best option filling the whole height
        double nrows1 = (int)Math.Ceiling(nrows_float);
        double ncols1 = (int)Math.Ceiling(length / nrows1);
        while (nrows1 * ratio < ncols1)
        {
            nrows1++;
            ncols1 = (int)Math.Ceiling(length / nrows1);
        }
        int cell_size1 = height / (int)nrows1;
        cell_size1 -= 20;
        // Find best option filling the whole width
        double ncols2 =(int) Math.Ceiling(ncols_float);
        double nrows2 = (int)Math.Ceiling(length / ncols2);
        while (ncols2 < nrows2 * ratio)
        {
            ncols2++;
            nrows2 = (int)Math.Ceiling(length / ncols2);
        }
        var cell_size2 = width / ncols2;
        cell_size2 -= 20;
        // Find the best values
        double nrows, ncols, cell_size;
        if (cell_size1 < cell_size2)
        {
            nrows = nrows2;
            ncols = ncols2;
            cell_size = cell_size2;
        }
        else
        {
            nrows = nrows1;
            ncols = ncols1;
            cell_size = cell_size1;
        }

        gridView.cellSize = new Vector2((float)cell_size, (float)cell_size);
        fetchCards(result.grid);
    }
    void fetchCards(Grid[] grids)
    {
        GameObject newCard;
        foreach (Grid grid in grids)
        {
            newCard = Instantiate(card, gridView.transform);
            newCard.GetComponent<CardController>().createCard(grid);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

}
