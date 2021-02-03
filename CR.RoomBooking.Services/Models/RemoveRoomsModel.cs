namespace CR.RoomBooking.Services.Models
{
    public sealed class RemoveRoomsModel
    {
        public int[] RoomIds { get; set; }
        public bool MoveBookings { get; set; }
        public int NewRoomId { get; set; }
    }
}
