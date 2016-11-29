var mShiftKeyOn = false;
var mCtrlKeyOn = false;
var mLastRow = null;

var TRAILERCUBE = 5293555, TRAILERWEIGHT = 42500;
var CARTONSINDEX = 10, PALLETSINDEX = 11, WEIGHTINDEX = 12, CUBEINDEX = 13;

function OnKeyDown(e) {
    switch (e.keyCode) {
        case 16: mShiftKeyOn = true; break;
        case 17: mCtrlKeyOn = true; break;
    }
}
function OnKeyUp(e) {
    switch (e.keyCode) {
        case 16: mShiftKeyOn = false; break;
        case 17: mCtrlKeyOn = false; break;
    }
}
function OnRowClick() {
    //Event handler for row click
    document.all.TLBody.focus();
    var row = window.event.srcElement.parentElement;
    if (row.tagName.toLowerCase() != "tr") return;
    if (mShiftKeyOn) {
        for (var i = 0; i < window.document.all.ctl00_cpBody_grdTLs.childNodes[0].childNodes.length; i++) { unselectRow(i); }
        if (mLastRow != null) {
            var rowID1 = parseInt(mLastRow.id);
            var rowID2 = parseInt(row.id);
            if (rowID2 >= rowID1) for (var i = rowID1; i <= rowID2; i++) { selectRow(i); }
            else if (rowID2 < rowID1) for (var i = rowID2; i <= rowID1; i++) { selectRow(i); }
        }
        else {
            row.className = 'SelectedRow';
            mLastRow = row;
        }
    }
    else if (mCtrlKeyOn) {
        row.className = row.className == 'SelectedRow' ? 'NormalRow' : 'SelectedRow';
        mLastRow = row == 'SelectedRow' ? row : null;
    }
    else {
        //Clear all rows and select clicked row
        for (var i = 0; i < window.document.all.ctl00_cpBody_grdTLs.childNodes[1].childNodes.length; i++) { unselectRow(i); }
        row.className = 'SelectedRow';
        mLastRow = row;
    }
    doCalculation();
}
function selectRow(index) {
    var row = document.getElementById(index.toString() + "row");
    if (row != null && row != 'undefined') row.className = 'SelectedRow';
}
function unselectRow(index) {
    var row = document.getElementById(index.toString() + "row");
    if (row != null && row != 'undefined') row.className = 'NormalRow';
}
function findTL() {
    //
    //if(window.event.keyCode == 13) {
    var grd = document.getElementById('ctl00_cpBody_grdTLs');
    for (var i = 0; i < grd.rows.length; i++) { unselectRow(i); }
    doCalculation();

    var txt = document.getElementById('txtFind').value;
    if (txt.length > 0) {
        for (var i = 1; i < grd.rows.length; i++) {
            var cell = grd.rows[i].cells[0];
            if (cell.innerHTML.substr(0, txt.length) == txt) {
                var pnl = document.getElementById('pnlTLs');
                pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                selectRow(i);
                doCalculation();
                break;
            }
        }
    }
    //}
}

function doCalculation() {
    //Get the grid and column indexes
    var oDoc = window.document.all;
	var grid = window.document.all.ctl00_cpBody_grdTLs;

    //Update cartons, pallets, weight, and cube for selected rows
    //grid is of form <table><tr class="SelectedRow"><td></td>...<td>cartons</td><td>pallets</td><td>weight</td><td>cube</td></tr></table>
	//so... grid.childNodes[0] is the table; grid.childNodes[0].childNodes[i] is a row
    //First row is header row
    var totalSelectedRows=0, totalWeight=0, totalPallets=0, totalCubeFt=0, totalCartons=0;
    for(var i=1; i<grid.childNodes[1].childNodes.length; i++) {
        if (grid.childNodes[1].childNodes[i].className == 'SelectedRow') {
			totalSelectedRows++;
			totalCartons = totalCartons + formatInt(grid.childNodes[1].childNodes[i].childNodes[CARTONSINDEX].innerText);
			totalPallets = totalPallets + formatInt(grid.childNodes[1].childNodes[i].childNodes[PALLETSINDEX].innerText);
			totalWeight = totalWeight + formatInt(grid.childNodes[1].childNodes[i].childNodes[WEIGHTINDEX].innerText);
			totalCubeFt = totalCubeFt + formatInt(grid.childNodes[1].childNodes[i].childNodes[CUBEINDEX].innerText);
		}
	}
	oDoc.TotalTLs.innerText = totalSelectedRows;
	oDoc.TotalWeight.innerText = formatNumber(totalWeight);
	oDoc.TotalCubeFt.innerText = formatNumber(totalCubeFt);
	oDoc.TotalPallets.innerText = formatNumber(totalPallets);
    oDoc.TotalCartons.innerText = formatNumber(totalCartons);
    
	//Update ISA weight/cube for selected rows
	var isaWeight=0, isaCubeFt=0;
    if(isNaN(parseFloat(oDoc.ISAWeight.value)))
		isaWeight = 0;
	else
		isaWeight = parseFloat(oDoc.ISAWeight.value);
	isaCubeFt = calculateISACube(isaWeight);
	oDoc.ISACubeFt.innerText = formatNumber(isaCubeFt);
	
	//Update total weight/cube and weight/cube% for selected rows
    var grandWeight = totalWeight + isaWeight;
	var grandCubeFt = totalCubeFt + isaCubeFt;
	oDoc.GrandWeight.innerText = formatNumber(grandWeight);
	oDoc.GrandCubeFt.innerText = formatNumber(grandCubeFt);
	oDoc.WeightPercent.innerText = parseInt(grandWeight * 100 / TRAILERWEIGHT) + "%";
	oDoc.CubePercent.innerText = parseInt(grandCubeFt * 100 / TRAILERCUBE) + "%";
}
function calculateISACube(isaWeight) { return parseInt(isaWeight * TRAILERCUBE / TRAILERWEIGHT); }
function formatNumber(numString) {
	numString = numString.toLocaleString();
	var exp = /\./;
	var decimalPos = numString.search(exp); //will search for decimal
	if (decimalPos != -1 && decimalPos > 0)
		return numString.substr(0,decimalPos);
	else
		return numString;
}
function formatInt(numString) { return parseInt(numString.replace(/\$|\,/g,'')); }