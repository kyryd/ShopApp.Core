using ShopApp.Core.Logic.BLoC.Converters;
using ShopApp.Core.Logic.BLoC.Discounts.Abstract;
using ShopApp.Core.Logic.BLoC.Inventory.Service.Abstract;
using ShopApp.Core.Logic.BLoC.Orders.Abstract;
using ShopApp.Core.Logic.BLoC.Payment.Service;
using ShopApp.Core.Logic.BLoC.Prices.Abstract;
using ShopApp.Core.Logic.BLoC.Shipment.Service.Abstract;
using ShopApp.Core.Logic.BLoC.Taxes.Abstract;
using ShopApp.Core.Models.Enums;
using ShopApp.Core.Models.Models.Abstract;
using ShopApp.Core.Models.Models.Discounts.Abstract;

namespace ShopApp.Core.Logic.BLoC.Orders
{
    public class OrderService<O, PRD>(
                                        O order,
                                        ICurrency currency,
                                        IInventoryService<O, PRD> stockService,
                                        IShipmentService<O, PRD> shipmentService,
                                        IPaymentService paymentService,
                                        IPriceCaluclator<IPrice> priceCaluclator,
                                        ITaxProvider taxProvider,
                                        IDiscountsService<IDiscount> discountsService,
                                        CurrencyConverterFactory<CurrencyConverter> converterFactory
                                                                                ) : IOrderService<PRD, O> where O : IOrder where PRD : IProduct
    {
        public O Order { get; init; } = order;
        protected IInventoryService<O, PRD> StockService { get; init; } = stockService;
        protected IShipmentService<O, PRD> DeliveryService { get; init; } = shipmentService;
        protected IPaymentService PaymentService { get; init; } = paymentService;
        protected IPriceCaluclator<IPrice> PriceCaluclator { get; } = priceCaluclator;
        protected IDiscountsService<IDiscount> DiscountsService { get; } = discountsService;
        protected ITaxProvider TaxProvider { get; } = taxProvider;
        protected CurrencyConverterFactory<CurrencyConverter> ConverterFactory { get; } = converterFactory;

        public IPrice GetTotal()
        {
            var discount = DiscountsService.AggregatedDiscount(Order);
            return PriceCaluclator.CalcTotal(Order.Product.PricePerUnit, Order.NumberOfUnits, discount, currency, TaxProvider.Tax);
        }

        public Task<PaymentState> ProcessPayment()
        {
            return Task.Run(async () =>
            {
                if (Order.PaymentState == PaymentState.Unpaid && await ReserveInStock())
                {
                    Order.PaymentState = PaymentState.InProcess;
                }
                else if (Order.PaymentState == PaymentState.InProcess)
                {
                    Order.PaymentState = PaymentState.Paid;
                }

                return Order.PaymentState;
            });
        }

        public Task<DeliveryState> Deliver()
        {
            if (Order.PaymentState.IsPaid())
            {
                return DeliveryService.Ship(Order);
            }
            else if (new HashSet<PaymentState>([PaymentState.InProcess, PaymentState.Unpaid]).Contains(Order.PaymentState))
            {
                return DeliveryService.DelayShipment(Order);
            }
            else if (new HashSet<PaymentState>([PaymentState.Cancelled, PaymentState.Refunded]).Contains(Order.PaymentState))
            {
                return DeliveryService.Cancel(Order);
            }
            return DeliveryService.CheckStatus(Order);

            //return Task.Run(() =>
            //{
            //    if (Order.PaymentState.IsPaid())
            //    {
            //        return Order.DeliveryState = Order.DeliveryState.IsShipped() ? DeliveryState.Delivered : DeliveryState.Shipped;
            //    }
            //    else if (new HashSet<PaymentState>([PaymentState.InProcess, PaymentState.Unpaid]).Contains(Order.PaymentState))
            //    {
            //        return Order.DeliveryState = DeliveryState.NotShipped;
            //    }
            //    else if (new HashSet<PaymentState>([PaymentState.Cancelled, PaymentState.Refunded]).Contains(Order.PaymentState))
            //    {
            //        return Order.DeliveryState = DeliveryState.Cancelled;
            //    }
            //    return Order.DeliveryState;
            //});
        }

        //private IPrice ApplyDiscount(IOrder order, IDiscount discount, ICurrency currency, ICurrencyConverter currencyConverter, IOrderService<IProduct, IOrder> orderService)
        //{
        //    IPrice totalPriceBeforeDiscount = new Price(currency, orderService.GetTotal().Amount, currencyConverter);
        //    IPrice afterDicount = new Price(currency, 0m, currencyConverter);

        //    if (discount.Value.Category.IsAbsolute())
        //    {
        //        var discountLimit = StrategyProvider.Strategy.MaxAbsoluteDicount.Value.Value;
        //        var disountValue = discount.Value.Value < discountLimit ? discount.Value.Value : discountLimit;

        //        afterDicount.Amount = totalPriceBeforeDiscount.Amount - disountValue;
        //    }
        //    else if (discount.Value.Category.IsPercentage())
        //    {
        //        var discountLimit = StrategyProvider.Strategy.MaxPercentDiscount.Value.Value;
        //        var disountValue = discount.Value.Value < discountLimit ? discount.Value.Value : discountLimit;

        //        afterDicount.Amount = totalPriceBeforeDiscount.Amount * disountValue / 100m;
        //    }
        //    else
        //    {
        //        throw new NotImplementedException();
        //    }

        //    return totalPriceBeforeDiscount;
        //}



        //public IPrice ApplyAllDisounts(IOrder order, IOrderService<IProduct, IOrder> orderService) => ApplyDiscount(
        //                                                               order: order,
        //                                                               discount: AcumulateDiscounts(new ReadOnlyCollection<IDiscount>(Discounts.ToList())),
        //                                                               currency: Currency,
        //                                                               currencyConverter: ConverterFactory.GetNewInstance(),
        //                                                               orderService: orderService);

        public async Task<bool> ReserveInStock() => await StockService.Reserve(Order) == StockState.Reserved;

        Task<PaymentState> IOrderService<PRD, O>.ProcessPayment()
        {
            throw new NotImplementedException();
        }

        Task<DeliveryState> IOrderService<PRD, O>.Deliver()
        {
            throw new NotImplementedException();
        }
    }
}
