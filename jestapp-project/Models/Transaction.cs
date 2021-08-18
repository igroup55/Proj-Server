using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jestapp_project.Models
{
    public class Transaction
    {
        int transtactionID;
        int userID1;
        int userID2;
        double creditAmount;
        DateTime transactionDate;


        public Transaction()
        {

        }

        public Transaction(int transtactionID, int userID1, int userID2, double creditAmount, DateTime transactionDate)
        {
            this.TranstactionID = transtactionID;
            this.UserID1 = userID1;
            this.UserID2 = userID2;
            this.CreditAmount = creditAmount;
            this.TransactionDate = transactionDate;
        }

        public int TranstactionID { get => transtactionID; set => transtactionID = value; }
        public int UserID1 { get => userID1; set => userID1 = value; }
        public int UserID2 { get => userID2; set => userID2 = value; }
        public double CreditAmount { get => creditAmount; set => creditAmount = value; }
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }


        
            public List<Transaction> GetTransactions(int UserID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetTransactions(UserID);
        }

        public int InsertTransaction(Transaction transaction)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertTransaction(transaction);
        }  
             
    }
}