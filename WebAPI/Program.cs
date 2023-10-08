using Application.DAO_Interfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Assignment1.Models;
using FileDataAccess;
using FileDataAccess.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserFileDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPostDao, PostFileDao>();
builder.Services.AddScoped<IPostLogic, PostLogic>();
builder.Services.AddScoped<ICommentDao, CommentFileDao>();
builder.Services.AddScoped<ICommentLogic, CommentLogic>();
builder.Services.AddScoped<IVoteDao, VoteFileDao>();
builder.Services.AddScoped<IVoteLogic, VoteLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();