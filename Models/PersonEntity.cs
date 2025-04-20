namespace ProtobufCRUDServer.Models;

public class PersonEntity: BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalCode { get; set; }
    public required DateTime Birthday { get; set; }
}