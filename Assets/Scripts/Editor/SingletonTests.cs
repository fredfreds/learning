﻿using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Singleton;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SingletonTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void IsDatabaseSingleton()
        {
            Database db = Database.Instance;
            Database db1 = Database.Instance;
            Assert.That(db, Is.SameAs(db1));
            Assert.That(Database.Count, Is.EqualTo(1));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SingletonTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
