<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportWork.aspx.cs" Inherits="DDWeb.ReportWork" %>

<!DOCTYPE html>
<html lang="en">

<head>
	<title>Home</title>
	<meta charset="utf-8">

	<meta name="viewport" content="width=device-width, initial-scale=1">

	<!-- jQuery & jQuery UI are required -->
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
	<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

	<!-- Flowchart CSS and JS -->
	<link rel="stylesheet" href="../Content/jquery.flowchart.css">
	<script src="../Scripts/jquery.flowchart.js"></script>

	<style>
		.flowchart-example-container {
			width: 800px;
			height: 400px;
			background: white;
			border: 1px solid #BBB;
			margin-bottom: 10px;
		}
	</style>
</head>

<body>
	<h1>報表編輯</h1>
	<div>報表名稱: </div>
	<input type="text" id="report_name" />
	<h3>報表流程</h3>
	<div id="chart_container">
		<div class="flowchart-example-container" id="flowchartworkspace"></div>
	</div>
	<div class="draggable_operators">
		<div class="draggable_operators_label">
			Operators (drag and drop them in the flowchart):
		</div>
	</div>
	<button class="create_operator">Create table</button>
	<button class="delete_selected_button">Delete selected table / link</button>
	<div id="operator_properties" style="display: block;">
		<label for="operator_title">Operator's title: </label><input id="operator_title" type="text">
	</div>
	<div id="link_properties" style="display: block;">
		<label for="link_color">Link's color: </label><input id="link_color" type="color">
	</div>
	<button class="get_data" id="get_data">Get data</button>
	<button class="set_data" id="set_data">Set data</button>
	<button id="save_local">Save to local storage</button>
	<button id="load_local">Load from local storage</button>
	<div>
		<textarea id="flowchart_data"></textarea>
	</div>
	<select name="table_name" id="table_name" runat="server" AutoPostBack="True"   onchange="getmysqlname('table_name',document.getElementById('table_name').value,'information_schema.columns','table_name','column_name',1,'col_test');"></select>
	<script type="text/javascript">
		/* global $ */
		$(document).ready(function() {
			var $flowchart = $('#flowchartworkspace');
			var $container = $flowchart.parent();


			// Apply the plugin on a standard, empty div...
			$flowchart.flowchart({
				data: defaultFlowchartData,
				defaultSelectedLinkColor: '#000055',
				grid: 10,
				multipleLinksOnInput: true,
				multipleLinksOnOutput: true
			});


			function getOperatorData($element) {
				var nbInputs = parseInt($element.data('nb-inputs'), 10);
				var nbOutputs = parseInt($element.data('nb-outputs'), 10);
				var data = {
					properties: {
						title: $element.text(),
						inputs: {},
						outputs: {}
					}
				};

				var i = 0;
				for (i = 0; i < nbInputs; i++) {
					data.properties.inputs['input_' + i] = {
						label: 'Input ' + (i + 1)
					};
				}
				for (i = 0; i < nbOutputs; i++) {
					data.properties.outputs['output_' + i] = {
						label: 'Output ' + (i + 1)
					};
				}

				return data;
			}



			//-----------------------------------------
			//--- operator and link properties
			//--- start
			var $operatorProperties = $('#operator_properties');
			$operatorProperties.hide();
			var $linkProperties = $('#link_properties');
			$linkProperties.hide();
			var $operatorTitle = $('#operator_title');
			var $linkColor = $('#link_color');

			$flowchart.flowchart({
				onOperatorSelect: function(operatorId) {
					$operatorProperties.show();
					$operatorTitle.val($flowchart.flowchart('getOperatorTitle', operatorId));
					return true;
				},
				onOperatorUnselect: function() {
					$operatorProperties.hide();
					return true;
				},
				onLinkSelect: function(linkId) {
					$linkProperties.show();
					$linkColor.val($flowchart.flowchart('getLinkMainColor', linkId));
					return true;
				},
				onLinkUnselect: function() {
					$linkProperties.hide();
					return true;
				}
			});

			$operatorTitle.keyup(function() {
				var selectedOperatorId = $flowchart.flowchart('getSelectedOperatorId');
				if (selectedOperatorId != null) {
					$flowchart.flowchart('setOperatorTitle', selectedOperatorId, $operatorTitle.val());
				}
			});

			$linkColor.change(function() {
				var selectedLinkId = $flowchart.flowchart('getSelectedLinkId');
				if (selectedLinkId != null) {
					$flowchart.flowchart('setLinkMainColor', selectedLinkId, $linkColor.val());
				}
			});
			//--- end
			//--- operator and link properties
			//-----------------------------------------

			//-----------------------------------------
			//--- delete operator / link button
			//--- start
			$flowchart.parent().siblings('.delete_selected_button').click(function() {
				$flowchart.flowchart('deleteSelected');
			});
			//--- end
			//--- delete operator / link button
			//-----------------------------------------



			//-----------------------------------------
			//--- create operator button
			//--- start
			var operatorI = 0;
			$flowchart.parent().siblings('.create_operator').click(function () {
				var operatorId = 'created_operator_' + operatorI;
				var operatorData = {
					top: ($flowchart.height() / 2) - 30,
					left: ($flowchart.width() / 2) - 100 + (operatorI * 10),
					properties: {
						title: 'Table ' + (operatorI + 3),
						inputs: {
							input_1: {
								label: 'Column 1',
							}
						},
						outputs: {
							output_1: {
								label: 'Column 1',
							}
						}
					}
				};

				operatorI++;

				$flowchart.flowchart('createOperator', operatorId, operatorData);

			});
			//--- end
			//--- create operator button
			//-----------------------------------------




			//-----------------------------------------
			//--- draggable operators
			//--- start
			//var operatorId = 0;
			var $draggableOperators = $('.draggable_operator');
			$draggableOperators.draggable({
				cursor: "move",
				opacity: 0.7,

				// helper: 'clone',
				appendTo: 'body',
				zIndex: 1000,

				helper: function(e) {
					var $this = $(this);
					var data = getOperatorData($this);
					return $flowchart.flowchart('getOperatorElement', data);
				},
				stop: function(e, ui) {
					var $this = $(this);
					var elOffset = ui.offset;
					var containerOffset = $container.offset();
					if (elOffset.left > containerOffset.left &&
						elOffset.top > containerOffset.top &&
						elOffset.left < containerOffset.left + $container.width() &&
						elOffset.top < containerOffset.top + $container.height()) {

						var flowchartOffset = $flowchart.offset();

						var relativeLeft = elOffset.left - flowchartOffset.left;
						var relativeTop = elOffset.top - flowchartOffset.top;

						var positionRatio = $flowchart.flowchart('getPositionRatio');
						relativeLeft /= positionRatio;
						relativeTop /= positionRatio;

						var data = getOperatorData($this);
						data.left = relativeLeft;
						data.top = relativeTop;

						$flowchart.flowchart('addOperator', data);
					}
				}
			});
			//--- end
			//--- draggable operators
			//-----------------------------------------


			//-----------------------------------------
			//--- save and load
			//--- start
			function Flow2Text() {
				var data = $flowchart.flowchart('getData');
				$('#flowchart_data').val(JSON.stringify(data, null, 2));
			}
			$('#get_data').click(Flow2Text);

			function Text2Flow() {
				var data = JSON.parse($('#flowchart_data').val());
				$flowchart.flowchart('setData', data);
			}
			$('#set_data').click(Text2Flow);

			/*global localStorage*/
			function SaveToLocalStorage() {
				if (typeof localStorage !== 'object') {
					alert('local storage not available');
					return;
				}
				Flow2Text();
				localStorage.setItem("stgLocalFlowChart", $('#flowchart_data').val());
			}
			$('#save_local').click(SaveToLocalStorage);

			function LoadFromLocalStorage() {
				if (typeof localStorage !== 'object') {
					alert('local storage not available');
					return;
				}
				var s = localStorage.getItem("stgLocalFlowChart");
				if (s != null) {
					$('#flowchart_data').val(s);
					Text2Flow();
				}
				else {
					alert('local storage empty');
				}
			}
			$('#load_local').click(LoadFromLocalStorage);
			//--- end
			//--- save and load
			//-----------------------------------------


		});


		var defaultFlowchartData = {operators: {
	operator1: {top: 20,left: 20,properties: {title: 'Table 1',inputs: {},outputs: {	output_1: {	label: 'Column 1',		}
						}
					}
				},
				operator2: {
					top: 80,
					left: 300,
					properties: {
						title: 'Table 2',
						inputs: {
							input_1: {
								label: 'Column 1',
							},
							input_2: {
								label: 'Column 2',
							},
						},
						outputs: {}
					}
				},
			},
			links: {
				link_1: {
					fromOperator: 'operator1',
					fromConnector: 'output_1',
					toOperator: 'operator2',
					toConnector: 'input_2',
				},
			}
		};
		
		if (false) console.log('remove lint unused warning', defaultFlowchartData);
		function getmysqlname(nowid, pvalue, ptable, pkey, pfields, pfqty, id1, id2, id3, id4, id5, id6, id7) {
            var myColName = []; 
            //pgetno需搭配getsqlname.aspx設定對應的代號,每個代號代表讀取的內容也不同
            //後面接著是id的名稱
            var xhttp;
            var nowobj = document.getElementById(nowid);
            if (window.XMLHttpRequest) {
                // code for modern browsers
                xhttp = new XMLHttpRequest();
            } else {
                // code for IE6, IE5
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
					var myobj = JSON.parse(this.responseText);
                    alert(this.responseText);
                    if (typeof id1 != 'undefined') {
                        if (myobj.fld1 != '') {
							document.getElementById(id1).value = myobj.fld1;
							var str = myobj.fld1;
							do {
								myColName.push(str.substring(0, str.indexOf(',')));
								str = str.substring(str.indexOf(',') +1, str.length);
                                // code block to be executed
                            }
							while (str.indexOf(',') != -1);
							if (str != '') {
                                myColName.push(str);
							}
                            localStorage.setItem('myColName', JSON.stringify(myColName));
                            //alert(localStorage.getItem('myColName'));
                        }
                    }
                    if (typeof id2 != 'undefined') {
                        if (myobj.fld2 != '') {
                            document.getElementById(id2).value = myobj.fld2;
                        }
                    }
                    if (typeof id3 != 'undefined') {
                        if (myobj.fld3 != '') {
                            document.getElementById(id3).value = myobj.fld3;
                        }
                    }
                    if (typeof id4 != 'undefined') {
                        if (myobj.fld4 != '') {
                            document.getElementById(id4).value = myobj.fld4;
                        }
                    }
                    if (typeof id5 != 'undefined') {
                        if (myobj.fld5 != '') {
                            document.getElementById(id5).value = myobj.fld5;
                        }
                    }
                    if (typeof id6 != 'undefined') {
                        if (myobj.fld6 != '') {
                            document.getElementById(id6).value = myobj.fld6;
                        }
                    }
                    if (typeof id7 != 'undefined') {
                        if (myobj.fld7 != '') {
                            document.getElementById(id7).value = myobj.fld7;
                        }
                    }
                }
            };
            var rText = encodeURI(pvalue);
            xhttp.open("POST", "api/getsqlname?v=" + rText + "&t=" + ptable + "&k=" + pkey + "&s=" + pfields + "&n=" + pfqty, true);
            //xhttp.open("GET", "getsqlname.aspx?v=" + pvalue + "&t=" + ptable + "&k=" + pkey + "&s=" + pfields + "&n=" + pfqty, true);
            xhttp.send();
        }
    </script>
  <input name="columns" type="hidden" id="columns" value="" runat="server"/>
  <input name="col_test" type ="text" id="col_test" runat ="server">
	<input name="contact" type="textarea" id="column_def" rows="4" cols="50" class="auto-style6" tabindex="2" runat="server"/>
</body>

</html>


