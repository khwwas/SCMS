/***************************** SYSTEM *****************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SYSTEM_CodeGeneration](
	[CodeGen_Id] [varchar](50) NOT NULL,
	[CodeGen_TableName] [varchar](50) NULL,
	[CodeGen_ColumnName] [varchar](50) NULL,
	[CodeGen_Prefix] [varchar](4) NULL,
	[CodeGen_Length] [int] NULL,
	[CodeGen_AutoTag] [int] NULL,
 CONSTRAINT [PK_SYSTEMCodeGeneration_CodeGenId] PRIMARY KEY CLUSTERED 
(
	[CodeGen_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_Module](
	[Mod_Id] [int] IDENTITY(1,1) NOT NULL,
	[Mod_Code] [varchar](50) NULL,
	[Mod_Desc] [varchar](500) NOT NULL,
	[Mod_Abbreviation] [varchar](50) NULL,
	[Mod_Url] [Varchar](50) NULL,
    [Mod_ImagePath] [Varchar](50) NULL,
	[Mod_Active] [bit] NULL,
	[Mod_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUP_Module] PRIMARY KEY CLUSTERED 
(
	[Mod_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_CompanyModule](
	[CmpMod_Id] [int] IDENTITY(1,1) NOT NULL,
	[Mod_Id] [int] NOT NULL,
	[Cmp_Id] [varchar](50) NOT NULL,
	[CmpMod_Active] [bit] NOT NULL,
	[CmpMod_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUP_CompanyModule] PRIMARY KEY CLUSTERED 
(
	[CmpMod_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--ALTER TABLE [dbo].[SETUP_CompanyModule] ADD  CONSTRAINT [DF_SETUP_CompanyModule_CmpMod_Active]  DEFAULT ((1)) FOR [CmpMod_Active]
--GO

CREATE TABLE [dbo].[SYSTEM_Nature](
	[Natr_Id] [varchar](50) NOT NULL,
	[Natr_Code] [varchar](50) NULL,
	[Natr_Title] [varchar](100) NULL,
	[Natr_Abbreviation] [varchar](100) NULL,
	[Natr_SystemTitle] [varchar](100) NULL,
	[Natr_Default] [int] NULL,
	[Natr_SortOrder] [int] NULL,
	[Natr_Active] [int] NULL,
 CONSTRAINT [PK_SYSTEMNature_NatrId] PRIMARY KEY CLUSTERED 
(
	[Natr_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SYSTEM_AccountNature](
	[AccNatr_Id] [varchar](50) NOT NULL,
	[AccNatr_Code] [varchar](50) NULL,
	[AccNatr_Title] [varchar](100) NULL,
	[AccNatr_Default] [int] NULL,
	[AccNatr_Active] [int] NULL,
 CONSTRAINT [PK_SYSTEMAccountNature_AccNatrId] PRIMARY KEY CLUSTERED 
(
	[AccNatr_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SYSTEM_Parms](
	[SysPrm_Id] [int] IDENTITY(1,1) NOT NULL,
	[SysPrm_Code] [varchar](60) NULL,
	[Mod_Id] [int] NULL,
	[SysPrm_RefColumn] [varchar](60) NULL,
	[SysPrm_RefData] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[SysPrm_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SYSTEM_Parms]  WITH CHECK ADD  CONSTRAINT [FK_SYSTEMParms_SETUPModule_ModId] FOREIGN KEY([Mod_Id])
REFERENCES [dbo].[SETUP_Module] ([Mod_Id])
GO

ALTER TABLE [dbo].[SYSTEM_Parms] CHECK CONSTRAINT [FK_SYSTEMParms_SETUPModule_ModId]
GO






SET ANSI_PADDING OFF
GO

/***************************** GL *****************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SECURITY_UserGroup](
	[UsrGrp_Id] [varchar](50) NOT NULL,
	[UsrGrp_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[UsrGrp_Title] [varchar](100) NULL,
	[UsrGrp_Active] [int] NULL,
	[UsrGrp_SortOrder] [int] NULL,
 CONSTRAINT [PK_SECURITYUserGroup_UsrGrpId] PRIMARY KEY CLUSTERED 
(
	[UsrGrp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SECURITY_UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_SECURITYUserGroup_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SECURITY_UserGroup] CHECK CONSTRAINT [FK_SECURITYUserGroup_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SECURITY_UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_SECURITYUserGroup_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SECURITY_UserGroup] CHECK CONSTRAINT [FK_SECURITYUserGroup_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SECURITY_User](
	[User_Id] [varchar](50) NOT NULL,
	[User_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[UsrGrp_Id] [varchar](50) NULL,
	[User_Title] [varchar](100) NULL,
	[User_Login] [varchar](100) NULL,
	[User_Password] [varchar](100) NULL,
	[User_Active] [int] NULL,
	[User_SortOrder] [int] NULL,
 CONSTRAINT [PK_SECURITYUser_UserId] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SECURITY_User]  WITH CHECK ADD  CONSTRAINT [FK_SECURITYUser_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SECURITY_User] CHECK CONSTRAINT [FK_SECURITYUser_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SECURITY_User]  WITH CHECK ADD  CONSTRAINT [FK_SECURITYUser_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SECURITY_User] CHECK CONSTRAINT [FK_SECURITYUser_SETUPLocation_LocId]
GO


ALTER TABLE [dbo].[SECURITY_User]  WITH CHECK ADD  CONSTRAINT [FK_SECURITYUser_SECURITYUserGroup_UsrGrpId] FOREIGN KEY([UsrGrp_Id])
REFERENCES [dbo].[SECURITY_UserGroup] ([UsrGrp_Id])
GO

ALTER TABLE [dbo].[SECURITY_User] CHECK CONSTRAINT [FK_SECURITYUser_SECURITYUserGroup_UsrGrpId]
GO

CREATE TABLE [dbo].[Security_MenuOptions](
	[Mnu_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Mnu_Code] [varchar](50) NULL,
	[Mod_Id] [int] NULL,
	[Mnu_Description] [varchar](100) NULL,
	[Mnu_Level] [varchar](50) NULL,
	[Mnu_SortOrder] [int] NULL,
	[Mnu_TargetUrl] [varchar](500) NULL,
	[Mnu_IsLineBreak] [bit] NULL,
	[Mnu_Active] [int] NULL,
CONSTRAINT [PK_Security_MenuOptions_Mnu_Id] PRIMARY KEY CLUSTERED 
(
	[Mnu_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Security_UserRights](
	[UsrSec_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UsrSec_Code] [varchar](50) NULL,
	[Grp_Id] [bigint] NOT NULL,
	[Mnu_Id] [bigint] NOT NULL,
	[Mod_Id] [int] NULL,
	[UsrSec_Active] [int] NULL,
 CONSTRAINT [PK_Security_UserRights] PRIMARY KEY CLUSTERED 
(
	[UsrSec_Id] ASC,
	[Grp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Security_UserRights]  WITH NOCHECK ADD  CONSTRAINT [FK_Security_UserRights_Security_Group] FOREIGN KEY([Grp_Id])
REFERENCES [dbo].[Security_UserGroup] ([Grp_Id])
GO

ALTER TABLE [dbo].[Security_UserRights] CHECK CONSTRAINT [FK_Security_UserRights_Security_Group]
GO

ALTER TABLE [dbo].[Security_UserRights]  WITH NOCHECK ADD  CONSTRAINT [FK_Security_UserRights_Security_MenuOptions] FOREIGN KEY([Mnu_Id])
REFERENCES [dbo].[Security_MenuOptions] ([Mnu_Id])
GO

ALTER TABLE [dbo].[Security_UserRights] CHECK CONSTRAINT [FK_Security_UserRights_Security_MenuOptions]
GO






CREATE TABLE [dbo].[SETUP_Company](
	[Cmp_Id] [varchar](50) NOT NULL,
	[Cmp_Code] [varchar](50) NULL,
	[Cmp_Title] [varchar](100) NULL,
	[Cmp_Address1] [varchar](100) NULL,
	[Cmp_Address2] [varchar](100) NULL,
	[Cmp_Email] [varchar](100) NULL,
	[Cmp_Phone] [varchar](100) NULL,
	[Cmp_Fax] [varchar](100) NULL,
	[Cmp_Active] [int] NULL,
	[Cmp_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUP_Company_CmpId] PRIMARY KEY CLUSTERED 
(
	[Cmp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[SETUP_Location](
	[Loc_Id] [varchar](50) NOT NULL,
	[Loc_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NOT NULL,
	[Loc_Title] [varchar](100) NULL,
	[Loc_Active] [int] NULL,
	[Loc_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPLocation_LocId] PRIMARY KEY CLUSTERED 
(
	[Loc_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Location]  WITH CHECK ADD  CONSTRAINT [FK_SETUPLocation_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Location] CHECK CONSTRAINT [FK_SETUPLocation_SETUPCompany_CmpId]
GO

CREATE TABLE [dbo].[SETUP_CodeAnalysis1](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis1_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis1]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis1_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis1] CHECK CONSTRAINT [FK_SETUPCodeAnalysis1_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis1]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis1_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis1] CHECK CONSTRAINT [FK_SETUPCodeAnalysis1_SETUPLocation_LocId]
GO


CREATE TABLE [dbo].[SETUP_CodeAnalysis2](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis2_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[SETUP_CodeAnalysis2]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis2_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis2] CHECK CONSTRAINT [FK_SETUPCodeAnalysis2_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis2]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis2_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis2] CHECK CONSTRAINT [FK_SETUPCodeAnalysis2_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_CodeAnalysis3](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis3_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[SETUP_CodeAnalysis3]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis3_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis3] CHECK CONSTRAINT [FK_SETUPCodeAnalysis3_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis3]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis3_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis3] CHECK CONSTRAINT [FK_SETUPCodeAnalysis3_SETUPLocation_LocId]
GO


CREATE TABLE [dbo].[SETUP_CodeAnalysis4](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis4_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis4]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis4_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis4] CHECK CONSTRAINT [FK_SETUPCodeAnalysis4_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis4]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis4_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis4] CHECK CONSTRAINT [FK_SETUPCodeAnalysis4_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_CodeAnalysis5](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis5_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis5]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis5_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis5] CHECK CONSTRAINT [FK_SETUPCodeAnalysis5_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis5]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis5_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis5] CHECK CONSTRAINT [FK_SETUPCodeAnalysis5_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_CodeAnalysis6](
	[CA_Id] [varchar](50) NOT NULL,
	[CA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CA_Title] [varchar](100) NULL,
	[CA_Active] [int] NULL,
	[CA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCodeAnalysis6_CAId] PRIMARY KEY CLUSTERED 
(
	[CA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis6]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis6_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis6] CHECK CONSTRAINT [FK_SETUPCodeAnalysis6_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis6]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCodeAnalysis6_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CodeAnalysis6] CHECK CONSTRAINT [FK_SETUPCodeAnalysis6_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_Country](
	[Cnty_Id] [varchar](50) NOT NULL,
	[Cnty_Code] [varchar](50) NULL,
	[Cnty_Title] [varchar](100) NULL,
	[Cnty_Active] [int] NULL,
	[Cnty_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCountry_CntyId] PRIMARY KEY CLUSTERED 
(
	[Cnty_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_City](
	[City_Id] [varchar](50) NOT NULL,
	[City_Code] [varchar](50) NULL,
	[Cnty_Id] [varchar](50) NOT NULL,
	[City_Title] [varchar](100) NULL,
	[City_Active] [int] NULL,
	[City_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCity_CityId] PRIMARY KEY CLUSTERED 
(
	[City_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_City]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCity_SETUPCountry_CntyId] FOREIGN KEY([Cnty_Id])
REFERENCES [dbo].[SETUP_Country] ([Cnty_Id])
GO

ALTER TABLE [dbo].[SETUP_City] CHECK CONSTRAINT [FK_SETUPCity_SETUPCountry_CntyId]
GO

CREATE TABLE [dbo].[SETUP_CustomerType](
	[CustType_Id] [varchar](50) NOT NULL,
	[CustType_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CustType_Title] [varchar](100) NULL,
	[CustType_Active] [int] NULL,
	[CustType_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCustomerType_CustTypeId] PRIMARY KEY CLUSTERED 
(
	[CustType_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_CustomerType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomerType_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CustomerType] CHECK CONSTRAINT [FK_SETUPCustomerType_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CustomerType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomerType_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CustomerType] CHECK CONSTRAINT [FK_SETUPCustomerType_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_Customer](
	[Cust_Id] [varchar](50) NOT NULL,
	[Cust_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CustType_Id] [varchar](50) NOT NULL,
	[City_Id] [varchar](50) NOT NULL,
	[Cust_Title] [varchar](100) NULL,
	[Cust_Address1] [varchar](100) NULL,
	[Cust_Address2] [varchar](100) NULL,
	[Cust_Email] [varchar](100) NULL,
	[Cust_Phone] [varchar](100) NULL,
	[Cust_Fax] [varchar](100) NULL,
	[Cust_Active] [int] NULL,
	[Cust_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUP_Customer_CustId] PRIMARY KEY CLUSTERED 
(	[Cust_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Customer]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomer_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Customer] CHECK CONSTRAINT [FK_SETUPCustomer_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_Customer]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomer_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_Customer] CHECK CONSTRAINT [FK_SETUPCustomer_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_Customer]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomer_SETUPCustomerType_CustTypeId] FOREIGN KEY([CustType_Id])
REFERENCES [dbo].[SETUP_CustomerType] ([CustType_Id])
GO

ALTER TABLE [dbo].[SETUP_Customer] CHECK CONSTRAINT [FK_SETUPCustomer_SETUPCustomerType_CustTypeId]
GO

ALTER TABLE [dbo].[SETUP_Customer]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCustomer_SETUPCity_CityId] FOREIGN KEY([City_Id])
REFERENCES [dbo].[SETUP_City] ([City_Id])
GO

ALTER TABLE [dbo].[SETUP_Customer] CHECK CONSTRAINT [FK_SETUPCustomer_SETUPCity_CityId]
GO

CREATE TABLE [dbo].[SETUP_SupplierType](
	[SuppType_Id] [varchar](50) NOT NULL,
	[SuppType_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[SuppType_Title] [varchar](100) NULL,
	[SuppType_Active] [int] NULL,
	[SuppType_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCustomerType_SuppTypeId] PRIMARY KEY CLUSTERED 
(
	[SuppType_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_SupplierType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSupplierType_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_SupplierType] CHECK CONSTRAINT [FK_SETUPSupplierType_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_SupplierType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSupplierType_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_SupplierType] CHECK CONSTRAINT [FK_SETUPSupplierType_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_Supplier](
	[Supp_Id] [varchar](50) NOT NULL,
	[Supp_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[SuppType_Id] [varchar](50) NOT NULL,
	[City_Id] [varchar](50) NOT NULL,
	[Supp_Title] [varchar](100) NULL,
	[Supp_Address1] [varchar](100) NULL,
	[Supp_Address2] [varchar](100) NULL,
	[Supp_Email] [varchar](100) NULL,
	[Supp_Phone] [varchar](100) NULL,
	[Supp_Fax] [varchar](100) NULL,
	[Supp_Active] [int] NULL,
	[Supp_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUP_Supplier_SuppId] PRIMARY KEY CLUSTERED 
(	[Supp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Supplier]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSupplier_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Supplier] CHECK CONSTRAINT [FK_SETUPSupplier_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_Supplier]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSupplier_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_Supplier] CHECK CONSTRAINT [FK_SETUPSupplier_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_Supplier]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSupplier_SETUPSupplierType_SuppTypeId] FOREIGN KEY([SuppType_Id])
REFERENCES [dbo].[SETUP_SupplierType] ([SuppType_Id])
GO

ALTER TABLE [dbo].[SETUP_Supplier] CHECK CONSTRAINT [FK_SETUPSupplier_SETUPSupplierType_SuppTypeId]
GO

ALTER TABLE [dbo].[SETUP_Supplier]  WITH CHECK ADD  CONSTRAINT [FK_SETUPSuppomer_SETUPCity_CityId] FOREIGN KEY([City_Id])
REFERENCES [dbo].[SETUP_City] ([City_Id])
GO

ALTER TABLE [dbo].[SETUP_Supplier] CHECK CONSTRAINT [FK_SETUPSuppomer_SETUPCity_CityId]
GO

CREATE TABLE [dbo].[SETUP_VoucherType](
	[VchrType_Id] [varchar](50) NOT NULL,
	[VchrType_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[VchrType_Title] [varchar](100) NULL,
	[VchrType_Prefix] [varchar](50) NULL,
	[VchrType_Active] [int] NULL,
	[VchrType_SortOrder] [int] NULL,
	[VchrType_CodeInitialization] [int] NULL,
 CONSTRAINT [PK_SETUPVoucherType_VchrTypeId] PRIMARY KEY CLUSTERED 
(
	[VchrType_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_VoucherType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPVoucherType_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_VoucherType] CHECK CONSTRAINT [FK_SETUPVoucherType_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_VoucherType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPVoucherType_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_VoucherType] CHECK CONSTRAINT [FK_SETUPVoucherType_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_VoucherTypeNarration](
	[VchrTypeNarr_Id] [varchar](50) NOT NULL,
	[VchrTypeNarr_Code] [varchar](50) NULL,
	[VchrType_Id] [varchar](50) NOT NULL,
	[VchrTypeNarr_Title] [varchar](100) NULL,
	[VchrTypeNarr_Active] [int] NULL,
	[VchrTypeNarr_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPVoucherTypeNarration_VchrTypeNarrId] PRIMARY KEY CLUSTERED 
(
	[VchrTypeNarr_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_VoucherTypeNarration]  WITH CHECK ADD  CONSTRAINT [FK_SETUPVchrTypeNarr_SETUPVchrType_VchrTypeId] FOREIGN KEY([VchrType_Id])
REFERENCES [dbo].[SETUP_VoucherType] ([VchrType_Id])
GO

ALTER TABLE [dbo].[SETUP_VoucherTypeNarration] CHECK CONSTRAINT [FK_SETUPVchrTypeNarr_SETUPVchrType_VchrTypeId]
GO

CREATE TABLE [dbo].[SETUP_Bank](
	[Bank_Id] [varchar](50) NOT NULL,
	[Bank_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[Bank_Title] [varchar](100) NULL,
	[Bank_Active] [int] NULL,
	[Bank_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPBank_BankId] PRIMARY KEY CLUSTERED 
(
	[Bank_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Bank]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBank_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Bank] CHECK CONSTRAINT [FK_SETUPBank_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_Bank]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBank_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_Bank] CHECK CONSTRAINT [FK_SETUPBank_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_BankAccount](
	[BankAcc_Id] [varchar](50) NOT NULL,
	[BankAcc_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[Bank_Id] [varchar](50) NOT NULL,
	[ChrtAcc_Id] [varchar](50) NOT NULL,
	[BankAcc_Title] [varchar](100) NULL,
	[BankAcc_Active] [int] NULL,
	[BankAcc_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPBankAccount_BankId] PRIMARY KEY CLUSTERED 
(
	[BankAcc_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBankAccount_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_BankAccount] CHECK CONSTRAINT [FK_SETUPBankAccount_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBankAccount_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_BankAccount] CHECK CONSTRAINT [FK_SETUPBankAccount_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBankAccount_SETUPBank_BankId] FOREIGN KEY([Bank_Id])
REFERENCES [dbo].[SETUP_Bank] ([Bank_Id])
GO

ALTER TABLE [dbo].[SETUP_BankAccount] CHECK CONSTRAINT [FK_SETUPBankAccount_SETUPBank_BankId]
GO

ALTER TABLE [dbo].[SETUP_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPBankAccount_SETUPChartOfAccount_ChrtAccId] FOREIGN KEY([ChrtAcc_Id])
REFERENCES [dbo].[SETUP_ChartOfAccount] ([ChrtAcc_Id])
GO

ALTER TABLE [dbo].[SETUP_BankAccount] CHECK CONSTRAINT [FK_SETUPBankAccount_SETUPChartOfAccount_ChrtAccId]
GO


CREATE TABLE [dbo].[SETUP_CalendarType](
	[CldrType_Id] [varchar](50) NOT NULL,
	[CldrType_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CldrType_Title] [varchar](100) NULL,
	[CldrType_Level] [int] NULL,
	[CldrType_Active] [int] NULL,
	[CldrType_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCalendarType_CldrTypeId] PRIMARY KEY CLUSTERED 
(
	[CldrType_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_CalendarType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCalendarType_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_CalendarType] CHECK CONSTRAINT [FK_SETUPCalendarType_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_CalendarType]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCalendarType_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_CalendarType] CHECK CONSTRAINT [FK_SETUPCalendarType_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_Calendar](
	[Cldr_Id] [varchar](50) NOT NULL,
	[Cldr_ParentRefId] [varchar](50) NULL,
	[Cldr_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[CldrType_Id] [varchar](50) NOT NULL,
	[Cldr_Title] [varchar](100) NULL,
	[Cldr_Prefix] [varchar](10) NULL,
	[Cldr_DateStart] [datetime] NULL,
	[Cldr_DateEnd] [datetime] NULL,
	[CldrType_Level] [int] NULL,
	[Cldr_Active] [int] NULL,
	[Cldr_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPCalendar_CldrId] PRIMARY KEY CLUSTERED 
(
	[Cldr_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Calendar]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCalendar_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Calendar] CHECK CONSTRAINT [FK_SETUPCalendar_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_Calendar]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCalendar_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_Calendar] CHECK CONSTRAINT [FK_SETUPCalendar_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_Calendar]  WITH CHECK ADD  CONSTRAINT [FK_SETUPCalendar_SETUPCalendarType_CldrTypeId] FOREIGN KEY([CldrType_Id])
REFERENCES [dbo].[SETUP_CalendarType] ([CldrType_Id])
GO

ALTER TABLE [dbo].[SETUP_Calendar] CHECK CONSTRAINT [FK_SETUPCalendar_SETUPCalendarType_CldrTypeId]
GO

CREATE TABLE [dbo].[SETUP_ChartOfAccount](
	[ChrtAcc_Id] [varchar](50) NOT NULL,
	[ChrtAcc_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[Natr_Id] [varchar](50) NULL,
	[AccNatr_Id] [varchar](50) NULL,
	[ChrtAcc_Title] [varchar](100) NULL,
	[ChrtAcc_Type] [int] NULL,
	[ChrtAcc_Level] [int] NULL,
	[ChrtAcc_BudgetLevel] [int] NULL,
	[ChrtAcc_Active] [int] NULL,
 CONSTRAINT [PK_SETUPCalendar_ChrtAccId] PRIMARY KEY CLUSTERED 
(
	[ChrtAcc_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPChartOfAccount_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount] CHECK CONSTRAINT [FK_SETUPChartOfAccount_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPChartOfAccount_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount] CHECK CONSTRAINT [FK_SETUPChartOfAccount_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPChartOfAccount_SYSTEMNature_NatrId] FOREIGN KEY([Natr_Id])
REFERENCES [dbo].[SYSTEM_Nature] ([Natr_Id])
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount] CHECK CONSTRAINT [FK_SETUPChartOfAccount_SYSTEMNature_NatrId]
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_SETUPChartOfAccount_SYSTEMAccountNature_AccNatrId] FOREIGN KEY([AccNatr_Id])
REFERENCES [dbo].[SYSTEM_AccountNature] ([AccNatr_Id])
GO

ALTER TABLE [dbo].[SETUP_ChartOfAccount] CHECK CONSTRAINT [FK_SETUPChartOfAccount_SYSTEMAccountNature_AccNatrId]
GO

CREATE TABLE [dbo].[GL_VchrMaster](
	[VchMas_Id] [varchar](50) NOT NULL,
	[VchMas_Code] [varchar](50) NULL,
	[VchMas_Date] [datetime] NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[VchrType_Id] [varchar](50) NOT NULL,
	[VchMas_Remarks] [varchar] (200) NULL,
	[VchMas_Status] [varchar](50) NULL,
	[VchMas_EnteredBy] [varchar] (100) NULL,
	[VchMas_EnteredDate] [datetime] NULL,
	[VchMas_ApprovedBy] [varchar] (100) NULL,
	[VchMas_ApprovedDate] [datetime] NULL,
	[SyncStatus] [bit] NOT NULL Default 0,
	[VchMas_ReconciliationDate] [datetime] NULL,
	[VchMas_Reconciliation] [Int] NULL,
 CONSTRAINT [PK_GLVchrMaster_VchMasId] PRIMARY KEY CLUSTERED 
(
	[VchMas_Id]
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GL_VchrMaster]  WITH CHECK ADD  CONSTRAINT [FK_GLVchrMaster_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[GL_VchrMaster] CHECK CONSTRAINT [FK_GLVchrMaster_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[GL_VchrMaster]  WITH CHECK ADD  CONSTRAINT [FK_GLVchrMaster_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[GL_VchrMaster] CHECK CONSTRAINT [FK_GLVchrMaster_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[GL_VchrMaster]  WITH CHECK ADD  CONSTRAINT [FK_GLVchrMaster_SETUPVoucherType_VchrTypeId] FOREIGN KEY([VchrType_Id])
REFERENCES [dbo].[SETUP_VoucherType] ([VchrType_Id])
GO

ALTER TABLE [dbo].[GL_VchrMaster] CHECK CONSTRAINT [FK_GLVchrMaster_SETUPVoucherType_VchrTypeId]
GO

CREATE TABLE [dbo].[GL_VchrDetail](
	[VchDet_Id] [varchar](50) NOT NULL,
	[VchMas_Id] [varchar](50) NOT NULL,
	[ChrtAcc_Id] [varchar] (50) NOT NULL,
	[VchMas_DrAmount] [decimal] (20,6) NULL,
	[VchMas_CrAmount] [decimal] (20,6) NULL,
	[VchDet_Remarks] [varchar] (200) NULL,
 CONSTRAINT [PK_GLVchrDetail_VchDetId] PRIMARY KEY CLUSTERED 
(
	[VchDet_Id]
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GL_VchrDetail]  WITH CHECK ADD  CONSTRAINT [FK_GLVchrDetail_GLVchrMaster_VchMasId] FOREIGN KEY([VchMas_Id])
REFERENCES [dbo].[GL_VchrMaster] ([VchMas_Id])
GO

ALTER TABLE [dbo].[GL_VchrDetail] CHECK CONSTRAINT [FK_GLVchrDetail_GLVchrMaster_VchMasId]
GO


ALTER TABLE [dbo].[GL_VchrDetail]  WITH CHECK ADD  CONSTRAINT [FK_GLVchrDetail_SETUPChartOfAccount_ChrtAccId] FOREIGN KEY([ChrtAcc_Id])
REFERENCES [dbo].[SETUP_ChartOfAccount] ([ChrtAcc_Id])
GO

ALTER TABLE [dbo].[GL_VchrDetail] CHECK CONSTRAINT [FK_GLVchrDetail_SETUPChartOfAccount_ChrtAccId]
GO

























CREATE TABLE [dbo].[SETUP_Shift](
	[Shft_Id] [varchar](50) NOT NULL,
	[Shft_Code] [varchar](50) NULL,
	[Shft_Title] [varchar](100) NULL,
	[Shft_Abbreviation] [varchar](50) NULL,
	[Shft_StartTime] [datetime] NULL,
	[Shft_EndTime] [datetime] NULL,
	[Shft_Active] [int] NULL,
	[Shft_SortOrder] [int] NULL,
	[Shift_BreakStartTime] [datetime] Null,
    [Shift_BreakEndTime] [datetime] null,
    [Shift_BreakDuration] [datetime] null,
    [Shift_GraceIn] [varchar](50) null,
    [Shift_GraceEarly] [varchar](50) null
 CONSTRAINT [PK_SETUPNationality_NatnId] PRIMARY KEY CLUSTERED 
(
	[Shft_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_EmployeeType](
	[EmpTyp_Id] [varchar](50) NOT NULL,
	[EmpTyp_Code] [varchar](50) NULL,
	[EmpTyp_Title] [varchar](100) NULL,
	[EmpTyp_Abbreviation] [varchar](50) NULL,
	[EmpTyp_Active] [int] NULL,
	[EmpTyp_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPEmployeeType_EmpTypId] PRIMARY KEY CLUSTERED 
(
	[EmpTyp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_Gender](
	[Gndr_Id] [varchar](50) NOT NULL,
	[Gndr_Code] [varchar](50) NULL,
	[Gndr_Title] [varchar](100) NULL,
	[Gndr_Abbreviation] [varchar](50) NULL,
	[Gndr_Active] [int] NULL,
	[Gndr_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPGender_GndrId] PRIMARY KEY CLUSTERED 
(
	[Gndr_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_Religion](
	[Rlgn_Id] [varchar](50) NOT NULL,
	[Rlgn_Code] [varchar](50) NULL,
	[Rlgn_Title] [varchar](100) NULL,
	[Rlgn_Abbreviation] [varchar](50) NULL,
	[Rlgn_Active] [int] NULL,
	[Rlgn_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPReligion_RlgnId] PRIMARY KEY CLUSTERED 
(
	[Rlgn_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_MaritalStatus](
	[MS_Id] [varchar](50) NOT NULL,
	[MS_Code] [varchar](50) NULL,
	[MS_Title] [varchar](100) NULL,
	[MS_Abbreviation] [varchar](50) NULL,
	[MS_Active] [int] NULL,
	[MS_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPMaritalStatus_MSId] PRIMARY KEY CLUSTERED 
(
	[MS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SETUP_Nationality](
	[Natn_Id] [varchar](50) NOT NULL,
	[Natn_Code] [varchar](50) NULL,
	[Natn_Title] [varchar](100) NULL,
	[Natn_Abbreviation] [varchar](50) NULL,
	[Natn_Active] [int] NULL,
	[Natn_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPNationality_NatnId] PRIMARY KEY CLUSTERED 
(
	[Natn_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 CREATE TABLE [dbo].[SETUP_LeaveType](
	[LevTyp_Id] [varchar](50) NOT NULL,
	[LevTyp_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[LevTyp_Title] [varchar](100) NULL,
	[LevTyp_Active] [int] NULL,
	[LevTyp_SortOrder] [int] NULL,
	[LevTyp_Abbreviation] [varchar](50) NULL,
	[LevTyp_Count] [varchar](100) NULL,
 CONSTRAINT [PK_SETUPLeaveType_LevTypId] PRIMARY KEY CLUSTERED 
(
	[LevTyp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[SETUP_LeaveGroup](
	[LevGrp_Id] [varchar](50) NOT NULL,
	[LevGrp_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[LevGrp_Title] [varchar](100) NULL,
	[LevGrp_Active] [int] NULL,
	[LevGrp_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPLeaveGroup_LevGrpId] PRIMARY KEY CLUSTERED 
(
	[LevGrp_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_LeaveGroup]  WITH CHECK ADD  CONSTRAINT [FK_SETUPLevGrp_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_LeaveGroup] CHECK CONSTRAINT [FK_SETUPLevGrp_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_LeaveGroup]  WITH CHECK ADD  CONSTRAINT [FK_SETUPLevGrp_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_LeaveGroup] CHECK CONSTRAINT [FK_SETUPLevGrp_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_Department](
	[Dpt_Id] [varchar](50) NOT NULL,
	[Dpt_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[Dpt_Title] [varchar](100) NULL,
	[Dpt_Active] [int] NULL,
	[Dpt_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPDepartment_DptId] PRIMARY KEY CLUSTERED 
(
	[Dpt_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_Department]  WITH CHECK ADD  CONSTRAINT [FK_SETUPDepartment_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_Department] CHECK CONSTRAINT [FK_SETUPDepartment_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_Department]  WITH CHECK ADD  CONSTRAINT [FK_SETUPDepartment_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_Department] CHECK CONSTRAINT [FK_SETUPDepartment_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_FunctionalArea](
	[FA_Id] [varchar](50) NOT NULL,
	[FA_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[FA_Title] [varchar](100) NULL,
	[FA_Active] [int] NULL,
	[FA_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPFunctionalArea_FAId] PRIMARY KEY CLUSTERED 
(
	[FA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_FunctionalArea]  WITH CHECK ADD  CONSTRAINT [FK_SETUPFunctionalArea_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_FunctionalArea] CHECK CONSTRAINT [FK_SETUPFunctionalArea_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_FunctionalArea]  WITH CHECK ADD  CONSTRAINT [FK_SETUPFunctionalArea_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_FunctionalArea] CHECK CONSTRAINT [FK_SETUPFunctionalArea_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_JobTitle](
	[JT_Id] [varchar](50) NOT NULL,
	[JT_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[JT_Title] [varchar](100) NULL,
	[JT_Active] [int] NULL,
	[JT_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPJobTitle_JTId] PRIMARY KEY CLUSTERED 
(
	[JT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_JobTitle]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobTitle_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_JobTitle] CHECK CONSTRAINT [FK_SETUPJobTitle_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_JobTitle]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobTitle_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_JobTitle] CHECK CONSTRAINT [FK_SETUPJobTitle_SETUPLocation_LocId]
GO

CREATE TABLE [dbo].[SETUP_JobPosition](
	[JP_Id] [varchar](50) NOT NULL,
	[JP_Code] [varchar](50) NULL,
	[Cmp_Id] [varchar](50) NULL,
	[Loc_Id] [varchar](50) NULL,
	[Dpt_Id] [varchar](50) NULL,
	[FA_Id] [varchar](50) NULL,
	[JT_Id] [varchar](50) NULL,
	[JP_Title] [varchar](100) NULL,
	[JP_Remarks] [varchar](2000) NULL,
	[JP_Active] [int] NULL,
	[JP_SortOrder] [int] NULL,
 CONSTRAINT [PK_SETUPJobPosition_JPId] PRIMARY KEY CLUSTERED 
(
	[JP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SETUP_JobPosition]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobPosition_SETUPCompany_CmpId] FOREIGN KEY([Cmp_Id])
REFERENCES [dbo].[SETUP_Company] ([Cmp_Id])
GO

ALTER TABLE [dbo].[SETUP_JobPosition] CHECK CONSTRAINT [FK_SETUPJobPosition_SETUPCompany_CmpId]
GO

ALTER TABLE [dbo].[SETUP_JobPosition]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobPosition_SETUPLocation_LocId] FOREIGN KEY([Loc_Id])
REFERENCES [dbo].[SETUP_Location] ([Loc_Id])
GO

ALTER TABLE [dbo].[SETUP_JobPosition] CHECK CONSTRAINT [FK_SETUPJobPosition_SETUPLocation_LocId]
GO

ALTER TABLE [dbo].[SETUP_JobPosition]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobPosition_SETUPDepartment_DptId] FOREIGN KEY([Dpt_Id])
REFERENCES [dbo].[SETUP_Department] ([Dpt_Id])
GO

ALTER TABLE [dbo].[SETUP_JobPosition] CHECK CONSTRAINT [FK_SETUPJobPosition_SETUPDepartment_DptId]
GO

ALTER TABLE [dbo].[SETUP_JobPosition]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobPosition_SETUPFunctionalArea_FAId] FOREIGN KEY([FA_Id])
REFERENCES [dbo].[SETUP_FunctionalArea] ([FA_Id])
GO

ALTER TABLE [dbo].[SETUP_JobPosition] CHECK CONSTRAINT [FK_SETUPJobPosition_SETUPFunctionalArea_FAId]
GO

ALTER TABLE [dbo].[SETUP_JobPosition]  WITH CHECK ADD  CONSTRAINT [FK_SETUPJobPosition_SETUPJobTitle_JTId] FOREIGN KEY([JT_Id])
REFERENCES [dbo].[SETUP_JobTitle] ([JT_Id])
GO

ALTER TABLE [dbo].[SETUP_JobPosition] CHECK CONSTRAINT [FK_SETUPJobPosition_SETUPJobTitle_JTId]
GO


CREATE TABLE [dbo].[Security_UserChartOfAccount](
	[UserCOA_Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGrp_Id] [varchar](50) NULL,
	[User_Id] [varchar](50) NULL,
	[ChrtAcc_Id] [varchar](50) NULL,
 CONSTRAINT [PK_Security_UserChartOfAccount] PRIMARY KEY CLUSTERED 
(
	[UserCOA_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Security_UserChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_Security_UserChartOfAccount_SECURITY_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[SECURITY_User] ([User_Id])
GO

ALTER TABLE [dbo].[Security_UserChartOfAccount] CHECK CONSTRAINT [FK_Security_UserChartOfAccount_SECURITY_User]
GO

ALTER TABLE [dbo].[Security_UserChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_Security_UserChartOfAccount_SECURITY_UserGroup] FOREIGN KEY([UserGrp_Id])
REFERENCES [dbo].[SECURITY_UserGroup] ([UsrGrp_Id])
GO

ALTER TABLE [dbo].[Security_UserChartOfAccount] CHECK CONSTRAINT [FK_Security_UserChartOfAccount_SECURITY_UserGroup]
GO

ALTER TABLE [dbo].[Security_UserChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_Security_UserChartOfAccount_SETUP_ChartOfAccount] FOREIGN KEY([ChrtAcc_Id])
REFERENCES [dbo].[SETUP_ChartOfAccount] ([ChrtAcc_Id])
GO

ALTER TABLE [dbo].[Security_UserChartOfAccount] CHECK CONSTRAINT [FK_Security_UserChartOfAccount_SETUP_ChartOfAccount]
GO




































SET ANSI_PADDING OFF
GO









