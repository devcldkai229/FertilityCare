using FertilityCare.Domain.Interfaces.Repositoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.UnitOfWorks;

public interface IUserUnitOfWork
{
    IUserProfileRepository _userProfileRepository { get; }

    IPatientPartnerRepository _patientPartnerRepository { get; }

    IPatientRepository _patientRepository { get; }

    IDoctorRepository _doctorRepository { get; }

}
