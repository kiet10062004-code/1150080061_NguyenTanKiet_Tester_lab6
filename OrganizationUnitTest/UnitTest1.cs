using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace OrganizationUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        string connStr = @"Data Source=.\SQLEXPRESS;
                           Initial Catalog=Organization_unit;
                           Integrated Security=True";

        private bool Validate(string unitId, string name)
        {
            if (string.IsNullOrWhiteSpace(unitId)) return false;
            if (string.IsNullOrWhiteSpace(name)) return false;
            return true;
        }

        private bool IsExist(string unitId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM OrganizationUnit WHERE UnitId=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", unitId);

                int c = (int)cmd.ExecuteScalar();
                return c > 0;
            }
        }

        private bool Save(string unitId, string name, string des)
        {
            if (!Validate(unitId, name))
                return false;

            if (IsExist(unitId))
                return false;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = @"INSERT INTO OrganizationUnit(UnitId,Name,Description)
                               VALUES(@id,@name,@des)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", unitId.Trim());
                cmd.Parameters.AddWithValue("@name", name.Trim());
                cmd.Parameters.AddWithValue("@des", des);

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        [TestMethod]
        public void TC01()
        {
            Assert.IsFalse(Validate("", "IT"));
        }

        [TestMethod]
        public void TC02()
        {
            Assert.IsFalse(Validate("U01", ""));
        }

        [TestMethod]
        public void TC03()
        {
            Assert.IsTrue(Validate("U01", "IT"));
        }

        [TestMethod]
        public void TC04()
        {
            Save("T01", "IT", "");
            Assert.IsTrue(IsExist("T01"));
        }

        [TestMethod]
        public void TC05()
        {
            Assert.IsFalse(IsExist("XXX999"));
        }

        [TestMethod]
        public void TC06()
        {
            bool kq = Save("N01", "Ke Toan", "abc");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC07()
        {
            Save("D01", "IT", "");
            bool kq = Save("D01", "IT", "");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void TC08()
        {
            bool kq = Save("X01", "", "");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void TC09()
        {
            bool kq = Save("", "IT", "");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void TC10()
        {
            string longName = new string('A', 200);
            bool kq = Save("L01", longName, "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC11()
        {
            bool kq = Save("L02", "IT", "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC12()
        {
            bool kq = Save("@@##", "IT", "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC13()
        {
            Save("CK01", "IT", "");
            Assert.IsTrue(IsExist("CK01"));
        }

        [TestMethod]
        public void TC14()
        {
            bool kq = Save("UNI01", "Phòng Nhân Sự", "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC15()
        {
            bool kq = Save("  TR01  ", "  IT  ", "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC16()
        {
            Save("TT01", "IT", "");
            bool kq = Save("TT01", "IT", "");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void TC17()
        {
            bool kq = Save("123", "456", "");
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC18()
        {
            string d = new string('B', 300);
            bool kq = Save("DES99", "IT", d);
            Assert.IsTrue(kq);
        }

        [TestMethod]
        public void TC19()
        {
            Save("EX01", "IT", "");
            Assert.IsTrue(IsExist("EX01"));
        }

        [TestMethod]
        public void TC20()
        {
            bool kq = Save("OK01", "IT", "Mô tả");
            Assert.IsTrue(kq);
        }
    }
}
