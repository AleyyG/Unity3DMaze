using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Objects")]
    public GameObject item;
    public List<Items> items;
    public GameObject ball;
    public Sprite checkSprite;

    private int price;
    void Start()
    {
        GetShop();
        price = PlayerPrefs.GetInt("totalStarsCount"); // buradaki para şuanda deneme olduğu için kapalı en son bu kod açılacak .
        for (int i = 0; i < items.Count; i++)
        {
            items[i].isGet = GetBool("isGet" + i);
        }

    }

    private void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].isGet = GetBool("isGet" + i);
        }
    }

    void GetShop()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject gameObject = Instantiate(item, transform); //item üretildi
            int tempId = i; //add listenerda i kullanamayacağımdan dolayı i ye geçici bir değer atadım.

            gameObject.transform.Find("Image").GetComponent<Image>().sprite = items[i].itemSprite; // üretilen itemin spriteını benim listemdeki sprite yaptım
            gameObject.transform.Find("Price").GetComponent<Text>().text = items[i].price.ToString(); // üretilen itemin ücretini benim listemdeki ücret yaptım 
            gameObject.transform.Find("buyButton").GetComponent<Button>().onClick.RemoveAllListeners();
            gameObject.transform.Find("buyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(tempId, gameObject)); //satın alma butonunu buldurdum ve tıkladığımda satın alma metodunu uygulattım
            gameObject.transform.GetComponent<Button>().onClick.AddListener(() => SelectItem(tempId, gameObject)); // seçim butonuna tıkla 

            if (GetBool("isGet" + i)) 
            {
                gameObject.transform.Find("Price").GetComponent<Text>().text = " PURCHASED "; // üretilen itemin textini değiştirdim.
                gameObject.transform.Find("buyButton").GetComponent<Image>().sprite = checkSprite; // başlangıçta butonun image i bu
                gameObject.transform.Find("buyButton").GetComponent<Button>().enabled = false; // butonun aktif olmasın
                var ballFind = GameObject.FindGameObjectWithTag("Ball"); // oynadığım topu buldur.
                ball.gameObject.GetComponent<MeshRenderer>().material = ballFind.gameObject.GetComponent<MeshRenderer>().material;
                SelectItem(tempId, gameObject);
            }

        }
    }

    public static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }
    public static bool GetBool(string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void BuyItem(int id, GameObject gameObject)
    {
        if (price >= items[id].price && !GetBool("isGet" + items[id])) // eğer param yetiyorsa ve item bende yoksa satın al 
        {
            ball.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; // ortada dönen topun materialini satın aldığım material yap
            gameObject.transform.Find("buyButton").GetComponent<Button>().enabled = false; //butonun işlevini kapat
            gameObject.transform.Find("buyButton").GetComponent<Image>().sprite = checkSprite; //butonun imageni değiştir.
            gameObject.transform.Find("Price").GetComponent<Text>().text = "PURCHASED"; //butonun textini satın alınca değiştir.
            MazeRenderer.Current.ballPrefab.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; // prefabın materyalini değiştir.
            var ballFind = GameObject.FindGameObjectWithTag("Ball"); // oyunda top tagine sahip objeyi bul
            ballFind.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; // objenin materyalini değiştir
            price -= items[id].price; // fiyatı düş
            LevelController.Current.SetStarText(); // yıldızların textini düzenle
            SetBool("isGet" + id, true); // kaydet
        }
        else
            Debug.Log("yetersiz para"); // buraya ses ekleneccek
    }
    void SelectItem(int id, GameObject gameObject)
    {
        if (items[id].isGet) //GetBool("isGet" + id)
        {
            ball.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; //topun materyalini değiştir.
            MazeRenderer.Current.ballPrefab.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; // prefabın materyalini değiştir.
            var ballFind = GameObject.FindGameObjectWithTag("Ball"); // oynadığım topu buldur.
            ballFind.gameObject.GetComponent<MeshRenderer>().material = items[id].itemMaterial; // oynadığım topun materyalini dğeiştir.
            gameObject.transform.Find("buyButton").GetComponent<Image>().sprite = checkSprite;
        }
    }

}
[System.Serializable]
public class Items
{
    public Sprite itemSprite;
    public Material itemMaterial;
    public int price;
    public bool isGet;
    public Items(Sprite itemSprite, Material itemMaterial, int price, bool isGet)
    {
        this.itemSprite = itemSprite;
        this.itemMaterial = itemMaterial;
        this.price = price;
        this.isGet = isGet;
    }
}
