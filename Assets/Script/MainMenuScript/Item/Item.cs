using UnityEngine;
[CreateAssetMenu(fileName="New Item",menuName="Inventory/Item")]
public class Item : ScriptableObject {
	public int Price;
	new public string name = "New Item";
	public string EnglishName;
	public string tagalogName;
	public Sprite icon;
	public bool isDefaultItem = false;
	public virtual void Use(){
		//Debug.Log (name+" is Used!!!");
	}

}
