<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Supplier Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var ddl_location = document.getElementById('ddl_location');
            var ddl_SupplierType = document.getElementById('ddl_SupplierType');
            var txt_Title = document.getElementById('txt_Title');
            var txt_Address = document.getElementById('txt_Address');
            var txt_Email = document.getElementById('txt_Email');
            var txt_Phone = document.getElementById('txt_Phone');
            var txt_Fax = document.getElementById('txt_Fax');

            if (ddl_location.value == 0) {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Select Location</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                ddl_location.focus();
                return;
            } else if (ddl_SupplierType.value == 0) {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Select Custome Type</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                ddl_CustomerType.focus();
                return;
            } else if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            } 
            else {
                var Url = document.getElementById('frm_SupplierSetup').action;
                Url += "Supplier/SaveSupplierRecord?Code=" + lcnt_txtSelectedCode.value + "&location=" + ddl_location.value + "&SupplierType=" + ddl_SupplierType.value + "&Title=" + txt_Title.value + "&Address=" + txt_Address.value + "&Email=" + txt_Email.value + "&Phone=" + txt_Phone.value + "&Fax=" + txt_Fax.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        alert(document.getElementById("SaveResult").value);
                        if (document.getElementById("SaveResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        }

        function ResetForm() {
            var lcnt_MessageBox = document.getElementById('MessageBox');

            lcnt_MessageBox.removeAttribute("class");
            lcnt_MessageBox.innerHTML = "";

            document.getElementById('txt_SelectedCode').value = "";
            document.getElementById('ddl_location').value = "";
            document.getElementById('ddl_SupplierType').value = "";
            document.getElementById('txt_Title').value = "";
            document.getElementById('txt_Address').value = "";
            document.getElementById('txt_Email').value = "";
            document.getElementById('txt_Phone').value = "";
            document.getElementById('txt_Fax').value = "";
            
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            //document.getElementById('txt_Code').value = Id;
            document.getElementById('ddl_location').value = document.getElementById('ddl_location' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_SupplierType').value = document.getElementById('ddl_SupplierType' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Address').value = document.getElementById('txt_Address' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Email').value = document.getElementById('txt_Email' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Phone').value = document.getElementById('txt_Phone' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Fax').value = document.getElementById('txt_Fax' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_SupplierSetup').action;

                Url += "Supplier/DeleteRecord?SuppID=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        if (document.getElementById("SaveResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    },
                    error: function (rs, e) {
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    }
                });
            }
        }

    </script>
    <form id="frm_SupplierSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Supplier Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                maxlength="100" value="[Auto]" readonly="readonly" /> 
                 <div class="Clear">
            </div>
                <div class="CustomCell" style="width: 115px; height: 30px;">
                Location</div>
                <%= Html.DropDownList("ddl_location", null, new { style = "width:900px; padding: 4px;" })%>
                <div class="Clear">
            </div>
                <div class="CustomCell" style="width: 115px; height: 30px;">
                Supplier Type</div>
                <%= Html.DropDownList("ddl_SupplierType", null, new { style = "width:900px; padding: 4px;" })%>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 107px; height: 30px; margin-top:8px;">
                Title</div>
            <input type="text" class="CustomText" style="width: 890px; margin-left:7px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 107px; height: 30px; margin-top:8px;">
                Address</div>
            <input type="text" class="CustomText" style="width: 890px; margin-left:7px;" id="txt_Address" name="txt_Address"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell">
                Email</div>
            <div class="CustomCell">
                Phone</div>
            <div class="CustomCell">
                Fax</div>
            <div class="Clear">
            </div>
            <input type="text" class="CustomText" id="txt_Email" name="txt_Email" maxlength="100" />
            <input type="text" class="CustomText" id="txt_Phone" name="txt_Phone" maxlength="100" />
            <input type="text" class="CustomText" id="txt_Fax" name="txt_Fax" maxlength="100" />
            <div class="Clear">
            </div>     
            
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="Cancel" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
            <hr />
            <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
