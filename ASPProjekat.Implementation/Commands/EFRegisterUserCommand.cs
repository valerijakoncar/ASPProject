using ASPProjekat.Application.Commands;
using ASPProjekat.Application.DataTransfer;
using ASPProjekat.Application.Email;
using ASPProjekat.Domain;
using ASPProjekat.EFDataAccess;
using ASPProjekat.Implementation.Validators;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPProjekat.Implementation.Commands
{
    public class EFRegisterUserCommand : IRegisterUserCommand
    {
        private readonly ASPProjekatContext _context;
        private readonly RegisterUserValidator _validator;
        private readonly IMapper _mapper;
        private readonly IEmailSender _sender;
        public int Id => 1;

        public string Name =>"User Registration";

        public EFRegisterUserCommand(ASPProjekatContext context, RegisterUserValidator validator, IMapper mapper, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _sender = sender;
        }

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            User newUser = _mapper.Map<User>(request);

            _context.Users.Add(newUser);
           
            _context.SaveChanges();

            _sender.Send(new SendMailDto
            {
                Content = "<h1>You've successfully registrated to our website Cool Stuff!</h1>",
                SendTo = request.Email,
                Subject = "Cool Stuff Registration"
            });
        }
    }
}
