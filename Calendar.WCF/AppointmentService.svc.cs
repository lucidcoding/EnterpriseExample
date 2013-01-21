using System;
using System.Linq;
using System.Transactions;
using Calendar.Data.Common;
using Calendar.Domain.Common;
using Calendar.Domain.RepositoryContracts;
using Calendar.Validation.Contracts;
using Calendar.WCF.Core;
using Calendar.WCF.DataTransferObjects;
using Calendar.WCF.Mappers;
using Calendar.WCF.Requests;
using StructureMap;

namespace Calendar.WCF
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentValidator _appointmentValidator;

        public AppointmentService()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
            _appointmentRepository = ObjectFactory.GetInstance<IAppointmentRepository>();
            _appointmentValidator = ObjectFactory.GetInstance<IAppointmentValidator>();
        }

        public AppointmentDto[] GetByEmployeeId(Guid employeeId)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var appointmentDtos = new AppointmentDtoMapper().Map(_appointmentRepository.GetByEmployeeId(employeeId).ToArray());
                    transactionScope.Complete();
                    return appointmentDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public AppointmentDto[] GetByIds(Guid[] ids)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var appointmentDtos = new AppointmentDtoMapper().Map(_appointmentRepository.GetByIds(ids.ToList()).ToArray());
                    transactionScope.Complete();
                    return appointmentDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public ValidationMessageCollection ValidateBook(ValidateBookRequest request)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var validationMessages = _appointmentValidator.ValidateBook(
                        request.EmployeeId,
                        request.DepartmentId,
                        request.Start,
                        request.End,
                        request.Description);

                    transactionScope.Complete();
                    return validationMessages;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
