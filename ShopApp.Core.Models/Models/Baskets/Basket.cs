// See https://aka.ms/new-console-template for more information
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;

public record Basket(IClient Consumer, IList<IBasketItem> Items) : Entity, IBasket;
