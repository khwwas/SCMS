Insert Into SETUP_Module Values('0001','General Ledger', 'GL', '../Home', '../img/GL.png', 1, 1 )

Insert Into SETUP_Module Values('0002','Time Management System', 'TMS', '../Home', '../img/TM.png', 1, 2)



Insert into Security_MenuOptions( Mnu_Code, Mnu_Description, Mnu_Level, Mnu_SortOrder, Mnu_TargetUrl, Mnu_IsLineBreak, Mnu_Active, Mod_Id ) 
Values ( '00041', 'Bank Reconciliation','2.3',104,'../BankReconciliation',Null,1,1)






INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00025', 'SETUP_Shift', 'Shft_Id', Null,5,1);
INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00026', 'SETUP_EmployeeType', 'EmpTyp_Id', Null,5,1);
INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00027', 'SETUP_Gender', 'Gndr_Id', Null,5,1);
INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00028', 'SETUP_Religion', 'Rlgn_Id', Null,5,1);
INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00029', 'SETUP_MaritalStatus', 'MS_Id', Null,5,1);
INSERT INTO SYSTEM_CodeGeneration (CodeGen_Id, CodeGen_TableName, CodeGen_ColumnName, CodeGen_Prefix, CodeGen_Length, CodeGen_AutoTag )  
VALUES ( '00030', 'SETUP_Nationality', 'Natn_Id', Null,5,1);

