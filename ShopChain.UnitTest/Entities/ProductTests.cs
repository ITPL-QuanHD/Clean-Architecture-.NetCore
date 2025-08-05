namespace ShopChain.Core.Entities.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void GetDiscountedPrice_NoDiscount_ReturnsSellPrice()
        {
            // Arrange
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.None,
                DiscountValue = 0
            };

            // Act
            var result = product.GetDiscountedPrice();

            // Assert
            Assert.AreEqual(100_000, result);
        }

        [TestMethod()]
        public void GetDiscountedPrice_AmountDiscount_ReturnsReducedPrice()
        {
            // Arrange
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 20_000
            };

            // Act
            var result = product.GetDiscountedPrice();

            // Assert
            Assert.AreEqual(80_000, result);
        }

        [TestMethod()]
        public void GetDiscountedPrice_PercentDiscount_ReturnsReducedPrice()
        {
            // Arrange
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.Percent,
                DiscountValue = 10 // 10%
            };

            // Act
            var result = product.GetDiscountedPrice();

            // Assert
            Assert.AreEqual(90_000, result);
        }

        [TestMethod()]
        public void GetDiscountedPrice_DiscountMoreThanSellPrice_ReturnsZero()
        {
            // Arrange
            var product = new Product
            {
                SellPrice = 50_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 60_000
            };

            // Act
            var result = product.GetDiscountedPrice();

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void GetAccumulatePoints_Disabled_ReturnsZero()
        {
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.None,
                IsPointAccumulateEnabled = false,
                PointAccumulateRate = 5
            };

            int result = product.GetAccumulatePoints();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAccumulatePoints_UseProductRate()
        {
            var product = new Product
            {
                SellPrice = 200_000,
                DiscountType = DiscountType.None,
                IsPointAccumulateEnabled = true,
                PointAccumulateRate = 5 // 5%
            };

            int result = product.GetAccumulatePoints();
            Assert.AreEqual(10_000, result); // 200_000 * 0.05 = 10,000
        }

        [TestMethod]
        public void GetAccumulatePoints_UseDefaultRate()
        {
            var product = new Product
            {
                SellPrice = 300_000,
                DiscountType = DiscountType.None,
                IsPointAccumulateEnabled = true,
                PointAccumulateRate = null // => lấy default
            };

            int result = product.GetAccumulatePoints(2); // defaultRate = 2%
            Assert.AreEqual(6_000, result); // 300_000 * 0.02 = 6,000
        }

        [TestMethod]
        public void GetAccumulatePoints_WithDiscount()
        {
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.Percent,
                DiscountValue = 10, // Giảm 10%
                IsPointAccumulateEnabled = true,
                PointAccumulateRate = 10
            };

            int result = product.GetAccumulatePoints();
            Assert.AreEqual(9_000, result); // (100,000 * 0.9) * 0.10 = 9,000
        }

        [TestMethod]
        public void GetAccumulatePoints_RoundDown()
        {
            var product = new Product
            {
                SellPrice = 105_000,
                DiscountType = DiscountType.None,
                IsPointAccumulateEnabled = true,
                PointAccumulateRate = 3 // 3%
            };

            int result = product.GetAccumulatePoints();
            Assert.AreEqual(3_150, result); // 105,000 * 0.03 = 3,150 (không làm tròn lên)
        }

        [TestMethod]
        public void GetFinalPrice_TaxExcluded_AddsTax()
        {
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Excluded,
                TaxRate = 10 // 10%
            };

            var result = product.GetFinalPrice();

            Assert.AreEqual(110_000m, result);
        }

        [TestMethod]
        public void GetFinalPrice_TaxExcluded_WithDiscount_AddsTaxToDiscounted()
        {
            var product = new Product
            {
                SellPrice = 200_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 20_000,
                TaxType = TaxType.Excluded,
                TaxRate = 5
            };

            var result = product.GetFinalPrice();

            // 200,000 - 20,000 = 180,000; 180,000 * 1.05 = 189,000
            Assert.AreEqual(189_000m, result);
        }

        [TestMethod]
        public void GetFinalPrice_TaxIncluded_ReturnsDiscountedPrice()
        {
            var product = new Product
            {
                SellPrice = 120_000,
                DiscountType = DiscountType.Percent,
                DiscountValue = 20, // giảm 20%
                TaxType = TaxType.Included,
                TaxRate = 8
            };

            var result = product.GetFinalPrice();

            // 120,000 * 0.8 = 96,000 (đã bao gồm thuế)
            Assert.AreEqual(96_000m, result);
        }

        [TestMethod]
        public void GetFinalPrice_TaxNone_ReturnsDiscountedPrice()
        {
            var product = new Product
            {
                SellPrice = 80_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 30_000,
                TaxType = TaxType.None,
                TaxRate = 20 // vẫn bỏ qua thuế
            };

            var result = product.GetFinalPrice();

            // 80,000 - 30,000 = 50,000
            Assert.AreEqual(50_000m, result);
        }

        [TestMethod]
        public void GetFinalPrice_TaxRateZero_ReturnsDiscountedPrice()
        {
            var product = new Product
            {
                SellPrice = 70_000,
                DiscountType = DiscountType.Percent,
                DiscountValue = 10, // giảm 10%
                TaxType = TaxType.Excluded,
                TaxRate = 0
            };

            var result = product.GetFinalPrice();

            // 70,000 * 0.9 = 63,000, cộng 0% thuế vẫn là 63,000
            Assert.AreEqual(63_000m, result);
        }

        [TestMethod]
        public void GetProfit_BasicCase()
        {
            var product = new Product
            {
                SellPrice = 200_000,
                OriginalPrice = 100_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Included,
                TaxRate = 0
            };

            var profit = product.GetProfit();

            // Lợi nhuận = Giá bán - Giá gốc
            Assert.AreEqual(100_000m, profit);
        }

        [TestMethod]
        public void GetProfit_WithDiscount()
        {
            var product = new Product
            {
                SellPrice = 100_000,
                OriginalPrice = 80_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 20_000,
                TaxType = TaxType.None
            };

            var profit = product.GetProfit();

            // Giá thực nhận: 100,000 - 20,000 = 80,000; lợi nhuận: 0
            Assert.AreEqual(0m, profit);
        }

        [TestMethod]
        public void GetProfit_TaxExcluded()
        {
            var product = new Product
            {
                SellPrice = 150_000,
                OriginalPrice = 100_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Excluded,
                TaxRate = 10
            };

            var profit = product.GetProfit();

            // Giá sau thuế: 150,000 * 1.1 = 165,000; lợi nhuận = 65,000
            Assert.AreEqual(65_000m, profit);
        }

        [TestMethod]
        public void GetProfit_OriginalPriceNull()
        {
            var product = new Product
            {
                SellPrice = 60_000,
                OriginalPrice = null,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Included
            };

            var profit = product.GetProfit();

            // Giá gốc = 0 mặc định
            Assert.AreEqual(60_000m, profit);
        }

        [TestMethod]
        public void GetProfit_NegativeProfit()
        {
            var product = new Product
            {
                SellPrice = 50_000,
                OriginalPrice = 80_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.None
            };

            var profit = product.GetProfit();

            // Lợi nhuận âm
            Assert.AreEqual(-30_000m, profit);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxExcluded()
        {
            var product = new Product
            {
                SellPrice = 100_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Excluded,
                TaxRate = 10
            };

            var tax = product.GetActualTaxAmount();

            // Giá chưa thuế, thuế = 100,000 * 10% = 10,000
            Assert.AreEqual(10_000m, tax);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxExcluded_WithDiscount()
        {
            var product = new Product
            {
                SellPrice = 200_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 20_000,
                TaxType = TaxType.Excluded,
                TaxRate = 5
            };

            var tax = product.GetActualTaxAmount();

            // (200,000 - 20,000) * 5% = 180,000 * 5% = 9,000
            Assert.AreEqual(9_000m, tax);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxIncluded()
        {
            var product = new Product
            {
                SellPrice = 110_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Included,
                TaxRate = 10
            };

            var tax = product.GetActualTaxAmount();

            // Giá đã có thuế, tách ra: 110,000 * 10 / 110 = 10,000
            Assert.AreEqual(10_000m, tax);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxIncluded_WithDiscount()
        {
            var product = new Product
            {
                SellPrice = 220_000,
                DiscountType = DiscountType.Percent,
                DiscountValue = 10, // giảm 10%
                TaxType = TaxType.Included,
                TaxRate = 10
            };

            var tax = product.GetActualTaxAmount();

            // (220,000 * 0.9) * 10 / 110 = 198,000 * 10 / 110 = 18,000
            Assert.AreEqual(18_000m, tax);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxNone_ReturnsZero()
        {
            var product = new Product
            {
                SellPrice = 150_000,
                DiscountType = DiscountType.Amount,
                DiscountValue = 50_000,
                TaxType = TaxType.None,
                TaxRate = 20
            };

            var tax = product.GetActualTaxAmount();

            Assert.AreEqual(0m, tax);
        }

        [TestMethod]
        public void GetActualTaxAmount_TaxRateNull_ReturnsZeroOrTachRaDung()
        {
            var product = new Product
            {
                SellPrice = 150_000,
                DiscountType = DiscountType.None,
                TaxType = TaxType.Excluded,
                TaxRate = null
            };

            var tax = product.GetActualTaxAmount();

            // Không có TaxRate, phải trả về 0
            Assert.AreEqual(0m, tax);
        }
    }
}
