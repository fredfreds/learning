using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Builder
{
    public class Weapon
    {
        public string Name;
        public int Damage;
        public int Weight;
        public bool HasShield;

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name}, {nameof(Damage)}:{Damage}, " +
                $"{nameof(Weight)}:{Weight}, {nameof(HasShield)}:{HasShield}";
        }
    }

    public class WeaponBuilder
    {
        protected Weapon weapon = new Weapon();
        public WeaponParamsBuilder WeaponParameters => new WeaponParamsBuilder(weapon);
        public WeaponInfoBuilder WeaponInformations => new WeaponInfoBuilder(weapon);

        public static implicit operator Weapon(WeaponBuilder b)
        {
            return b.weapon;
        }
    }

    public class WeaponParamsBuilder : WeaponBuilder
    {
        public WeaponParamsBuilder(Weapon w)
        {
            this.weapon = w;
        }

        public WeaponParamsBuilder SetDamage(int d)
        {
            weapon.Damage = d;
            return this;
        }

        public WeaponParamsBuilder SetShield(bool s)
        {
            weapon.HasShield = s;
            return this;
        }

        public WeaponParamsBuilder SetWeight(int w)
        {
            weapon.Weight = w;
            return this;
        }
    }

    public class WeaponInfoBuilder : WeaponBuilder
    {
        public WeaponInfoBuilder(Weapon w)
        {
            this.weapon = w;
        }

        public WeaponInfoBuilder SetName(string n)
        {
            weapon.Name = n;
            return this;
        }
    }

    public class Builder : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        WeaponBuilder weaponBuilder = new WeaponBuilder();
        Weapon weapon;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            weapon = weaponBuilder
                .WeaponInformations.SetName("ATG 45")
                .WeaponParameters.SetDamage(14)
                                 .SetShield(true)
                                 .SetWeight(4);

            InfoText.text = weapon.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}