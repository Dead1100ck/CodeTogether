using CodeTogether.DTO;


namespace CodeTogether.Application.Models.Responses.Rooms
{
    public class RoomResponseModel
    {
        public Guid Id { get; set; }


        public RoomResponseModel MappingRoomToRoomResponse(Room room)
        {
            return new RoomResponseModel
            {
                Id = room.Id
            };
        }
    }
}
