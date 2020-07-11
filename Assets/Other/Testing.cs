using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class A
    {
        public virtual void Foo()
        {
            Debug.Log("A");
        }
    }

    public class B : A
    {
        public override void Foo()
        {
            Debug.Log("B");
        }
    }
    public struct S : IDisposable
    {
        private bool dispose;
        public void Dispose()
        {
            dispose = true;
        }
        public bool GetDispose()
        {
            return dispose;
        }
    }

    public class T
    {
        public readonly int A;
        public T(int a)
        {
            A = a;
        }
    }

    public class St
    {
        static St()
        {
            Debug.Log("one");
        }
    }

    public class Testing : MonoBehaviour
    {
        void Start()
        {
            A obj1 = new B();
            obj1.Foo();

            S s = new S();
            using (s)
            {
                Debug.Log(s.GetDispose());
            }

            Debug.Log(s.GetDispose());

            List<Action> actions = new List<Action>();
            for (var count = 0; count < 10; count++)
            {
                actions.Add(() => Debug.Log(count));
            }
            foreach (var action in actions)
            {
                action();
            }

            int i = 1;
            object obj = i;
            ++i;
            Debug.Log(i);
            Debug.Log(obj);
            //Debug.Log((short)obj);

            var s1 = string.Format("{0}{1}", "abc", "cba");
            var s2 = "abc" + "cba";
            var s3 = "abccba";

            Debug.Log(s1 == s2);
            Debug.Log((object)s1 == (object)s2);
            Debug.Log(s2 == s3);
            Debug.Log((object)s2 == (object)s3);

            try
            {
                var array = new int[] { 1, 2 };
                Debug.Log(array[5]);
            }
            catch (ApplicationException e)
            {
                Debug.Log(1);
            }
            catch (SystemException e)
            {
                Debug.Log(2);
            }
            catch (Exception e)
            {
                Debug.Log(3);
            }

            T t = new T(3546);
            Debug.Log(t.A);

            St st = new St();
            St st2 = new St();
        }
    }
}