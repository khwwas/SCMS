/****** Object:  StoredProcedure [dbo].[sp_GetUserGroupList]    Script Date: 02/05/2013 21:37:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUserGroupList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From SECURITY_UserGroup 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_GetUserList]    Script Date: 02/06/2013 01:14:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUserList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SECURITY_User,
	       SECURITY_UserGroup
	 Where ( SECURITY_User.UsrGrp_Id = SECURITY_UserGroup.UsrGrp_Id ) 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_GetUserMenuRights]    Script Date: 04/11/2013 20:37:47 ******/
-- =============================================
-- Author:		<Author,,Waqas Akhtar>
-- Create date: 13/03/2013
-- Description:	Create for retrieving data from usermenuoptions and selected menu options for groups
-- =============================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_GetUserMenuRights](@GroupId varchar(50))
As
Begin
  Select Security_MenuOptions.Mnu_Id,
         Security_MenuOptions.Mnu_Description,
         Security_MenuOptions.Mnu_TargetUrl,
         Security_MenuOptions.Mnu_Level,
         Security_MenuOptions.Mnu_SortOrder,
         Security_MenuOptions.Mnu_IsLineBreak,
         Security_MenuOptions.Mod_Id,
         IsNull(Security_UserRights.Mnu_Id,0) as SelectedMenu 
    From Security_MenuOptions Left Outer Join Security_UserRights On
              ( Security_UserRights.Mnu_Id = Security_MenuOptions.Mnu_Id ) And
              ( Security_UserRights.Grp_Id = @GroupId )
Order By Security_MenuOptions.Mnu_SortOrder
 End
 GO



/****** Object:  StoredProcedure [dbo].[sp_GetCompanyList]    Script Date: 02/05/2013 21:18:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCompanyList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From SETUP_Company 
END

GO

/****** Object:  StoredProcedure [dbo].[sp_GetLocationList]    Script Date: 02/05/2013 21:18:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetLocationList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SETUP_Location,
	       SETUP_Company 
	 Where ( SETUP_Location.Cmp_Id = SETUP_Company.Cmp_Id )
END
GO









/****** Object:  StoredProcedure [dbo].[sp_GetCityList]    Script Date: 02/05/2013 21:18:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCityList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From SETUP_City 
END
GO

/****** Object:  StoredProcedure [dbo].[sp_GetVoucherTypesList]    Script Date: 02/23/2013 11:26:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetVoucherTypesList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	 From SETUP_VoucherType Left Join SETUP_Location On 
	           SETUP_VoucherType.Loc_Id = SETUP_Location.Loc_Id
END

GO

/****** Object:  StoredProcedure [dbo].[sp_GetVoucherTypeNarrationList]    Script Date: 03/02/2013 21:34:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetVoucherTypeNarrationList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SETUP_VoucherTypeNarration,
	       SETUP_VoucherType 
	 Where ( SETUP_VoucherTypeNarration.VchrType_Id = SETUP_VoucherType.VchrType_Id )
END


GO














/****** Object:  StoredProcedure [dbo].[sp_VoucherEntryConsole]    Script Date: 10/08/2013 23:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_VoucherEntryConsole]
@AllLocation int, @LocationId varchar(10), 
@AllVoucherType int, @VoucherTypeId varchar(10), 
@AllDate int, @DateFrom varchar, @DateTo varchar 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  SELECT SETUP_Location.Loc_Id,
	         SETUP_Location.Loc_Title,   
	         SETUP_VoucherType.VchrType_Id,
			 SETUP_VoucherType.VchrType_Title,
			 GL_VchrMaster.VchMas_Id,   
			 GL_VchrMaster.VchMas_Code,   
			 GL_VchrMaster.VchMas_Date,   
			 GL_VchrMaster.VchMas_Remarks,   
			 GL_VchrMaster.VchMas_Status,
			 ( Select ISNULL( Round( Sum( ISNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) ), 0 ), 0 )
			     From GL_VchrDetail
			    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) 
			 ) As TotalDrAmount,
			 ( Select ISNULL( Sum( ISNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) ), 0 )
			     From GL_VchrDetail
			    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) 
			 ) As TotalCrAmount,
			 ( Select ISNULL( Sum( ISNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) ), 0 ) - 
			          ISNULL( Sum( ISNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) ), 0 )
			     From GL_VchrDetail
			    Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) 
			 ) As DifferenceAmount
		FROM GL_VchrMaster,   
			 SETUP_Location,   
			 SETUP_VoucherType  
	   WHERE ( SETUP_Location.Loc_Id = GL_VchrMaster.Loc_Id ) and  
			 ( SETUP_VoucherType.VchrType_Id = GL_VchrMaster.VchrType_Id ) and
			 ( @AllLocation = 1 Or SETUP_Location.Loc_Id =  @LocationId ) and
			 ( @AllVoucherType = 1 or SETUP_VoucherType.VchrType_Id = @VoucherTypeId ) and
			 ( @AllDate = 1 Or CONVERT( DateTime, GL_VchrMaster.VchMas_Date, 103 )
			   BETWEEN CONVERT(DateTime, @DateFrom, 103 ) and CONVERT(DateTime, @DateTo, 103 ) )
	Order By SETUP_Location.Loc_Title,   
			 SETUP_VoucherType.VchrType_Title,
			 GL_VchrMaster.VchMas_Date;

END
GO





















/****** Object:  StoredProcedure [dbo].[sp_GetFunctionalAreaList]    Script Date: 03/31/2013 16:23:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetFunctionalAreaList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SETUP_FunctionalArea Left Outer Join SETUP_Location On
	            SETUP_FunctionalArea.Loc_Id = SETUP_Location.Loc_Id
END


/****** Object:  StoredProcedure [dbo].[sp_GetJobPositionList]    Script Date: 03/31/2013 16:23:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetJobPositionList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SETUP_JobPosition LEFT JOIN SETUP_Location ON 
	            SETUP_JobPosition.Loc_Id = SETUP_Location.Loc_Id
	       LEFT JOIN SETUP_Department ON 
		        SETUP_JobPosition.Dpt_Id = SETUP_Department.Dpt_Id
	       LEFT JOIN SETUP_FunctionalArea ON 
		        SETUP_JobPosition.FA_Id = SETUP_FunctionalArea.FA_Id
	       LEFT JOIN SETUP_JobTitle ON 
		        SETUP_JobPosition.JT_Id = SETUP_JobTitle.JT_Id
	   
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_GetUserChartOfAccountByGroupId](@GroupId varchar(50))
As
Begin
  Select SETUP_ChartOfAccount.*,
  Security_UserChartOfAccount.UserGrp_Id,
  Security_UserChartOfAccount.User_Id,
         IsNull(Security_UserChartOfAccount.ChrtAcc_Id,0) as SelectedChartOfAccount
    From SETUP_ChartOfAccount Left Outer Join Security_UserChartOfAccount On
              ( Security_UserChartOfAccount.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And
              ( Security_UserChartOfAccount.UserGrp_Id = @GroupId )
Order By 
         SETUP_ChartOfAccount.ChrtAcc_Code,SETUP_ChartOfAccount.ChrtAcc_Level
 End 
 GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_GetUserChartOfAccountByUserId](@UserId varchar(50))
As
Begin
  Select SETUP_ChartOfAccount.*,
  Security_UserChartOfAccount.UserGrp_Id,
  Security_UserChartOfAccount.User_Id,
         IsNull(Security_UserChartOfAccount.ChrtAcc_Id,0) as SelectedChartOfAccount
    From SETUP_ChartOfAccount Left Outer Join Security_UserChartOfAccount On
              ( Security_UserChartOfAccount.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And
              ( Security_UserChartOfAccount.User_Id = @UserId )
Order By 
         SETUP_ChartOfAccount.ChrtAcc_Code,SETUP_ChartOfAccount.ChrtAcc_Level
End 
GO




/****** Object:  StoredProcedure [dbo].[sp_BankReconciliation]    Script Date: 10/08/2013 21:22:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_BankReconciliation]
@AllLocation int, @LocationId varchar, 
@AllDate int, @DateFrom varchar, @DateTo varchar 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Select Distinct SETUP_Location.Loc_Id,
	       SETUP_Location.Loc_Title,
	       SETUP_Bank.Bank_Title + ' [' + SETUP_BankAccount.BankAcc_Title + ']' As Bank_Title,
	       GL_VchrMaster.VchMas_Id,
	       GL_VchrMaster.VchMas_Date,
	       GL_VchrMaster.VchMas_Code,
	       IsNULL( ( Select Sum( IsNULL( GL_VchrDetail.VchMas_CrAmount, 0 ) )
	                   From GL_VchrDetail
	                  Where ( GL_VchrMaster.VchMas_Id = GL_VchrDetail.VchMas_Id ) And
	                        ( ISNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) <= 0 )
	       ), 0 ) As VchMas_CrAmount,
	       GL_VchrMaster.VchMas_Remarks,
	       GL_VchrMaster.VchMas_Reconciliation,
	       GL_VchrMaster.VchMas_ReconciliationDate
	  From Setup_bank,
	       SETUP_BankAccount,
	       SETUP_ChartOfAccount,
	       GL_VchrDetail,
	       GL_VchrMaster,
	       SETUP_Location 
	 Where ( SETUP_BankAccount.Bank_Id = SETUP_Bank.Bank_Id ) And
	       ( SETUP_BankAccount.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And
	       ( GL_VchrDetail.ChrtAcc_Id = SETUP_ChartOfAccount.ChrtAcc_Id ) And
	       ( GL_VchrDetail.VchMas_Id = GL_VchrMaster.VchMas_Id ) And
	       ( Lower( Left( GL_VchrMaster.VchMas_Code, 3 ) ) = Lower( 'BPV' ) ) And
	       ( GL_VchrMaster.Loc_Id = SETUP_Location.Loc_Id ) And
		   ( ISNULL( GL_VchrDetail.VchMas_DrAmount, 0 ) <= 0 ) And
		   ( @AllLocation = 1 Or SETUP_Location.Loc_Id =  @LocationId ) and
		   ( @AllDate = 1 Or CONVERT( DateTime, GL_VchrMaster.VchMas_Date, 103 )
		     BETWEEN CONVERT(DateTime, @DateFrom, 103 ) and CONVERT(DateTime, @DateTo, 103 ) )
  Order By SETUP_Location.Loc_Title,   
           Bank_Title,
		   GL_VchrMaster.VchMas_Date;

END





































/****** Object:  StoredProcedure [dbo].[sp_GetLeaveTypesList]    Script Date: 04/12/2013 00:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLeaveTypesList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	  From SETUP_LeaveType Left Join
	       SETUP_Location 
	 On SETUP_LeaveType.Loc_Id = SETUP_Location.Loc_Id
END
Go


