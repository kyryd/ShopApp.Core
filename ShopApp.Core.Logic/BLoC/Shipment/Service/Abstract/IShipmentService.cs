﻿using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;

namespace ShopApp.Core.Logic.BLoC.Shipment.Service.Abstract
{
    public interface IShipmentService<O, P> where P : IProduct where O : IOrder
    {
        Task<DeliveryState> DelayShipment(O order);
        Task<DeliveryState> Ship(O order);
        Task<DeliveryState> Cancel(O order);
        Task<DeliveryState> CheckStatus(O order);
        Task<DeliveryState> Return(O order);
    }
}
