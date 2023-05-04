using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
using System.Security.Policy;

namespace sms.Controllers
{
  public class EmployeeController : Controller
  {
	    private ApplicationDbContext _context;

		public EmployeeController(ApplicationDbContext Context)
		{
            this._context=Context;
		}
        public ActionResult Index()
        {

            ViewBag.DepartmentId = _context.Departments.ToList();
            ViewBag.EmployeeId = _context.Employees.ToList();
            ViewBag.dataSource = _context.Outs.ToList();

            return View();
        }
        public ActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Employees.ToList();

            DataOperations operation = new DataOperations();   
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
               DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
	       if (dm.Search != null && dm.Search.Count > 0)
           {
               DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
           }
			if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
			    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Employee>().Count();
            if (dm.Skip != 0)//Paging
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public ActionResult Insert([FromBody]CRUDModel<Employee>  value)
        {

            ViewBag.DepartmentId = _context.Departments.ToList();
            ViewBag.EmployeeId = _context.Employees.ToList();
            ViewBag.dataSource = _context.Outs.ToList();
            //do stuff
            _context.Employees.Add(value.Value);
			_context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody]CRUDModel<Employee>  value)
        {
            //do stuff
			var ord = value;

           Employee val = _context.Employees.Where(or => or.EmployeeId == ord.Value.EmployeeId).FirstOrDefault();
            val.EmployeeId=ord.Value.EmployeeId;
            val.EmployeeName=ord.Value.EmployeeName;
            val.DepartmentId=ord.Value.DepartmentId;
            val.Department=ord.Value.Department;
            val.Outs=ord.Value.Outs;
            val.User=ord.Value.User;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody]CRUDModel<Employee> value)
        {
            //do stuff
			Employee order = _context.Employees.Where(c => c.EmployeeId == (int)value.Key).FirstOrDefault();
            _context.Employees.Remove(order);
            _context.SaveChanges();
            return Json(order);
        }
   }
}
