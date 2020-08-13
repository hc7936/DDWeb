<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataConnect.aspx.cs" Inherits="DDWeb.DataConnect" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 190px;
        }
        .auto-style4 {
            width: 190px;
            height: 37px;
        }
        .auto-style5 {
            height: 37px;
        }
        .auto-style6 {
            width: 228px;
            height: 125px;
        }
    </style>
<div class="content_c_l"></div>
<div class="content_c">
	<div id="title">資料連接 > 編輯</div>
    <div class="sp10"></div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" class="tcss" >
      <tr>
        <th class="auto-style1">連接主機：</th>
        <td><input name="connect_server" type="text" id="connect_server" style="width:400px; height: 30px;" class="required" tabindex="1" runat="server"/></td>
      </tr>
      <tr>
        <th class="auto-style1">帳戶：</th>
        <td><input name="connect_acc" type="text" id="connect_acc" style="width:228px; height: 30px;" class="required" tabindex="1" runat="server"/></td>
      </tr>
      <tr>
        <th class="auto-style1">密碼：</th>
        <td><input name="connect_pass" type="password" id="connect_pass" style="width:228px; height: 30px;" class="required" tabindex="1" runat="server"/></td>
      </tr>
      <tr>
        <td colspan="2" align="left">
          
          <input name="button" type="submit" id="button"  class="butc_ok"  value="  確定新增  " runat="server"/>
          
          <input name="Submit3" id="Submit3" type="button" class="butc_clear" value="  返回  " style="cursor:hand" runat="server" />
          <input name="doing" type="hidden" id="doing" value="1" runat="server"/>
          <input name="dosts" type="hidden" id="dosts" value="new" runat="server"/>
          <input name="search_word" type="hidden" id="search_word" value="" runat="server"/>
          <input name="s_idept" type="hidden" id="hm" value="">
			<input name="s_lang" type="hidden" id="hm" value="">
        </td>
      </tr>
    </table>
</div>

    </asp:Content>
