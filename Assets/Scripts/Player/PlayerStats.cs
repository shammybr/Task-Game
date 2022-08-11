using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Money;
    public GameObject EquippedHat;
    public GameObject ItemDB;
    public PlayerHUD PlayerHUD;
    public List<AudioClip> AudioClips;

    AnimatorOverrideController _animatorOverrideController;
    Animator _hatAnimator;
    ItemData.EItemID _equippedHat;

    AudioSource _audioData;
    private void Awake()  {

    }

    // Start is called before the first frame update
    void Start()
    {
        _hatAnimator = EquippedHat.GetComponent<Animator>();
        _animatorOverrideController = new AnimatorOverrideController();
        _animatorOverrideController.runtimeAnimatorController = _hatAnimator.runtimeAnimatorController;
        _hatAnimator.runtimeAnimatorController = _animatorOverrideController;
        _hatAnimator.runtimeAnimatorController.name = "OverrideController";
        _audioData = GetComponent<AudioSource>();
        PlayerHUD.UpdateMoney(Money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public ItemData.EItemID GetEquippedHat()
    {
        return _equippedHat;
    }

    public void EquipHat(ItemData.EItemID HatID)  {

        _equippedHat = HatID;

        List<AnimationClip> _hatAnimations;

        _hatAnimations = ItemDB.GetComponent<ItemDatabase>().GetItemAnimation(HatID);

        //equip new hat animations
        _animatorOverrideController["HatIdleDown"] = _hatAnimations[0];
        _animatorOverrideController["HatIdleLeft"] = _hatAnimations[1];
        _animatorOverrideController["HatIdleRight"] = _hatAnimations[2];
        _animatorOverrideController["HatIdleUp"] = _hatAnimations[3];
        _animatorOverrideController["HatRunDown"] = _hatAnimations[4];
        _animatorOverrideController["HatRunLeft"] = _hatAnimations[5];
        _animatorOverrideController["HatRunRight"] = _hatAnimations[6];
        _animatorOverrideController["HatRunUp"] = _hatAnimations[7];

        _audioData.PlayOneShot(AudioClips[0]);
    }



    public void UnequipHat() {
        _equippedHat = ItemData.EItemID.NoHat;

        List<AnimationClip> _hatAnimations = ItemDB.GetComponent<ItemDatabase>().GetItemAnimation(0);

        //equip empty animations
        _animatorOverrideController["HatIdleDown"] = _hatAnimations[0];
        _animatorOverrideController["HatIdleLeft"] = _hatAnimations[1];
        _animatorOverrideController["HatIdleRight"] = _hatAnimations[2];
        _animatorOverrideController["HatIdleUp"] = _hatAnimations[3];
        _animatorOverrideController["HatRunDown"] = _hatAnimations[4];
        _animatorOverrideController["HatRunLeft"] = _hatAnimations[5];
        _animatorOverrideController["HatRunRight"] = _hatAnimations[6];
        _animatorOverrideController["HatRunUp"] = _hatAnimations[7];
        //refresh sprite
        EquippedHat.GetComponent<SpriteRenderer>().sprite = null;
        _audioData.PlayOneShot(AudioClips[0]);
    }

    public void AddMoney(int Amount)  {
        Money += Amount;
        PlayerHUD.UpdateMoney(Money);
    }

    public void SubtractMoney(int Amount)  {
        Money -= Amount;
        PlayerHUD.UpdateMoney(Money);
    }
}
