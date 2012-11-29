using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HumanResources.Application.Contracts;
using HumanResources.UI.Calendar.WCF;
using HumanResources.UI.Helpers;
using HumanResources.UI.ViewModels;
using StructureMap;

namespace HumanResources.UI.Controllers  
{
    /// <remarks>
    /// I'm well aware that there are some design issues here, such as the fact that it needs to get the employee entity
    /// twice, once to display holiday entitlement/remaining, and another time to get calendar entries. Also,
    /// when getting the list of employees, it gets all calendar entries for each one. This can be avoided but I'm leaving
    /// it for the sake of speed and simplicity. It's EDA I'm demonstrating here, not fine tuning NHibernate behavior.
    /// </remarks>
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBookingService _bookingService;
        private readonly IHolidayService _holidayService;

        public EmployeeController(IEmployeeService employeeService, IBookingService bookingService, IHolidayService holidayService)
        {
            _employeeService = employeeService;
            _bookingService = bookingService;
            _holidayService = holidayService;
        }

        public ActionResult Index(Guid? employeeId)
        {
            var viewModel = new EmployeeViewModel();
            var employees = _employeeService.GetAll();
            viewModel.Employees = new SelectList(employees, "Id", "FullName", employeeId);
            viewModel.EmployeeId = employeeId;

            if (employeeId.HasValue)
            {
                var employeeService = ObjectFactory.GetInstance<IEmployeeService>();
                var employee = employeeService.GetById(employeeId.Value);
                viewModel.HolidayEntitlement = employee.HolidayEntitlement;
                viewModel.RemainingHoliday = employee.RemainingHoliday;
            }

            return View(viewModel);
        }

        public JsonResult CalendarData(Guid? employeeId)
        {
            var calendarEntries = new List<object>();

            if (employeeId.HasValue)
            {
                var bookings = _bookingService.Search(new SearchBookingsRequest
                                                          {
                                                              EmployeeId = employeeId.Value
                                                          });

                var holidays =
                    _holidayService.GetByIds(
                        bookings.Where(x => x.BookingType.Description == "Holiday").Select(y => y.Id).ToList());

                foreach (var booking in bookings)
                {
                    var holiday = holidays.SingleOrDefault(x => x.Id == booking.Id);

                    calendarEntries.Add(new 
                                            {
                                                id = booking.Id,
                                                employeeId = booking.EmployeeId,
                                                isHoliday = holiday != null,
                                                title = holiday != null ? "Holiday" : "",
                                                start = DateHelper.ToUnixTimespan(booking.Start),
                                                end = DateHelper.ToUnixTimespan(booking.End),
                                                isoStart = booking.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                                                isoEnd = booking.End.ToString("yyyy-MM-dd HH:mm:ss"),
                                                backgroundColor = holiday != null ? "cadetblue" : "lightgrey",
                                                borderColor = holiday != null ? "cadetblue" : "lightgrey",
                                            });
                }
            }

            return Json(calendarEntries, JsonRequestBehavior.AllowGet);
        }
    }
}
