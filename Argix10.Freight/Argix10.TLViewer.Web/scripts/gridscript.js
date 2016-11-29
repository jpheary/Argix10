var mShiftKeyOn = false;
var mCtrlKeyOn = false;
var mLastRow = null;

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
	    for (var i = 0; i < window.document.all.grdTLs.childNodes[0].childNodes.length; i++) { unselectRow(i); }
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
        for (var i = 0; i < window.document.all.grdTLs.childNodes[0].childNodes.length; i++) { unselectRow(i); }
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
        var grd = document.getElementById('grdTLs');
        for(var i=0; i<grd.rows.length; i++) { unselectRow(i); }
        doCalculation();
        
        var txt = document.getElementById('txtFind').value;
        if(txt.length > 0) {
            for(var i=1; i<grd.rows.length; i++) {
                var cell = grd.rows[i].cells[0];
                if(cell.innerHTML.substr(0, txt.length) == txt) {
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
