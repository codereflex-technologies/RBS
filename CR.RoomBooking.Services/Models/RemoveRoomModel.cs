namespace CR.RoomBooking.Services.Models
{
    public sealed class RemoveRoomModel
    {
        public bool MoveBookings { get; set; }
        public int NewRoomId { get; set; }
    }
}
