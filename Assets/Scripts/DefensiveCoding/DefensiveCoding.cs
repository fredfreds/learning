using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefensiveCoding
{
    public class DefensiveCoding : MonoBehaviour
    {
        public void CalculateSomething(int cost, int price)
        {

        }

        public void TestTryCatch()
        {
            try
            {
                CalculateSomething(1, 2);
            }
            catch (ValidationException ex) when (ex.ParamName == "cost")
            {

                throw;
            }
        }

        public void TestThrow()
        {
            throw new ValidationException("Not Found");
        }
    }

    public class ValidationException : ArgumentException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, string paramName):base(message, paramName) { }
        public ValidationException(string message, Exception inner) : base(message, inner) { }
        protected ValidationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}