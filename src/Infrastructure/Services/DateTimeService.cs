using disclone.Application.Common.Interfaces;

namespace disclone.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
