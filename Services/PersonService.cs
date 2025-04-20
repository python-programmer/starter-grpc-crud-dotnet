using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProtobufCRUDServer.Data;
using ProtobufCRUDServer.Models;
using ProtobufCRUDServer.Infrastructures;
using AutoMapper;


namespace ProtobufCRUDServer.Services;

public class PersonService : PersonHandler.PersonHandlerBase {
    private readonly ILogger<PersonService> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PersonService(AppDbContext dbContext, IMapper mapper, ILogger<PersonService> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        var result = request.Validation();
        if(!result.IsValid) 
        {
            string error = "";
            foreach(var failure in result.Errors)
            {
                error += failure.ErrorMessage + Environment.NewLine;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, error));
        }

        PersonEntity? person = await _dbContext.People.SingleOrDefaultAsync(p => p.NationalCode == request.NationalCode);

        if(person != null) {
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"{request.NationalCode} already exists"));
        }

        person = _mapper.Map<PersonEntity>(request);

        await _dbContext.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CreatePersonResponse>(person);
    }

    public override async Task<GetPersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
    {
        var result = request.Validation();
        if(!result.IsValid) 
        {
            string error = "";
            foreach(var failure in result.Errors)
            {
                error += failure.ErrorMessage + Environment.NewLine;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, error));
        }

        PersonEntity? person = await _dbContext.FindAsync<PersonEntity>(request.Id);

        if(person == null) {
            throw new RpcException(new Status(StatusCode.NotFound, $"No any person with Id={request.Id} "));
        }

        return _mapper.Map<GetPersonResponse>(person);
    }

    public override async Task<GetAllPersonResponse> GetAllPerson(GetAllPersonRequest request, ServerCallContext context)
    {
        var result = request.Validation();
        if(!result.IsValid) 
        {
            string error = "";
            foreach(var failure in result.Errors)
            {
                error += failure.ErrorMessage + Environment.NewLine;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, error));
        }
        int page = 0;
        int pageSize = 10;
        if(request.Page != 0) {
            page = request.Page;
        }

        if(request.PageSize != 0) {
            pageSize = request.PageSize;
        }

        var people = new GetAllPersonResponse();
        var personList = await _dbContext.People
                            .Where(p => string.IsNullOrEmpty(request.SearchTerm) ||
                                    p.FirstName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                    p.LastName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                                    p.NationalCode.Contains(request.SearchTerm.ToLower()))
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .OrderBy(p => p.Id)
                            .ToListAsync();

        people.People.AddRange(_mapper.Map<List<GetPersonResponse>>(personList));

        return people;
    }

    public override async Task<UpdatePersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
    {
        var result = request.Validation();
        if(!result.IsValid) 
        {
            string error = "";
            foreach(var failure in result.Errors)
            {
                error += failure.ErrorMessage + Environment.NewLine;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, error));
        }

        PersonEntity? person = await _dbContext.FindAsync<PersonEntity>(request.Id);

        if(person == null) {
            throw new RpcException(new Status(StatusCode.NotFound, $"No any person with Id={request.Id} "));
        }

        if(person.NationalCode != request.NationalCode) {
            PersonEntity? personExists = await _dbContext.People.SingleOrDefaultAsync(p => p.NationalCode == request.NationalCode);

            if(personExists != null) {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{request.NationalCode} already exists"));
            }
        }

        _mapper.Map(request, person);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<UpdatePersonResponse>(person);
    }

    public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
    {
        var result = request.Validation();
        if(!result.IsValid) 
        {
            string error = "";
            foreach(var failure in result.Errors)
            {
                error += failure.ErrorMessage + Environment.NewLine;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, error));
        }

        PersonEntity? person = await _dbContext.FindAsync<PersonEntity>(request.Id);

        if(person == null) {
            throw new RpcException(new Status(StatusCode.NotFound, $"No any person with Id={request.Id} "));
        }

        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<DeletePersonResponse>(person);
    }
}