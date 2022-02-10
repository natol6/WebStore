using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_returns_View()
        {
            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
               .Returns<ProductFilter>(f => new ProductsPage(Enumerable.Empty<Product>(), 0));

            var controller = new HomeController();

            var result = controller.Index(product_data_mock.Object);

            Assert.IsType<ViewResult>(result);
        }
        
        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException()
        {
            const string exception_message = "Message";

            var controller = new HomeController();

            controller.Throw(exception_message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Message()
        {
            const string exception_message = "Message";

            var controller = new HomeController();

            Exception? exception = null;
            try
            {
                controller.Throw(exception_message);
            }
            catch (Exception e)
            {
                exception = e;
            }

            var application_exception = Assert.IsType<ApplicationException>(exception);

            var actual_exception_message = application_exception.Message;

            Assert.Equal(exception_message, actual_exception_message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Message2()
        {
            const string exception_message = "Message";

            var controller = new HomeController();

            var application_exception = Assert.Throws<ApplicationException>(() => controller.Throw(exception_message));

            var actual_exception_message = application_exception.Message;

            Assert.Equal(exception_message, actual_exception_message);
        }

        [TestMethod]
        public void Status_with_id_404_Returns_RedirectToAction_Error404()
        {
            const string status_404 = "404";
            const string expected_action_name = nameof(HomeController.Error404);

            var controller = new HomeController();

            var result = controller.Status(status_404);

            var redirect_action_result = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirect_action_result.ControllerName);

            var actual_action_name = redirect_action_result.ActionName;
            Assert.Equal(expected_action_name, actual_action_name);
        }
    }
}
