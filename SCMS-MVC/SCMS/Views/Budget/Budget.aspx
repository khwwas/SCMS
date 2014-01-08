<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<SCMS.Models.BudgetMaster>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Budget Entry
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContenct" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .ui-autocomplete
        {
            max-height: 200px;
            overflow-y: auto;
            width: auto;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txt_Date').Zebra_DatePicker({
                format: 'm/d/Y'
            });

//            $(".currency").each(function () {
//                $(this).keyup(function () {
//                    $(this).val(formatCurrency($(this).val()));
//                });
//                $(this).val(formatCurrency($(this).val()));
//            });

            $("#btn_AddNewRow").click(function () {
                var Id = parseInt($(".detailRow").last().find("select").attr("id").replace("ListBudgetDetail_", "").replace("__Account", "").trim()) + 1;
                var comboData = $("#AccountCodesList").val().split(',');
                var htmlString = "<div class='detailRow' style='float: left; width: auto;'>";
                htmlString += "<div class='CustomCell' style='width: 260px; height: 30px;'>";
                htmlString += "<select id='ListBudgetDetail_" + Id + "__Account' class='.acc' style='width: 155px;' name='ListBudgetDetail[" + Id + "].Account'";
                for (var index = 0; index < comboData.length; index++) {
                    var comboArr = comboData[index].split(':');
                    htmlString += "<option value='" + comboArr[0] + "'>" + comboArr[1] + "</option>";
                }
                htmlString += "</select>";
                htmlString += "</div>";
                htmlString += "<div class='CustomCell' style='width: 80px; height: 30px;'>";
                htmlString += " <input type='text' class='CustomText' style='width: 60px;' name='ListBudgetDetail[" + Id + "].TotalAmount' value='0' onblur='SetMonthlyAmount(this);' maxlength='50' />";
                htmlString += "</div>";
                for (var index = 0; index < 12; index++) {
                    htmlString += "<div class='CustomCell' style='width: 70px; height: 30px;'>";
                    htmlString += " <input type='text' class='CustomText' style='width: 50px;' name='ListBudgetDetail[" + Id + "].Month" + (parseInt(index) + 1).toString() + "' value='0' onblur='SetTotalAmount(this);' maxlength='50' />";
                    htmlString += "</div>";
                }
                htmlString += "</div>";
                $(".detailRow").last().after(htmlString);
                $("#ListBudgetDetail_" + Id + "__Account").combobox();
            });
        });

        function formatCurrency(num) {
            num = String(num), fnum = new Array();
            num = num.match(/\d/g).reverse();
            i = 1;
            $.each(num, function (k, v) {
                fnum.push(v);
                if (i % 3 == 0) {
                    if (k < (num.length - 1)) {
                        fnum.push(",");
                    }
                }
                i++;
            });

            fnum = fnum.reverse().join("");
            return (fnum);
        }

        $(document).on('submit', "#frm_Budget", function (e) {
            e.preventDefault();
            if ($("#txt_Date").val() == "") {
                FadeIn($("#MessageBox"));
                $("#MessageBox").html("<h5>Error!</h5><p>Please select date.</p>");
                $("#MessageBox").attr("class", "message error");
                scroll(0, 0);
                FadeOut($("#MessageBox"));
                $("#txt_Date").focus();
                return;
            }
            $("#Waiting_Image").show();
            $("#btn_Save").hide();
            $("#BudgetType").removeAttr("disabled");
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),

                success: function (result) {
                    FadeIn("#MessageBox");
                    if (result != null && result != "" && result != "0") {

                        $("#txt_BudgetMasterId").val(result.split(":")[0]);
                        $("#txt_Code").val(result.split(":")[1]);
                        $("#MessageBox").html("<h5>Success!</h5><p>Record saved successfully.</p>");
                        $("#MessageBox").attr("class", "message success");
                    }
                    else {
                        $("#MessageBox").html("<h5>Error!</h5><p>Unable to save record.</p>");
                        $("#MessageBox").attr("class", "message error");
                    }
                    $("#Waiting_Image").hide();
                    $("#btn_Save").show();
                    $("#BudgetType").attr("disabled", "disabled");
                    scroll(0, 0);
                    FadeOut("#MessageBox");
                },
                error: function (result) {
                    alert("Error Occured!")
                }
            });

            return false;
        });

        function ResetForm() {
            window.location = "../Budget";
        }

        function SetMonthlyAmount(obj) {
            if ($(obj).val() != "") {
                var TotalVal = parseInt($(obj).val());
                var PermonthVal = parseInt(TotalVal / 12);
                for (var index = 1; index <= 11; index++) {
                    $("[name='" + $(obj).attr("name").replace("TotalAmount", "Month" + index.toString()) + "']").val(PermonthVal);
                }
                index = 12;
                $("[name='" + $(obj).attr("name").replace("TotalAmount", "Month" + index.toString()) + "']").val(TotalVal - (PermonthVal * 11));
            }
        }

        function SetTotalAmount(obj) {
            var Id = $(obj).attr("name").replace("ListBudgetDetail[", "").replace("].Month12", "").replace("].Month11", "").replace("].Month10", "").replace("].Month9", "").replace("].Month8", "").replace("].Month7", "").replace("].Month6", "").replace("].Month5", "").replace("].Month4", "").replace("].Month3", "").replace("].Month2", "").replace("].Month1", "");
            var TotalVal = 0;
            for (var index = 1; index <= 12; index++) {
                TotalVal += parseInt($("[name='ListBudgetDetail[" + Id + "].Month" + index + "']").val());
            }
            $("[name='ListBudgetDetail[" + Id + "].TotalAmount']").val(TotalVal);
        }
    </script>
    <form id="frm_Budget" action='<%=Url.Content("~/Budget/SaveBudget") %>' method="post">
    <input type="hidden" id="txt_BudgetMasterId" name="MasterId" value='<%=Model.MasterId %>' />
    <div class="box round first fullpage grid">
        <h2>
            Budget Entry</h2>
        <div class="block">
            <div id="MessageBox" style="position: fixed; top: 0px; width: 95%;">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Budget #</div>
            <div class="CustomCell" style="width: 320px; height: 30px;">
                <%=Html.TextBoxFor(m => m.Code, new { @class = "CustomText", @style = "width: 250px; font-weight: bold;", @id = "txt_Code", @maxlength = "15", @readonly = "readonly" })%>
            </div>
            <div class="CustomCell" style="width: 40px; height: 30px;">
                Date</div>
            <div class="CustomCell" style="width: 278px; height: 30px;">
                <%=Html.TextBoxFor(m => m.Date, new { @class = "CustomText", @style = "width: 220px;", @id = "txt_Date", @maxlength = "12" })%>
            </div>
        </div>
        <div class="CustomCell" style="width: 54px; height: 30px;">
            Status</div>
        <div class="CustomCell" style="width: 320px; height: 30px;">
            <select id="ddl_Status" name="Status" class="CustomText" style="width: 251px;">
                <option value="Pending">Pending </option>
                <option value="Approved" <%=Model.Status=="Approved"?"selected":"" %>>Approved </option>
            </select>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Budget Type</div>
        <div class="CustomCell" style="width: 320px; height: 30px;">
            <%= Html.DropDownList("BudgetType", null, new { @style = "width:265px;", @disabled = "disabled" })%>
        </div>
        <div class="CustomCell" style="width: 40px; height: 30px;">
            Year</div>
        <div class="CustomCell" style="width: 278px; height: 30px;">
            <%= Html.DropDownList("CalendarYear", null, new { style = "width:235px;" })%>
        </div>
        <div class="CustomCell" style="width: 54px; height: 30px;">
            Location
        </div>
        <div class="CustomCell" style="width: 320px; height: 30px;">
            <%= Html.DropDownList("Location", null, new { style = "width:251px;" })%>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Remarks</div>
        <div class="CustomCell" style="width: 960px; height: 30px;">
            <%=Html.TextBoxFor(m => m.Remarks, new { @class = "CustomText", @style = "width: 942px;", @id = "txt_Remarks", @maxlength = "180" })%>
        </div>
        <hr style="padding: 0; margin-bottom: 5px;" />
        <div class="CustomCell" style="width: 260px;">
            Account Title
        </div>
        <div class="CustomCell" style="width: 80px; text-align: center">
            Budget Approved</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Jan</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Feb</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Mar</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Apr</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            May</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Jun</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Jul</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Aug</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Sep</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Oct</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Nov</div>
        <div class="CustomCell" style="width: 70px; text-align: center">
            Dec</div>
        <div class="Clear">
        </div>
        <div id="DetailContainer">
            <%string AccountCodes = ViewData["ChartOfAccountCodesWithTitles"].ToString().Replace("'", "&#39"); %>
            <input type="hidden" id="AccountCodesList" name="AccountCodesList" value='<%=AccountCodes %>' />
            <% for (int index = 0; index < Model.ListBudgetDetail.Count; index++)
               {%>
            <div class="detailRow" style="float: left; width: auto;">
                <% ViewData["ListBudgetDetail[" + index + "].Account"] = new SelectList((List<SCMSDataLayer.DB.SETUP_ChartOfAccount>)ViewData["ddl_Account"], "ChrtAcc_Id", "ChrtAcc_Title", Model.ListBudgetDetail[index].Account); %>
                <div class="CustomCell" style="width: 260px; height: 30px;">
                    <%= Html.DropDownList("ListBudgetDetail[" + index + "].Account", null, new { @style = "width:155px;",@class=".acc" })%>
                    <script type="text/jscript">
                        $(document).ready(function () {
                            $("#ListBudgetDetail_<%=index %>__Account").combobox();
                        });
                    </script>
                </div>
                <div class="CustomCell" style="width: 80px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 60px;" name="ListBudgetDetail[<%=index %>].TotalAmount"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].TotalAmount) %>" maxlength="50"
                        onblur="SetMonthlyAmount(this);" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month1"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month1) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month2"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month2) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month3"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month3) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month4"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month4) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month5"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month5) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month6"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month6) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month7"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month7) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month8"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month8) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month9"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month9) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month10"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month10) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month11"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month11) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 70px; height: 30px;">
                    <input type="text" class="CustomText currency" style="width: 50px;" name="ListBudgetDetail[<%=index %>].Month12"
                        value="<%=Convert.ToInt32(Model.ListBudgetDetail[index].Month12) %>" onblur="SetTotalAmount(this);"
                        maxlength="50" />
                </div>
            </div>
            <%} %>
            <div class="img_Container" style="float: left;">
                <img id="btn_AddNewRow" alt="Add New" src="../../img/add.png" style="width: 30px;
                    cursor: pointer;" />
            </div>
            <div class="Clear">
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="submit" value="Save" class="btn btn-blue" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="New" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
