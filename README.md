# A Starter GRPC Project with .Net(EF Core, FluentValidation, SeriLog, Pagination, CRUD)

![overview](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/person-list.png?raw=true)


In this project, We built a starter project for any gRPC project. We used this entity model for our purpose:

```
public class PersonEntity: BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalCode { get; set; }
    public required DateTime Birthday { get; set; }
}
```

## How to run

1. First of all, clone or download the project.

2. Open the project in your favorite IDE(Visual Studio, Rider or VS Code). I used VS Code.

3. Update database.

```
dotnet ef database update
```
4. Run
```
dotnet run
```

## How to test

To test the functionality, The easiest way is `Postman`. First, you need to download it. After opening it, Click the new button on top.

![postman the new button](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/postman-new-button.png?raw=true)


Choose the gRPC item.

![postman choosing grpc](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/choosing-grpc.png?raw=true)

Enter `localhost:8000` into the URL box and be sure that the lock icon next before it is active

![postman entering the url and enabling tls](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/enable-tls.png?raw=true)

In the next dropdown, choose `Import a .proto file`. File picker open up immediately. go to the project directory and from Protos folder, select `person.proto`

![postman importing the person proto file](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/importing-new-proto-file-to-postman.png?raw=true)

Now, you can see the person methods. 

![postman proto methods](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/postman-methods.png?raw=true)

Let's try the CreatePerson method.

Click the message tab under the above button and dropdown. and fill it with the below code:

```
{
    "first_name": "new_user_8",
    "last_name": "new_user_8",
    "national_code": "1234567888",
    "birthday": "01-02-1903"
}
```

![create a new person](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/create-person-input.png?raw=true)

Finally, hit the invoke button next to the methods dropdown.

If everything is correct. you'll see the result.

![create a new person result](https://github.com/python-programmer/starter-grpc-crud-dotnet/blob/main/Images/create-person-result.png?raw=true)









