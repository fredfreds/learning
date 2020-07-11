using System.Collections;
using System.Collections.Generic;
using DesignPatternsNew.Tanks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TankTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TankTestSimplePasses()
        {
            FuelTank fuelTank = new FuelTank();
            fuelTank.Position = new Vector3(0, 0, 0);
            fuelTank.Velocity = new Vector3(0, 1, 0);
            fuelTank.Rotation = new Vector3(0, 10, 0);
            fuelTank.Fuel = 1;
            fuelTank.SetPosition(fuelTank.GetPosition() + fuelTank.GetVelocity());
            Assert.AreEqual(fuelTank.Position, fuelTank.Velocity);
            Commands commands = new Commands(fuelTank);
            commands.Execute();
            Assert.IsTrue(fuelTank.CanMove);
            Assert.AreEqual(fuelTank.Position, new Vector3(0, 2, 0));
            Assert.AreEqual(fuelTank.Rotation, new Vector3(0, 10, 0));
        }

        [Test]
        public void TankTestSimplePasses2()
        {
            FuelTank fuelTank = new FuelTank();
            fuelTank.Position = new Vector3(0, 0, 0);
            fuelTank.Velocity = new Vector3(0, 1, 0);
            fuelTank.Rotation = new Vector3(0, 10, 0);
            fuelTank.Fuel = 1;
            Commands commands = new Commands(fuelTank);
            commands.Execute();
            Assert.IsTrue(fuelTank.CanMove);
            Assert.AreEqual(fuelTank.Position, new Vector3(0, 1, 0));
            Assert.AreEqual(fuelTank.Rotation, new Vector3(0, 10, 0));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TankTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
