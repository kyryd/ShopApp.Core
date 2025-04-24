using ShopApp.Core.Logic.BLoC.Shipment.Service.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;

namespace ShopApp.Core.Logic.BLoC.Shipment.Service
{
    public sealed class ShipmentServiceMock<O, P> : IShipmentService<O, P> where O : IOrder where P : IProduct
    {
        public async Task<DeliveryState> Cancel(O order)
        {
            return await Task.Run(() => order.DeliveryState = order.DeliveryState switch
            {
                DeliveryState.NotShipped => DeliveryState.Cancelled,
                _ => order.DeliveryState
            });
        }

        public async Task<DeliveryState> CheckStatus(O order)
        {
            return await Task.Run(() => order.DeliveryState);
        }

        public async Task<DeliveryState> DelayShipment(O order)
        {
            return await Task.Run(() => order.DeliveryState = order.DeliveryState switch
            {
                DeliveryState.Shipped => DeliveryState.Delayed,
                DeliveryState.ReadyForShipping => DeliveryState.Delayed,
                _ => order.DeliveryState
            });

        }

        public async Task<DeliveryState> Return(O order)
        {
            return await Task.Run(() => order.DeliveryState = order.DeliveryState switch
            {
                DeliveryState.Delivered => DeliveryState.Returned,
                _ => order.DeliveryState
            });
        }

        public async Task<DeliveryState> Ship(O order)
        {
            return await Task.Run(() => order.DeliveryState = order.DeliveryState switch
            {
                DeliveryState.NotShipped => DeliveryState.Shipped,
                DeliveryState.Shipped => DeliveryState.Delivered,
                _ => order.DeliveryState
            });
        }



    }
}
