using System;
using System.Transactions;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    // Any changes we make to the data in the DB will be automatically rolled back
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }

        // This attribute can only be applied to test methods
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}
