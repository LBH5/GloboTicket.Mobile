using System.Globalization;
using GloboTicket.Admin.Mobile.ViewModel;

namespace GloboTicket.Admin.Mobile.Converters;


public class StatusEnumToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not null && value is EventStatusEnum status)
        {
            return status switch
            {
                EventStatusEnum.OnSale => "On Sale",
                EventStatusEnum.AlmostSoldOut => "Almost Sold Out",
                EventStatusEnum.SalesClosed => "Ticket Sales Closed",
                EventStatusEnum.Canceled => "Event Canceled",  
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}