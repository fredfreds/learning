using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CleanCodingPrinciples
{
    public class CleanCodingPrinciples : MonoBehaviour
    {
        #region Assign Booleans Implicitly
        public bool WrongAssignBooleans()
        {
            int cash = 6;
            bool takeSomething = false;

            if(cash > 5)
            {
                takeSomething = true;
            }
            else
            {
                takeSomething = false;
            }

            return takeSomething;
        }

        public bool CorrectAssignBooleans()
        {
            int cash = 6;
            bool takeSomething = cash > 5;

            return takeSomething;
        }
        #endregion

        #region Use Positive conditionals
        public bool CorrectUsePositiveConditionals()
        {
            bool loggedIn = false;
            if (loggedIn)
                return true;
            else
                return false;
        }
        public bool WrongUsePositiveConditionals()
        {
            bool isNotLoggedIn = false;
            if (!isNotLoggedIn)
                return true;
            else
                return false;
        }
        #endregion

        // Ternary: 
        // int registrationFee = isSpeaker ? 0 : 50;

        // Be Strongly Typed: 
        // if(employee.type == EmployeeType.Customer)

        // Magic Numbers:
        // const int legalAge = 21;
        // if(age > legalAge) { }

        #region Encapsulate Complex Conditionals
        private bool ValidFile(string fileExtension, bool isAdmin, bool isActive)
        {
            List<string> validFileExtensions = new List<string>() { "png", "jpeg" };
            bool validFile = validFileExtensions.Contains(fileExtension);
            bool userCanViewFile = isAdmin || isActive;
            return validFile && userCanViewFile;
        }
        #endregion

        #region Polymorphism Over Switch
        public class WrongExamplePolymorphismOverSwitch
        {
            public class UserWrongExample
            {
                public enum Status
                {
                    Active,
                    InActive,
                    Blocked
                }

                public Status UserStatus;
            }
            public void LoginUser(UserWrongExample user)
            {
                switch (user.UserStatus)
                {
                    case UserWrongExample.Status.Active:
                        break;
                    case UserWrongExample.Status.InActive:
                        break;
                    case UserWrongExample.Status.Blocked:
                        break;
                    default:
                        break;
                }
            }
        }

        public class CorrectExamplePolymorphismOverSwitch
        {
            public abstract class UserCorrect
            {
                public int Status;

                public abstract void Login();
            }

            public class ActiveUser : UserCorrect
            {
                public override void Login()
                {
                }
            }

            public class InActiveUser : UserCorrect
            {
                public override void Login()
                {
                }
            }

            public class BlockedUser : UserCorrect
            {
                public override void Login()
                {
                }
            }

            public void LoginUser(UserCorrect user)
            {
                user.Login();
            }
        }
        #endregion

        // Be Declarative
        // return users
        //    .Where(u => u.AccountBalance < minBalance)
        //    .Where(u => u.Status == Status.Active);

        #region Avoid Flag Arguments
        public class WrongExampleAvoidFlagArguments
        {
            public void SaveUser(string user, string email)
            {

            }
        }

        public class CorrectExampleAvoidFlagArguments
        {
            public void SaveUser(string user)
            {

            }

            public void SaveEmail(string email)
            {

            }
        }
        #endregion

        #region Try / Catch Body Standalone
        public class WrongExampleTryCatch
        {
            public void Test()
            {
                try
                {
                    // many lines of code
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        public class CorrectExampleTryCatch
        {
            public void Save()
            {
                // many lines of code
            }

            public void Test()
            {
                try
                {
                    Save();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
        #endregion
    }
}