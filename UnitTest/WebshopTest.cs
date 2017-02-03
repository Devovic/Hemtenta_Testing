using Hemtenta_Antonio_Mirkovic.webshop;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    //Metoderna som måste testats är AddProduct,RemoveProduct,Checkout. Properties man testar är _amount, TotalCost och Price.
    //Exceptions ska kastas tex när man inte har tillräckligt med pengar för checkout eller när man försöker ta bort mer amount än vad man har. Eller priset är under 0.
    //Ibasket har domänerna heltal största och minsta tillåtna värdet för int, Product har null eller ett object. Decimal har samma som int.
    //Iwebshop har Null eller ett objekt.
    public class WebshopTest
    {
        IBasket iBasket;
        Mock<IBilling> IBillMock;

        public WebshopTest()
        {
            iBasket = new Basket();
            IBillMock = new Mock<IBilling>();
        }

        [Fact]
        public void AddProduct_Success()
        {
            Product item = new Product() { Price = 20 };
            iBasket.AddProduct(item, 5);
            Assert.Equal(100, iBasket.TotalCost);
        }

        [Fact]
        public void AddProduct_PriceIs_0_Throw()
        {
            Product item = new Product() { Price = 0 };
            Assert.Throws<Exception>(() => iBasket.AddProduct(item, 5));
        }

        [Fact]
        public void AddProduct_AmountIs_0_Throw()
        {
            Product item = new Product() { Price = 20 };
            Assert.Throws<Exception>(() => iBasket.AddProduct(item, 0));
        }

        [Fact]
        public void RemoveProduct_Success()
        {     
                  
            Basket basket = new Basket();
            Product item = new Product() { Price = 20 };
            basket.AddProduct(item, 15);


            basket.RemoveProduct(item, 5);

            Assert.Equal(10, basket._amount);
        }

        [Fact]
        public void RemoveProduct_TooHigh_Amount_Throw()
        {

            Basket basket = new Basket();
            Product item = new Product() { Price = 20 };
            basket.AddProduct(item, 15);

            Assert.Throws<Exception>(() => basket.RemoveProduct(item, 20));
        }

        [Fact]
        public void RemoveProduct_Under_0_Throw()
        {

            Basket basket = new Basket();
            Product item = new Product() { Price = 20 };
            basket.AddProduct(item, 15);

            Assert.Throws<Exception>(() => basket.RemoveProduct(item, -20));
        }

        [Fact]
        public void Checkout_Success()
        {
            Product item = new Product() { Price = 20 };
            Webshop wh = new Webshop();

            IBillMock.SetupGet(x => x.Balance).Returns(100);
            iBasket.AddProduct(item, 5);
            wh.SetBasket(iBasket);

            var mockk  = IBillMock.Object;

            wh.Checkout(mockk);

            IBillMock.Verify(x => x.Pay(wh._Totalcost), Times.Once());
        }

        [Fact]
        public void Checkout_IBillingIs_Null_Throw()
        {
            Webshop wh = new Webshop();
            Assert.Throws<Exception>(() => wh.Checkout(null));
        }
    }
}
