using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Data
{
    public interface IDataHelper<Table>
    {
        //Read

        List<Table> GetAllData();
        List<Table> GetDataByUser(string userId);
        List<Table> Search(string searchItem);
        Table Find(int id);

        //Write

        int Add(Table table);
        int Edit(int id, Table table);
        int Delete(int id);
    }
}
