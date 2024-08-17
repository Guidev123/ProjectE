﻿
using Microsoft.AspNetCore.Identity;
using ProjectE.Core.Account;
using ProjectE.Core.Commands.Account;
using ProjectE.UserAPI.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace ProjectE.UserAPI.Endpoints.Identity
{
    public class RegisterCustomEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app.MapPost("/register-custom", Handle).RequireAuthorization();
        private static async Task<IResult> Handle(RegisterCommand model, UserManager<User> userManager)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Cpf))
            {
                return Results.BadRequest();
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Cpf = new(model.Cpf)
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Results.Ok();
            }

            return Results.BadRequest(result.Errors);
        }
    }
}
