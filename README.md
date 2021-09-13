# BankProjectAdoFiles
This project contains code for Ado.net connections for Bank Project


What i have done is added this project's reference in the Bank Project



sql code->


create database BankLibrary;

use BankLibrary;

create table SBAccount(AccountNumber int, CustomerName varchar(30), CustomerAddress varchar(30), CurrentBalance decimal);

create table SBTransaction(TransactionId int Identity(1,1), TransactionDate date, AccountNumber int, Amount decimal, TransactionType varchar(50));

select * from SBAccount;

select * from SBTransaction;


Insert into SBTransaction(TransactionDate, AccountNumber, Amount, TransactionType) values(SYSDATETIME(), 1, 10, 'internet');


