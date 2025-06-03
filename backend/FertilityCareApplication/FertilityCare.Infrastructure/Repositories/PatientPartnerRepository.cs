using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityCare.Domain.Entities;
using FertilityCare.Infrastructure.Data;
using FertilityCare.Shared.Exceptions;
using FertilityCare.UseCase.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FertilityCare.Infrastructure.Repositories
{
    public class PatientPartnerRepository : IPatientPartnerRepository
    {
        private readonly FertilityCareDBContext _context;

        public PatientPartnerRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var partner = await _context.PatientPartners.FindAsync(id);
            if (partner == null)
            {
                throw new NotFoundException($"Partner with Id: {id} does not exist!");
            }

            _context.PatientPartners.Remove(partner);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientPartner>> FindAllAsync()
        {
            return await _context.PatientPartners.ToListAsync();
        }

        public async Task<PatientPartner> FindByIdAsync(Guid id)
        {
            var partner = await _context.PatientPartners.FindAsync(id);
            if (partner == null)
            {
                throw new NotFoundException($"Partner with Id: {id} does not exist!");
            }

            return partner;
        }

        public async Task<PatientPartner?> FindPartnerByEmailAsync(string email)
        {
            return await _context.PatientPartners
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<PatientPartner?> FindPartnerByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.PatientPartners
                .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<PatientPartner>> FindPartnersByPatientIdAsync(Guid patientId)
        {
            var partnerId = await _context.Patients
                .Where(p => p.Id == patientId)
                .Select(p => p.PatientParnerId)
                .FirstOrDefaultAsync();

            if (partnerId == null) return Enumerable.Empty<PatientPartner>();

            var partner = await _context.PatientPartners
                .Where(p => p.Id == partnerId)
                .ToListAsync();

            return partner;
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _context.PatientPartners.AnyAsync(p => p.Id == id);
        }

        public async Task<PatientPartner> SaveAsync(PatientPartner entity)
        {
            await _context.PatientPartners.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<PatientPartner>> SearchByEmailKeywordAsync(string keyword)
        {
            return await _context.PatientPartners
                .Where(p => p.Email != null && p.Email.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<PatientPartner>> SearchByPhoneKeywordAsync(string keyword)
        {
            return await _context.PatientPartners
                .Where(p => p.PhoneNumber != null && p.PhoneNumber.Contains(keyword))
                .ToListAsync();
        }

        public async Task<PatientPartner> UpdateAsync(PatientPartner entity)
        {
            var exists = await _context.PatientPartners.AnyAsync(p => p.Id == entity.Id);
            if (!exists)
            {
                throw new NotFoundException($"Partner with Id: {entity.Id} does not exist!");
            }

            _context.PatientPartners.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
