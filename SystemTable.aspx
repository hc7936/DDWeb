<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemTable.aspx.cs" Inherits="DDWeb.SystemTable" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javaScript'>
function checkstatus(control) {
        var ItemStatusString = "";
        var obj = document.getElementById('ItemStatus');
        var inputs = document.getElementsByTagName('input');
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == 'checkbox') {
                if(inputs[i].checked)
                  ItemStatusString += "1";
                else  
                  ItemStatusString += "0";
             }
        }
	    obj.value = ItemStatusString;
}
function changestatus(control) {
        if (control.checked){
           var inputs = document.getElementsByTagName('input');
           for (var i = 1; i < inputs.length; i++) {
               if (inputs[i].type == 'checkbox') {
                  if(!inputs[i].checked)
                    inputs[i].checked=true ;
               }
            }   
        }else{
           var inputs = document.getElementsByTagName('input');
           for (var i = 1; i < inputs.length; i++) {
               if (inputs[i].type == 'checkbox') {
                  if(inputs[i].checked)
                    inputs[i].checked=false;
               }
            }  
        }
}
    </script>
<body>
<div class="content_c_l"></div>
<div class="content_c">
<div id="title">系統檔案列表
  <input name="excelButton" id="excelButton" type="button" class="butc_admin" value="匯出Excel" runat="server" />
</div>
<div class="t_list_c">

      單頁
<input name="limit" type="text" class="required" id="limit" style="width:30px;" value="50" runat="server" />
        筆 / 關鍵字搜尋
       
        <input name="search_word" type="text"  id="search_word" style="width:300px;" value="" runat="server"  />
        <input type="submit" name="button" id="button" class="butc_search"  value="送出" runat="server"  />
	</div>
<div>

</div>


<div id="list">
<asp:GridView ID="dvSys" runat="server" CellPadding="4" ForeColor="#333333" 
    GridLines="None" AllowPaging="True" PageSize="50" OnPageIndexChanging="dvSys_PageIndexChanging" OnRowEditing="dvSys_RowEditing">
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <Columns>
        <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="編輯" />
            <asp:TemplateField ShowHeader="False">
            </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#999999" />
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
</asp:GridView>	
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>			
</div>
	<div></div>
	<div>
	<input name="type" type="hidden" id="type" value="cur" />
	<input name="dosel" type="hidden" id="dosel" value="Y">
	<input name="dostyle" type="hidden" id="dostyle" value="1">
	<input name="hm" type="hidden" id="hm" value="1">
    <input name="s_idept" type="hidden" id="hm" value="">
    <input name="s_lang" type="hidden" id="hm" value="">
    </div>
</div>

</asp:Content>


