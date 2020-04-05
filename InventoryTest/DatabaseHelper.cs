using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {
                
        }

        public void Save<T>(object entityToSave) where T : InventoryDBEntities
        {
            //var customer = (T)entityToSave;
            //if(customer != null)
            //    T.cc
            //Customers.Remove(customer);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (System.Data.Entity.Infrastructure.DbUpdateException)
            //{
            //    MessageBox.Show("Cannot Delete Customer");
            //}
        }
    }
}
