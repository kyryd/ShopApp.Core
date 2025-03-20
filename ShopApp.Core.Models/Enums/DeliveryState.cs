namespace ShopApp.Core.Models.Enums
{
    public enum DeliveryState
    {
        NotShipped = 0,
        ReadyForShipping = 1,
        Shipped = 2,
        Delivered = 4,
        Returned = 5,
        Cancelled = 6,
        Delayed = 7, 
        InernationalShipping = 8,
        LocalShipping = 9,
        Pending = 10
    }

    public static class DeliveryStateExtensions
    {
        public static bool IsShipped(this DeliveryState state) => state == DeliveryState.Shipped;
        public static bool IsDelivered(this DeliveryState state) => state == DeliveryState.Delivered;
        public static bool IsReturned(this DeliveryState state) => state == DeliveryState.Returned;
        public static bool IsCancelled(this DeliveryState state) => state == DeliveryState.Cancelled;

        public static bool IsDelayed(this DeliveryState state) => state == DeliveryState.Delayed;

        public static bool IsShipping(this DeliveryState state) => state == DeliveryState.InernationalShipping || state == DeliveryState.LocalShipping;
    }
}
