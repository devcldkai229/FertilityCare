using Castle.Core.Logging;
using FertilityCare.Domain.Entities;
using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using FertilityCare.UseCase.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class PatientPartnerRepository : IPatientPartnerRepository
    {

        private readonly FertilityCareDBContext _context;


        public PatientPartnerRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<PatientPartner> CreateAsync(PatientPartner entity)
        {
            await _context.PatientPartners.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var loadedPartner = await _context.PatientPartners.FindAsync(id);
            if (loadedPartner is null)
            {
                throw new NotFoundException($"Partner with Id: {id.ToString()} not exist!");
            }

            _context.PatientPartners.Remove(loadedPartner);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var loadedPartner = await _context.PatientPartners.FindAsync(id);
            if (loadedPartner is null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<PatientPartner>> GetAllAsync()
        {
            return await _context.PatientPartners.ToListAsync();
        }

        public async Task<PatientPartner> GetByEmailAsync(string email)
        {
            return await _context.PatientPartners.Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<PatientPartner> GetByPhoneNumber(string phone)
        {
            return await _context.PatientPartners.Where(p => p.ContactNumber == phone).FirstOrDefaultAsync();
        }


        public async Task<PatientPartner> GetByIdAsync(Guid id)
        {
            var loadedPartner = await _context.PatientPartners.FindAsync(id);
            if (loadedPartner is null)
            {
                throw new NotFoundException($"Partner with Id: {id.ToString()} not exist!");
            }

            return loadedPartner;
        }

        public async Task<IEnumerable<PatientPartner>> SearchByNameAsync(string name)
        {
            return await _context.PatientPartners.Where(p => p.FullName.Contains(name)).ToListAsync();
        }

        public async Task<PatientPartner> UpdateAsync(PatientPartner entity)
        {
            _context.PatientPartners.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
