using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleRemember.Database;
using TeleRemember.Database.Table;
using TeleRemember.Server.Payload;

namespace TeleRemember.Server
{
    public class Model
    {
        private readonly ListDbContext _context;
        private readonly ILogger<Model> _logger;

        public Model(
            ILogger<Model> logger,
            ListDbContext context) 
        { 
            _context = context;
            _logger = logger;
        }

        public async Task InsertTodo(NewPayload payload)
        {
            var toDo = new ToDo
            {
                Title = payload.Title!,
                Description = payload.Description!,
                Link = payload.Link!,
                Due = payload.Due!,
                Priority = payload.Priority!,
                CreatedBy = payload.CreatedBy!,
                CreatedDate = payload.CreatedDate
            };
            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDo>> GetAllTodo()
        {
            return await _context.ToDo.ToListAsync();
        }
    }
}
