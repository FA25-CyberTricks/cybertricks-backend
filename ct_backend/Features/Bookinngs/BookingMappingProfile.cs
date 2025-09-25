using ct_backend.Domain.Entities;

namespace ct_backend.Features.Bookinngs
{
    public class BookingMappingProfile
        : AbstractMappingProfile<Booking, BookingDto, CreateBookingRequest, UpdateBookingRequest>{ }
}
