using System;
using AspNet_project.Data;
using AspNet_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNet_project.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<Dictionary<string, string>> GetAllSettingAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}

