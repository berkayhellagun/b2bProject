using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSupplierDal : EfEntityRepositoryBase<Supplier, DBContext>, ISupplierDal
    {
        public SupplierDetail GetSupplierDetail(int id)
        {
            using (var db = new DBContext())
            {
                var result = from supplier in db.Suppliers
                             //join category in db.Categories on supplier.CategoryId equals category.Id
                             join user in db.Users on supplier.SupplierContactPersonId equals user.UserId
                             where supplier.SupplierId == id
                             select new SupplierDetail
                             {
                                 SupplierId = supplier.SupplierId,
                                 SupplierDescription = supplier.SupplierDescription,
                                 //CategoryId = category.Id,
                                 //CategoryName = category.Name,
                                 EmployeeCount = supplier.EmployeeCount,
                                 SupplierContactPersonId = supplier.SupplierContactPersonId,
                                 PersonName = user.FirstName + " " + user.LastName,
                                 PersonEmail = user.Email,
                                 SupplierCountry = supplier.SupplierCountry,
                                 SupplierFoundYear = supplier.SupplierFoundYear,
                                 SupplierGiro = supplier.SupplierGiro,
                                 SupplierMail = supplier.SupplierMail,
                                 SupplierName = supplier.SupplierName,
                                 SupplierTaxNo = supplier.SupplierTaxNo,
                                 SupplierTelephoneNumber = supplier.SupplierTelephoneNumber
                             };
                return result.First();
            }
        }
    }
}
