using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseWork;

namespace CourseWorkTests
{
    [TestClass]
    public class UnitTest1
    {
        private static SqlConnection _conn;
        private SqlDataAdapter _adapt;
        private readonly String _userId;
        private SqlCommandBuilder _scbuild;
        private string _testID = "41162d1e-1bca-4f46-b621-d3907a98be6a              ";

        /////////////////////////////////////////////////////////////////////
        [TestInitialize]
        public void PrepareConnection()
        {
            _conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=KurstTest;Trusted_Connection=True;");
            _conn.Open();
        }

        [TestMethod]
        public void oneYearCalcTest()
        {
            int test = Funcs.OneYearPlan(_testID,_conn);
            Assert.AreEqual(test, 2600);
        }

        /////////////////////////////////////////////////////////////////////
        [ClassCleanup]
        static public void AfterTests()
        {
            _conn.Close();
        }
    }
}
