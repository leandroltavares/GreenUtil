using GreenUtil.Test.Dummy;
using GreenUtil.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace GreenUtil.Test.Web
{
    [TestClass]
    [TestCategory("external")]
    public class WebUtilTest
    {
        [TestMethod]
        public void WhenGETWebRequestWithComplexTypeThenShouldReturnFilledJSON()
        {
            //Arrange
            string url = "https://reqres.in/api/users?page=2";

            //Act
            var instance = WebUtil.Get<JsonListUser>(url);

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(JsonListUser));
            Assert.IsNotNull(instance.Total);
            Assert.AreNotEqual(string.Empty, instance.Total);

            Assert.IsNotNull(instance.total_pages);
            Assert.AreNotEqual(string.Empty, instance.total_pages);
            
            Assert.AreNotEqual(0, instance.Users);
        }

        [TestMethod]
        public void WhenGETWebRequestWithComplexAndHeadersTypeThenShouldReturnFilledJSON()
        {
            //Arrange
            string url = "https://reqres.in/api/users?page=2";
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.Accept, "application/json");

            //Act
            var instance = WebUtil.Get<JsonListUser>(url, headers);

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(JsonListUser));
            Assert.IsNotNull(instance.Total);
            Assert.AreNotEqual(string.Empty, instance.Total);

            Assert.IsNotNull(instance.total_pages);
            Assert.AreNotEqual(string.Empty, instance.total_pages);

            Assert.AreNotEqual(0, instance.Users);
        }


        [TestMethod]
        public void WhenGETWebRequestWithDynamicThenShouldReturnFilledJSON()
        {
            //Arrange
            string url = "https://reqres.in/api/users?page=2";

            //Act
            var instance = WebUtil.Get<dynamic>(url);

            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void WhenGETUrlIsNullThenShouldThrowException()
        {
            //Arrange
            Assert.ThrowsException<ArgumentNullException>(() => WebUtil.Get<dynamic>(null));
        }

        [TestMethod]
        public void WhenPOSTWebRequestWithComplexTypeThenShouldReturnFilledJSON()
        {
            //Arrange
            string url = "https://reqres.in/api/users";
            string postData = $"{{\"name\": \"test_name{Guid.NewGuid()}\",\"job\": \"leader\"}}";

            //Act
            var instance = WebUtil.Post<dynamic>(url, postData);

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.ToString().Contains("createdAt"));
        }

        [TestMethod]
        public void WhenPOSTWebRequestWithComplexAndHeadersTypeThenShouldReturnFilledJSON()
        {
            //Arrange
            string url = "https://reqres.in/api/users";
            string postData = $"{{\"name\": \"test_name{Guid.NewGuid()}\",\"job\": \"leader\"}}";
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.Accept, "application/json");

            //Act
            var instance = WebUtil.Post<dynamic>(url, postData, headers);

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.ToString().Contains("createdAt"));
        }

        [TestMethod]
        public void WhenPOSTUrlIsNullThenShouldThrowException()
        {
            //Arrange
            Assert.ThrowsException<ArgumentNullException>(() => WebUtil.Post<dynamic>(null, string.Empty));
        }
    }
}
