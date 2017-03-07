﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DataAccessDB : DataAccess
    {
        //private ActivAIDDB db;

        public DataAccessDB()
        {
            ;//db = new ActivAIDDB();
        }

        public string query(Query query)
        {
            //logic to handle query
            //return query.attributeMap["fileName"].Item1;
            string fileName = "";
            foreach (Attrib attribute in query.attributeList)
            {
                //keywords
                //db.getFileElements(attribute.name, keywords);
                fileName = fileName + attribute.value;
            }
            return fileName;
        }
    }
}
