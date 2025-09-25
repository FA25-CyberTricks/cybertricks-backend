using ct.backend.Domain.Entities;

namespace ct.backend.Features.Bookinngs
{
    public class BookingMappingProfile
        : AbstractMappingProfile<Booking, BookingDto, CreateBookingRequest, UpdateBookingRequest>{ }
}
