using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class MemorySript : MonoBehaviour
{
    private VisualElement firstCard = null;
    private VisualElement secondCard = null;
    private Coroutine waitCoroutine;
    private System.Random rnd = new System.Random();
    public void Start()
    {
        List<Color> colors = new List<Color>() { Color.red, Color.red, Color.blue, Color.blue, Color.yellow, Color.yellow };
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement table=root.Q("Table");
        table.RegisterCallback<MouseDownEvent>(TableCallBack);
        List<VisualElement> cards = table.Query("Card").ToList();
        foreach (VisualElement card in cards)
        {
            card.style.backgroundColor = Color.white;
            card.userData = RandomColor(colors);
        }
    }

    private Color RandomColor(List<Color> colors)
    {
        Color color= Color.white;
        if (colors.Count > 0)
        {
            int randomIndex= rnd.Next(0, colors.Count);
            color = colors[randomIndex];
            colors.RemoveAt(randomIndex);
        }
        return color;
    }

    private void TableCallBack(MouseDownEvent e)
    {
        VisualElement element = e.target as VisualElement;
        if (element.name == "Table") return;
        if (element.style.backgroundColor != Color.white) return;
        if (firstCard == null)
        {
            firstCard = element;
            element.style.backgroundColor = (Color)element.userData;
            firstCard.style.backgroundColor = (Color)element.userData;
            return;
        }
        if(secondCard == null)
        {
            secondCard = element;
            element.style.backgroundColor = (Color)element.userData;
            secondCard.style.backgroundColor = (Color)element.userData;
            waitCoroutine = StartCoroutine(WaitCoroutine());
        }
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1);
        if (firstCard.style.backgroundColor != secondCard.style.backgroundColor)
        {
            firstCard.style.backgroundColor= Color.white;
            secondCard.style.backgroundColor= Color.white;
        }
        firstCard = null;
        secondCard = null;
    }
}
