using Hemtenta_Antonio_Mirkovic.bank;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{//mer tester här!
    public class BankTest
    {
        IAccount IAccount;

        public BankTest()
        {
            IAccount = new Account();
        }

        [Fact]
        public void Deposit_money_success()
        {
            IAccount.Deposit(23);

            Assert.Equal(23, IAccount.Amount);
        }

        [Fact]
        public void Deposit_money_Invalid_Zero_Amount_Throw()
        {
            Assert.Throws<IllegalAmountException>(() => IAccount.Deposit(0));
        }

        [Fact]
        public void Withdraw_Success()
        {
            IAccount.Deposit(23);
            IAccount.Withdraw(22);

            Assert.Equal(1, IAccount.Amount);
        }

        [Fact]
        public void Withdraw_insufficientfunds()
        {
            IAccount.Deposit(100);

            Assert.Throws<InsufficientFundsException>(() => IAccount.Withdraw(200));
        }
        [Fact]
        public void Withdraw__Illigal_Amount_Throw()
        {

            Assert.Throws<OperationNotPermittedException>(() => IAccount.Withdraw(-200));
        }

        [Fact]
        public void Transfer_money_ok()
        {
            IAccount.Deposit(10);

            var destination = new Account();

            IAccount.TransferFunds(destination, 5);

            Assert.Equal(5, IAccount.Amount);
            Assert.Equal(5, destination.Amount);
        }
        [Fact]
        public void Transfer_Not_Enough_Money_Throw()
        {
            IAccount.Deposit(10);

            var destination = new Account();

            Assert.Throws<InsufficientFundsException>(() => IAccount.TransferFunds(destination, 200));
        }

        [Fact]
        public void Transfer_Operation_Not_Permited()
        {
            IAccount.Deposit(10);

            var destination = new Account();

            Assert.Throws<OperationNotPermittedException>(() => IAccount.TransferFunds(destination, -200));
        }
    }
}
