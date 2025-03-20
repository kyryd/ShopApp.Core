using ShopApp.Core.BLoC;
using ShopApp.Core.BLoC.Converters;
using ShopApp.Core.Enums;
using ShopApp.Core.Models;
using ShopApp.Core.Models.Abstract;
using ShopApp.Core.Models.Core;
using ShopApp.Core.Models.Discounts;
using System.Collections.ObjectModel;

namespace ShopApp.Core.Tests.DataSources
{
    internal static class TestData
    {
        internal enum ProductCategoryData
        {
            FreshProduce,
            DairyProducts,
            BakeryBread,
            MeatSeafood,
            FrozenFoods,
            PantryStaples,
            SnacksSweets,
            Beverages,
            HealthSpecialtyFoods,
            InternationalEthnicFoods,
            BabyKidsFood,
            PetFood
        }

        internal static readonly HashSet<ICurrency> CurrenciesList = new(
            [
                new Currency(Currencies.PLN),
                new Currency(Currencies.USD),
                new Currency(Currencies.EUR),
                new Currency(Currencies.UAH),
            ]
        );


        internal static readonly IDictionary<(Currencies from, Currencies to), decimal> ExchangeRates = new Dictionary<(Currencies, Currencies), decimal>
        {
            { (Currencies.USD, Currencies.EUR), 0.85m },
            { (Currencies.EUR, Currencies.USD), 1.18m },
            { (Currencies.USD, Currencies.PLN), 3.8m },
            { (Currencies.PLN, Currencies.USD), 0.26m },
            { (Currencies.EUR, Currencies.PLN), 4.5m },
            { (Currencies.PLN, Currencies.EUR), 0.22m },
            { (Currencies.USD, Currencies.UAH), 27.0m },
            { (Currencies.UAH, Currencies.USD), 0.037m },
            { (Currencies.EUR, Currencies.UAH), 31.8m },
            { (Currencies.UAH, Currencies.EUR), 0.031m },
            { (Currencies.PLN, Currencies.UAH), 7.1m },
            { (Currencies.UAH, Currencies.PLN), 0.14m }
        };

        internal static CurrencyConverter CurrencyConverter = new CurrencyConverter(ExchangeRates);


        internal static readonly IDictionary<ProductCategoryData, ICategory> ProductCategories = new Dictionary<ProductCategoryData, ICategory>
        {
            { ProductCategoryData.FreshProduce, new Category(nameof(ProductCategoryData.FreshProduce), "Fruits, vegetables, herbs, and organic options.") },
            { ProductCategoryData.DairyProducts, new Category(nameof(ProductCategoryData.DairyProducts), "Milk, cheese, butter, yogurt, and plant-based alternatives.") },
            { ProductCategoryData.BakeryBread, new Category(nameof(ProductCategoryData.BakeryBread), "Bread, rolls, bagels, pastries, and cakes.") },
            { ProductCategoryData.MeatSeafood, new Category(nameof(ProductCategoryData.MeatSeafood), "Fresh meat, poultry, fish, shellfish, and deli items.") },
            { ProductCategoryData.FrozenFoods, new Category(nameof(ProductCategoryData.FrozenFoods), "Frozen meals, vegetables, fruits, and desserts.") },
            { ProductCategoryData.PantryStaples, new Category(nameof(ProductCategoryData.PantryStaples), "Grains, pasta, rice, canned goods, sauces, and oils.") },
            { ProductCategoryData.SnacksSweets, new Category(nameof(ProductCategoryData.SnacksSweets), "Chips, crackers, candy, chocolate, and cookies.") },
            { ProductCategoryData.Beverages, new Category(nameof(ProductCategoryData.Beverages), "Coffee, tea, juices, sodas, water, and alcohol(if applicable).") },
            { ProductCategoryData.HealthSpecialtyFoods, new Category(nameof(ProductCategoryData.HealthSpecialtyFoods), "Gluten-free, vegan, keto, and low-sugar products.") },
            { ProductCategoryData.InternationalEthnicFoods, new Category(nameof(ProductCategoryData.InternationalEthnicFoods), "Ingredients and ready-made dishes from global cuisines.") },
            { ProductCategoryData.BabyKidsFood, new Category(nameof(ProductCategoryData.BabyKidsFood), "Infant formula, baby food, and snacks for children.") },
            { ProductCategoryData.PetFood, new Category(nameof(ProductCategoryData.PetFood), "Cat, dog, and other pet-friendly treats.") }
        };

        internal static readonly HashSet<CategoryDiscount> CategoryDiscounts =
        [
            new CategoryDiscount(DiscountType.ForVip, new DateOnly(2025, 1, 1), new DateOnly(2025, 12, 31), new DecimalValue(10, ValueCategory.percentage)),
            new CategoryDiscount(DiscountType.OnTotalPrice, new DateOnly(2025, 2, 1), new DateOnly(2025, 11, 30), new DecimalValue(15,ValueCategory.percentage)),
            new CategoryDiscount(DiscountType.OnTripleProduct, new DateOnly(2025, 3, 1), new DateOnly(2025, 10, 31), new DecimalValue(20, ValueCategory.percentage)),
            new CategoryDiscount(DiscountType.OnDoubleProduct, new DateOnly(2025, 4, 1), new DateOnly(2025, 9, 30), new DecimalValue(25, ValueCategory.percentage)),
            new CategoryDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(30, ValueCategory.percentage))
        ];

        internal static readonly HashSet<ProductDiscount> ProductDiscounts =
        [
            new ProductDiscount(DiscountType.ForVip, new DateOnly(2025, 1, 1), new DateOnly(2025, 12, 31), new DecimalValue(5, ValueCategory.percentage)),
            new ProductDiscount(DiscountType.OnTotalPrice, new DateOnly(2025, 2, 1), new DateOnly(2025, 11, 30), new DecimalValue(10, ValueCategory.percentage)),
            new ProductDiscount(DiscountType.OnTripleProduct, new DateOnly(2025, 3, 1), new DateOnly(2025, 10, 31), new DecimalValue(15, ValueCategory.percentage)),
            new ProductDiscount(DiscountType.OnDoubleProduct, new DateOnly(2025, 4, 1), new DateOnly(2025, 9, 30), new DecimalValue(20, ValueCategory.percentage)),
            new ProductDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 5, 1), new DateOnly(2025, 8, 31), new DecimalValue(25, ValueCategory.percentage))
        ];

        internal static readonly HashSet<OrderDiscount> OrderDiscounts = new()
        {
            new OrderDiscount(DiscountType.ForVip, new DateOnly(2025, 1, 1), new DateOnly(2025, 12, 31), new DecimalValue(7, ValueCategory.percentage)),
            new OrderDiscount(DiscountType.OnTotalPrice, new DateOnly(2025, 2, 1), new DateOnly(2025, 11, 30), new DecimalValue(3, ValueCategory.percentage)),
            new OrderDiscount(DiscountType.OnTripleProduct, new DateOnly(2025, 3, 1), new DateOnly(2025, 10, 31), new DecimalValue(40, ValueCategory.percentage)),
            new OrderDiscount(DiscountType.OnDoubleProduct, new DateOnly(2025, 4, 1), new DateOnly(2025, 9, 30), new DecimalValue(20, ValueCategory.percentage)),
            new OrderDiscount(DiscountType.WhenHappyHour, new DateOnly(2025, 1, 1), new DateOnly(2025, 8, 31), new DecimalValue(15, ValueCategory.percentage)),
            new OrderDiscount(DiscountType.WhenHappyHour, new DateOnly(2023, 1, 1), new DateOnly(2023, 8, 31), new DecimalValue(10, ValueCategory.percentage))
        };

        internal static readonly ReadOnlyCollection<IProduct> Products = new(
            [
                new Product(
                    "Apple",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 1.2m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.FreshProduce],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Banana",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 0.5m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.FreshProduce],
                    CategoryDiscounts.Skip(2).Take(2).ToList(),
                    ProductDiscounts.Skip(2).Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Milk",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 1.5m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.DairyProducts],
                    CategoryDiscounts.Skip(3).Take(1).ToList(),
                    ProductDiscounts.Skip(3).Take(1).ToList(),
                    Units.Litre
                ),
                new Product(
                    "Cheese",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 2.5m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.DairyProducts],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Bread",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 2.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.BakeryBread],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Cake",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 15.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.BakeryBread],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Chicken",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 5.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.MeatSeafood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Salmon",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 10.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.MeatSeafood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Frozen Pizza",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 4.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.FrozenFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Frozen Vegetables",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 3.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.FrozenFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Pasta",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 1.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.PantryStaples],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Rice",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 1.2m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.PantryStaples],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Chips",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 1.5m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.SnacksSweets],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Chocolate",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 2.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.SnacksSweets],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Coffee",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 3.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.Beverages],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Tea",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 2.5m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.Beverages],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Gluten-Free Bread",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 3.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.HealthSpecialtyFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Vegan Cheese",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 4.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.HealthSpecialtyFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Sushi",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 8.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.InternationalEthnicFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Tacos",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 6.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.InternationalEthnicFoods],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Baby Formula",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 20.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.BabyKidsFood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Baby Snacks",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 5.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.BabyKidsFood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Piece
                ),
                new Product(
                    "Dog Food",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 10.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.PetFood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                ),
                new Product(
                    "Cat Food",
                    new Price(CurrenciesList.First(c => c.Value == Currencies.USD), 8.0m, CurrencyConverter),
                    ProductCategories[ProductCategoryData.PetFood],
                    CategoryDiscounts.Take(2).ToList(),
                    ProductDiscounts.Take(2).ToList(),
                    Units.Kg
                )
            ]
        );


        internal static readonly ReadOnlyCollection<IOrder> Orders = new(
            [
                new Order
                (
                    DateTime.Now,
                    Products.First(),
                    [..OrderDiscounts.Skip(0).Take(2)],
                    2
                ),
                new Order
                (
                    DateTime.Now.AddDays(-1),
                    Products.Skip(1).First(),
                    [..OrderDiscounts.Skip(0).Take(5)],
                    3
                ),
                new Order
                (
                    DateTime.Now.AddDays(-2),
                    Products.Skip(2).First(),
                    [OrderDiscounts.Skip(2).First()],
                    1                   
                )
            ]
        );




    }
}
