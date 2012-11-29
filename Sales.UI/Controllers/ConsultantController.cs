using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Domain.Global;
using Sales.UI.Calendar.WCF;
using Sales.UI.Helpers;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;
using System.Transactions;

namespace Sales.UI.Controllers
{
    public class ConsultantController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IBookingService _bookingService;
        private readonly IBus _bus;

        public ConsultantController(
            IAppointmentService appointmentService,
            IEmployeeService employeeService,
            IBookingService bookingService,
            IBus bus)
        {
            _appointmentService = appointmentService;
            _employeeService = employeeService;
            _bookingService = bookingService;
            _bus = bus;
        }

        public ActionResult Index(Guid? consultantId)
        {
            var viewModel = new ViewConsultantViewModel();
            var consultants = _employeeService.GetAll();
            viewModel.Consultants = new SelectList(consultants, "Id", "FullName", consultantId);
            viewModel.ConsultantId = consultantId;
            return View(viewModel);
        }

        public JsonResult CalendarData(Guid? consultantId)
        {
            var calendarEntries = new List<object>();

            if (consultantId.HasValue)
            {
                var bookings = _bookingService.Search(new SearchBookingsRequest
                {
                    EmployeeId = consultantId.Value
                });

                var appointments =
                    _appointmentService.GetByIds(
                        bookings.Where(x => x.BookingType.Description == "SalesAppointment").Select(y => y.Id).ToList());

                foreach (var booking in bookings)
                {
                    var appointment = appointments.SingleOrDefault(x => x.Id == booking.Id);

                    calendarEntries.Add(new
                    {
                        id = booking.Id,
                        employeeId = booking.EmployeeId,
                        isAppointment = appointment != null,
                        title = appointment != null ? appointment.LeadName : "",
                        start = DateHelper.ToUnixTimespan(booking.Start),
                        end = DateHelper.ToUnixTimespan(booking.End),
                        isoStart = booking.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                        isoEnd = booking.End.ToString("yyyy-MM-dd HH:mm:ss"),
                        address = appointment != null ? appointment.Address : "",
                        backgroundColor = appointment != null ? "indianred" : "lightgrey",
                        borderColor = appointment != null ? "indianred" : "lightgrey",
                    });
                }
            }

            return Json(calendarEntries, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(string forename, string surname)
        {
            var employeeId = Guid.NewGuid();

            //Do I actually need this transaction here?
            //using (var transactionScope = new TransactionScope())
            //{
            //    var registerEmployee = new RegisterEmployee
            //    {
            //        Id = employeeId,
            //        DepartmentId = Constants.SalesDepartmentId,
            //        Forename = viewModel.Forename,
            //        Surname = viewModel.Surname
            //    };

            //    _bus.Send(registerEmployee);
            //    transactionScope.Complete();
            //}

            return Json(new { text = forename + " " + surname, value = employeeId }, JsonRequestBehavior.AllowGet);
        }
    }
}
