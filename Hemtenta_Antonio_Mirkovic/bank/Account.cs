using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.bank
{
    public class Account : IAccount
    {
        double _amount;

        public Account()
        {
        }

        public double Amount
        {
            get
            {
                return _amount;
            }
        }

        public void Deposit(double amount)
        {
            if (double.IsNaN(amount) || amount == 0 || amount < 0 || double.IsNegativeInfinity(amount) || double.IsPositiveInfinity(amount))
            {
                throw new IllegalAmountException("Invalid Amount!");
            }
            _amount += amount;
        }

        public void TransferFunds(IAccount destination, double amount)
        {
            if (amount <= 0)
            {
                throw new OperationNotPermittedException("Operation Not Permited");
            }

            else if (amount < _amount)
            {
                destination.Deposit(amount);
                _amount -= amount;
            }
            else
            {
                throw new InsufficientFundsException("Not Enough Money");
            }
            
        }

        public void Withdraw(double amount)
        {
            if (amount > _amount)
            {
                throw new InsufficientFundsException("Not Enough Money");
            }
            else if (amount <= 0)
            {
                throw new OperationNotPermittedException("Operation Not Permited");
            }
            else
            {
                _amount -= amount;
            }

        }
    }
}
