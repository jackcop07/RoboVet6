using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoboVet6.Data.DbContext;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;

namespace RoboVet6.DataAccess.Repositories
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly ApplicationDbContext _context;

        public DiaryRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DiaryModel>> GetAllDiaries(string name)
        {
            var collection = _context.Diaries as IQueryable<DiaryModel>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            return await collection.ToListAsync();
        }

        public async Task<DiaryModel> GetDiaryByDiaryId(int diaryId)
        {
            return await _context.Diaries.FirstOrDefaultAsync(x => x.Id == diaryId);
        }

        public async Task UpdateDiary(DiaryModel diary)
        {
            await _context.SaveChangesAsync();
        }

        public async Task InsertDiary(DiaryModel diary)
        {
            await _context.Diaries.AddAsync(diary);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DiaryExists(int diaryId)
        {
            var diary = await _context.Diaries.FindAsync(diaryId);

            return diary != null;
        }
    }
}
