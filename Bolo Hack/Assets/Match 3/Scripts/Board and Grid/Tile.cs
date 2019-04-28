using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Salu2 testing testing
public class Tile : MonoBehaviour
{
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static Tile previousSelected = null;

    private SpriteRenderer render;
    private bool isSelected = false;

    private int tag = -1;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateTag();
    }

    public void UpdateTag()
    {
        if (render.sprite.name == "characters_0001" || render.sprite.name == "Puffles_Sprite_0")
        {
            tag = 0;
        }
        else if (render.sprite.name == "characters_0002" || render.sprite.name == "Puffles_Sprite_4")
        {
            tag = 4;
        }
        else if (render.sprite.name == "characters_0003" || render.sprite.name == "Puffles_Sprite_2")
        {
            tag = 2;
        }
        else if (render.sprite.name == "characters_0004")
        {
            tag = 3;
        }
        else if (render.sprite.name == "characters_0005")
        {
            tag = 1;
        }
        else if (render.sprite.name == "characters_0006") {
            tag = 5;
        }
        else if (render.sprite.name == "characters_0007") {
            tag = 6;
        }
    }

    public int getTag()
    {
        return tag;
    }

    private void Select()
    {
        Debug.Log("My tag is: " + tag);
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
        SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect()
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected = null;
    }

    void OnMouseDown()
    {
        if (render.sprite == null || BoardManager.instance.IsShifting || render.sprite.name == "stoneheadSprite")
        {
            return;
        }

        if (isSelected)
        {
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            {
                Select();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    SwapSprite(previousSelected.render);
                    previousSelected.ClearAllMatches();
                    if (!(ClearAllMatches()))
                    {
                        //Revert changes from selections
                        BoardManager.instance.IsShifting = true;
                        render.color = Color.white;
                        Invoke("RevertSelect", 0.5f);
                    }
                    else
                    {
                        previousSelected.Deselect();
                    }

                }
                else
                {
                    previousSelected.GetComponent<Tile>().Deselect();
                    Select();
                }
            }
        }
    }

    public void RevertSelect()
    {
        SwapSprite(previousSelected.render);
        previousSelected.Deselect();
        BoardManager.instance.IsShifting = false;
    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (render.sprite == render2.sprite)
        {
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = render.sprite;
        render.sprite = tempSprite;
        SFXManager.instance.PlaySFX(Clip.Swap);
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            //if(hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite.name == "stoneheadSprite")
            //{
            //    return null;
            //}
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        //Debug.Log("The id of the adjacent tag is: " + hit.collider.GetComponent<Tile>().getTag() + " and my own tag is: " + tag);
        //while (hit.collider != null && hit.collider.GetComponent<Tile>().getTag() == tag)
        {
            if(hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
            {
                Debug.Log("Amazing-A");
                Debug.Log(hit.collider.GetComponent<Tile>().getTag());
                Debug.Log("My tag is: " + tag);
            }
            if(hit.collider != null && hit.collider.GetComponent<Tile>().getTag() == tag)
            {
                Debug.Log("Amazing-B");
            }
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles;
    }

    private void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++) { matchingTiles.AddRange(FindMatch(paths[i])); }
        if (matchingTiles.Count >= 2)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                if(matchingTiles[i].GetComponent<SpriteRenderer>().sprite.name != "stoneheadSprite")
                {
                    matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
                }
            }
            matchFound = true;
        }
    }

    private bool matchFound = false;
    public bool ClearAllMatches()
    {

        if (render.sprite == null)
            return false;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;
            StopCoroutine(BoardManager.instance.FindNullTiles());
            StartCoroutine(BoardManager.instance.FindNullTiles());
            SFXManager.instance.PlaySFX(Clip.Clear);
            return true;
        }
        else
        {
            return false;
        }
    }

}