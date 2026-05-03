using BNS.Entities.Interfaces;

namespace BNS.Services
{
    public class CurrentDateTimeService : ICurrentDateTimeService
    {
        public CurrentDateTimeService()
        {
            dateToday = DateTime.UtcNow.AddHours(5).AddMinutes(30).Date;
            dateTimeToday = DateTime.Now.ToUniversalTime().AddHours(5).AddMinutes(30);
        }

        public DateTime dateToday { get; }
        public DateTime dateTimeToday { get; }
    }
}
