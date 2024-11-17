using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Equipment",menuName="Inventory/Equipment")]
public class ItemEquipment : Item {
	public EquipmentSlot EquipSlot;
	public EquipmentRarity EquipRarity;
	public int DamageModifier;
	public int ArmorModifier;
	public int StrengthModifier;
	public string EqupInfo;
	public string TagalogEqupInfo;
	public bool VisibleToCharacter;
	public int CharacterIndex;
	public SkinnedMeshRenderer mesh;

	public int equipLevel;
	public override void Use(){


		base.Use ();
	
		EquipmentManager.Instance.Equip (this);
		InventoryRemove ();
	}

	public void InventoryRemove(){
		Inventory.Instance.Remove (this);


	}
	public enum EquipmentRarity{common,uncommon,rare,veryrare,epic,legendary}
}
public enum EquipmentSlot{head,body,arms,feet,weapon,shield}

