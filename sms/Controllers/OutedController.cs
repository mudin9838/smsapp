using Syncfusion.EJ2.Base;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sms.Models;
using sms.Data;
namespace sms.Controllers
{
  public class OutedController : Controller
  {
	    private ApplicationDbContext _context;

		public OutedController(ApplicationDbContext Context)
		{
            this._context=Context;
		}
        public ActionResult Index()
        {
	        return View();
        }
        public ActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {

            IEnumerable DataSource = _context.Outs.ToList();

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
            int count = DataSource.Cast<Out>().Count();
            if (dm.Skip != 0)//Paging
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return  Json(new { result = DataSource, count = count });
        }
        public ActionResult Insert([FromBody]CRUDModel<Out>  value)
        {
            //do stuff
			_context.Outs.Add(value.Value);
			_context.SaveChanges();
            return Json(value);

        }
        public ActionResult Update([FromBody]CRUDModel<Out>  value)
        {
            //do stuff
			var ord = value;

           Out val = _context.Outs.Where(or => or.OutId == ord.Value.OutId).FirstOrDefault();
            val.OutId=ord.Value.OutId;
            val.RecieptNo=ord.Value.RecieptNo;
            val.ParentId=ord.Value.ParentId;
            val.DepartmentId=ord.Value.DepartmentId;
            val.EmployeeId=ord.Value.EmployeeId;
            val.From=ord.Value.From;
            val.To=ord.Value.To;
            val.Serie=ord.Value.Serie;
            val.Quantity=ord.Value.Quantity;
            val.EachPrice=ord.Value.EachPrice;
            val.Vat=ord.Value.Vat;
            val.TotalPrice=ord.Value.TotalPrice;
            val.OutDate=ord.Value.OutDate;
            val.StockId=ord.Value.StockId;
            val.StatusId=ord.Value.StatusId;
            val.StatusdelId=ord.Value.StatusdelId;
            val.Department=ord.Value.Department;
            val.Employee=ord.Value.Employee;
            val.Parent=ord.Value.Parent;
            val.Status=ord.Value.Status;
            val.Statusdel=ord.Value.Statusdel;
            val.Stock=ord.Value.Stock;
            val.User=ord.Value.User;
            _context.SaveChanges();
            return Json(value);
        }
        public ActionResult Delete([FromBody]CRUDModel<Out> value)
        {
            //do stuff
			Out order = _context.Outs.Where(c => c.OutId == (int)value.Key).FirstOrDefault();
            _context.Outs.Remove(order);
            _context.SaveChanges();
            return Json(order);
        }
   }
}
