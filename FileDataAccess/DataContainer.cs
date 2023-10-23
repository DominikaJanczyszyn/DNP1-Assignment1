﻿using Assignment1.Models;


namespace FileDataAccess;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post?> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Vote> Votes { get; set; }
    
    
}